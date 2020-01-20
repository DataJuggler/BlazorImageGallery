

#region using statements

using ApplicationLogicComponent.DataBridge;
using DataAccessComponent.DataManager;
using DataAccessComponent.DataManager.Writers;
using DataAccessComponent.StoredProcedureManager.DeleteProcedures;
using DataAccessComponent.StoredProcedureManager.FetchProcedures;
using DataAccessComponent.StoredProcedureManager.InsertProcedures;
using DataAccessComponent.StoredProcedureManager.UpdateProcedures;
using ObjectLibrary.BusinessObjects;
using System;
using System.Collections.Generic;

#endregion


namespace ApplicationLogicComponent.DataOperations
{

    #region class ArtistMethods
    /// <summary>
    /// This class contains methods for modifying a 'Artist' object.
    /// </summary>
    public class ArtistMethods
    {

        #region Private Variables
        private DataManager dataManager;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new Creates a new 'ArtistMethods' object.
        /// </summary>
        public ArtistMethods(DataManager dataManagerArg)
        {
            // Save Argument
            this.DataManager = dataManagerArg;
        }
        #endregion

        #region Methods

            #region DeleteArtist(Artist)
            /// <summary>
            /// This method deletes a 'Artist' object.
            /// </summary>
            /// <param name='List<PolymorphicObject>'>The 'Artist' to delete.
            /// <returns>A PolymorphicObject object with a Boolean value.
            internal PolymorphicObject DeleteArtist(List<PolymorphicObject> parameters, DataConnector dataConnector)
            {
                // Initial Value
                PolymorphicObject returnObject = new PolymorphicObject();

                // If the data connection is connected
                if ((dataConnector != null) && (dataConnector.Connected == true))
                {
                    // Create Delete StoredProcedure
                    DeleteArtistStoredProcedure deleteArtistProc = null;

                    // verify the first parameters is a(n) 'Artist'.
                    if (parameters[0].ObjectValue as Artist != null)
                    {
                        // Create Artist
                        Artist artist = (Artist) parameters[0].ObjectValue;

                        // verify artist exists
                        if(artist != null)
                        {
                            // Now create deleteArtistProc from ArtistWriter
                            // The DataWriter converts the 'Artist'
                            // to the SqlParameter[] array needed to delete a 'Artist'.
                            deleteArtistProc = ArtistWriter.CreateDeleteArtistStoredProcedure(artist);
                        }
                    }

                    // Verify deleteArtistProc exists
                    if(deleteArtistProc != null)
                    {
                        // Execute Delete Stored Procedure
                        bool deleted = this.DataManager.ArtistManager.DeleteArtist(deleteArtistProc, dataConnector);

                        // Create returnObject.Boolean
                        returnObject.Boolean = new NullableBoolean();

                        // If delete was successful
                        if(deleted)
                        {
                            // Set returnObject.Boolean.Value to true
                            returnObject.Boolean.Value = NullableBooleanEnum.True;
                        }
                        else
                        {
                            // Set returnObject.Boolean.Value to false
                            returnObject.Boolean.Value = NullableBooleanEnum.False;
                        }
                    }
                }
                else
                {
                    // Raise Error Data Connection Not Available
                    throw new Exception("The database connection is not available.");
                }

                // return value
                return returnObject;
            }
            #endregion

            #region FetchAll()
            /// <summary>
            /// This method fetches all 'Artist' objects.
            /// </summary>
            /// <param name='List<PolymorphicObject>'>The 'Artist' to delete.
            /// <returns>A PolymorphicObject object with all  'Artists' objects.
            internal PolymorphicObject FetchAll(List<PolymorphicObject> parameters, DataConnector dataConnector)
            {
                // Initial Value
                PolymorphicObject returnObject = new PolymorphicObject();

                // locals
                List<Artist> artistListCollection =  null;

                // Create FetchAll StoredProcedure
                FetchAllArtistsStoredProcedure fetchAllProc = null;

                // If the data connection is connected
                if ((dataConnector != null) && (dataConnector.Connected == true))
                {
                    // Get ArtistParameter
                    // Declare Parameter
                    Artist paramArtist = null;

                    // verify the first parameters is a(n) 'Artist'.
                    if (parameters[0].ObjectValue as Artist != null)
                    {
                        // Get ArtistParameter
                        paramArtist = (Artist) parameters[0].ObjectValue;
                    }

                    // Now create FetchAllArtistsProc from ArtistWriter
                    fetchAllProc = ArtistWriter.CreateFetchAllArtistsStoredProcedure(paramArtist);
                }

                // Verify fetchAllProc exists
                if(fetchAllProc!= null)
                {
                    // Execute FetchAll Stored Procedure
                    artistListCollection = this.DataManager.ArtistManager.FetchAllArtists(fetchAllProc, dataConnector);

                    // if dataObjectCollection exists
                    if(artistListCollection != null)
                    {
                        // set returnObject.ObjectValue
                        returnObject.ObjectValue = artistListCollection;
                    }
                }
                else
                {
                    // Raise Error Data Connection Not Available
                    throw new Exception("The database connection is not available.");
                }

                // return value
                return returnObject;
            }
            #endregion

            #region FindArtist(Artist)
            /// <summary>
            /// This method finds a 'Artist' object.
            /// </summary>
            /// <param name='List<PolymorphicObject>'>The 'Artist' to delete.
            /// <returns>A PolymorphicObject object with a Boolean value.
            internal PolymorphicObject FindArtist(List<PolymorphicObject> parameters, DataConnector dataConnector)
            {
                // Initial Value
                PolymorphicObject returnObject = new PolymorphicObject();

                // locals
                Artist artist = null;

                // If the data connection is connected
                if ((dataConnector != null) && (dataConnector.Connected == true))
                {
                    // Create Find StoredProcedure
                    FindArtistStoredProcedure findArtistProc = null;

                    // verify the first parameters is a 'Artist'.
                    if (parameters[0].ObjectValue as Artist != null)
                    {
                        // Get ArtistParameter
                        Artist paramArtist = (Artist) parameters[0].ObjectValue;

                        // verify paramArtist exists
                        if(paramArtist != null)
                        {
                            // Now create findArtistProc from ArtistWriter
                            // The DataWriter converts the 'Artist'
                            // to the SqlParameter[] array needed to find a 'Artist'.
                            findArtistProc = ArtistWriter.CreateFindArtistStoredProcedure(paramArtist);
                        }

                        // Verify findArtistProc exists
                        if(findArtistProc != null)
                        {
                            // Execute Find Stored Procedure
                            artist = this.DataManager.ArtistManager.FindArtist(findArtistProc, dataConnector);

                            // if dataObject exists
                            if(artist != null)
                            {
                                // set returnObject.ObjectValue
                                returnObject.ObjectValue = artist;
                            }
                        }
                    }
                    else
                    {
                        // Raise Error Data Connection Not Available
                        throw new Exception("The database connection is not available.");
                    }
                }

                // return value
                return returnObject;
            }
            #endregion

            #region InsertArtist (Artist)
            /// <summary>
            /// This method inserts a 'Artist' object.
            /// </summary>
            /// <param name='List<PolymorphicObject>'>The 'Artist' to insert.
            /// <returns>A PolymorphicObject object with a Boolean value.
            internal PolymorphicObject InsertArtist(List<PolymorphicObject> parameters, DataConnector dataConnector)
            {
                // Initial Value
                PolymorphicObject returnObject = new PolymorphicObject();

                // locals
                Artist artist = null;

                // If the data connection is connected
                if ((dataConnector != null) && (dataConnector.Connected == true))
                {
                    // Create Insert StoredProcedure
                    InsertArtistStoredProcedure insertArtistProc = null;

                    // verify the first parameters is a(n) 'Artist'.
                    if (parameters[0].ObjectValue as Artist != null)
                    {
                        // Create Artist Parameter
                        artist = (Artist) parameters[0].ObjectValue;

                        // verify artist exists
                        if(artist != null)
                        {
                            // Now create insertArtistProc from ArtistWriter
                            // The DataWriter converts the 'Artist'
                            // to the SqlParameter[] array needed to insert a 'Artist'.
                            insertArtistProc = ArtistWriter.CreateInsertArtistStoredProcedure(artist);
                        }

                        // Verify insertArtistProc exists
                        if(insertArtistProc != null)
                        {
                            // Execute Insert Stored Procedure
                            returnObject.IntegerValue = this.DataManager.ArtistManager.InsertArtist(insertArtistProc, dataConnector);
                        }

                    }
                    else
                    {
                        // Raise Error Data Connection Not Available
                        throw new Exception("The database connection is not available.");
                    }
                }

                // return value
                return returnObject;
            }
            #endregion

            #region UpdateArtist (Artist)
            /// <summary>
            /// This method updates a 'Artist' object.
            /// </summary>
            /// <param name='List<PolymorphicObject>'>The 'Artist' to update.
            /// <returns>A PolymorphicObject object with a value.
            internal PolymorphicObject UpdateArtist(List<PolymorphicObject> parameters, DataConnector dataConnector)
            {
                // Initial Value
                PolymorphicObject returnObject = new PolymorphicObject();

                // locals
                Artist artist = null;

                // If the data connection is connected
                if ((dataConnector != null) && (dataConnector.Connected == true))
                {
                    // Create Update StoredProcedure
                    UpdateArtistStoredProcedure updateArtistProc = null;

                    // verify the first parameters is a(n) 'Artist'.
                    if (parameters[0].ObjectValue as Artist != null)
                    {
                        // Create Artist Parameter
                        artist = (Artist) parameters[0].ObjectValue;

                        // verify artist exists
                        if(artist != null)
                        {
                            // Now create updateArtistProc from ArtistWriter
                            // The DataWriter converts the 'Artist'
                            // to the SqlParameter[] array needed to update a 'Artist'.
                            updateArtistProc = ArtistWriter.CreateUpdateArtistStoredProcedure(artist);
                        }

                        // Verify updateArtistProc exists
                        if(updateArtistProc != null)
                        {
                            // Execute Update Stored Procedure
                            bool saved = this.DataManager.ArtistManager.UpdateArtist(updateArtistProc, dataConnector);

                            // Create returnObject.Boolean
                            returnObject.Boolean = new NullableBoolean();

                            // If save was successful
                            if(saved)
                            {
                                // Set returnObject.Boolean.Value to true
                                returnObject.Boolean.Value = NullableBooleanEnum.True;
                            }
                            else
                            {
                                // Set returnObject.Boolean.Value to false
                                returnObject.Boolean.Value = NullableBooleanEnum.False;
                            }
                        }
                        else
                        {
                            // Raise Error Data Connection Not Available
                            throw new Exception("The database connection is not available.");
                        }
                    }
                }

                // return value
                return returnObject;
            }
            #endregion

        #endregion

        #region Properties

            #region DataManager 
            public DataManager DataManager 
            {
                get { return dataManager; }
                set { dataManager = value; }
            }
            #endregion

        #endregion

    }
    #endregion

}
