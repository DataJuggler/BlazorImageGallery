

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObjectLibrary.BusinessObjects;
using Microsoft.AspNetCore.Components;
using BlazorImageGallery.Util;
using DataJuggler.Blazor.FileUpload;
using DataGateway.Services;

#endregion

namespace BlazorImageGallery.Components
{

    #region class ImageListViewer
    /// <summary>
    /// This component is used to display the images for an Artist, or a mixed group of images by many artists.
    /// </summary>
    public partial class ImageListViewer
    {
        
        #region Private Variables
        private Image selectedImage;
        private string selectedImageCSS;
        private string message;
        private string imageStyle1;
        private string imageUrl1;
        private int columnNumber;
        private int rowNumber;
        #endregion

        #region Methods

            #region OnFileUploaded(UploadedFileInfo uploadedFileInfo)
            /// <summary>
            /// This method is called by DataJuggler.Blazor.FileUpload after a file is uploaded.
            /// </summary>
            /// <param name="uploadedFileInfo"></param>
            private async void OnFileUploaded(UploadedFileInfo uploadedFileInfo)
            {
                // local
                // bool abortUpdate = false;

                // if aborted
                if (uploadedFileInfo.Aborted)
                {
                    // get the status
                    this.Message = uploadedFileInfo.ErrorMessage;
                }
                else
                {
                    // if the value for HasArtist is true
                    if (HasArtist)
                    {
                        // Create a new instance of an 'Image' object.
                        Image image = new Image();

                        // set the image properties
                        image.Name = uploadedFileInfo.Name;
                        image.OwnerId = Artist.Id;
                        image.CreatedDate = DateTime.Now;
                        image.Extension = uploadedFileInfo.Extension;
                        image.FileSize = (int) uploadedFileInfo.Size;
                        image.FullPath = uploadedFileInfo.FullPath;
                        image.Height = uploadedFileInfo.Height;
                        image.Width = uploadedFileInfo.Width;
                        image.SitePath = uploadedFileInfo.FullPath;
                        image.ImageUrl = "../Images/Gallery/" + Artist.Name + "/" + uploadedFileInfo.FullName;

                        // If the value for the property Artist.HasImages is true
                        if ((HasSelectedArtist) && (SelectedArtist.HasImages))
                        {
                            // add to the count; we do not have a delete, so no worry about out of order for this sample
                            image.ImageNumber = GalleryManager.SelectedArtist.Images.Count + 1;
                        }
                        else
                        {
                            // set to 1 for the first item
                            image.ImageNumber = 1;
                        }
                        image.Visible = true;

                        // perform the save
                        bool saved = await ImageService.SaveImage(ref image);

                        // if saved
                        if (saved)
                        {
                            // if saved
                            if ((HasGalleryManager) && (GalleryManager.HasIndexPage))
                            {
                                // we only need to refresh once
                                // abortUpdate = true;
                                int position = GalleryManager.FindPosition(Artist.Id);

                                // if the position is in range
                                if ((position >= 1) && (position < 5))
                                {
                                    // Reset the selected artist
                                    GalleryManager.SetSelectedArtist(position, GalleryManager.ArtistPageIndex);    

                                    // Update the IndexPage
                                    GalleryManager.IndexPage.Refresh();
                                }
                            }
                        }
                    }
                }

                // Refresh the UI
                StateHasChanged();

                // if we haven't already updated
                // if (!abortUpdate)
                // {
                    // Refresh the UI
                    // StateHasChanged();
                // }
            }
            #endregion

            #region OnInitializedAsync()
            /// <summary>
            /// Create a new instance of an OnInitializedAsync
            /// </summary>
            /// <returns></returns>
            protected override Task OnInitializedAsync()
            {
                // Start off at negative 1, so the first increment goes to 0
                ColumnNumber = -1;

                return base.OnInitializedAsync();
            }
            #endregion

            #region OnReset(string notUsedButRequiredArg)
            /// <summary>
            /// (Optional) This event callback is called by DataJuggler.Blazor.FileUpload after the ResetButton is clicked.
            /// </summary>
            /// <param name="notUsedButRequiredArg">InvokeAsync sends an object parameter, so I believe this or something is required.</param>
            private void OnReset(string notUsedButRequiredArg)
            {
                
            }
            #endregion

        #endregion

        #region Properties

            #region Artist
            /// <summary>
            /// This read only property returns the GalleryManager.Artist
            /// </summary>
            public Artist Artist
            {
                get
                {
                    // initial value
                    Artist artist = null;

                    // if the value for HasGalleryManager is true
                    if (HasGalleryManager)
                    {
                        // set the return value
                        artist = GalleryManager.Artist;
                    }

                    // return value
                    return artist;
                }
            }
            #endregion

            #region ColumnNumber
            /// <summary>
            /// This property gets or sets the value for 'ColumnNumber'.
            /// </summary>
            public int ColumnNumber
            {
                get { return columnNumber; }
                set { columnNumber = value; }
            }
            #endregion
            
            #region GalleryManager
            /// <summary>
            /// The GalleryManager is available to all components
            /// </summary>
            [CascadingParameter] GalleryManager GalleryManager { get; set; }
            #endregion

            #region GalleryOwner
            /// <summary>
            /// This read only property returns the GalleryManager.SelectedArtist
            /// </summary>
            public Artist GalleryOwner
            {
                get
                {
                    // initial value
                    Artist galleryOwner = null;

                    // if the value for HasGalleryManager is true
                    if (HasGalleryManager)
                    {
                        // set the return value
                        galleryOwner = GalleryManager.SelectedArtist;
                    }

                    // return value
                    return galleryOwner;
                }
            }
            #endregion

            #region GalleryTitle
            /// <summary>
            /// This read only property returns true the value for 'GalleryTitle'.
            /// </summary>
            public string GalleryTitle
            {
                get
                {
                    // initial value
                    string galleryTitle = "";
                    
                    // if the value for HasGalleryOwner is true
                    if (HasGalleryOwner)
                    {
                        // Show the gallery owner
                        galleryTitle = GalleryOwner.Name + "'s Gallery";
                    }
                    
                    // return value
                    return galleryTitle;
                }
            }
            #endregion

            #region GalleryUploadPath
            /// <summary>
            /// This read only property returns the path to upload too
            /// </summary>
            public string GalleryUploadPath
            {
                get
                {
                    // initial value
                    string galleryPath = "";

                    // if the value for HasArtist is true
                    if (HasArtist)
                    {
                        // get the path for this gallery
                        galleryPath = "wwwroot/images/gallery/" + Artist.Name.Replace(" ", "");
                    }

                    // return value
                    return galleryPath;
                }
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
            
            #region HasGalleryOwner
            /// <summary>
            /// This property returns true if this object has a 'GalleryOwner'.
            /// </summary>
            public bool HasGalleryOwner
            {
                get
                {
                    // initial value
                    bool hasGalleryOwner = (this.GalleryOwner != null);
                    
                    // return value
                    return hasGalleryOwner;
                }
            }
            #endregion
            
            #region HasGalleryTitle
            /// <summary>
            /// This property returns true if the 'GalleryTitle' exists.
            /// </summary>
            public bool HasGalleryTitle
            {
                get
                {
                    // initial value
                    bool hasGalleryTitle = (!String.IsNullOrEmpty(this.GalleryTitle));
                    
                    // return value
                    return hasGalleryTitle;
                }
            }
            #endregion
            
            #region HasImages
            /// <summary>
            /// This property returns true if this object has an 'Images'.
            /// </summary>
            public bool HasImages
            {
                get
                {
                    // initial value
                    bool hasImages = (this.Images != null);
                    
                    // return value
                    return hasImages;
                }
            }
            #endregion
            
            #region HasMessage
            /// <summary>
            /// This property returns true if the 'Message' exists.
            /// </summary>
            public bool HasMessage
            {
                get
                {
                    // initial value
                    bool hasMessage = (!String.IsNullOrEmpty(this.Message));
                    
                    // return value
                    return hasMessage;
                }
            }
            #endregion
            
            #region HasSelectedArtist
            /// <summary>
            /// This property returns true if this object has a 'SelectedArtist'.
            /// </summary>
            public bool HasSelectedArtist
            {
                get
                {
                    // initial value
                    bool hasSelectedArtist = (this.SelectedArtist != null);
                    
                    // return value
                    return hasSelectedArtist;
                }
            }
            #endregion
            
            #region HasSelectedImage
            /// <summary>
            /// This property returns true if this object has a 'SelectedImage'.
            /// </summary>
            public bool HasSelectedImage
            {
                get
                {
                    // initial value
                    bool hasSelectedImage = (this.SelectedImage != null);
                    
                    // return value
                    return hasSelectedImage;
                }
            }
            #endregion
            
            #region Images
            /// <summary>
            /// This property gets or sets the value for 'Images'.
            /// </summary>
            public List<Image> Images
            {
                get 
                { 
                    // initial value
                    List<Image> images = null;

                    // If the value for the property .HasGalleryManager is true
                    if ((HasGalleryManager) && (GalleryManager.HasSelectedArtist))
                    {
                        // set the return value
                        images = GalleryManager.SelectedArtist.Images;
                    }

                    // set the return value
                    return images; 
                }
            }
            #endregion

            #region ImageStyle1
            /// <summary>
            /// This property gets or sets the value for 'ImageStyle1'.
            /// </summary>
            public string ImageStyle1
            {
                get { return imageStyle1; }
                set { imageStyle1 = value; }
            }
            #endregion
            
            #region ImageUrl1
            /// <summary>
            /// This property gets or sets the value for 'ImageUrl1'.
            /// </summary>
            public string ImageUrl1
            {
                get { return imageUrl1; }
                set { imageUrl1 = value; }
            }
            #endregion
            
            #region IsArtistGalleryOwner
            /// <summary>
            /// This read only property returns true if the GalleryManager.Artist is the SelectedArtist
            /// </summary>
            public bool IsArtistGalleryOwner
            {
                get
                {
                    // initiial value
                    bool isArtistGalleryOwner = false;

                    // verify the GalleryManager.Artist and GalleryManager.SelectedArtist both exist
                    if ((HasArtist) && (HasGalleryOwner))
                    {
                        // set the return value
                        isArtistGalleryOwner = (GalleryManager.Artist.Id == GalleryManager.SelectedArtist.Id);
                    }

                    // return value
                    return isArtistGalleryOwner;
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

            #region RowNumber
            /// <summary>
            /// This property gets or sets the value for 'RowNumber'.
            /// </summary>
            public int RowNumber
            {
                get { return rowNumber; }
                set { rowNumber = value; }
            }
            #endregion
            
            #region SelectedArtist
            /// <summary>
            /// This read only property 
            /// </summary>
            public Artist SelectedArtist
            {
                get
                {
                    // initial value
                    Artist selectedArtist = null;

                    // if the GalleryManager exists
                    if (HasGalleryManager)
                    {
                        // Set the selectedArtist
                        selectedArtist = GalleryManager.SelectedArtist;
                    }

                    // return value
                    return selectedArtist;
                }
            }
            #endregion
            
            #region SelectedImage
            /// <summary>
            /// This property gets or sets the value for 'SelectedImage'.
            /// </summary>
            public Image SelectedImage
            {
                get { return selectedImage; }
                set { selectedImage = value; }
            }
            #endregion

            #region SelectedImageCSS
            /// <summary>
            /// This property gets or sets the value for 'SelectedImageCSS'.
            /// </summary>
            public string SelectedImageCSS
            {
                get { return selectedImageCSS; }
                set { selectedImageCSS = value; }
            }
            #endregion

            #region SelectedImageCSS
            /// <summary>
            /// This property gets or sets the value for 'SelectedImageCSS'.
            /// </summary>
            public string SelectedImageURL
            {
                get 
                {
                    // initial value
                    string selectedImageURL = "";

                    // if the value for HasSelectedImage is true
                    if (HasSelectedImage)
                    {
                        // Set the return value
                        selectedImageURL = SelectedImage.ImageUrl;
                    }

                    // test only


                    // return value
                    return selectedImageURL;
                }
            }
            #endregion
            
            #region ShowWelcomeMessage
            /// <summary>
            /// This property gets or sets the value for 'ShowWelcomeMessage'.
            /// </summary>
            public bool ShowWelcomeMessage
            {
                get 
                { 
                    // initial value
                    bool showWelcomeMessage = true;

                    // if the value for HasGalleryManager is true
                    if (HasGalleryManager)
                    {
                        // Show the Welcome message if there is not a selected artist
                        showWelcomeMessage = !GalleryManager.HasSelectedArtist;

                        // we have to check if a Login is in process
                        if ((showWelcomeMessage) && (GalleryManager.HasIndexPage))
                        {
                            // Is a Login or SignUp in Progress
                            showWelcomeMessage = !GalleryManager.IndexPage.LoginOrSignUpInProgress;
                        }
                    }

                    // set the return value
                    return showWelcomeMessage;
                }
            }
            #endregion

        #endregion

    }
    #endregion

}
