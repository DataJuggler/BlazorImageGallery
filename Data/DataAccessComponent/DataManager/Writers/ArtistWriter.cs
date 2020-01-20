
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

    #region class ArtistWriter
    /// <summary>
    /// This class is used for converting a 'Artist' object to
    /// the SqlParameter[] to perform the CRUD methods.
    /// </summary>
    public class ArtistWriter : ArtistWriterBase
    {

        #region Static Methods

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
            public static new FindArtistStoredProcedure CreateFindArtistStoredProcedure(Artist artist)
            {
                // Initial Value
                FindArtistStoredProcedure findArtistStoredProcedure = null;

                // verify artist exists
                if(artist != null)
                {
                    // Instanciate findArtistStoredProcedure
                    findArtistStoredProcedure = new FindArtistStoredProcedure();

                    // if artist.FindByEmailAddress is true
                    if (artist.FindByEmailAddress)
                    {
                        // Change the procedure name
                        findArtistStoredProcedure.ProcedureName = "Artist_FindByEmailAddress";
                        
                        // Create the @EmailAddress parameter
                        findArtistStoredProcedure.Parameters = SqlParameterHelper.CreateSqlParameters("@EmailAddress", artist.EmailAddress);
                    }
                    else
                    {
                        // Now create parameters for this procedure
                        findArtistStoredProcedure.Parameters = CreatePrimaryKeyParameter(artist);
                    }
                }

                // return value
                return findArtistStoredProcedure;
            }
            #endregion
            
        #endregion

    }
    #endregion

}
