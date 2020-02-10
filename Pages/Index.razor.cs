

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using ObjectLibrary.BusinessObjects;
using ObjectLibrary.Models;
using DataJuggler.UltimateHelper.Core;
using BlazorImageGallery.Util;
using DataGateway.Services;
using DataJuggler.Blazor.Components;
using BlazorImageGallery.Components;
using DataJuggler.Blazor.Components.Interfaces;
using Microsoft.AspNetCore.Components;

#endregion

namespace BlazorImageGallery.Pages
{

    #region class Index
    /// <summary>
    /// This class is the code behind for the Index page
    /// </summary>
    public partial class Index : IBlazorComponentParent
    {

        #region Private Variables
        private Artist artist;
        private GalleryManager galleryManager;
        private int artistId;
        private List<IBlazorComponent> children;
        private Login login;
        private string passwordHash;
        private string message;
        private bool loginOrSignUpInProgress;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of an Index page object
        /// </summary>
        public Index()
        {
            // Perform initializations for this object
            Init();
        }
        #endregion

        #region Methods

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

            #region Init()
            /// <summary>
            /// This method performs initializations for this object.
            /// </summary>
            public void Init()
            {
                // Create a new collection of 'IBlazorComponent' objects.
                this.Children = new List<IBlazorComponent>();
            }
            #endregion
            
            #region LoadImagesForArtist(int artistId)
            /// <summary>
            /// This method returns a list of Images For Artist
            /// </summary>
            public async Task<List<Image>> LoadImagesForArtist(int artistId)
            {
                // initial value
                List<Image> images = null;
                
                // initial value
                images = await ImageService.GetImageList(artistId);

                // return the list              
                return images;
            }
            #endregion
            
            #region LoginComplete(LoginResponse loginResponse)
            /// <summary>
            /// This method is called by the Login control after a login attempt.
            /// </summary>
            /// <param name="loginResponse"></param>
            private async void LoginComplete(LoginResponse loginResponse)
            {
                // If the loginResponse object exists
                if (NullHelper.Exists(loginResponse))
                {
                    // if the login was successful
                    if (loginResponse.Success)
                    {  
                        // Erase any messages
                        this.Message = "";

                        // Set the artist
                        this.Artist = loginResponse.Artist;

                        // if we do not have a Gallerymanager
                        if (!HasGalleryManager)                        
                        {
                            // Create a new instance of a 'GalleryManager' object.
                            this.GalleryManager = new GalleryManager(this);
                        }

                        // Set the Artist
                        this.GalleryManager.Artist = this.Artist;

                        // Load the Artists in case this is a new artist
                        this.GalleryManager.Artists = await ArtistService.GetArtistList();

                        // Call Refresh
                        Refresh();
                    }
                }
            }
            #endregion

            #region OnInitializedAsync()
            /// <summary>
            /// This method is used to restored values stored in local storage
            /// </summary>
            protected override async Task OnInitializedAsync()
            {
                // Create the GalleryManager
                this.GalleryManager = new GalleryManager(this);

                // Load the Artists
                this.GalleryManager.Artists = await ArtistService.GetArtistList();
            }
            #endregion

            #region ReceiveData(Message message)
            /// <summary>
            /// This method Receives Data from a child or parent component
            /// </summary>
            public void ReceiveData(Message message)
            {
                // If the message object exists
                if (NullHelper.Exists(message))
                {
                    // if a NewArtist signed up
                    if (message.Text == "Artist Logged In")
                    {
                        // if the parameters collection exists
                        if (message.HasParameters)
                        {
                            // iterate the parameters                            
                            foreach (NamedParameter parameter in message.Parameters)                                           
                            {
                                // if this is the name
                                if (parameter.Name == "Artist")
                                {
                                    // Get the login response
                                    LoginResponse loginResponse = parameter.Value as LoginResponse;
                                    
                                    // If the loginResponse object exists
                                    if (NullHelper.Exists(loginResponse))
                                    {
                                        // Update the UI that we have a login
                                        LoginComplete(loginResponse);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        // Set the message text
                        this.Message = message.Text;

                        // Update the UI
                        Refresh();
                    }
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
                    // If this is the Login component
                    if (component.Name == "Login")
                    {
                        // Set the Signup control
                        this.Login = component as Login;
                    }

                   // add this child
                   Children.Add(component);
                }
            }
            #endregion

        #endregion

        #region Properties

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
            
            #region ArtistId
            /// <summary>
            /// This property gets or sets the value for 'ArtistId'.
            /// </summary>
            [Parameter]
            public int ArtistId
            {
                get { return artistId; }
                set { artistId = value; }
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
            
            #region GalleryManager
            /// <summary>
            /// This property gets or sets the value for 'GalleryManager'.
            /// </summary>
            public GalleryManager GalleryManager
            {
                get { return galleryManager; }
                set { galleryManager = value; }
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
            
            #region HasGalleryManager
            /// <summary>
            /// This property returns true if this object has a 'GalleryManager'.
            /// </summary>
            public bool HasGalleryManager
            {
                get
                {
                    // initial value
                    bool hasGalleryManager = (this.GalleryManager != null);
                    
                    // return value
                    return hasGalleryManager;
                }
            }
            #endregion
            
            #region Login
            /// <summary>
            /// This property gets or sets the value for 'Login'.
            /// </summary>
            public Login Login
            {
                get { return login; }
                set { login = value; }
            }
            #endregion
            
            #region LoginOrSignUpInProgress
            /// <summary>
            /// This property gets or sets the value for 'LoginOrSignUpInProgress'.
            /// </summary>
            public bool LoginOrSignUpInProgress
            {
                get { return loginOrSignUpInProgress; }
                set { loginOrSignUpInProgress = value; }
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
            
            #region PasswordHash
            /// <summary>
            /// This property gets or sets the value for 'PasswordHash'.
            /// </summary>
            [Parameter]
            public string PasswordHash
            {
                get { return passwordHash; }
                set { passwordHash = value; }
            }
            #endregion
            
        #endregion

    }
    #endregion

}

