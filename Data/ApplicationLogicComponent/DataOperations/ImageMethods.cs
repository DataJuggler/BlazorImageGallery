

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

    #region class ImageMethods
    /// <summary>
    /// This class contains methods for modifying a 'Image' object.
    /// </summary>
    public class ImageMethods
    {

        #region Private Variables
        private DataManager dataManager;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a new Creates a new 'ImageMethods' object.
        /// </summary>
        public ImageMethods(DataManager dataManagerArg)
        {
            // Save Argument
            this.DataManager = dataManagerArg;
        }
        #endregion

        #region Methods

            #region DeleteImage(Image)
            /// <summary>
            /// This method deletes a 'Image' object.
            /// </summary>
            /// <param name='List<PolymorphicObject>'>The 'Image' to delete.
            /// <returns>A PolymorphicObject object with a Boolean value.
            internal PolymorphicObject DeleteImage(List<PolymorphicObject> parameters, DataConnector dataConnector)
            {
                // Initial Value
                PolymorphicObject returnObject = new PolymorphicObject();

                // If the data connection is connected
                if ((dataConnector != null) && (dataConnector.Connected == true))
                {
                    // Create Delete StoredProcedure
                    DeleteImageStoredProcedure deleteImageProc = null;

                    // verify the first parameters is a(n) 'Image'.
                    if (parameters[0].ObjectValue as Image != null)
                    {
                        // Create Image
                        Image image = (Image) parameters[0].ObjectValue;

                        // verify image exists
                        if(image != null)
                        {
                            // Now create deleteImageProc from ImageWriter
                            // The DataWriter converts the 'Image'
                            // to the SqlParameter[] array needed to delete a 'Image'.
                            deleteImageProc = ImageWriter.CreateDeleteImageStoredProcedure(image);
                        }
                    }

                    // Verify deleteImageProc exists
                    if(deleteImageProc != null)
                    {
                        // Execute Delete Stored Procedure
                        bool deleted = this.DataManager.ImageManager.DeleteImage(deleteImageProc, dataConnector);

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
            /// This method fetches all 'Image' objects.
            /// </summary>
            /// <param name='List<PolymorphicObject>'>The 'Image' to delete.
            /// <returns>A PolymorphicObject object with all  'Images' objects.
            internal PolymorphicObject FetchAll(List<PolymorphicObject> parameters, DataConnector dataConnector)
            {
                // Initial Value
                PolymorphicObject returnObject = new PolymorphicObject();

                // locals
                List<Image> imageListCollection =  null;

                // Create FetchAll StoredProcedure
                FetchAllImagesStoredProcedure fetchAllProc = null;

                // If the data connection is connected
                if ((dataConnector != null) && (dataConnector.Connected == true))
                {
                    // Get ImageParameter
                    // Declare Parameter
                    Image paramImage = null;

                    // verify the first parameters is a(n) 'Image'.
                    if (parameters[0].ObjectValue as Image != null)
                    {
                        // Get ImageParameter
                        paramImage = (Image) parameters[0].ObjectValue;
                    }

                    // Now create FetchAllImagesProc from ImageWriter
                    fetchAllProc = ImageWriter.CreateFetchAllImagesStoredProcedure(paramImage);
                }

                // Verify fetchAllProc exists
                if(fetchAllProc!= null)
                {
                    // Execute FetchAll Stored Procedure
                    imageListCollection = this.DataManager.ImageManager.FetchAllImages(fetchAllProc, dataConnector);

                    // if dataObjectCollection exists
                    if(imageListCollection != null)
                    {
                        // set returnObject.ObjectValue
                        returnObject.ObjectValue = imageListCollection;
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

            #region FindImage(Image)
            /// <summary>
            /// This method finds a 'Image' object.
            /// </summary>
            /// <param name='List<PolymorphicObject>'>The 'Image' to delete.
            /// <returns>A PolymorphicObject object with a Boolean value.
            internal PolymorphicObject FindImage(List<PolymorphicObject> parameters, DataConnector dataConnector)
            {
                // Initial Value
                PolymorphicObject returnObject = new PolymorphicObject();

                // locals
                Image image = null;

                // If the data connection is connected
                if ((dataConnector != null) && (dataConnector.Connected == true))
                {
                    // Create Find StoredProcedure
                    FindImageStoredProcedure findImageProc = null;

                    // verify the first parameters is a 'Image'.
                    if (parameters[0].ObjectValue as Image != null)
                    {
                        // Get ImageParameter
                        Image paramImage = (Image) parameters[0].ObjectValue;

                        // verify paramImage exists
                        if(paramImage != null)
                        {
                            // Now create findImageProc from ImageWriter
                            // The DataWriter converts the 'Image'
                            // to the SqlParameter[] array needed to find a 'Image'.
                            findImageProc = ImageWriter.CreateFindImageStoredProcedure(paramImage);
                        }

                        // Verify findImageProc exists
                        if(findImageProc != null)
                        {
                            // Execute Find Stored Procedure
                            image = this.DataManager.ImageManager.FindImage(findImageProc, dataConnector);

                            // if dataObject exists
                            if(image != null)
                            {
                                // set returnObject.ObjectValue
                                returnObject.ObjectValue = image;
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

            #region InsertImage (Image)
            /// <summary>
            /// This method inserts a 'Image' object.
            /// </summary>
            /// <param name='List<PolymorphicObject>'>The 'Image' to insert.
            /// <returns>A PolymorphicObject object with a Boolean value.
            internal PolymorphicObject InsertImage(List<PolymorphicObject> parameters, DataConnector dataConnector)
            {
                // Initial Value
                PolymorphicObject returnObject = new PolymorphicObject();

                // locals
                Image image = null;

                // If the data connection is connected
                if ((dataConnector != null) && (dataConnector.Connected == true))
                {
                    // Create Insert StoredProcedure
                    InsertImageStoredProcedure insertImageProc = null;

                    // verify the first parameters is a(n) 'Image'.
                    if (parameters[0].ObjectValue as Image != null)
                    {
                        // Create Image Parameter
                        image = (Image) parameters[0].ObjectValue;

                        // verify image exists
                        if(image != null)
                        {
                            // Now create insertImageProc from ImageWriter
                            // The DataWriter converts the 'Image'
                            // to the SqlParameter[] array needed to insert a 'Image'.
                            insertImageProc = ImageWriter.CreateInsertImageStoredProcedure(image);
                        }

                        // Verify insertImageProc exists
                        if(insertImageProc != null)
                        {
                            // Execute Insert Stored Procedure
                            returnObject.IntegerValue = this.DataManager.ImageManager.InsertImage(insertImageProc, dataConnector);
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

            #region UpdateImage (Image)
            /// <summary>
            /// This method updates a 'Image' object.
            /// </summary>
            /// <param name='List<PolymorphicObject>'>The 'Image' to update.
            /// <returns>A PolymorphicObject object with a value.
            internal PolymorphicObject UpdateImage(List<PolymorphicObject> parameters, DataConnector dataConnector)
            {
                // Initial Value
                PolymorphicObject returnObject = new PolymorphicObject();

                // locals
                Image image = null;

                // If the data connection is connected
                if ((dataConnector != null) && (dataConnector.Connected == true))
                {
                    // Create Update StoredProcedure
                    UpdateImageStoredProcedure updateImageProc = null;

                    // verify the first parameters is a(n) 'Image'.
                    if (parameters[0].ObjectValue as Image != null)
                    {
                        // Create Image Parameter
                        image = (Image) parameters[0].ObjectValue;

                        // verify image exists
                        if(image != null)
                        {
                            // Now create updateImageProc from ImageWriter
                            // The DataWriter converts the 'Image'
                            // to the SqlParameter[] array needed to update a 'Image'.
                            updateImageProc = ImageWriter.CreateUpdateImageStoredProcedure(image);
                        }

                        // Verify updateImageProc exists
                        if(updateImageProc != null)
                        {
                            // Execute Update Stored Procedure
                            bool saved = this.DataManager.ImageManager.UpdateImage(updateImageProc, dataConnector);

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
