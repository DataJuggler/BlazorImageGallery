

#region using statements

using System;
using DataJuggler.Net.Core.Delegates;
using DataJuggler.Net.Core.Enumerations;

#endregion


namespace ObjectLibrary.BusinessObjects
{

    #region class Image
    public partial class Image
    {

        #region Private Variables
        private DateTime createdDate;
        private string extension;
        private int fileSize;
        private string fullPath;
        private int height;
        private int id;
        private int imageNumber;
        private string imageUrl;
        private string name;
        private int ownerId;
        private string sitePath;
        private bool visible;
        private int width;
        private ItemChangedCallback callback;
        #endregion

        #region Methods

            #region UpdateIdentity(int id)
            // <summary>
            // This method provides a 'setter'
            // functionality for the Identity field.
            // </summary>
            public void UpdateIdentity(int id)
            {
                // Update The Identity field
                this.id = id;
            }
            #endregion

        #endregion

        #region Properties

            #region DateTime CreatedDate
            public DateTime CreatedDate
            {
                get
                {
                    return createdDate;
                }
                set
                {
                    // local
                    bool hasChanges = (CreatedDate != value);

                    // Set the value
                    createdDate = value;

                    // if the Callback exists and changes occurred
                    if ((HasCallback) && (hasChanges))
                    {
                        // Notify the Callback changes have occurred
                        Callback(this, ChangeTypeEnum.ItemChanged);
                    }
                }
            }
            #endregion

            #region string Extension
            public string Extension
            {
                get
                {
                    return extension;
                }
                set
                {
                    // local
                    bool hasChanges = (Extension != value);

                    // Set the value
                    extension = value;

                    // if the Callback exists and changes occurred
                    if ((HasCallback) && (hasChanges))
                    {
                        // Notify the Callback changes have occurred
                        Callback(this, ChangeTypeEnum.ItemChanged);
                    }
                }
            }
            #endregion

            #region int FileSize
            public int FileSize
            {
                get
                {
                    return fileSize;
                }
                set
                {
                    // local
                    bool hasChanges = (FileSize != value);

                    // Set the value
                    fileSize = value;

                    // if the Callback exists and changes occurred
                    if ((HasCallback) && (hasChanges))
                    {
                        // Notify the Callback changes have occurred
                        Callback(this, ChangeTypeEnum.ItemChanged);
                    }
                }
            }
            #endregion

            #region string FullPath
            public string FullPath
            {
                get
                {
                    return fullPath;
                }
                set
                {
                    // local
                    bool hasChanges = (FullPath != value);

                    // Set the value
                    fullPath = value;

                    // if the Callback exists and changes occurred
                    if ((HasCallback) && (hasChanges))
                    {
                        // Notify the Callback changes have occurred
                        Callback(this, ChangeTypeEnum.ItemChanged);
                    }
                }
            }
            #endregion

            #region int Height
            public int Height
            {
                get
                {
                    return height;
                }
                set
                {
                    // local
                    bool hasChanges = (Height != value);

                    // Set the value
                    height = value;

                    // if the Callback exists and changes occurred
                    if ((HasCallback) && (hasChanges))
                    {
                        // Notify the Callback changes have occurred
                        Callback(this, ChangeTypeEnum.ItemChanged);
                    }
                }
            }
            #endregion

            #region int Id
            public int Id
            {
                get
                {
                    return id;
                }
            }
            #endregion

            #region int ImageNumber
            public int ImageNumber
            {
                get
                {
                    return imageNumber;
                }
                set
                {
                    // local
                    bool hasChanges = (ImageNumber != value);

                    // Set the value
                    imageNumber = value;

                    // if the Callback exists and changes occurred
                    if ((HasCallback) && (hasChanges))
                    {
                        // Notify the Callback changes have occurred
                        Callback(this, ChangeTypeEnum.ItemChanged);
                    }
                }
            }
            #endregion

            #region string ImageUrl
            public string ImageUrl
            {
                get
                {
                    return imageUrl;
                }
                set
                {
                    // local
                    bool hasChanges = (ImageUrl != value);

                    // Set the value
                    imageUrl = value;

                    // if the Callback exists and changes occurred
                    if ((HasCallback) && (hasChanges))
                    {
                        // Notify the Callback changes have occurred
                        Callback(this, ChangeTypeEnum.ItemChanged);
                    }
                }
            }
            #endregion

            #region string Name
            public string Name
            {
                get
                {
                    return name;
                }
                set
                {
                    // local
                    bool hasChanges = (Name != value);

                    // Set the value
                    name = value;

                    // if the Callback exists and changes occurred
                    if ((HasCallback) && (hasChanges))
                    {
                        // Notify the Callback changes have occurred
                        Callback(this, ChangeTypeEnum.ItemChanged);
                    }
                }
            }
            #endregion

            #region int OwnerId
            public int OwnerId
            {
                get
                {
                    return ownerId;
                }
                set
                {
                    // local
                    bool hasChanges = (OwnerId != value);

                    // Set the value
                    ownerId = value;

                    // if the Callback exists and changes occurred
                    if ((HasCallback) && (hasChanges))
                    {
                        // Notify the Callback changes have occurred
                        Callback(this, ChangeTypeEnum.ItemChanged);
                    }
                }
            }
            #endregion

            #region string SitePath
            public string SitePath
            {
                get
                {
                    return sitePath;
                }
                set
                {
                    // local
                    bool hasChanges = (SitePath != value);

                    // Set the value
                    sitePath = value;

                    // if the Callback exists and changes occurred
                    if ((HasCallback) && (hasChanges))
                    {
                        // Notify the Callback changes have occurred
                        Callback(this, ChangeTypeEnum.ItemChanged);
                    }
                }
            }
            #endregion

            #region bool Visible
            public bool Visible
            {
                get
                {
                    return visible;
                }
                set
                {
                    // local
                    bool hasChanges = (Visible != value);

                    // Set the value
                    visible = value;

                    // if the Callback exists and changes occurred
                    if ((HasCallback) && (hasChanges))
                    {
                        // Notify the Callback changes have occurred
                        Callback(this, ChangeTypeEnum.ItemChanged);
                    }
                }
            }
            #endregion

            #region int Width
            public int Width
            {
                get
                {
                    return width;
                }
                set
                {
                    // local
                    bool hasChanges = (Width != value);

                    // Set the value
                    width = value;

                    // if the Callback exists and changes occurred
                    if ((HasCallback) && (hasChanges))
                    {
                        // Notify the Callback changes have occurred
                        Callback(this, ChangeTypeEnum.ItemChanged);
                    }
                }
            }
            #endregion

            #region bool IsNew
            public bool IsNew
            {
                get
                {
                    // Initial Value
                    bool isNew = (this.Id < 1);

                    // return value
                    return isNew;
                }
            }
            #endregion

            #region ItemChangedCallback Callback
            public ItemChangedCallback Callback
            {
                get
                {
                    return callback;
                }
                set
                {
                    callback = value;
                }
            }
            #endregion

            #region bool HasCallback
            public bool HasCallback
            {
                get
                {
                    // Initial Value
                    bool hasCallback = (this.Callback != null);

                    // return value
                    return hasCallback;
                }
            }
            #endregion

        #endregion

    }
    #endregion

}
