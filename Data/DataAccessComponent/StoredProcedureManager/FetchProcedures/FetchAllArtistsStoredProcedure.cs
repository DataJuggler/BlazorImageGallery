

#region using statements

using DataAccessComponent.StoredProcedureManager.DeleteProcedures;
using DataAccessComponent.StoredProcedureManager.FetchProcedures;
using DataAccessComponent.StoredProcedureManager.InsertProcedures;
using DataAccessComponent.StoredProcedureManager.UpdateProcedures;
using DataJuggler.Net;
using System;

#endregion


namespace DataAccessComponent.StoredProcedureManager.FetchProcedures
{

    #region class FetchAllArtistsStoredProcedure
    /// <summary>
    /// This class is used to FetchAll 'Artist' objects.
    /// </summary>
    public class FetchAllArtistsStoredProcedure : StoredProcedure
    {

        #region Private Variables
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of a 'FetchAllArtistsStoredProcedure' object.
        /// </summary>
        public FetchAllArtistsStoredProcedure()
        {
            // Perform Initialization
            Init();
        }
        #endregion

        #region Methods

            #region Init()
            /// <summary>
            /// Set Procedure Properties
            /// </summary>
            private void Init()
            {
                // Set Properties For This Proc

                // Set ProcedureName
                this.ProcedureName = "Artist_FetchAll";

                // Set tableName
                this.TableName = "Artist";
            }
            #endregion

        #endregion

        #region Properties

        #endregion

    }
    #endregion

}
