

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

    #region class ArtistManager
    /// <summary>
    /// This class is used to manage a 'Artist' object.
    /// </summary>
    public class ArtistManager
    {

        #region Private Variables
        private DataManager dataManager;
        private DataHelper dataHelper;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of a 'ArtistManager' object.
        /// </summary>
        public ArtistManager(DataManager dataManagerArg)
        {
            // Set DataManager
            this.DataManager = dataManagerArg;

            // Perform Initialization
            Init();
        }
        #endregion

        #region Methods

            #region DeleteArtist()
            /// <summary>
            /// This method deletes a 'Artist' object.
            /// </summary>
            /// <returns>True if successful false if not.</returns>
            /// </summary>
            public bool DeleteArtist(DeleteArtistStoredProcedure deleteArtistProc, DataConnector databaseConnector)
            {
                // Initial Value
                bool deleted = false;

                // Verify database connection is connected
                if ((databaseConnector != null) && (databaseConnector.Connected))
                {
                    // Execute Non Query
                    deleted = this.DataHelper.DeleteRecord(deleteArtistProc, databaseConnector);
                }

                // return value
                return deleted;
            }
            #endregion

            #region FetchAllArtists()
            /// <summary>
            /// This method fetches a  'List<Artist>' object.
            /// This method uses the 'Artists_FetchAll' procedure.
            /// </summary>
            /// <returns>A 'List<Artist>'</returns>
            /// </summary>
            public List<Artist> FetchAllArtists(FetchAllArtistsStoredProcedure fetchAllArtistsProc, DataConnector databaseConnector)
            {
                // Initial Value
                List<Artist> artistCollection = null;

                // Verify database connection is connected
                if ((databaseConnector != null) && (databaseConnector.Connected))
                {
                    // First Get Dataset
                    DataSet allArtistsDataSet = this.DataHelper.LoadDataSet(fetchAllArtistsProc, databaseConnector);

                    // Verify DataSet Exists
                    if(allArtistsDataSet != null)
                    {
                        // Get DataTable From DataSet
                        DataTable table = this.DataHelper.ReturnFirstTable(allArtistsDataSet);

                        // if table exists
                        if(table != null)
                        {
                            // Load Collection
                            artistCollection = ArtistReader.LoadCollection(table);
                        }
                    }
                }

                // return value
                return artistCollection;
            }
            #endregion

            #region FindArtist()
            /// <summary>
            /// This method finds a  'Artist' object.
            /// This method uses the 'Artist_Find' procedure.
            /// </summary>
            /// <returns>A 'Artist' object.</returns>
            /// </summary>
            public Artist FindArtist(FindArtistStoredProcedure findArtistProc, DataConnector databaseConnector)
            {
                // Initial Value
                Artist artist = null;

                // Verify database connection is connected
                if ((databaseConnector != null) && (databaseConnector.Connected))
                {
                    // First Get Dataset
                    DataSet artistDataSet = this.DataHelper.LoadDataSet(findArtistProc, databaseConnector);

                    // Verify DataSet Exists
                    if(artistDataSet != null)
                    {
                        // Get DataTable From DataSet
                        DataRow row = this.DataHelper.ReturnFirstRow(artistDataSet);

                        // if row exists
                        if(row != null)
                        {
                            // Load Artist
                            artist = ArtistReader.Load(row);
                        }
                    }
                }

                // return value
                return artist;
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

            #region InsertArtist()
            /// <summary>
            /// This method inserts a 'Artist' object.
            /// This method uses the 'Artist_Insert' procedure.
            /// </summary>
            /// <returns>The identity value of the new record.</returns>
            /// </summary>
            public int InsertArtist(InsertArtistStoredProcedure insertArtistProc, DataConnector databaseConnector)
            {
                // Initial Value
                int newIdentity = -1;

                // Verify database connection is connected
                if ((databaseConnector != null) && (databaseConnector.Connected))
                {
                    // Execute Non Query
                    newIdentity = this.DataHelper.InsertRecord(insertArtistProc, databaseConnector);
                }

                // return value
                return newIdentity;
            }
            #endregion

            #region UpdateArtist()
            /// <summary>
            /// This method updates a 'Artist'.
            /// This method uses the 'Artist_Update' procedure.
            /// </summary>
            /// <returns>True if successful false if not.</returns>
            /// </summary>
            public bool UpdateArtist(UpdateArtistStoredProcedure updateArtistProc, DataConnector databaseConnector)
            {
                // Initial Value
                bool saved = false;

                // Verify database connection is connected
                if ((databaseConnector != null) && (databaseConnector.Connected))
                {
                    // Execute Update.
                    saved = this.DataHelper.UpdateRecord(updateArtistProc, databaseConnector);
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
