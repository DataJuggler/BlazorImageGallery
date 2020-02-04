

#region using statements

using BlazorImageGallery.Models;
using DataJuggler.Blazor.Components;
using DataJuggler.Blazor.Components.Interfaces;
using DataGateway.Services;
using DataJuggler.Blazor.FileUpload;
using DataJuggler.UltimateHelper.Core;
using Microsoft.AspNetCore.Components;
using ObjectLibrary.BusinessObjects;
using ObjectLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace BlazorImageGallery.Components
{

    #region class Login
    /// <summary>
    /// This is the code behind for the login control.
    /// </summary>
    public partial class Login : IBlazorComponentParent, IProgressSubscriber, IBlazorComponent
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
        private SignUp signUp;
        private ProgressBar progressBar;
        private bool loginProcessed;
        private bool loginComplete;
        private IBlazorComponentParent parent;
        private List<IBlazorComponent> children;
        private string name;
        private LoginResponse loginResponse;
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

                // if the Parent exists
                if (HasParent)
                {
                    // Create a message
                    Message message = new Message();

                    // Send a clear message
                    message.Text = "";

                    // Send a message
                    Parent.ReceiveData(message);
                }
                
                // Update the UI
                StateHasChanged();
            }
            #endregion

            #region FindChildByName(string name)
            /// <summary>
            /// This method returns the Child Component By Name
            /// </summary>
            public IBlazorComponent FindChildByName(string name)
            {
                // initial value
                IBlazorComponent child = null;
                
                // if the value for HasChildren is true
                if ((HasChildren) && (!String.IsNullOrEmpty(name)))
                {
                    // Iterate the collection of IBlazorComponent objects
                    foreach (IBlazorComponent childComponent in Children)
                    {
                        // if this is the item being sought
                        if (childComponent.Name == name)
                        {
                            // set the return value
                            child = childComponent;
                            
                            // break out of the loop
                            break;
                        }
                    }
                }
                
                // return value
                return child;
            }
            #endregion
            
            #region HandleLogin()
            /// <summary>
            /// Handle the user logging in
            /// </summary>
            private void HandleLogin()
            {
                try
                {
                    // if the value for HasProgressBar is true
                    if (HasProgressBar)
                    {
                        // Set ShowProgress to true
                        ShowProgress = true;
                        
                        // Reset just in case this is being run again
                        LoginProcessed = false;
                        LoginComplete = false;

                        // Start the Progressbar timer
                        progressBar.Start();
                    }                    
                }
                catch (Exception error)
                {
                    // for debugging only
                    DebugHelper.WriteDebugError("HandleLogin", "Login.cs", error);
                }
                finally
                {  
                    // Refresh the UI
                    StateHasChanged();
                }
            }
            #endregion
            
            #region HandleRememberPassword()
            /// <summary>
            /// This method Handle Remember Password
            /// </summary>
            public async void HandleRememberPassword()
            {
                // if remember login details is true
                if ((HasLoginResponse) && (RememberLogin) && (LoginResponse.HasArtist))
                {
                    // Set the artist
                    artist = LoginResponse.Artist;

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
                Name = "Login";

                // Create a new collection of 'IBlazorComponent' objects.
                this.Children = new List<IBlazorComponent>();

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

            #region NotifyIndexPage()
            /// <summary>
            /// This method Notify Index Page
            /// </summary>
            public async void NotifyIndexPage()
            {
                try
                {
                    // if the value for HasLoginResponse is true
                    if (HasLoginResponse)
                    {
                        // set the Artist
                        artist = LoginResponse.Artist;

                        // Notify the caller of the Login
                        await OnLogin.InvokeAsync(loginResponse);
                    }
                }
                catch (Exception error)
                {
                    // For debugging only
                    DebugHelper.WriteDebugError("NotifyIndexPage", "Login", error);
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

            #region OnLoginComplete(LoginResponse loginResponse)
            /// <summary>
            /// This method receieves the response from the login executed in another thread
            /// </summary>
            /// <param name="loginResponse"></param>
            public void OnLoginComplete(LoginResponse loginResponse)
            {
                try
                {
                    // If the loginResponse object exists
                    if (NullHelper.Exists(loginResponse, ProgressBar))
                    {
                        // if the loginResponse exists
                        if (NullHelper.Exists(loginResponse))
                        {
                            // if success
                            if (loginResponse.Success)
                            {
                                // Clear the Message
                                this.Message = "";

                                // Store the LoginResponse
                                LoginResponse = loginResponse;

                                // We can't call the event call back from this thread
                                LoginComplete = true;      
                            }
                            else
                            {
                                // if the ProgressBar exists
                                if (HasProgressBar)
                                {
                                    // Stop the ProgressBar
                                    ProgressBar.Stop();
                                }

                                // if the Parent exists
                                if (HasParent)
                                {
                                    // create a message to send
                                    Message message = new Message();

                                    // Set the text
                                    message.Text = loginResponse.Message;

                                    // Send a message to the parent
                                    Parent.ReceiveData(message);;
                                }
                            }
                        }

                        // Refresh the UI
                        Refresh();
                    }
                }
                catch (Exception error)
                {
                    // for debugging only 
                    DebugHelper.WriteDebugError("OnLoginComplete", "Login.razor.cs", error);
                }
            }
            #endregion

            #region OnSignUpComplete(LoginResponse loginResponse)
            /// <summary>
            /// This method receieves the response from the SignUp control
            /// </summary>
            /// <param name="loginResponse"></param>
            public void OnSignUpComplete(LoginResponse loginResponse)
            {
                try
                {
                    // If the loginResponse object exists
                    if (NullHelper.Exists(loginResponse))
                    {
                        // if the loginResponse exists
                        if ((NullHelper.Exists(loginResponse)) && (loginResponse.Success))
                        {
                            // Store the LoginResponse
                            LoginResponse = loginResponse;

                            // Notify the IndexPage
                            NotifyIndexPage();
                        }

                        // Refresh the UI
                        Refresh();
                    }
                }
                catch (Exception error)
                {
                    // for debugging only 
                    DebugHelper.WriteDebugError("OnLoginComplete", "Login.razor.cs", error);
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

            #region ProcessLogin(object data)
            /// <summary>
            /// This method calls the Artistervice to perform the Login
            /// </summary>
            public static async void ProcessLogin(object data)
            {
                // local
                LoginResponse loginResponse = null;

                // cast as a Loginmodel object
                LoginModel loginModel = data as LoginModel;

                // if the loginModel exists
                if (NullHelper.Exists(loginModel))
                {
                    // If the StoredPasswordHash exists, then we know this is from a RememberPassword
                    bool passwordIsAlreadyHashed = TextHelper.Exists(loginModel.StoredPasswordHash);

                    // if the value for passwordIsAlreadyHashed is true
                    if (passwordIsAlreadyHashed)
                    {
                        // Get the loginResponse using the StoredPasswordHash
                        loginResponse = await ArtistService.Login(loginModel.EmailAddress, loginModel.StoredPasswordHash, passwordIsAlreadyHashed);     
                    }
                    else
                    {
                        // Get the loginResponse
                        loginResponse = await ArtistService.Login(loginModel.EmailAddress, loginModel.Password);     
                    }

                    // if the delegate exists
                    if (NullHelper.Exists(loginModel.OnLoginComplete))
                    {   
                        // Notify the delegate that the login has finished
                        loginModel.OnLoginComplete(loginResponse);
                    }
                }
            }
            #endregion
            
            #region ReceiveData(Message message)
            /// <summary>
            /// This method Receives Data from a child or parent component
            /// </summary>
            public async void ReceiveData(Message message)
            {
                // If the message object exists
                if (NullHelper.Exists(message))
                {
                    // if a NewArtist signed up
                    if (message.Text == "New Artist Signed Up")
                    {
                        // if the parameters collection exists
                        if (message.HasParameters)
                        {
                            // iterate the parameters                            
                            foreach (NamedParameter parameter in message.Parameters)                                           
                            {
                                // if this is the name
                                if (parameter.Name == "New Artist")
                                {
                                    // get the current Artist
                                    artist = parameter.Value as Artist;

                                    // Create a LoginResponse object
                                    LoginResponse loginResponse = new LoginResponse();

                                    // Set to true
                                    loginResponse.Success = true;

                                    // set the Artist
                                    loginResponse.Artist = artist;

                                    // Notify the caller of the Login
                                    await OnLogin.InvokeAsync(loginResponse);
                                }
                            }
                        }
                    }
                }
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
                    if (!LoginProcessed)
                    {
                        // create a new loginModel object
                        LoginModel loginModel = new LoginModel(EmailAddress, Password, StoredPasswordHash, OnLoginComplete);

                        // Start the ProcessLogin Thread
                        Thread thread = new Thread(ProcessLogin);
                        thread.IsBackground = true;
                        thread.Start(loginModel);

                        // if the Parent exists
                        if (HasParent)
                        {
                            // create a message to send to the parent before this control starts 
                            Message messageToSend = new Message();

                            // Clear any text
                            messageToSend.Text = "";

                            // Send a message to the parent to clear any messages being shown (if any)
                            Parent.ReceiveData(messageToSend);
                        }
                    
                        // Process the login
                        LoginProcessed = true;
                    }

                    // if the Login has finished
                    if (LoginComplete)
                    {  
                        // Stop the ProgressBar
                        ProgressBar.Stop();

                        // Handle store password or not
                        HandleRememberPassword();

                        // Notify the Index page
                        NotifyIndexPage();
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    DebugHelper.WriteDebugError("Refresh", "Login", error);
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

            #region Register(IBlazorComponent component)
            /// <summary>
            /// This method Registers the component with this component.
            /// </summary>
            public void Register(IBlazorComponent component)
            {
                // If the component object exists
                if (NullHelper.Exists(component, Children))
                {
                    // If this is the SignUp component
                    if (component.Name == "SignUp")
                    {
                        // Set the Signup control
                        this.SignUp = component as SignUp;

                        // Setup the delegate to call
                        this.SignUp.SignUpCallback = OnSignUpComplete;
                    }

                    // add this child
                   Children.Add(component);
                }
            }
            #endregion

            #region Register(ProgressBar progressBar)
            /// <summary>
            /// method returns the
            /// </summary>
            public void Register(ProgressBar progressBar)
            {
                // Store the Progress
                ProgressBar = progressBar;
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
            /// This method Set Action New User Sign Up
            /// </summary>
            public void SetActionNewUserSignUp()
            {
                // Set to false
                NoAction = false;

                // Set Action
                Action = "NewUserSignUp";

                // update the UI
                StateHasChanged();
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
            
            #region Children
            /// <summary>
            /// This property gets or sets the value for 'Children'.
            /// </summary>
            public List<IBlazorComponent> Children
            {
                get { return children; }
                set { children = value; }
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
            
            #region HasChildren
            /// <summary>
            /// This property returns true if this object has a 'Children'.
            /// </summary>
            public bool HasChildren
            {
                get
                {
                    // initial value
                    bool hasChildren = (this.Children != null);
                    
                    // return value
                    return hasChildren;
                }
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
            
            #region HasSignUp
            /// <summary>
            /// This property returns true if this object has a 'SignUp'.
            /// </summary>
            public bool HasSignUp
            {
                get
                {
                    // initial value
                    bool hasSignUp = (this.SignUp != null);
                    
                    // return value
                    return hasSignUp;
                }
            }
            #endregion
            
            #region LoginComplete
            /// <summary>
            /// This property gets or sets the value for 'LoginComplete'.
            /// </summary>
            public bool LoginComplete
            {
                get { return loginComplete; }
                set { loginComplete = value; }
            }
            #endregion
            
            #region LoginProcessed
            /// <summary>
            /// This property gets or sets the value for 'LoginProcessed'.
            /// </summary>
            public bool LoginProcessed
            {
                get { return loginProcessed; }
                set { loginProcessed = value; }
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

            #region OnLogin
            /// <summary>
            /// This is the EventCallback that is called after logging in
            /// </summary>
            [Parameter] public EventCallback<LoginResponse> OnLogin { get; set; }
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

            #region Navigator
            /// <summary>
            /// This propery is injected so you can use NavigateTo
            /// </summary>
            [Inject]
            NavigationManager Navigator { get; set; }
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
            /// This property gets or sets the value for 'Progress'.
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
                set 
                { 
                    rememberLogin = value;

                    // if false
                    if (!rememberLogin)
                    {
                        // Erase the value
                        StoredPasswordHash = "";

                        // Remove the local stored password values
                        RemovedLocalStoreItems();
                    }
                }
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
            
            #region SignUp
            /// <summary>
            /// This property gets or sets the value for 'SignUp'.
            /// </summary>
            public SignUp SignUp
            {
                get { return signUp; }
                set { signUp = value; }
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
