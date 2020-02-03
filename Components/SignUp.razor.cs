

#region using statements

using System.Collections.Generic;
using DataGateway.Services;
using DataJuggler.Blazor.FileUpload;
using DataJuggler.Core.Cryptography;
using DataJuggler.UltimateHelper.Core;
using Microsoft.AspNetCore.Components;
using ObjectLibrary.BusinessObjects;
using ObjectLibrary.Models;
using BlazorImageGallery.Models;
using System;
using System.IO;
using DataJuggler.Blazor.Components.Interfaces;
using DataJuggler.Blazor.Components;
using System.Threading.Tasks;
using System.Threading;

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
        private ProgressBar progressBar;
        private string name;
        private bool signUpProcessed;
        private bool signUpComplete;
        private IBlazorComponentParent parent;
        private LoginResponse loginResponse;
        private SignUpModel signUpModel;
        private SignUpFinishedCallback signUpCallback;
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
            private void HandleNewUserSignUp()
            {
                try
                {
                    // if the value for HasProgressBar is true
                    if (HasProgressBar)
                    {  
                        // Reset just in case this has been run previously
                        SignUpProcessed = false;
                        SignUpComplete = false;

                        // Start the Progressbar timer
                        this.ProgressBar.Start();
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

            #region OnSignUpComplete(LoginResponse loginResponse)
            /// <summary>
            /// This method receieves the response from the login executed in another thread
            /// </summary>
            /// <param name="loginResponse"></param>
            public void OnSignUpComplete(LoginResponse loginResponse)
            {
                // If the loginResponse object exists
                if (NullHelper.Exists(loginResponse))
                {
                    // if the loginResponse exists
                    if ((NullHelper.Exists(loginResponse)) && (loginResponse.Success))
                    {
                        // Store the LoginResponse
                        LoginResponse = loginResponse;

                        // We can't call the event call back from this thread
                        SignUpComplete = true;                        
                    }

                    // Refresh the UI
                    Refresh();
                }
            }
            #endregion

            #region ProcessNewUserSignUp(SignUpModel signUpObject)
            /// <summary>
            /// This method Process New User Sign Up
            /// </summary>
            public async void ProcessNewUserSignUp(object signUpObject)
            {
                // cast the object as a signUpModel object
                this.SignUpModel = signUpObject as SignUpModel;

                // if the value for HasSignUpModel is true
                if (HasSignUpModel)
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
                    if (TextHelper.Exists(key, password))
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

                        // if the value for HasProgressBar is true
                        if (HasProgressBar)
                        {
                            // Stop and hide
                            ProgressBar.Stop();
                        }
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
                        artist.EmailAddress = signUpModel.EmailAddress;
                        artist.PasswordHash = passwordHash;
                        artist.CreatedDate = DateTime.Now;
                        artist.Active = true;
                        artist.Name = signUpModel.DisplayName;
                        artist.CreatedDate = DateTime.Now;
                        artist.LastUpdated = DateTime.Now;
                        artist.FolderPath = artistPath;
                        artist.ProfilePicture = signUpModel.ProfilePictureUrl;
                        artist.Active = true;

                        // Validate this artist
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
                                this.LoginResponse = await ArtistService.Login(artist.EmailAddress, artist.PasswordHash, true);

                                // The SignUp is complete
                                SignUpComplete = true;
                            }
                        }
                        else
                        {
                            abort = true;
                        }
                    }
                }
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
                try
                {
                    // If the LoginProcessed
                    if (!SignUpProcessed)
                    {
                        // Create a new instance of a 'SignUpModel' object.
                        SignUpModel signUpModel = new SignUpModel();

                        // Set the properties on the SignUpModel
                        signUpModel.DisplayName = DisplayName;
                        signUpModel.EmailAddress = EmailAddress;
                        signUpModel.Password = Password;
                        signUpModel.ProfilePictureUrl = ProfileImageUrl;
                        signUpModel.SignUpFinishedCallback = OnSignUpComplete;

                        // Create a Thread to Process the Signup
                        Thread thread = new Thread(ProcessNewUserSignUp);

                        // Set the value for the property 'IsBackground' to true                        
                        thread.IsBackground = true;

                        // Startup the thread and pass in the SignUp model
                        thread.Start(signUpModel);
                    
                        // Process the login
                        SignUpProcessed = true;
                    }

                    // if the SignUp has finished
                    if ((SignUpComplete) && (HasProgressBar))
                    {  
                        // Stop the ProgressBar
                        ProgressBar.Stop();

                        // if the loginResponse exists
                        if (HasLoginResponse)
                        {
                            // Notify the Login page that we have logged in a user
                            SignUpCallback(LoginResponse);
                        }
                    }
                }
                catch (Exception error)
                {
                    // For debugging only
                    DebugHelper.WriteDebugError("Refresh", "SignUp.razor.cs", error);
                }
            }
            #endregion

            #region Refresh()
            /// <summary>
            /// This method is called by a Sprite when as it refreshes.
            /// </summary>
            public void Refresh()
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
                // Store the ProgressBar
                this.ProgressBar = progressBar;
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
                
                // if the artist exists
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
            
            #region HasLoginResponse
            /// <summary>
            /// This property returns true if this object has a 'LoginResponse'.
            /// </summary>
            public bool HasLoginResponse
            {
                get
                {
                    // initial value
                    bool hasLoginResponse = (this.LoginResponse != null);
                    
                    // return value
                    return hasLoginResponse;
                }
            }
            #endregion
            
            #region HasMessage
            /// <summary>
            /// This property returns true if this object has a 'Message'.
            /// </summary>
            public bool HasMessage
            {
                get
                {
                    // initial value
                    bool hasMessage = (this.Message != null);
                    
                    // return value
                    return hasMessage;
                }
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
            
            #region HasProgressBar
            /// <summary>
            /// This property returns true if this object has a 'ProgressBar'.
            /// </summary>
            public bool HasProgressBar
            {
                get
                {
                    // initial value
                    bool hasProgressBar = (this.ProgressBar != null);
                    
                    // return value
                    return hasProgressBar;
                }
            }
            #endregion
            
            #region HasSignUpCallback
            /// <summary>
            /// This property returns true if this object has a 'SignUpCallback'.
            /// </summary>
            public bool HasSignUpCallback
            {
                get
                {
                    // initial value
                    bool hasSignUpCallback = (this.SignUpCallback != null);
                    
                    // return value
                    return hasSignUpCallback;
                }
            }
            #endregion
            
            #region HasSignUpModel
            /// <summary>
            /// This property returns true if this object has a 'SignUpModel'.
            /// </summary>
            public bool HasSignUpModel
            {
                get
                {
                    // initial value
                    bool hasSignUpModel = (this.SignUpModel != null);
                    
                    // return value
                    return hasSignUpModel;
                }
            }
            #endregion
            
            #region LoginResponse
            /// <summary>
            /// This property gets or sets the value for 'LoginResponse'.
            /// </summary>
            public LoginResponse LoginResponse
            {
                get { return loginResponse; }
                set { loginResponse = value; }
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
            
            #region ProgressBar
            /// <summary>
            /// This property gets or sets the value for 'ProgressBar'.
            /// </summary>
            public ProgressBar ProgressBar
            {
                get { return progressBar; }
                set { progressBar = value; }
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
            #endregion

            #region SignUpCallback
            /// <summary>
            /// This property gets or sets the value for 'SignUpCallback'.
            /// </summary>
            public SignUpFinishedCallback SignUpCallback
            {
                get { return signUpCallback; }
                set { signUpCallback = value; }
            }
            #endregion
            
            #region SignUpComplete
            /// <summary>
            /// This property gets or sets the value for 'SignUpComplete'.
            /// </summary>
            public bool SignUpComplete
            {
                get { return signUpComplete; }
                set { signUpComplete = value; }
            }
            #endregion

            #region SignUpModel
            /// <summary>
            /// This property gets or sets the value for 'SignUpModel'.
            /// </summary>
            public SignUpModel SignUpModel
            {
                get { return signUpModel; }
                set { signUpModel = value; }
            }
            #endregion
            
            #region SignUpProcessed
            /// <summary>
            /// This property gets or sets the value for 'SignUpProcessed'.
            /// </summary>
            public bool SignUpProcessed
            {
                get { return signUpProcessed; }
                set { signUpProcessed = value; }
            }
            #endregion

        #endregion

    }
    #endregion

}
