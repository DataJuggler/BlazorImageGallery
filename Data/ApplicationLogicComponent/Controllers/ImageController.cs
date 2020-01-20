

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

    #region class ImageController
    /// <summary>
    /// This class controls a(n) 'Image' object.
    /// </summary>
    public class ImageController
    {

        #region Private Variables
        private ErrorHandler errorProcessor;
        private ApplicationController appController;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new 'ImageController' object.
        /// </summary>
        public ImageController(ErrorHandler errorProcessorArg, ApplicationController appControllerArg)
        {
            // Save Arguments
            this.ErrorProcessor = errorProcessorArg;
            this.AppController = appControllerArg;
        }
        #endregion

        #region Methods

            #region CreateImageParameter
            /// <summary>
            /// This method creates the parameter for a 'Image' data operation.
            /// </summary>
            /// <param name='image'>The 'Image' to use as the first
            /// parameter (parameters[0]).</param>
            /// <returns>A List<PolymorphicObject> collection.</returns>
            private List<PolymorphicObject> CreateImageParameter(Image image)
            {
                // Initial Value
                List<PolymorphicObject> parameters = new List<PolymorphicObject>();

                // Create PolymorphicObject to hold the parameter
                PolymorphicObject parameter = new PolymorphicObject();

                // Set parameter.ObjectValue
                parameter.ObjectValue = image;

                // Add userParameter to parameters
                parameters.Add(parameter);

                // return value
                return parameters;
            }
            #endregion

            #region Delete(Image tempImage)
            /// <summary>
            /// Deletes a 'Image' from the database
            /// This method calls the DataBridgeManager to execute the delete using the
            /// procedure 'Image_Delete'.
            /// </summary>
            /// <param name='image'>The 'Image' to delete.</param>
            /// <returns>True if the delete is successful or false if not.</returns>
            public bool Delete(Image tempImage)
            {
                // locals
                bool deleted = false;

                // Get information for calling 'DataBridgeManager.PerformDataOperation' method.
                string methodName = "DeleteImage";
                string objectName = "ApplicationLogicComponent.Controllers";

                try
                {
                    // verify tempimage exists before attemptintg to delete
                    if(tempImage != null)
                    {
                        // Create Delegate For DataOperation
                        ApplicationController.DataOperationMethod deleteImageMethod = this.AppController.DataBridge.DataOperations.ImageMethods.DeleteImage;

                        // Create parameters for this method
                        List<PolymorphicObject> parameters = CreateImageParameter(tempImage);

                        // Perform DataOperation
                        PolymorphicObject returnObject = this.AppController.DataBridge.PerformDataOperation(methodName, objectName, deleteImageMethod, parameters);

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

            #region FetchAll(Image tempImage)
            /// <summary>
            /// This method fetches a collection of 'Image' objects.
            /// This method used the DataBridgeManager to execute the fetch all using the
            /// procedure 'Image_FetchAll'.</summary>
            /// <param name='tempImage'>A temporary Image for passing values.</param>
            /// <returns>A collection of 'Image' objects.</returns>
            public List<Image> FetchAll(Image tempImage)
            {
                // Initial value
                List<Image> imageList = null;

                // Get information for calling 'DataBridgeManager.PerformDataOperation' method.
                string methodName = "FetchAll";
                string objectName = "ApplicationLogicComponent.Controllers";

                try
                {
                    // Create DataOperation Method
                    ApplicationController.DataOperationMethod fetchAllMethod = this.AppController.DataBridge.DataOperations.ImageMethods.FetchAll;

                    // Create parameters for this method
                    List<PolymorphicObject> parameters = CreateImageParameter(tempImage);

                    // Perform DataOperation
                    PolymorphicObject returnObject = this.AppController.DataBridge.PerformDataOperation(methodName, objectName, fetchAllMethod , parameters);

                    // If return object exists
                    if ((returnObject != null) && (returnObject.ObjectValue as List<Image> != null))
                    {
                        // Create Collection From ReturnObject.ObjectValue
                        imageList = (List<Image>) returnObject.ObjectValue;
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
                return imageList;
            }
            #endregion

            #region Find(Image tempImage)
            /// <summary>
            /// Finds a 'Image' object by the primary key.
            /// This method used the DataBridgeManager to execute the 'Find' using the
            /// procedure 'Image_Find'</param>
            /// </summary>
            /// <param name='tempImage'>A temporary Image for passing values.</param>
            /// <returns>A 'Image' object if found else a null 'Image'.</returns>
            public Image Find(Image tempImage)
            {
                // Initial values
                Image image = null;

                // Get information for calling 'DataBridgeManager.PerformDataOperation' method.
                string methodName = "Find";
                string objectName = "ApplicationLogicComponent.Controllers";

                try
                {
                    // If object exists
                    if(tempImage != null)
                    {
                        // Create DataOperation
                        ApplicationController.DataOperationMethod findMethod = this.AppController.DataBridge.DataOperations.ImageMethods.FindImage;

                        // Create parameters for this method
                        List<PolymorphicObject> parameters = CreateImageParameter(tempImage);

                        // Perform DataOperation
                        PolymorphicObject returnObject = this.AppController.DataBridge.PerformDataOperation(methodName, objectName, findMethod , parameters);

                        // If return object exists
                        if ((returnObject != null) && (returnObject.ObjectValue as Image != null))
                        {
                            // Get ReturnObject
                            image = (Image) returnObject.ObjectValue;
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
                return image;
            }
            #endregion

            #region Insert(Image image)
            /// <summary>
            /// Insert a 'Image' object into the database.
            /// This method uses the DataBridgeManager to execute the 'Insert' using the
            /// procedure 'Image_Insert'.</param>
            /// </summary>
            /// <param name='image'>The 'Image' object to insert.</param>
            /// <returns>The id (int) of the new  'Image' object that was inserted.</returns>
            public int Insert(Image image)
            {
                // Initial values
                int newIdentity = -1;

                // Get information for calling 'DataBridgeManager.PerformDataOperation' method.
                string methodName = "Insert";
                string objectName = "ApplicationLogicComponent.Controllers";

                try
                {
                    // If Imageexists
                    if(image != null)
                    {
                        ApplicationController.DataOperationMethod insertMethod = this.AppController.DataBridge.DataOperations.ImageMethods.InsertImage;

                        // Create parameters for this method
                        List<PolymorphicObject> parameters = CreateImageParameter(image);

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

            #region Save(ref Image image)
            /// <summary>
            /// Saves a 'Image' object into the database.
            /// This method calls the 'Insert' or 'Update' method.
            /// </summary>
            /// <param name='image'>The 'Image' object to save.</param>
            /// <returns>True if successful or false if not.</returns>
            public bool Save(ref Image image)
            {
                // Initial value
                bool saved = false;

                // If the image exists.
                if(image != null)
                {
                    // Is this a new Image
                    if(image.IsNew)
                    {
                        // Insert new Image
                        int newIdentity = this.Insert(image);

                        // if insert was successful
                        if(newIdentity > 0)
                        {
                            // Update Identity
                            image.UpdateIdentity(newIdentity);

                            // Set return value
                            saved = true;
                        }
                    }
                    else
                    {
                        // Update Image
                        saved = this.Update(image);
                    }
                }

                // return value
                return saved;
            }
            #endregion

            #region Update(Image image)
            /// <summary>
            /// This method Updates a 'Image' object in the database.
            /// This method used the DataBridgeManager to execute the 'Update' using the
            /// procedure 'Image_Update'.</param>
            /// </summary>
            /// <param name='image'>The 'Image' object to update.</param>
            /// <returns>True if successful else false if not.</returns>
            public bool Update(Image image)
            {
                // Initial value
                bool saved = false;

                // Get information for calling 'DataBridgeManager.PerformDataOperation' method.
                string methodName = "Update";
                string objectName = "ApplicationLogicComponent.Controllers";

                try
                {
                    if(image != null)
                    {
                        // Create Delegate
                        ApplicationController.DataOperationMethod updateMethod = this.AppController.DataBridge.DataOperations.ImageMethods.UpdateImage;

                        // Create parameters for this method
                        List<PolymorphicObject> parameters = CreateImageParameter(image);
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
