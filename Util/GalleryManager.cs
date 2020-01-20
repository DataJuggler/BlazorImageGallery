

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObjectLibrary.BusinessObjects;

#endregion

namespace BlazorImageGallery.Util
{

    #region class GalleryManager
    /// <summary>
    /// This class is used to provide a CascadingParameter so values
    /// such as the current Artist can be available to other components.
    /// </summary>
    public class GalleryManager
    {

        #region Private Variables
        private Artist artist;
        private Artist selectedArtist;
        private List<Artist> artists;
        #endregion

        #region GalleryManager()
        /// <summary>
        /// Create a new instance of GalleryManager object
        /// </summary>
        public GalleryManager()
        {   
                
        }
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
            
            #region Artists
            /// <summary>
            /// This property gets or sets the value for 'Artists'.
            /// </summary>
            public List<Artist> Artists
            {
                get { return artists; }
                set { artists = value; }
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
            
            #region SelectedArtist
            /// <summary>
            /// This property gets or sets the value for 'SelectedArtist'.
            /// </summary>
            public Artist SelectedArtist
            {
                get { return selectedArtist; }
                set { selectedArtist = value; }
            }
            #endregion
            
        #endregion

    }
    #endregion

}
