

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObjectLibrary.BusinessObjects;
using DataJuggler.UltimateHelper.Core;

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

        #region Constructor()
        /// <summary>
        /// Create a new instance of GalleryManager object
        /// </summary>
        public GalleryManager()
        {   
            
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
            public void SetSelectedArtist(int positionNumber, int pageIndex)
            {
                // if there is a collection of selected artists
                if (ListHelper.HasOneOrMoreItems(Artists))
                {
                    // set the index
                    int index = (pageIndex * 5) + positionNumber - 1;

                    // verify this value is in range
                    if ((index >= 0) && (index < Artists.Count))
                    {
                        // Set the SelectedArtist
                        this.SelectedArtist = Artists[index];
                    }
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
