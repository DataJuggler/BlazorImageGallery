
#region using statements

using ApplicationLogicComponent.Controllers;
using ApplicationLogicComponent.DataOperations;
using DataAccessComponent.DataManager;
using ObjectLibrary.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

#endregion

namespace DataGateway
{

    #region class Gateway
    /// <summary>
    /// This class is used to manage DataOperations
    /// between the client and the DataAccessComponent.
    /// Do not change the method name or the parameters for the
    /// code generated methods or they will be recreated the next 
    /// time you code generate with DataTier.Net. If you need additional
    /// parameters passed to a method either create an override or
    /// add or set properties to the temp object that is passed in.
    /// </summary>
    public class Gateway
    {

        #region Private Variables
        private ApplicationController appController;
        private string connectionName;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of a Gateway object.
        /// </summary>
        public Gateway(string connectionName = "")
        {
            // store the ConnectionName
            this.ConnectionName = connectionName;

            // Perform Initializations for this object
            Init();
        }
        #endregion

        #region Methods
        
            #region DeleteArtist(int id, Artist tempArtist = null)
            /// <summary>
            /// This method is used to delete Artist objects.
            /// </summary>
            /// <param name="id">Delete the Artist with this id</param>
            /// <param name="tempArtist">Pass in a tempArtist to perform a custom delete.</param>
            public bool DeleteArtist(int id, Artist tempArtist = null)
            {
                // initial value
                bool deleted = false;
        
                // if the AppController exists
                if (this.HasAppController)
                {
                    // if the tempArtist does not exist
                    if (tempArtist == null)
                    {
                        // create a temp Artist
                        tempArtist = new Artist();
                    }
        
                    // if the id is set
                    if (id > 0)
                    {
                        // set the primary key
                        tempArtist.UpdateIdentity(id);
                    }
        
                    // perform the delete
                    deleted = this.AppController.ControllerManager.ArtistController.Delete(tempArtist);
                }
        
                // return value
                return deleted;
            }
            #endregion
        
            #region DeleteImage(int id, Image tempImage = null)
            /// <summary>
            /// This method is used to delete Image objects.
            /// </summary>
            /// <param name="id">Delete the Image with this id</param>
            /// <param name="tempImage">Pass in a tempImage to perform a custom delete.</param>
            public bool DeleteImage(int id, Image tempImage = null)
            {
                // initial value
                bool deleted = false;
        
                // if the AppController exists
                if (this.HasAppController)
                {
                    // if the tempImage does not exist
                    if (tempImage == null)
                    {
                        // create a temp Image
                        tempImage = new Image();
                    }
        
                    // if the id is set
                    if (id > 0)
                    {
                        // set the primary key
                        tempImage.UpdateIdentity(id);
                    }
        
                    // perform the delete
                    deleted = this.AppController.ControllerManager.ImageController.Delete(tempImage);
                }
        
                // return value
                return deleted;
            }
            #endregion
        
            #region ExecuteNonQuery(string procedureName, SqlParameter[] sqlParameters)
            /// <summary>
            /// This method Executes a Non Query StoredProcedure
            /// </summary>
            public PolymorphicObject ExecuteNonQuery(string procedureName, SqlParameter[] sqlParameters)
            {
                // initial value
                PolymorphicObject returnValue = new PolymorphicObject();

                // locals
                List<PolymorphicObject> parameters = new List<PolymorphicObject>();

                // create the parameters
                PolymorphicObject procedureNameParameter = new PolymorphicObject();
                PolymorphicObject sqlParametersParameter = new PolymorphicObject();

                // if the procedureName exists
                if (!String.IsNullOrEmpty(procedureName))
                {
                    // Create an instance of the SystemMethods object
                    SystemMethods systemMethods = new SystemMethods();

                    // setup procedureNameParameter
                    procedureNameParameter.Name = "ProcedureName";
                    procedureNameParameter.Text = procedureName;

                    // setup sqlParametersParameter
                    sqlParametersParameter.Name = "SqlParameters";
                    sqlParametersParameter.ObjectValue = sqlParameters;

                    // Add these parameters to the parameters
                    parameters.Add(procedureNameParameter);
                    parameters.Add(sqlParametersParameter);

                    // get the dataConnector
                    DataAccessComponent.DataManager.DataConnector dataConnector = GetDataConnector();

                    // Execute the query
                    returnValue = systemMethods.ExecuteNonQuery(parameters, dataConnector);
                }

                // return value
                return returnValue;
            }
            #endregion

            #region FindArtist(int id, Artist tempArtist = null)
            /// <summary>
            /// This method is used to find 'Artist' objects.
            /// </summary>
            /// <param name="id">Find the Artist with this id</param>
            /// <param name="tempArtist">Pass in a tempArtist to perform a custom find.</param>
            public Artist FindArtist(int id, Artist tempArtist = null)
            {
                // initial value
                Artist artist = null;

                // if the AppController exists
                if (this.HasAppController)
                {
                    // if the tempArtist does not exist
                    if (tempArtist == null)
                    {
                        // create a temp Artist
                        tempArtist = new Artist();
                    }

                    // if the id is set
                    if (id > 0)
                    {
                        // set the primary key
                        tempArtist.UpdateIdentity(id);
                    }

                    // perform the find
                    artist = this.AppController.ControllerManager.ArtistController.Find(tempArtist);
                }

                // return value
                return artist;
            }
            #endregion

            #region FindArtistByEmailAddress(string emailAddress)
            /// <summary>
            /// This method is used to find 'Artist' objects for the EmailAddress given.
            /// </summary>
            public Artist FindArtistByEmailAddress(string emailAddress)
            {
                // initial value
                Artist artist = null;
                
                // Create a temp Artist object
                Artist tempArtist = new Artist();
                
                // Set the value for FindByEmailAddress to true
                tempArtist.FindByEmailAddress = true;
                
                // Set the value for EmailAddress
                tempArtist.EmailAddress = emailAddress;
                
                // Perform the find
                artist = FindArtist(0, tempArtist);
                
                // return value
                return artist;
            }
            #endregion
            
            #region FindImage(int id, Image tempImage = null)
            /// <summary>
            /// This method is used to find 'Image' objects.
            /// </summary>
            /// <param name="id">Find the Image with this id</param>
            /// <param name="tempImage">Pass in a tempImage to perform a custom find.</param>
            public Image FindImage(int id, Image tempImage = null)
            {
                // initial value
                Image image = null;

                // if the AppController exists
                if (this.HasAppController)
                {
                    // if the tempImage does not exist
                    if (tempImage == null)
                    {
                        // create a temp Image
                        tempImage = new Image();
                    }

                    // if the id is set
                    if (id > 0)
                    {
                        // set the primary key
                        tempImage.UpdateIdentity(id);
                    }

                    // perform the find
                    image = this.AppController.ControllerManager.ImageController.Find(tempImage);
                }

                // return value
                return image;
            }
            #endregion

            #region GetDataConnector()
            /// <summary>
            /// This method (safely) returns the Data Connector from the
            /// AppController.DataBridget.DataManager.DataConnector
            /// </summary>
            private DataConnector GetDataConnector()
            {
                // initial value
                DataConnector dataConnector = null;

                // if the AppController exists
                if (this.AppController != null)
                {
                    // return the DataConnector from the AppController
                    dataConnector = this.AppController.GetDataConnector();
                }

                // return value
                return dataConnector;
            }
            #endregion

            #region GetLastException()
            /// <summary>
            /// This method returns the last Exception from the AppController if one exists.
            /// Always test for null before refeferencing the Exception returned as it will be null 
            /// if no errors were encountered.
            /// </summary>
            /// <returns></returns>
            public Exception GetLastException()
            {
                // initial value
                Exception exception = null;

                // If the AppController object exists
                if (this.HasAppController)
                {
                    // return the Exception from the AppController
                    exception = this.AppController.Exception;

                    // Set to null after the exception is retrieved so it does not return again
                    this.AppController.Exception = null;
                }

                // return value
                return exception;
            }
            #endregion

            #region Init()
            /// <summary>
            /// Perform Initializations for this object.
            /// </summary>
            private void Init()
            {
                // Create Application Controller
                this.AppController = new ApplicationController(ConnectionName);
            }
            #endregion

            #region LoadArtists(Artist tempArtist = null)
            /// <summary>
            /// This method loads a collection of 'Artist' objects.
            /// </summary>
            public List<Artist> LoadArtists(Artist tempArtist = null)
            {
                // initial value
                List<Artist> artists = null;

                // if the AppController exists
                if (this.HasAppController)
                {
                    // perform the load
                    artists = this.AppController.ControllerManager.ArtistController.FetchAll(tempArtist);
                }

                // return value
                return artists;
            }
            #endregion

            #region LoadImages(Image tempImage = null)
            /// <summary>
            /// This method loads a collection of 'Image' objects.
            /// </summary>
            public List<Image> LoadImages(Image tempImage = null)
            {
                // initial value
                List<Image> images = null;

                // if the AppController exists
                if (this.HasAppController)
                {
                    // perform the load
                    images = this.AppController.ControllerManager.ImageController.FetchAll(tempImage);
                }

                // return value
                return images;
            }
            #endregion

            #region SaveArtist(ref Artist artist)
            /// <summary>
            /// This method is used to save 'Artist' objects.
            /// </summary>
            /// <param name="artist">The Artist to save.</param>
            public bool SaveArtist(ref Artist artist)
            {
                // initial value
                bool saved = false;

                // if the AppController exists
                if (this.HasAppController)
                {
                    // perform the save
                    saved = this.AppController.ControllerManager.ArtistController.Save(ref artist);
                }

                // return value
                return saved;
            }
            #endregion

            #region SaveImage(ref Image image)
            /// <summary>
            /// This method is used to save 'Image' objects.
            /// </summary>
            /// <param name="image">The Image to save.</param>
            public bool SaveImage(ref Image image)
            {
                // initial value
                bool saved = false;

                // if the AppController exists
                if (this.HasAppController)
                {
                    // perform the save
                    saved = this.AppController.ControllerManager.ImageController.Save(ref image);
                }

                // return value
                return saved;
            }
            #endregion

        #endregion

        #region Properties

            #region AppController
            /// <summary>
            /// This property gets or sets the value for 'AppController'.
            /// </summary>
            public ApplicationController AppController
            {
                get { return appController; }
                set { appController = value; }
            }
            #endregion

            #region ConnectionName
            /// <summary>
            /// This property gets or sets the value for 'ConnectionName'.
            /// </summary>
            public string ConnectionName
            {
                get { return connectionName; }
                set { connectionName = value; }
            }
            #endregion
            
            #region HasAppController
            /// <summary>
            /// This property returns true if this object has an 'AppController'.
            /// </summary>
            public bool HasAppController
            {
                get
                {
                    // initial value
                    bool hasAppController = (this.AppController != null);

                    // return value
                    return hasAppController;
                }
            }
            #endregion

            #region HasConnectionName
            /// <summary>
            /// This property returns true if the 'ConnectionName' exists.
            /// </summary>
            public bool HasConnectionName
            {
                get
                {
                    // initial value
                    bool hasConnectionName = (!String.IsNullOrEmpty(this.ConnectionName));
                    
                    // return value
                    return hasConnectionName;
                }
            }
            #endregion
            
        #endregion

    }
    #endregion

}

