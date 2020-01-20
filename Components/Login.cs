

#region using statements

using System;
using Microsoft.AspNetCore.ProtectedBrowserStorage;
using System.Threading.Tasks;
using DataJuggler.UltimateHelper.Core;
using ObjectLibrary.BusinessObjects;
using ObjectLibrary.Enumerations;
using ObjectLibrary.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using DataGateway.Services;
using DataJuggler.Core.Cryptography;
using DataJuggler.Blazor.FileUpload;
using System.IO;

#endregion

namespace BlazorImageGallery.Components
{

    #region class Login
    /// <summary>
    /// This is the code behind for the login control.
    /// </summary>
    public partial class Login
    {
        
        #region Private Variables
        private bool noAction;
        private string action;
        private string message;
        private string password;
        private string confirmPassword;
        private string displayName;
        private string emailAddress;
        private Artist artist;
        private string profileImageUrl;
        private string profileImageStyle;
        private bool showUploadButton;
        private bool showProfileMenu;
        private bool rememberLogin;
        private string storedPasswordHash;
        private bool showProgress;
        private string blueProgress;
        private const string NoProfileImagePath = "../images/avatars/NoProfileImage.png";
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a 'Login' object.
        /// </summary>
        public Login()
        {
        }
        #endregion
        
        #region Methods
            
            #region Cancel()
            /// <summary>
            /// method Cancel
            /// </summary>
            private void Cancel()
            {
                // reset
                action = "";
                noAction = true;
                OnReset("FromCancel");
                
                // Update the UI
                StateHasChanged();
            }
            #endregion
            
            #region HandleLogin()
            /// <summary>
            /// Handle the user logging in
            /// </summary>
            private async void HandleLogin()
            {
                // local
                LoginResponse loginResponse = null;

                try
                {
                    // If the StoredPasswordHash exists, then we know this is from a RememberPassword
                    bool passwordIsAlreadyHashed = TextHelper.Exists(StoredPasswordHash);

                    // if the value for passwordIsAlreadyHashed is true
                    if (passwordIsAlreadyHashed)
                    {
                        // Get the loginResponse using the StoredPasswordHash
                        loginResponse = await ArtistService.Login(emailAddress, StoredPasswordHash, passwordIsAlreadyHashed);     
                    }
                    else
                    {
                        // Get the loginResponse
                        loginResponse = await ArtistService.Login(emailAddress, password);     
                    }
                
                    // if the loginResponse exists
                    if ((NullHelper.Exists(loginResponse)) && (loginResponse.Success))
                    {
                        // set the player
                        artist = loginResponse.Artist;

                        // Notify the caller of the Login
                        await OnLogin.InvokeAsync(loginResponse);

                        // Refresh
                        StateHasChanged();

                        // if remember login details is true
                        if (RememberLogin)
                        {
                            await ProtectedLocalStore.SetAsync("RememberLogin", rememberLogin);
                            await ProtectedLocalStore.SetAsync("ArtistEmailAddress", artist.EmailAddress);
                            await ProtectedLocalStore.SetAsync("ArtistPasswordHash", artist.PasswordHash);
                        }
                        else
                        {
                            // Remove any locally stored items
                            RemovedLocalStoreItems();
                        }
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    DebugHelper.WriteDebugError("HandleLogin", "Login.cs", error);
                }
            }
            #endregion
            
            #region HandleNewUserSignUp()
            /// <summary>
            /// This method creates a new user and saves itl
            /// </summary>
            private async void HandleNewUserSignUp()
            {
                try
                {
                    // locals
                    string passwordHash = "";
                    bool abort = false;
                    message = "";

                    // Show the progress bar
                    ShowProgress = true;

                    // create a new Artist
                    this.Artist = new Artist();
                
                    // get the key
                    string key = EnvironmentVariableHelper.GetEnvironmentVariableValue("BlazorImageGallery");
                
                    // if the key was found
                    if (TextHelper.Exists(key))
                    {
                        // get the encryptedPassword
                        passwordHash = CryptographyHelper.GeneratePasswordHash(password, key, 3);
                    }
                
                    // set the artistPath
                    string artistPath = Path.Combine("Images/Gallery", displayName.Replace(" ", ""));

                    // saves going to the database to lookup if the artist exists
                    if (Directory.Exists(artistPath))
                    {
                        // abort due to artist already exists
                        abort = true;

                        // Set the message
                        Message = "This artist already exists. Sign in if this is you.";
                    }
                    else
                    {
                        // create the artistFolder
                        Directory.CreateDirectory(artistPath);
                    }
                
                    // if we should continue
                    if (!abort)
                    {
                        // set the bound properties
                        artist.EmailAddress = emailAddress;
                        artist.PasswordHash = passwordHash;
                        artist.CreatedDate = DateTime.Now;
                        artist.Active = true;
                        artist.Name = displayName;
                        artist.CreatedDate = DateTime.Now;
                        artist.LastUpdated = DateTime.Now;
                        artist.FolderPath = artistPath;
                        artist.ProfilePicture = ProfileImageUrl;
                        artist.Active = true;
                
                        // Validate this player
                        bool isValid = ValidateArtist();
                
                        // if isValid
                        if (isValid)
                        {
                            // perform the save
                            bool saved = await ArtistService.SaveArtist(ref artist);
                    
                            // if saved
                            if (saved)
                            {
                                // Get the loginResponse
                                LoginResponse loginResponse = await ArtistService.Login(emailAddress, password, false);
                        
                                // if the loginResponse exists
                                if (NullHelper.Exists(loginResponse))
                                {
                                    // set the player
                                    artist = loginResponse.Artist;
                            
                                    // Perform the Login
                                    await OnLogin.InvokeAsync(loginResponse);
                                }
                            }

                            // Refresh
                            StateHasChanged();
                        }
                        else
                        {
                            abort = true;
                        }
                   }                   
                }
                catch (Exception error)
                {
                    // Cancel the login
                    Cancel();

                    // for debugging only for now
                    DebugHelper.WriteDebugError("HandleNewUserSignUp", "Login.cs", error);
                }
                finally
                {
                    // turn off progress
                    ShowProgress = false;
                }
            }
            #endregion
            
            #region Init()
            /// <summary>
            /// This method performs initializations for this object.
            /// </summary>
            public void Init()
            {
                // Set defaults
                this.NoAction = true;
                Action = "";
                ProfileImageUrl = NoProfileImagePath;
                ShowUploadButton = true;

                // Erase the displayName property
                displayName = "";

                // if RememberLogin is false
                if (!RememberLogin)
                {
                    // Erase all these values
                    EmailAddress = "";
                    Password = "";
                    StoredPasswordHash = "";
                }
            }
            #endregion

            #region OnFileUploaded(UploadedFileInfo uploadedFileInfo)
            /// <summary>
            /// This method is called by DataJuggler.Blazor.FileUpload after a file is uploaded.
            /// </summary>
            /// <param name="uploadedFileInfo"></param>
            private void OnFileUploaded(UploadedFileInfo uploadedFileInfo)
            {
                // if aborted
                if (uploadedFileInfo.Aborted)
                {
                    // get the status
                    this.Message = uploadedFileInfo.ErrorMessage;
                }
                else
                {
                    //// get the status
                    //// status = "The file " + uploadedFileInfo.FullName + " was uploaded.";

                    //// other information about the file is available
                    //DateTime lastModified = uploadedFileInfo.LastModified;
                    //string nameAsItIsOnDisk = uploadedFileInfo.NameWithPartialGuid;
                    //string partialGuid = uploadedFileInfo.PartialGuid;
                    //long size = uploadedFileInfo.Size;
                    //string type = uploadedFileInfo.Type;

                    // Set the ProfileImageUrl
                    ProfileImageUrl = "../images/artists/" + uploadedFileInfo.FullName;

                    // Handled for custom button
                    showUploadButton = false;

                    // The above information can be used to display, and / or process a file
                }

                // Refresh the UI
                StateHasChanged();
            }
            #endregion

            #region OnInitializedAsync()
            /// <summary>
            /// This method is used to restored values stored in local storage
            /// </summary>
            protected override async Task OnInitializedAsync()
            {
                try
                {
                    // get the values from local storage if present
                    RememberLogin = await ProtectedLocalStore.GetAsync<bool>("RememberLogin");

                    // if RememberLogin is true
                    if (RememberLogin)
                    {
                        emailAddress = await ProtectedLocalStore.GetAsync<string>("ArtistEmailAddress");
                        storedPasswordHash = await ProtectedLocalStore.GetAsync<string>("ArtistPasswordHash");
                        password = "**********";
                    }
                    else
                    {
                        // erase
                        emailAddress = "";
                        storedPasswordHash = "";
                        password = "";                       
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    DebugHelper.WriteDebugError("OnInitializedAsync", "Login.cs", error);
                }
                finally
                {
                    // Always call this
                    Init();
                }
            }
            #endregion
            
            #region OnReset(string notUsedButRequiredArg)
            /// <summary>
            /// (Optional) This event callback is called by DataJuggler.Blazor.FileUpload after the ResetButton is clicked.
            /// </summary>
            /// <param name="notUsedButRequiredArg">InvokeAsync sends an object parameter, so I believe this or something is required.</param>
            private void OnReset(string notUsedButRequiredArg)
            {
                // reset back to Default
                profileImageUrl= NoProfileImagePath;

                 // toggle to true
                ShowUploadButton = true;
            }
            #endregion
            
            #region RemovedLocalStoreItems()
            /// <summary>
            /// This method Removed Local Store Items
            /// </summary>
            public async void RemovedLocalStoreItems()
            {
                try
                {
                    // if the ProtectedLocalStore exists
                    if (ProtectedLocalStore != null)
                    {
                        // delete doesn't seem to work, so I am setting to false
                        await ProtectedLocalStore.SetAsync("RememberLogin", false);

                        // Remove all items
                        //await ProtectedLocalStore.DeleteAsync("RememberPassword");
                        //await ProtectedLocalStore.DeleteAsync("ArtistEmailAddress");
                        //await ProtectedLocalStore.DeleteAsync("ArtistPasswordHash");
                    }
                }
                catch (Exception error)
                {   
                    // for debugging only
                    DebugHelper.WriteDebugError("RemoveLocalStoreItems", "Login.cs", error);
                }
            }
            #endregion
            
            #region SetActionLoginExistingUser()
            /// <summary>
            /// Set the action to LoginExistingUser
            /// </summary>
            private void SetActionLoginExistingUser()
            {
                // there is action
                noAction = false;
                
                // set the action to Signup
                action = "LoginExistingUser";
            }
            #endregion
            
            #region SetActionNewUserSignUp()
            /// <summary>
            /// Set the action to NewUserSignUp so the UI changes
            /// </summary>
            private void SetActionNewUserSignUp()
            {
                // there is action
                noAction = false;

                 // Turn the button back on if canceled
                ShowUploadButton = true;
                
                // set the action to Signup
                action = "NewUserSignUp";
            }
            #endregion

            #region SignOut()
            /// <summary>
            /// This method Sign Out
            /// </summary>
            public void SignOut()
            {
                // Erase the current Artist
                this.Artist = null;
                
                // Clear the actions and any values - resets the control to two buttons Sign Up and Login
                Init();

                // Remove any stored values
                if (!RememberLogin)
                {
                    // Remove any local store items
                    RemovedLocalStoreItems();
                }

                // Refresh the UI
                StateHasChanged();
            }
            #endregion
            
            #region ToggleProfileMenu()
            /// <summary>
            /// This method Toggle Profile Menu
            /// </summary>
            public void ToggleProfileMenu()
            {
                // Update the value to the value it is not currently
                ShowProfileMenu = !showProfileMenu;

                // The StateHasChanged
                StateHasChanged();
            }
            #endregion
            
            #region ValidateArtist()
            /// <summary>
            /// This method ensures we have a valid Artist
            /// </summary>
            /// <returns></returns>
            private bool ValidateArtist()
            {
                // initial value
                bool isValid = false;
                
                // if the player exists
                if (NullHelper.Exists(artist))
                {
                    // if the emailAddress and DisplayName
                    if (TextHelper.Exists(artist.Name, artist.EmailAddress, password, confirmPassword))
                    {
                        // If this passwords match, this is valid
                        bool passwordsMatch = TextHelper.IsEqual(password, confirmPassword);
                        
                        // if passwords do not match
                        if (!passwordsMatch)
                        {
                            // set the message
                            message = "Passwords do not match.";
                        }
                        else
                        {
                            // if Name is not set
                            if (artist.Name == "Name")
                            {
                                // Set the message
                                message = "You must set the Name";
                            }
                            else if (artist.EmailAddress == "Email")
                            {
                                // Set the message
                                message = "You must enter a valid email";
                            }
                            else if (password == "Password")
                            {
                                // Set the message
                                message = "You must enter a Password other than Password";
                            }
                            else if (confirmPassword == "Confirm Password")
                            {
                                // Set the message
                                message = "You must enter a Confirm Password other than Confirm Password";
                            }
                            else
                            {
                                // erase the message
                                message = "";
                                
                                // probably more to do
                                isValid = true;
                            }
                        }
                    }
                    else if (!TextHelper.Exists(artist.Name))
                    {
                        // set the message
                        message = "DisplayName is required.";
                    }
                    else if (!TextHelper.Exists(artist.EmailAddress))
                    {
                        // set the message
                        message = "EmailAddress is required.";
                    }
                    else if (!TextHelper.Exists(password))
                    {
                        // set the message
                        message = "Password is required.";
                    }
                    else if (!TextHelper.Exists(confirmPassword))
                    {
                        // set the message
                        message = "Confirm Password is required.";
                    }
                }
                
                // return value
                return isValid;
            }
            #endregion
            
        #endregion
        
        #region Properties
            
            #region Action
            /// <summary>
            /// This property gets or sets the value for 'Action'.
            /// </summary>
            public string Action
            {
                get { return action; }
                set { action = value; }
            }
            #endregion
            
            #region Artist
            /// <summary>
            /// This property gets or sets the value for 'Artist'.
            /// </summary>
            public Artist Artist
            {
                get { return artist; }
                set { artist = value; }
            }
            #endregion
            
            #region BlueProgress
            /// <summary>
            /// This property gets or sets the value for 'BlueProgress'.
            /// </summary>
            public string BlueProgress
            {
                get { return blueProgress; }
                set { blueProgress = value; }
            }
            #endregion
            
            #region ConfirmPassword
            /// <summary>
            /// This property gets or sets the value for 'ConfirmPassword'.
            /// </summary>
            public string ConfirmPassword
            {
                get { return confirmPassword; }
                set { confirmPassword = value; }
            }
            #endregion
            
            #region DisplayName
            /// <summary>
            /// This property gets or sets the value for 'DisplayName'.
            /// </summary>
            public string DisplayName
            {
                get { return displayName; }
                set { displayName = value; }
            }
            #endregion
            
            #region EmailAddress
            /// <summary>
            /// This property gets or sets the value for 'EmailAddress'.
            /// </summary>
            public string EmailAddress
            {
                get { return emailAddress; }
                set { emailAddress = value; }
            }
            #endregion

            #region HasArtist
            /// <summary>
            /// This property returns true if this object has an 'Artist'.
            /// </summary>
            public bool HasArtist
            {
                get
                {
                    // initial value
                    bool hasArtist = (this.Artist != null);
                    
                    // return value
                    return hasArtist;
                }
            }
            #endregion
            
            #region Message
            /// <summary>
            /// This property gets or sets the value for 'Message'.
            /// </summary>
            public string Message
            {
                get { return message; }
                set { message = value; }
            }
            #endregion
         
            #region NoAction
            /// <summary>
            /// This property gets or sets the value for 'NoAction'.
            /// </summary>
            public bool NoAction
            {
                get { return noAction; }
                set { noAction = value; }
            }
            #endregion

            #region OnLogin
            /// <summary>
            /// This is the delegate to call after logging in
            /// </summary>
            [Parameter] public EventCallback<LoginResponse> OnLogin { get; set; }
            #endregion
            
            #region Password
            /// <summary>
            /// This property gets or sets the value for 'Password'.
            /// </summary>
            public string Password
            {
                get { return password; }
                set { password = value; }
            }
            #endregion
            
            #region ProfileImageStyle
            /// <summary>
            /// This property gets or sets the value for 'ProfileImageStyle'.
            /// </summary>
            public string ProfileImageStyle
            {
                get { return profileImageStyle; }
                set { profileImageStyle = value; }
            }
            #endregion
            
            #region ProfileImageUrl
            /// <summary>
            /// This property gets or sets the value for 'ProfileImageUrl'.
            /// </summary>
            public string ProfileImageUrl
            {
                get { return profileImageUrl; }
                set { profileImageUrl = value; }
            }
            #endregion
            
            #region RememberLogin
            /// <summary>
            /// This property gets or sets the value for 'RememberLogin'.
            /// </summary>
            public bool RememberLogin
            {
                get { return rememberLogin; }
                set { rememberLogin = value; }
            }
            #endregion
            
            #region ShowProfileMenu
            /// <summary>
            /// This property gets or sets the value for 'ShowProfileMenu'.
            /// </summary>
            public bool ShowProfileMenu
            {
                get { return showProfileMenu; }
                set { showProfileMenu = value; }
            }
            #endregion
            
            #region ShowProgress
            /// <summary>
            /// This property gets or sets the value for 'ShowProgress'.
            /// </summary>
            public bool ShowProgress
            {
                get { return showProgress; }
                set { showProgress = value; }
            }
            #endregion
            
            #region ShowUploadButton
            /// <summary>
            /// This property gets or sets the value for 'ShowUploadButton'.
            /// </summary>
            public bool ShowUploadButton
            {
                get { return showUploadButton; }
                set { showUploadButton = value; }
            }
            #endregion
            
            #region StoredPasswordHash
            /// <summary>
            /// This property gets or sets the value for 'StoredPasswordHash'.
            /// </summary>
            public string StoredPasswordHash
            {
                get { return storedPasswordHash; }
                set { storedPasswordHash = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
