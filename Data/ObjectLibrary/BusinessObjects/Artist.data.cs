

#region using statements

using System;
using DataJuggler.Net.Core.Delegates;
using DataJuggler.Net.Core.Enumerations;

#endregion


namespace ObjectLibrary.BusinessObjects
{

    #region class Artist
    public partial class Artist
    {

        #region Private Variables
        private bool active;
        private DateTime createdDate;
        private string emailAddress;
        private string folderPath;
        private int id;
        private int imagesCount;
        private bool isAdmin;
        private DateTime lastUpdated;
        private string name;
        private string passwordHash;
        private string profilePicture;
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

            #region bool Active
            public bool Active
            {
                get
                {
                    return active;
                }
                set
                {
                    // local
                    bool hasChanges = (Active != value);

                    // Set the value
                    active = value;

                    // if the Callback exists and changes occurred
                    if ((HasCallback) && (hasChanges))
                    {
                        // Notify the Callback changes have occurred
                        Callback(this, ChangeTypeEnum.ItemChanged);
                    }
                }
            }
            #endregion

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

            #region string EmailAddress
            public string EmailAddress
            {
                get
                {
                    return emailAddress;
                }
                set
                {
                    // local
                    bool hasChanges = (EmailAddress != value);

                    // Set the value
                    emailAddress = value;

                    // if the Callback exists and changes occurred
                    if ((HasCallback) && (hasChanges))
                    {
                        // Notify the Callback changes have occurred
                        Callback(this, ChangeTypeEnum.ItemChanged);
                    }
                }
            }
            #endregion

            #region string FolderPath
            public string FolderPath
            {
                get
                {
                    return folderPath;
                }
                set
                {
                    // local
                    bool hasChanges = (FolderPath != value);

                    // Set the value
                    folderPath = value;

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

            #region int ImagesCount
            public int ImagesCount
            {
                get
                {
                    return imagesCount;
                }
                set
                {
                    // local
                    bool hasChanges = (ImagesCount != value);

                    // Set the value
                    imagesCount = value;

                    // if the Callback exists and changes occurred
                    if ((HasCallback) && (hasChanges))
                    {
                        // Notify the Callback changes have occurred
                        Callback(this, ChangeTypeEnum.ItemChanged);
                    }
                }
            }
            #endregion

            #region bool IsAdmin
            public bool IsAdmin
            {
                get
                {
                    return isAdmin;
                }
                set
                {
                    // local
                    bool hasChanges = (IsAdmin != value);

                    // Set the value
                    isAdmin = value;

                    // if the Callback exists and changes occurred
                    if ((HasCallback) && (hasChanges))
                    {
                        // Notify the Callback changes have occurred
                        Callback(this, ChangeTypeEnum.ItemChanged);
                    }
                }
            }
            #endregion

            #region DateTime LastUpdated
            public DateTime LastUpdated
            {
                get
                {
                    return lastUpdated;
                }
                set
                {
                    // local
                    bool hasChanges = (LastUpdated != value);

                    // Set the value
                    lastUpdated = value;

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

            #region string PasswordHash
            public string PasswordHash
            {
                get
                {
                    return passwordHash;
                }
                set
                {
                    // local
                    bool hasChanges = (PasswordHash != value);

                    // Set the value
                    passwordHash = value;

                    // if the Callback exists and changes occurred
                    if ((HasCallback) && (hasChanges))
                    {
                        // Notify the Callback changes have occurred
                        Callback(this, ChangeTypeEnum.ItemChanged);
                    }
                }
            }
            #endregion

            #region string ProfilePicture
            public string ProfilePicture
            {
                get
                {
                    return profilePicture;
                }
                set
                {
                    // local
                    bool hasChanges = (ProfilePicture != value);

                    // Set the value
                    profilePicture = value;

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
