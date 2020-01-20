

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObjectLibrary.BusinessObjects;
using Microsoft.AspNetCore.Components;
using BlazorImageGallery.Util;

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
        #endregion

        #region Properties

            #region GalleryManager
            /// <summary>
            /// The GalleryManager is available to all components
            /// </summary>
            [CascadingParameter] GalleryManager GalleryManager { get; set; }
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
            
        #endregion

    }
    #endregion

}
