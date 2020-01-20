

#region using statements

using ApplicationLogicComponent.DataBridge;
using ApplicationLogicComponent.DataOperations;
using ApplicationLogicComponent.Logging;
using ObjectLibrary.BusinessObjects;
using System;
using System.Collections.Generic;

#endregion


namespace ApplicationLogicComponent.Controllers
{

    #region class ArtistController
    /// <summary>
    /// This class controls a(n) 'Artist' object.
    /// </summary>
    public class ArtistController
    {

        #region Private Variables
        private ErrorHandler errorProcessor;
        private ApplicationController appController;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new 'ArtistController' object.
        /// </summary>
        public ArtistController(ErrorHandler errorProcessorArg, ApplicationController appControllerArg)
        {
            // Save Arguments
            this.ErrorProcessor = errorProcessorArg;
            this.AppController = appControllerArg;
        }
        #endregion

        #region Methods

            #region CreateArtistParameter
            /// <summary>
            /// This method creates the parameter for a 'Artist' data operation.
            /// </summary>
            /// <param name='artist'>The 'Artist' to use as the first
            /// parameter (parameters[0]).</param>
            /// <returns>A List<PolymorphicObject> collection.</returns>
            private List<PolymorphicObject> CreateArtistParameter(Artist artist)
            {
                // Initial Value
                List<PolymorphicObject> parameters = new List<PolymorphicObject>();

                // Create PolymorphicObject to hold the parameter
                PolymorphicObject parameter = new PolymorphicObject();

                // Set parameter.ObjectValue
                parameter.ObjectValue = artist;

                // Add userParameter to parameters
                parameters.Add(parameter);

                // return value
                return parameters;
            }
            #endregion

            #region Delete(Artist tempArtist)
            /// <summary>
            /// Deletes a 'Artist' from the database
            /// This method calls the DataBridgeManager to execute the delete using the
            /// procedure 'Artist_Delete'.
            /// </summary>
            /// <param name='artist'>The 'Artist' to delete.</param>
            /// <returns>True if the delete is successful or false if not.</returns>
            public bool Delete(Artist tempArtist)
            {
                // locals
                bool deleted = false;

                // Get information for calling 'DataBridgeManager.PerformDataOperation' method.
                string methodName = "DeleteArtist";
                string objectName = "ApplicationLogicComponent.Controllers";

                try
                {
                    // verify tempartist exists before attemptintg to delete
                    if(tempArtist != null)
                    {
                        // Create Delegate For DataOperation
                        ApplicationController.DataOperationMethod deleteArtistMethod = this.AppController.DataBridge.DataOperations.ArtistMethods.DeleteArtist;

                        // Create parameters for this method
                        List<PolymorphicObject> parameters = CreateArtistParameter(tempArtist);

                        // Perform DataOperation
                        PolymorphicObject returnObject = this.AppController.DataBridge.PerformDataOperation(methodName, objectName, deleteArtistMethod, parameters);

                        // If return object exists
                        if (returnObject != null)
                        {
                            // Test For True
                            if (returnObject.Boolean.Value == NullableBooleanEnum.True)
                            {
                                // Set Deleted To True
                                deleted = true;
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    // If ErrorProcessor exists
                    if (this.ErrorProcessor != null)
                    {
                        // Log the current error
                        this.ErrorProcessor.LogError(methodName, objectName, error);
                    }
                }

                // return value
                return deleted;
            }
            #endregion

            #region FetchAll(Artist tempArtist)
            /// <summary>
            /// This method fetches a collection of 'Artist' objects.
            /// This method used the DataBridgeManager to execute the fetch all using the
            /// procedure 'Artist_FetchAll'.</summary>
            /// <param name='tempArtist'>A temporary Artist for passing values.</param>
            /// <returns>A collection of 'Artist' objects.</returns>
            public List<Artist> FetchAll(Artist tempArtist)
            {
                // Initial value
                List<Artist> artistList = null;

                // Get information for calling 'DataBridgeManager.PerformDataOperation' method.
                string methodName = "FetchAll";
                string objectName = "ApplicationLogicComponent.Controllers";

                try
                {
                    // Create DataOperation Method
                    ApplicationController.DataOperationMethod fetchAllMethod = this.AppController.DataBridge.DataOperations.ArtistMethods.FetchAll;

                    // Create parameters for this method
                    List<PolymorphicObject> parameters = CreateArtistParameter(tempArtist);

                    // Perform DataOperation
                    PolymorphicObject returnObject = this.AppController.DataBridge.PerformDataOperation(methodName, objectName, fetchAllMethod , parameters);

                    // If return object exists
                    if ((returnObject != null) && (returnObject.ObjectValue as List<Artist> != null))
                    {
                        // Create Collection From ReturnObject.ObjectValue
                        artistList = (List<Artist>) returnObject.ObjectValue;
                    }
                }
                catch (Exception error)
                {
                    // If ErrorProcessor exists
                    if (this.ErrorProcessor != null)
                    {
                        // Log the current error
                        this.ErrorProcessor.LogError(methodName, objectName, error);
                    }
                }

                // return value
                return artistList;
            }
            #endregion

            #region Find(Artist tempArtist)
            /// <summary>
            /// Finds a 'Artist' object by the primary key.
            /// This method used the DataBridgeManager to execute the 'Find' using the
            /// procedure 'Artist_Find'</param>
            /// </summary>
            /// <param name='tempArtist'>A temporary Artist for passing values.</param>
            /// <returns>A 'Artist' object if found else a null 'Artist'.</returns>
            public Artist Find(Artist tempArtist)
            {
                // Initial values
                Artist artist = null;

                // Get information for calling 'DataBridgeManager.PerformDataOperation' method.
                string methodName = "Find";
                string objectName = "ApplicationLogicComponent.Controllers";

                try
                {
                    // If object exists
                    if(tempArtist != null)
                    {
                        // Create DataOperation
                        ApplicationController.DataOperationMethod findMethod = this.AppController.DataBridge.DataOperations.ArtistMethods.FindArtist;

                        // Create parameters for this method
                        List<PolymorphicObject> parameters = CreateArtistParameter(tempArtist);

                        // Perform DataOperation
                        PolymorphicObject returnObject = this.AppController.DataBridge.PerformDataOperation(methodName, objectName, findMethod , parameters);

                        // If return object exists
                        if ((returnObject != null) && (returnObject.ObjectValue as Artist != null))
                        {
                            // Get ReturnObject
                            artist = (Artist) returnObject.ObjectValue;
                        }
                    }
                }
                catch (Exception error)
                {
                    // If ErrorProcessor exists
                    if (this.ErrorProcessor != null)
                    {
                        // Log the current error
                        this.ErrorProcessor.LogError(methodName, objectName, error);
                    }
                }

                // return value
                return artist;
            }
            #endregion

            #region Insert(Artist artist)
            /// <summary>
            /// Insert a 'Artist' object into the database.
            /// This method uses the DataBridgeManager to execute the 'Insert' using the
            /// procedure 'Artist_Insert'.</param>
            /// </summary>
            /// <param name='artist'>The 'Artist' object to insert.</param>
            /// <returns>The id (int) of the new  'Artist' object that was inserted.</returns>
            public int Insert(Artist artist)
            {
                // Initial values
                int newIdentity = -1;

                // Get information for calling 'DataBridgeManager.PerformDataOperation' method.
                string methodName = "Insert";
                string objectName = "ApplicationLogicComponent.Controllers";

                try
                {
                    // If Artistexists
                    if(artist != null)
                    {
                        ApplicationController.DataOperationMethod insertMethod = this.AppController.DataBridge.DataOperations.ArtistMethods.InsertArtist;

                        // Create parameters for this method
                        List<PolymorphicObject> parameters = CreateArtistParameter(artist);

                        // Perform DataOperation
                        PolymorphicObject returnObject = this.AppController.DataBridge.PerformDataOperation(methodName, objectName, insertMethod , parameters);

                        // If return object exists
                        if (returnObject != null)
                        {
                            // Set return value
                            newIdentity = returnObject.IntegerValue;
                        }
                    }
                }
                catch (Exception error)
                {
                    // If ErrorProcessor exists
                    if (this.ErrorProcessor != null)
                    {
                        // Log the current error
                        this.ErrorProcessor.LogError(methodName, objectName, error);
                    }
                }

                // return value
                return newIdentity;
            }
            #endregion

            #region Save(ref Artist artist)
            /// <summary>
            /// Saves a 'Artist' object into the database.
            /// This method calls the 'Insert' or 'Update' method.
            /// </summary>
            /// <param name='artist'>The 'Artist' object to save.</param>
            /// <returns>True if successful or false if not.</returns>
            public bool Save(ref Artist artist)
            {
                // Initial value
                bool saved = false;

                // If the artist exists.
                if(artist != null)
                {
                    // Is this a new Artist
                    if(artist.IsNew)
                    {
                        // Insert new Artist
                        int newIdentity = this.Insert(artist);

                        // if insert was successful
                        if(newIdentity > 0)
                        {
                            // Update Identity
                            artist.UpdateIdentity(newIdentity);

                            // Set return value
                            saved = true;
                        }
                    }
                    else
                    {
                        // Update Artist
                        saved = this.Update(artist);
                    }
                }

                // return value
                return saved;
            }
            #endregion

            #region Update(Artist artist)
            /// <summary>
            /// This method Updates a 'Artist' object in the database.
            /// This method used the DataBridgeManager to execute the 'Update' using the
            /// procedure 'Artist_Update'.</param>
            /// </summary>
            /// <param name='artist'>The 'Artist' object to update.</param>
            /// <returns>True if successful else false if not.</returns>
            public bool Update(Artist artist)
            {
                // Initial value
                bool saved = false;

                // Get information for calling 'DataBridgeManager.PerformDataOperation' method.
                string methodName = "Update";
                string objectName = "ApplicationLogicComponent.Controllers";

                try
                {
                    if(artist != null)
                    {
                        // Create Delegate
                        ApplicationController.DataOperationMethod updateMethod = this.AppController.DataBridge.DataOperations.ArtistMethods.UpdateArtist;

                        // Create parameters for this method
                        List<PolymorphicObject> parameters = CreateArtistParameter(artist);
                        // Perform DataOperation
                        PolymorphicObject returnObject = this.AppController.DataBridge.PerformDataOperation(methodName, objectName, updateMethod , parameters);

                        // If return object exists
                        if ((returnObject != null) && (returnObject.Boolean != null) && (returnObject.Boolean.Value == NullableBooleanEnum.True))
                        {
                            // Set saved to true
                            saved = true;
                        }
                    }
                }
                catch (Exception error)
                {
                    // If ErrorProcessor exists
                    if (this.ErrorProcessor != null)
                    {
                        // Log the current error
                        this.ErrorProcessor.LogError(methodName, objectName, error);
                    }
                }

                // return value
                return saved;
            }
            #endregion

        #endregion

        #region Properties

            #region AppController
            public ApplicationController AppController
            {
                get { return appController; }
                set { appController = value; }
            }
            #endregion

            #region ErrorProcessor
            public ErrorHandler ErrorProcessor
            {
                get { return errorProcessor; }
                set { errorProcessor = value; }
            }
            #endregion

        #endregion

    }
    #endregion

}
