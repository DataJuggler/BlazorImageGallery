

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

    #region class ImageWriterBase
    /// <summary>
    /// This class is used for converting a 'Image' object to
    /// the SqlParameter[] to perform the CRUD methods.
    /// </summary>
    public class ImageWriterBase
    {

        #region Static Methods

            #region CreatePrimaryKeyParameter(Image image)
            /// <summary>
            /// This method creates the sql Parameter[] array
            /// that holds the primary key value.
            /// </summary>
            /// <param name='image'>The 'Image' to get the primary key of.</param>
            /// <returns>A SqlParameter[] array which contains the primary key value.
            /// to delete.</returns>
            internal static SqlParameter[] CreatePrimaryKeyParameter(Image image)
            {
                // Initial Value
                SqlParameter[] parameters = new SqlParameter[1];

                // verify user exists
                if (image != null)
                {
                    // Create PrimaryKey Parameter
                    SqlParameter @Id = new SqlParameter("@Id", image.Id);

                    // Set parameters[0] to @Id
                    parameters[0] = @Id;
                }

                // return value
                return parameters;
            }
            #endregion

            #region CreateDeleteImageStoredProcedure(Image image)
            /// <summary>
            /// This method creates an instance of an
            /// 'DeleteImage'StoredProcedure' object and
            /// creates the sql parameter[] array needed
            /// to execute the procedure 'Image_Delete'.
            /// </summary>
            /// <param name="image">The 'Image' to Delete.</param>
            /// <returns>An instance of a 'DeleteImageStoredProcedure' object.</returns>
            public static DeleteImageStoredProcedure CreateDeleteImageStoredProcedure(Image image)
            {
                // Initial Value
                DeleteImageStoredProcedure deleteImageStoredProcedure = new DeleteImageStoredProcedure();

                // Now Create Parameters For The DeleteProc
                deleteImageStoredProcedure.Parameters = CreatePrimaryKeyParameter(image);

                // return value
                return deleteImageStoredProcedure;
            }
            #endregion

            #region CreateFindImageStoredProcedure(Image image)
            /// <summary>
            /// This method creates an instance of a
            /// 'FindImageStoredProcedure' object and
            /// creates the sql parameter[] array needed
            /// to execute the procedure 'Image_Find'.
            /// </summary>
            /// <param name="image">The 'Image' to use to
            /// get the primary key parameter.</param>
            /// <returns>An instance of an FetchUserStoredProcedure</returns>
            public static FindImageStoredProcedure CreateFindImageStoredProcedure(Image image)
            {
                // Initial Value
                FindImageStoredProcedure findImageStoredProcedure = null;

                // verify image exists
                if(image != null)
                {
                    // Instanciate findImageStoredProcedure
                    findImageStoredProcedure = new FindImageStoredProcedure();

                    // Now create parameters for this procedure
                    findImageStoredProcedure.Parameters = CreatePrimaryKeyParameter(image);
                }

                // return value
                return findImageStoredProcedure;
            }
            #endregion

            #region CreateInsertParameters(Image image)
            /// <summary>
            /// This method creates the sql Parameters[] needed for
            /// inserting a new image.
            /// </summary>
            /// <param name="image">The 'Image' to insert.</param>
            /// <returns></returns>
            internal static SqlParameter[] CreateInsertParameters(Image image)
            {
                // Initial Values
                SqlParameter[] parameters = new SqlParameter[12];
                SqlParameter param = null;

                // verify imageexists
                if(image != null)
                {
                    // Create [CreatedDate] Parameter
                    param = new SqlParameter("@CreatedDate", SqlDbType.DateTime);

                    // If image.CreatedDate does not exist.
                    if ((image.CreatedDate == null) || (image.CreatedDate.Year < 1900))
                    {
                        // Set the value to 1/1/1900
                        param.Value = new DateTime(1900, 1, 1);
                    }
                    else
                    {
                        // Set the parameter value
                        param.Value = image.CreatedDate;
                    }

                    // set parameters[0]
                    parameters[0] = param;

                    // Create [Extension] parameter
                    param = new SqlParameter("@Extension", image.Extension);

                    // set parameters[1]
                    parameters[1] = param;

                    // Create [FileSize] parameter
                    param = new SqlParameter("@FileSize", image.FileSize);

                    // set parameters[2]
                    parameters[2] = param;

                    // Create [FullPath] parameter
                    param = new SqlParameter("@FullPath", image.FullPath);

                    // set parameters[3]
                    parameters[3] = param;

                    // Create [Height] parameter
                    param = new SqlParameter("@Height", image.Height);

                    // set parameters[4]
                    parameters[4] = param;

                    // Create [ImageNumber] parameter
                    param = new SqlParameter("@ImageNumber", image.ImageNumber);

                    // set parameters[5]
                    parameters[5] = param;

                    // Create [ImageUrl] parameter
                    param = new SqlParameter("@ImageUrl", image.ImageUrl);

                    // set parameters[6]
                    parameters[6] = param;

                    // Create [Name] parameter
                    param = new SqlParameter("@Name", image.Name);

                    // set parameters[7]
                    parameters[7] = param;

                    // Create [OwnerId] parameter
                    param = new SqlParameter("@OwnerId", image.OwnerId);

                    // set parameters[8]
                    parameters[8] = param;

                    // Create [SitePath] parameter
                    param = new SqlParameter("@SitePath", image.SitePath);

                    // set parameters[9]
                    parameters[9] = param;

                    // Create [Visible] parameter
                    param = new SqlParameter("@Visible", image.Visible);

                    // set parameters[10]
                    parameters[10] = param;

                    // Create [Width] parameter
                    param = new SqlParameter("@Width", image.Width);

                    // set parameters[11]
                    parameters[11] = param;
                }

                // return value
                return parameters;
            }
            #endregion

            #region CreateInsertImageStoredProcedure(Image image)
            /// <summary>
            /// This method creates an instance of an
            /// 'InsertImageStoredProcedure' object and
            /// creates the sql parameter[] array needed
            /// to execute the procedure 'Image_Insert'.
            /// </summary>
            /// <param name="image"The 'Image' object to insert</param>
            /// <returns>An instance of a 'InsertImageStoredProcedure' object.</returns>
            public static InsertImageStoredProcedure CreateInsertImageStoredProcedure(Image image)
            {
                // Initial Value
                InsertImageStoredProcedure insertImageStoredProcedure = null;

                // verify image exists
                if(image != null)
                {
                    // Instanciate insertImageStoredProcedure
                    insertImageStoredProcedure = new InsertImageStoredProcedure();

                    // Now create parameters for this procedure
                    insertImageStoredProcedure.Parameters = CreateInsertParameters(image);
                }

                // return value
                return insertImageStoredProcedure;
            }
            #endregion

            #region CreateUpdateParameters(Image image)
            /// <summary>
            /// This method creates the sql Parameters[] needed for
            /// update an existing image.
            /// </summary>
            /// <param name="image">The 'Image' to update.</param>
            /// <returns></returns>
            internal static SqlParameter[] CreateUpdateParameters(Image image)
            {
                // Initial Values
                SqlParameter[] parameters = new SqlParameter[13];
                SqlParameter param = null;

                // verify imageexists
                if(image != null)
                {
                    // Create parameter for [CreatedDate]
                    // Create [CreatedDate] Parameter
                    param = new SqlParameter("@CreatedDate", SqlDbType.DateTime);

                    // If image.CreatedDate does not exist.
                    if ((image.CreatedDate == null) || (image.CreatedDate.Year < 1900))
                    {
                        // Set the value to 1/1/1900
                        param.Value = new DateTime(1900, 1, 1);
                    }
                    else
                    {
                        // Set the parameter value
                        param.Value = image.CreatedDate;
                    }


                    // set parameters[0]
                    parameters[0] = param;

                    // Create parameter for [Extension]
                    param = new SqlParameter("@Extension", image.Extension);

                    // set parameters[1]
                    parameters[1] = param;

                    // Create parameter for [FileSize]
                    param = new SqlParameter("@FileSize", image.FileSize);

                    // set parameters[2]
                    parameters[2] = param;

                    // Create parameter for [FullPath]
                    param = new SqlParameter("@FullPath", image.FullPath);

                    // set parameters[3]
                    parameters[3] = param;

                    // Create parameter for [Height]
                    param = new SqlParameter("@Height", image.Height);

                    // set parameters[4]
                    parameters[4] = param;

                    // Create parameter for [ImageNumber]
                    param = new SqlParameter("@ImageNumber", image.ImageNumber);

                    // set parameters[5]
                    parameters[5] = param;

                    // Create parameter for [ImageUrl]
                    param = new SqlParameter("@ImageUrl", image.ImageUrl);

                    // set parameters[6]
                    parameters[6] = param;

                    // Create parameter for [Name]
                    param = new SqlParameter("@Name", image.Name);

                    // set parameters[7]
                    parameters[7] = param;

                    // Create parameter for [OwnerId]
                    param = new SqlParameter("@OwnerId", image.OwnerId);

                    // set parameters[8]
                    parameters[8] = param;

                    // Create parameter for [SitePath]
                    param = new SqlParameter("@SitePath", image.SitePath);

                    // set parameters[9]
                    parameters[9] = param;

                    // Create parameter for [Visible]
                    param = new SqlParameter("@Visible", image.Visible);

                    // set parameters[10]
                    parameters[10] = param;

                    // Create parameter for [Width]
                    param = new SqlParameter("@Width", image.Width);

                    // set parameters[11]
                    parameters[11] = param;

                    // Create parameter for [Id]
                    param = new SqlParameter("@Id", image.Id);
                    parameters[12] = param;
                }

                // return value
                return parameters;
            }
            #endregion

            #region CreateUpdateImageStoredProcedure(Image image)
            /// <summary>
            /// This method creates an instance of an
            /// 'UpdateImageStoredProcedure' object and
            /// creates the sql parameter[] array needed
            /// to execute the procedure 'Image_Update'.
            /// </summary>
            /// <param name="image"The 'Image' object to update</param>
            /// <returns>An instance of a 'UpdateImageStoredProcedure</returns>
            public static UpdateImageStoredProcedure CreateUpdateImageStoredProcedure(Image image)
            {
                // Initial Value
                UpdateImageStoredProcedure updateImageStoredProcedure = null;

                // verify image exists
                if(image != null)
                {
                    // Instanciate updateImageStoredProcedure
                    updateImageStoredProcedure = new UpdateImageStoredProcedure();

                    // Now create parameters for this procedure
                    updateImageStoredProcedure.Parameters = CreateUpdateParameters(image);
                }

                // return value
                return updateImageStoredProcedure;
            }
            #endregion

            #region CreateFetchAllImagesStoredProcedure(Image image)
            /// <summary>
            /// This method creates an instance of a
            /// 'FetchAllImagesStoredProcedure' object and
            /// creates the sql parameter[] array needed
            /// to execute the procedure 'Image_FetchAll'.
            /// </summary>
            /// <returns>An instance of a(n) 'FetchAllImagesStoredProcedure' object.</returns>
            public static FetchAllImagesStoredProcedure CreateFetchAllImagesStoredProcedure(Image image)
            {
                // Initial value
                FetchAllImagesStoredProcedure fetchAllImagesStoredProcedure = new FetchAllImagesStoredProcedure();

                // return value
                return fetchAllImagesStoredProcedure;
            }
            #endregion

        #endregion

    }
    #endregion

}
