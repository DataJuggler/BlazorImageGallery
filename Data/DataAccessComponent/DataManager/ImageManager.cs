

#region using statements

using DataAccessComponent.DataManager.Readers;
using DataAccessComponent.StoredProcedureManager.DeleteProcedures;
using DataAccessComponent.StoredProcedureManager.FetchProcedures;
using DataAccessComponent.StoredProcedureManager.InsertProcedures;
using DataAccessComponent.StoredProcedureManager.UpdateProcedures;
using ObjectLibrary.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;

#endregion


namespace DataAccessComponent.DataManager
{

    #region class ImageManager
    /// <summary>
    /// This class is used to manage a 'Image' object.
    /// </summary>
    public class ImageManager
    {

        #region Private Variables
        private DataManager dataManager;
        private DataHelper dataHelper;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of a 'ImageManager' object.
        /// </summary>
        public ImageManager(DataManager dataManagerArg)
        {
            // Set DataManager
            this.DataManager = dataManagerArg;

            // Perform Initialization
            Init();
        }
        #endregion

        #region Methods

            #region DeleteImage()
            /// <summary>
            /// This method deletes a 'Image' object.
            /// </summary>
            /// <returns>True if successful false if not.</returns>
            /// </summary>
            public bool DeleteImage(DeleteImageStoredProcedure deleteImageProc, DataConnector databaseConnector)
            {
                // Initial Value
                bool deleted = false;

                // Verify database connection is connected
                if ((databaseConnector != null) && (databaseConnector.Connected))
                {
                    // Execute Non Query
                    deleted = this.DataHelper.DeleteRecord(deleteImageProc, databaseConnector);
                }

                // return value
                return deleted;
            }
            #endregion

            #region FetchAllImages()
            /// <summary>
            /// This method fetches a  'List<Image>' object.
            /// This method uses the 'Images_FetchAll' procedure.
            /// </summary>
            /// <returns>A 'List<Image>'</returns>
            /// </summary>
            public List<Image> FetchAllImages(FetchAllImagesStoredProcedure fetchAllImagesProc, DataConnector databaseConnector)
            {
                // Initial Value
                List<Image> imageCollection = null;

                // Verify database connection is connected
                if ((databaseConnector != null) && (databaseConnector.Connected))
                {
                    // First Get Dataset
                    DataSet allImagesDataSet = this.DataHelper.LoadDataSet(fetchAllImagesProc, databaseConnector);

                    // Verify DataSet Exists
                    if(allImagesDataSet != null)
                    {
                        // Get DataTable From DataSet
                        DataTable table = this.DataHelper.ReturnFirstTable(allImagesDataSet);

                        // if table exists
                        if(table != null)
                        {
                            // Load Collection
                            imageCollection = ImageReader.LoadCollection(table);
                        }
                    }
                }

                // return value
                return imageCollection;
            }
            #endregion

            #region FindImage()
            /// <summary>
            /// This method finds a  'Image' object.
            /// This method uses the 'Image_Find' procedure.
            /// </summary>
            /// <returns>A 'Image' object.</returns>
            /// </summary>
            public Image FindImage(FindImageStoredProcedure findImageProc, DataConnector databaseConnector)
            {
                // Initial Value
                Image image = null;

                // Verify database connection is connected
                if ((databaseConnector != null) && (databaseConnector.Connected))
                {
                    // First Get Dataset
                    DataSet imageDataSet = this.DataHelper.LoadDataSet(findImageProc, databaseConnector);

                    // Verify DataSet Exists
                    if(imageDataSet != null)
                    {
                        // Get DataTable From DataSet
                        DataRow row = this.DataHelper.ReturnFirstRow(imageDataSet);

                        // if row exists
                        if(row != null)
                        {
                            // Load Image
                            image = ImageReader.Load(row);
                        }
                    }
                }

                // return value
                return image;
            }
            #endregion

            #region Init()
            /// <summary>
            /// Perorm Initialization For This Object
            /// </summary>
            private void Init()
            {
                // Create DataHelper object
                this.DataHelper = new DataHelper();
            }
            #endregion

            #region InsertImage()
            /// <summary>
            /// This method inserts a 'Image' object.
            /// This method uses the 'Image_Insert' procedure.
            /// </summary>
            /// <returns>The identity value of the new record.</returns>
            /// </summary>
            public int InsertImage(InsertImageStoredProcedure insertImageProc, DataConnector databaseConnector)
            {
                // Initial Value
                int newIdentity = -1;

                // Verify database connection is connected
                if ((databaseConnector != null) && (databaseConnector.Connected))
                {
                    // Execute Non Query
                    newIdentity = this.DataHelper.InsertRecord(insertImageProc, databaseConnector);
                }

                // return value
                return newIdentity;
            }
            #endregion

            #region UpdateImage()
            /// <summary>
            /// This method updates a 'Image'.
            /// This method uses the 'Image_Update' procedure.
            /// </summary>
            /// <returns>True if successful false if not.</returns>
            /// </summary>
            public bool UpdateImage(UpdateImageStoredProcedure updateImageProc, DataConnector databaseConnector)
            {
                // Initial Value
                bool saved = false;

                // Verify database connection is connected
                if ((databaseConnector != null) && (databaseConnector.Connected))
                {
                    // Execute Update.
                    saved = this.DataHelper.UpdateRecord(updateImageProc, databaseConnector);
                }

                // return value
                return saved;
            }
            #endregion

        #endregion

        #region Properties

            #region DataHelper
            /// <summary>
            /// This object uses the Microsoft Data
            /// Application Block to execute stored
            /// procedures.
            /// </summary>
            internal DataHelper DataHelper
            {
                get { return dataHelper; }
                set { dataHelper = value; }
            }
            #endregion

            #region DataManager
            /// <summary>
            /// A reference to this objects parent.
            /// </summary>
            internal DataManager DataManager
            {
                get { return dataManager; }
                set { dataManager = value; }
            }
            #endregion

        #endregion

    }
    #endregion

}
