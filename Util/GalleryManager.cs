

#region using statements

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObjectLibrary.BusinessObjects;
using DataJuggler.UltimateHelper.Core;
using BlazorImageGallery.Pages;
using DataGateway.Services;

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
        private Index indexPage;
        private bool resetDisplay;
        private int artistPageIndex;
        #endregion

        #region Constructor(Index indexPage)
        /// <summary>
        /// Create a new instance of GalleryManager object
        /// </summary>
        public GalleryManager(Index indexPage)
        {   
            // Store the Index page
            this.IndexPage = indexPage;
        }
        #endregion

        #region Methods

            #region FindArtistIndex(int id)
            /// <summary>
            /// This method returns the Artist Index
            /// </summary>
            public int FindArtistIndex(int id)
            {
                // initial value
                int artistIndex = -1;

                // local
                int tempIndex = -1;

                // If the Artists collection exists and has one or more items
                if (ListHelper.HasOneOrMoreItems(Artists))
                {
                    foreach (Artist artist in Artists)
                    {
                        // Increment the value for tempIndex
                        tempIndex++;

                        // if this is the Id being sought
                        if (artist.Id == id)
                        {
                            // set the return value
                            artistIndex = tempIndex;

                            // break out of loop
                            break;
                        }
                    }
                }
                
                // return value
                return artistIndex;
            }
            #endregion
            
            #region FindPosition(int artistId)
            /// <summary>
            /// This method returns the Position
            /// </summary>
            public int FindPosition(int artistId)
            {
                // initial value
                int position = 0;

                // Get the index
                int index = FindArtistIndex(artistId);

                // if the index was found
                if (index >= 0)
                {
                    // set the return value
                    position = index % 5 + 1;
                }
                
                // return value
                return position;
            }
            #endregion
            
            #region SetSelectedArtist(int positionNumber, int pageIndex)
            /// <summary>
            /// This method Set Selected Artist
            /// </summary>
            public async void SetSelectedArtist(int positionNumber, int pageIndex)
            {
                try
                {
                    // if there is a collection of selected artists
                    if (ListHelper.HasOneOrMoreItems(Artists))
                    {
                        // set the index
                        int index = (pageIndex * 5) + positionNumber - 1;

                        // verify this value is in range
                        if ((index >= 0) && (index < Artists.Count))
                        {
                            // Storing this so we can call this again
                            this.ArtistPageIndex = pageIndex;

                            // Set the SelectedArtist
                            this.SelectedArtist = Artists[index];    
                        
                            // if the value for HasIndexPage is true
                            if ((HasIndexPage) && (HasSelectedArtist))
                            {
                                // load the images for this artist
                                SelectedArtist.Images = await IndexPage.LoadImagesForArtist(SelectedArtist.Id);
                            }
                        }
                    }    
                }
                catch (System.Exception error)
                {
                    // for debugging only
                    DebugHelper.WriteDebugError("SetSelectedArtist", "GalleryManager", error);
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
            
            #region ArtistPageIndex
            /// <summary>
            /// This property gets or sets the value for 'ArtistPageIndex'.
            /// </summary>
            public int ArtistPageIndex
            {
                get { return artistPageIndex; }
                set { artistPageIndex = value; }
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
            
            #region HasIndexPage
            /// <summary>
            /// This property returns true if this object has an 'IndexPage'.
            /// </summary>
            public bool HasIndexPage
            {
                get
                {
                    // initial value
                    bool hasIndexPage = (this.IndexPage != null);
                    
                    // return value
                    return hasIndexPage;
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
            
            #region IndexPage
            /// <summary>
            /// This property gets or sets the value for 'IndexPage'.
            /// </summary>
            public Index IndexPage
            {
                get { return indexPage; }
                set { indexPage = value; }
            }
            #endregion
            
            #region SelectedArtist
            /// <summary>
            /// This property gets or sets the value for 'SelectedArtist'.
            /// </summary>
            public Artist SelectedArtist
            {
                get { return selectedArtist; }
                set 
                {       
                    // set the value
                    selectedArtist = value;

                    // if the value for HasIndexPage is true
                    if (HasIndexPage)
                    {
                        // Update the UI (to hide the 
                        IndexPage.Refresh();
                    }
                }
            }
            #endregion
            
        #endregion

    }
    #endregion

}
