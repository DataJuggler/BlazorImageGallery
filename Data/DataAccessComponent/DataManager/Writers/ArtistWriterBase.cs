

#region using statements

using DataAccessComponent.StoredProcedureManager.DeleteProcedures;
using DataAccessComponent.StoredProcedureManager.FetchProcedures;
using DataAccessComponent.StoredProcedureManager.InsertProcedures;
using DataAccessComponent.StoredProcedureManager.UpdateProcedures;
using ObjectLibrary.BusinessObjects;
using System;
using System.Data;
using System.Data.SqlClient;

#endregion


namespace DataAccessComponent.DataManager.Writers
{

    #region class ArtistWriterBase
    /// <summary>
    /// This class is used for converting a 'Artist' object to
    /// the SqlParameter[] to perform the CRUD methods.
    /// </summary>
    public class ArtistWriterBase
    {

        #region Static Methods

            #region CreatePrimaryKeyParameter(Artist artist)
            /// <summary>
            /// This method creates the sql Parameter[] array
            /// that holds the primary key value.
            /// </summary>
            /// <param name='artist'>The 'Artist' to get the primary key of.</param>
            /// <returns>A SqlParameter[] array which contains the primary key value.
            /// to delete.</returns>
            internal static SqlParameter[] CreatePrimaryKeyParameter(Artist artist)
            {
                // Initial Value
                SqlParameter[] parameters = new SqlParameter[1];

                // verify user exists
                if (artist != null)
                {
                    // Create PrimaryKey Parameter
                    SqlParameter @Id = new SqlParameter("@Id", artist.Id);

                    // Set parameters[0] to @Id
                    parameters[0] = @Id;
                }

                // return value
                return parameters;
            }
            #endregion

            #region CreateDeleteArtistStoredProcedure(Artist artist)
            /// <summary>
            /// This method creates an instance of an
            /// 'DeleteArtist'StoredProcedure' object and
            /// creates the sql parameter[] array needed
            /// to execute the procedure 'Artist_Delete'.
            /// </summary>
            /// <param name="artist">The 'Artist' to Delete.</param>
            /// <returns>An instance of a 'DeleteArtistStoredProcedure' object.</returns>
            public static DeleteArtistStoredProcedure CreateDeleteArtistStoredProcedure(Artist artist)
            {
                // Initial Value
                DeleteArtistStoredProcedure deleteArtistStoredProcedure = new DeleteArtistStoredProcedure();

                // Now Create Parameters For The DeleteProc
                deleteArtistStoredProcedure.Parameters = CreatePrimaryKeyParameter(artist);

                // return value
                return deleteArtistStoredProcedure;
            }
            #endregion

            #region CreateFindArtistStoredProcedure(Artist artist)
            /// <summary>
            /// This method creates an instance of a
            /// 'FindArtistStoredProcedure' object and
            /// creates the sql parameter[] array needed
            /// to execute the procedure 'Artist_Find'.
            /// </summary>
            /// <param name="artist">The 'Artist' to use to
            /// get the primary key parameter.</param>
            /// <returns>An instance of an FetchUserStoredProcedure</returns>
            public static FindArtistStoredProcedure CreateFindArtistStoredProcedure(Artist artist)
            {
                // Initial Value
                FindArtistStoredProcedure findArtistStoredProcedure = null;

                // verify artist exists
                if(artist != null)
                {
                    // Instanciate findArtistStoredProcedure
                    findArtistStoredProcedure = new FindArtistStoredProcedure();

                    // Now create parameters for this procedure
                    findArtistStoredProcedure.Parameters = CreatePrimaryKeyParameter(artist);
                }

                // return value
                return findArtistStoredProcedure;
            }
            #endregion

            #region CreateInsertParameters(Artist artist)
            /// <summary>
            /// This method creates the sql Parameters[] needed for
            /// inserting a new artist.
            /// </summary>
            /// <param name="artist">The 'Artist' to insert.</param>
            /// <returns></returns>
            internal static SqlParameter[] CreateInsertParameters(Artist artist)
            {
                // Initial Values
                SqlParameter[] parameters = new SqlParameter[10];
                SqlParameter param = null;

                // verify artistexists
                if(artist != null)
                {
                    // Create [Active] parameter
                    param = new SqlParameter("@Active", artist.Active);

                    // set parameters[0]
                    parameters[0] = param;

                    // Create [CreatedDate] Parameter
                    param = new SqlParameter("@CreatedDate", SqlDbType.DateTime);

                    // If artist.CreatedDate does not exist.
                    if ((artist.CreatedDate == null) || (artist.CreatedDate.Year < 1900))
                    {
                        // Set the value to 1/1/1900
                        param.Value = new DateTime(1900, 1, 1);
                    }
                    else
                    {
                        // Set the parameter value
                        param.Value = artist.CreatedDate;
                    }

                    // set parameters[1]
                    parameters[1] = param;

                    // Create [EmailAddress] parameter
                    param = new SqlParameter("@EmailAddress", artist.EmailAddress);

                    // set parameters[2]
                    parameters[2] = param;

                    // Create [FolderPath] parameter
                    param = new SqlParameter("@FolderPath", artist.FolderPath);

                    // set parameters[3]
                    parameters[3] = param;

                    // Create [ImagesCount] parameter
                    param = new SqlParameter("@ImagesCount", artist.ImagesCount);

                    // set parameters[4]
                    parameters[4] = param;

                    // Create [IsAdmin] parameter
                    param = new SqlParameter("@IsAdmin", artist.IsAdmin);

                    // set parameters[5]
                    parameters[5] = param;

                    // Create [LastUpdated] Parameter
                    param = new SqlParameter("@LastUpdated", SqlDbType.DateTime);

                    // If artist.LastUpdated does not exist.
                    if ((artist.LastUpdated == null) || (artist.LastUpdated.Year < 1900))
                    {
                        // Set the value to 1/1/1900
                        param.Value = new DateTime(1900, 1, 1);
                    }
                    else
                    {
                        // Set the parameter value
                        param.Value = artist.LastUpdated;
                    }

                    // set parameters[6]
                    parameters[6] = param;

                    // Create [Name] parameter
                    param = new SqlParameter("@Name", artist.Name);

                    // set parameters[7]
                    parameters[7] = param;

                    // Create [PasswordHash] parameter
                    param = new SqlParameter("@PasswordHash", artist.PasswordHash);

                    // set parameters[8]
                    parameters[8] = param;

                    // Create [ProfilePicture] parameter
                    param = new SqlParameter("@ProfilePicture", artist.ProfilePicture);

                    // set parameters[9]
                    parameters[9] = param;
                }

                // return value
                return parameters;
            }
            #endregion

            #region CreateInsertArtistStoredProcedure(Artist artist)
            /// <summary>
            /// This method creates an instance of an
            /// 'InsertArtistStoredProcedure' object and
            /// creates the sql parameter[] array needed
            /// to execute the procedure 'Artist_Insert'.
            /// </summary>
            /// <param name="artist"The 'Artist' object to insert</param>
            /// <returns>An instance of a 'InsertArtistStoredProcedure' object.</returns>
            public static InsertArtistStoredProcedure CreateInsertArtistStoredProcedure(Artist artist)
            {
                // Initial Value
                InsertArtistStoredProcedure insertArtistStoredProcedure = null;

                // verify artist exists
                if(artist != null)
                {
                    // Instanciate insertArtistStoredProcedure
                    insertArtistStoredProcedure = new InsertArtistStoredProcedure();

                    // Now create parameters for this procedure
                    insertArtistStoredProcedure.Parameters = CreateInsertParameters(artist);
                }

                // return value
                return insertArtistStoredProcedure;
            }
            #endregion

            #region CreateUpdateParameters(Artist artist)
            /// <summary>
            /// This method creates the sql Parameters[] needed for
            /// update an existing artist.
            /// </summary>
            /// <param name="artist">The 'Artist' to update.</param>
            /// <returns></returns>
            internal static SqlParameter[] CreateUpdateParameters(Artist artist)
            {
                // Initial Values
                SqlParameter[] parameters = new SqlParameter[11];
                SqlParameter param = null;

                // verify artistexists
                if(artist != null)
                {
                    // Create parameter for [Active]
                    param = new SqlParameter("@Active", artist.Active);

                    // set parameters[0]
                    parameters[0] = param;

                    // Create parameter for [CreatedDate]
                    // Create [CreatedDate] Parameter
                    param = new SqlParameter("@CreatedDate", SqlDbType.DateTime);

                    // If artist.CreatedDate does not exist.
                    if ((artist.CreatedDate == null) || (artist.CreatedDate.Year < 1900))
                    {
                        // Set the value to 1/1/1900
                        param.Value = new DateTime(1900, 1, 1);
                    }
                    else
                    {
                        // Set the parameter value
                        param.Value = artist.CreatedDate;
                    }


                    // set parameters[1]
                    parameters[1] = param;

                    // Create parameter for [EmailAddress]
                    param = new SqlParameter("@EmailAddress", artist.EmailAddress);

                    // set parameters[2]
                    parameters[2] = param;

                    // Create parameter for [FolderPath]
                    param = new SqlParameter("@FolderPath", artist.FolderPath);

                    // set parameters[3]
                    parameters[3] = param;

                    // Create parameter for [ImagesCount]
                    param = new SqlParameter("@ImagesCount", artist.ImagesCount);

                    // set parameters[4]
                    parameters[4] = param;

                    // Create parameter for [IsAdmin]
                    param = new SqlParameter("@IsAdmin", artist.IsAdmin);

                    // set parameters[5]
                    parameters[5] = param;

                    // Create parameter for [LastUpdated]
                    // Create [LastUpdated] Parameter
                    param = new SqlParameter("@LastUpdated", SqlDbType.DateTime);

                    // If artist.LastUpdated does not exist.
                    if ((artist.LastUpdated == null) || (artist.LastUpdated.Year < 1900))
                    {
                        // Set the value to 1/1/1900
                        param.Value = new DateTime(1900, 1, 1);
                    }
                    else
                    {
                        // Set the parameter value
                        param.Value = artist.LastUpdated;
                    }


                    // set parameters[6]
                    parameters[6] = param;

                    // Create parameter for [Name]
                    param = new SqlParameter("@Name", artist.Name);

                    // set parameters[7]
                    parameters[7] = param;

                    // Create parameter for [PasswordHash]
                    param = new SqlParameter("@PasswordHash", artist.PasswordHash);

                    // set parameters[8]
                    parameters[8] = param;

                    // Create parameter for [ProfilePicture]
                    param = new SqlParameter("@ProfilePicture", artist.ProfilePicture);

                    // set parameters[9]
                    parameters[9] = param;

                    // Create parameter for [Id]
                    param = new SqlParameter("@Id", artist.Id);
                    parameters[10] = param;
                }

                // return value
                return parameters;
            }
            #endregion

            #region CreateUpdateArtistStoredProcedure(Artist artist)
            /// <summary>
            /// This method creates an instance of an
            /// 'UpdateArtistStoredProcedure' object and
            /// creates the sql parameter[] array needed
            /// to execute the procedure 'Artist_Update'.
            /// </summary>
            /// <param name="artist"The 'Artist' object to update</param>
            /// <returns>An instance of a 'UpdateArtistStoredProcedure</returns>
            public static UpdateArtistStoredProcedure CreateUpdateArtistStoredProcedure(Artist artist)
            {
                // Initial Value
                UpdateArtistStoredProcedure updateArtistStoredProcedure = null;

                // verify artist exists
                if(artist != null)
                {
                    // Instanciate updateArtistStoredProcedure
                    updateArtistStoredProcedure = new UpdateArtistStoredProcedure();

                    // Now create parameters for this procedure
                    updateArtistStoredProcedure.Parameters = CreateUpdateParameters(artist);
                }

                // return value
                return updateArtistStoredProcedure;
            }
            #endregion

            #region CreateFetchAllArtistsStoredProcedure(Artist artist)
            /// <summary>
            /// This method creates an instance of a
            /// 'FetchAllArtistsStoredProcedure' object and
            /// creates the sql parameter[] array needed
            /// to execute the procedure 'Artist_FetchAll'.
            /// </summary>
            /// <returns>An instance of a(n) 'FetchAllArtistsStoredProcedure' object.</returns>
            public static FetchAllArtistsStoredProcedure CreateFetchAllArtistsStoredProcedure(Artist artist)
            {
                // Initial value
                FetchAllArtistsStoredProcedure fetchAllArtistsStoredProcedure = new FetchAllArtistsStoredProcedure();

                // return value
                return fetchAllArtistsStoredProcedure;
            }
            #endregion

        #endregion

    }
    #endregion

}
