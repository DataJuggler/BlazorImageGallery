

#region using statements

using System.Collections.Generic;
using DataGateway.Services;
using DataJuggler.Blazor.FileUpload;
using DataJuggler.Core.Cryptography;
using DataJuggler.UltimateHelper.Core;
using Microsoft.AspNetCore.Components;
using ObjectLibrary.BusinessObjects;
using ObjectLibrary.Models;
using System;
using System.IO;
using DataJuggler.Blazor.Components.Interfaces;
using DataJuggler.Blazor.Components;

#endregion

namespace BlazorImageGallery.Components
{

    #region class SignUp
    /// <summary>
    /// This page is used to sign up a user
    /// </summary>
    public partial class SignUp : IProgressSubscriber, IBlazorComponent
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
        private ProgressBar progress;
        private string name;
        private IBlazorComponentParent parent;
        private const string NoProfileImagePath = "../images/avatars/NoProfileImage.png";
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of a Signup object
        /// </summary>
        public SignUp()
        {
            // Perform initializations for this object
            Init();
        }
        #endregion

        #region Methods

            #region Cancel()
            /// <summary>
            /// method Cancel
            /// </summary>
            private void Cancel()
            {  
                // Update the UI
                StateHasChanged();
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

                    // create a new Artist
                    this.Artist = new Artist();

                    // get the key
                    string key = EnvironmentVariableHelper.GetEnvironmentVariableValue("BlazorImageGallery");

                    // if the key was found
                    if (TextHelper.Exists(key))
                    {
                         // if the value for HasProgress is true
                        if (HasProgress)
                        {
                            // Start the Progress
                            Progress.Start();
                        }

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
                                    // Stop the Progress (and hide) -- isn't working
                                    Progress.Stop();

                                    // set the player
                                    artist = loginResponse.Artist;

                                    // if the value for HasParent is true
                                    if (HasParent)
                                    {
                                        // Send Data to the parent
                                        
                                        // Create a message to send to the parent
                                        Message message = new Message();

                                        // Set the Text
                                        message.Text = "New Artist Signed Up";

                                        // Create a new instance of a 'NamedParameter' object.
                                        NamedParameter namedParameter = new NamedParameter();

                                        // Set the NamedParameter
                                        namedParameter.Name = "New Artist";
                                        namedParameter.Value = artist;

                                        // Add this parameter
                                        message.Parameters.Add(namedParameter);

                                        // Send the data to the parent
                                        Parent.ReceiveData(message);
                                    }
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
                    // if the value for HasProgress is true
                    if (HasProgress)
                    {
                        // turn off progress
                        Progress.Stop();
                    }
                }
            }
            #endregion

            #region Init()
            /// <summary>
            /// This method performs initializations for this object.
            /// </summary>
            public void Init()
            {
                // set Default Image
                ProfileImageUrl = NoProfileImagePath;
                Name = "SignUp";
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

                    // The above information can be used to display, and / or process a file
                }

                // Refresh the UI
                StateHasChanged();
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
            }
            #endregion

            #region ReceiveData(Message message)
            /// <summary>
            /// This method Receive Data
            /// </summary>
            public void ReceiveData(Message message)
            {
                // receive messages from a child or parent component
            }
            #endregion
            
            #region Refresh(string message)
            /// <summary>
            /// method returns the
            /// </summary>
            public void Refresh(string message)
            {
               // Update the UI
                InvokeAsync(() =>
                {
                    StateHasChanged();
                });
            }
            #endregion
            
            #region Register(ProgressBar progressBar)
            /// <summary>
            /// method returns the
            /// </summary>
            public void Register(ProgressBar progressBar)
            {
                // Store the Progress
                Progress = progressBar;
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
            
            #region HasParent
            /// <summary>
            /// This property returns true if this object has a 'Parent'.
            /// </summary>
            public bool HasParent
            {
                get
                {
                    // initial value
                    bool hasParent = (this.Parent != null);
                    
                    // return value
                    return hasParent;
                }
            }
            #endregion
            
            #region HasProgress
            /// <summary>
            /// This property returns true if this object has a 'Progress'.
            /// </summary>
            public bool HasProgress
            {
                get
                {
                    // initial value
                    bool hasProgress = (this.Progress != null);
                    
                    // return value
                    return hasProgress;
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

            #region Name
            /// <summary>
            /// This property gets or sets the value for 'Name'.
            /// </summary>
            [Parameter]
            public string Name
            {
                get { return name; }
                set { name = value; }
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
            
            #region Parent
            /// <summary>
            /// This property gets or sets the value for 'Parent'.
            /// </summary>
            [Parameter]
            public IBlazorComponentParent Parent
            {
                get { return parent; }
                set 
                { 
                    // set the parent
                    parent = value;

                    // if the value for HasParent is true
                    if (HasParent)
                    {
                        // Register with the parent to receive messages from the parent
                        Parent.Register(this);
                    }
                }
            }
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
            
            #region Progress
            /// <summary>
            /// This property gets or sets the value for 'ProgressBar'.
            /// </summary>
            public ProgressBar Progress
            {
                get { return progress; }
                set { progress = value; }
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
            
            #region ShowUploadButton
            /// <summary>
            /// This property gets or sets the value for 'ShowUploadButton'.
            /// </summary>
            public bool ShowUploadButton
            {
                get { return showUploadButton; }
                set { showUploadButton = value; }
            }

        public ProgressBar ProgressBar { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        #endregion

        #endregion

    }
    #endregion

}
