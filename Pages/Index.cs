

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObjectLibrary.BusinessObjects;
using ObjectLibrary.Models;
using DataJuggler.UltimateHelper.Core;
using BlazorImageGallery.Util;
using DataGateway.Services;
using System.Threading.Tasks;

#endregion

namespace BlazorImageGallery.Pages
{

    #region class Index
    /// <summary>
    /// This class is the code behind for the Index page
    /// </summary>
    public partial class Index
    {

        #region Private Variables
        private Artist artist;
        private GalleryManager galleryManager;
        #endregion

        #region Methods

            #region LoadImagesForArtist()
            /// <summary>
            /// This method returns a list of Images For Artist
            /// </summary>
            public async Task<List<Image>> LoadImagesForArtist()
            {
                // initial value
                List<Image> images = null;

                // if the Artist exists and the ArtistId is set
                if ((HasArtist) && (Artist.IsNew))
                {
                    // initial value
                    images = await ImageService.GetImageList(Artist.Id);

                    // Create the dataWatcher
                    ImageDataWatcher dataWatcher = new ImageDataWatcher();

                    // Watch these to
                    dataWatcher.Watch(images);
                }

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
                        // Set the artist
                        this.Artist = loginResponse.Artist;

                        // if the value for HasArtist is true
                        if (HasArtist)
                        {
                            // Load the images
                            Artist.Images = await LoadImagesForArtist();
                        }

                        // Create a new instance of a 'GalleryManager' object.
                        this.GalleryManager = new GalleryManager();

                        // Set the Artist
                        this.GalleryManager.Artist = this.Artist;

                        // refresh the UI
                        this.StateHasChanged();
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
                this.GalleryManager = new GalleryManager();

                // Load the Artists
                this.GalleryManager.Artists = await ArtistService.GetArtistList();
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
            
        #endregion

    }
    #endregion

}

