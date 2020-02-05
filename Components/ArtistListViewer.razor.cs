

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObjectLibrary.BusinessObjects;
using Microsoft.AspNetCore.Components;
using BlazorImageGallery.Util;
using DataJuggler.UltimateHelper.Core;

#endregion

namespace BlazorImageGallery.Components
{

    #region class ArtistListViewer
    /// <summary>
    /// This component is used to display a list of artists.
    /// </summary>
    public partial class ArtistListViewer
    {
        
        #region Private Variables
        private string artistStyle1;
        private string artistStyle2;
        private string artistStyle3;
        private string artistStyle4;
        private string artistStyle5;
        private int pageIndex;
        private bool showPrevButton;
        private bool showNextButton;
        public const int ArtistsPerPage = 5;        
        #endregion

        #region ArtistListViewer()
        /// <summary>
        /// Create a new instance of an ArtistListViewer object
        /// </summary>
        public ArtistListViewer()
        {
            // Perform initializations for this object
            Init();
        }
        #endregion

        #region Methods

            #region GetTotalPages()
            /// <summary>
            /// This method returns the Total Pages
            /// </summary>
            public int GetTotalPages()
            {
                // initial value
                int totalPages = 0;

                // If the Artists collection exists and has one or more items
                if (ListHelper.HasOneOrMoreItems(Artists))
                {
                    // set the number of artists
                    int count = Artists.Count;

                    // if we are at the 5th element
                    if (count % ArtistsPerPage == 0)
                    {
                        // divide count by 5
                        double temp = (double) count / ArtistsPerPage;

                        // set the return value
                        totalPages = NumericHelper.RoundDown(temp);
                    }
                    else
                    {
                        // divide count by 5
                        double temp = (double) count / ArtistsPerPage;

                        // set the return value
                        totalPages = NumericHelper.RoundDown(temp) + 1;
                    }
                }
                
                // return value
                return totalPages;
            }
            #endregion
            
            #region Init()
            /// <summary>
            /// This method performs initializations for this object.
            /// </summary>
            public void Init()
            {
                
            }
            #endregion

            #region NextButton_Click()
            /// <summary>
            /// This method Next Button _ Click
            /// </summary>
            public void NextButton_Click()
            {
               // Increment the value for PageIndex
               PageIndex++;
            }
            #endregion

            #region PrevButton_Click()
            /// <summary>
            /// This method Prev Button _ Click
            /// </summary>
            public void PrevButton_Click()
            {
                // Decrement the value for PageIndex
                PageIndex--;
            }
            #endregion
            
        #endregion

        #region Properties

            #region Artist1
            /// <summary>
            /// This read only property returns the Artist at Artist1Index, if the Artist1Index 
            /// is in range (greater than or equal to zero and less than Artists.Count).
            /// </summary>
            public Artist Artist1
            {
                get
                {
                    // initial value
                    Artist artist = null;
                    
                    // if the ArtistsCollection exists, and Artist1Index is in range
                    if ((HasArtists) && (Artist1Index >= 0) && (Artist1Index < Artists.Count))
                    {
                        // set the return value
                        artist = Artists[Artist1Index];
                    }
                    
                    // return value
                    return artist;
                }
            }
            #endregion

            #region Artist1Index
            /// <summary>
            /// This read only property returns the value for 'Artist1Index'.
            /// </summary>
            public int Artist1Index
            {
                get
                {
                    // return value
                    return (PageIndex * 5);
                }
            }
            #endregion

            #region Artist1Name
            /// <summary>
            /// This read only property returns the Name for Artist1, if Artist1 exists.
            /// </summary>
            public string Artist1Name
            {
                get
                {
                    // initial value
                    string artist1Name = "";
                    
                    // if the value for HasArtist1 is true
                    if (HasArtist1)
                    {
                        // set the return value
                        artist1Name = Artist1.Name;
                    }
                    
                    // return value
                    return artist1Name;
                }
            }
            #endregion

            #region Artist1ProfilePictureUrl
            /// <summary>
            /// This read only property returns the ProfilePicture for Artist1, if Artist1 exists.
            /// </summary>
            public string Artist1ProfilePictureUrl
            {
                get
                {
                    // initial value
                    string profilePictureUrl = "";
                    
                    // if the value for HasArtist1 is true
                    if (HasArtist1)
                    {
                        // set the return value
                        profilePictureUrl = Artist1.ProfilePicture;
                    }
                    
                    // return value
                    return profilePictureUrl;
                }
            }
            #endregion
            
            #region Artist2
            /// <summary>
            /// This read only property returns the Artist at Artist2Index, if the Artist2Index 
            /// is in range (greater than or equal to zero and less than Artists.Count).
            /// </summary>
            public Artist Artist2
            {
                get
                {
                    // initial value
                    Artist artist = null;
                    
                    // if the ArtistsCollection exists, and Artist2Index is in range
                    if ((HasArtists) && (Artist2Index >= 0) && (Artist2Index < Artists.Count))
                    {
                        // set the return value
                        artist = Artists[Artist2Index];
                    }
                    
                    // return value
                    return artist;
                }
            }
            #endregion
            
            #region Artist2Index
            /// <summary>
            /// This read only property returns the value for 'Artist2Index'.
            /// </summary>
            public int Artist2Index
            {
                get
                {
                    // return value
                    return (PageIndex * 5) + 1;
                }
            }
            #endregion

            #region Artist2Name
            /// <summary>
            /// This read only property returns the Name for Artist2, if Artist2 exists.
            /// </summary>
            public string Artist2Name
            {
                get
                {
                    // initial value
                    string artist2Name = "";
                    
                    // if the value for HasArtist2 is true
                    if (HasArtist2)
                    {
                        // set the return value
                        artist2Name = Artist2.Name;
                    }
                    
                    // return value
                    return artist2Name;
                }
            }
            #endregion

            #region Artist2ProfilePictureUrl
            /// <summary>
            /// This read only property returns the ProfilePicture for Artist2, if Artist2 exists.
            /// </summary>
            public string Artist2ProfilePictureUrl
            {
                get
                {
                    // initial value
                    string profilePictureUrl = "";
                    
                    // if the value for HasArtist2 is true
                    if (HasArtist2)
                    {
                        // set the return value
                        profilePictureUrl = Artist2.ProfilePicture;
                    }
                    
                    // return value
                    return profilePictureUrl;
                }
            }
            #endregion
            
            #region Artist3
            /// <summary>
            /// This read only property returns the Artist at Artist3Index, if the Artist3Index 
            /// is in range (greater than or equal to zero and less than Artists.Count).
            /// </summary>
            public Artist Artist3
            {
                get
                {
                    // initial value
                    Artist artist = null;
                    
                    // if the ArtistsCollection exists, and Artist3Index is in range
                    if ((HasArtists) && (Artist3Index >= 0) && (Artist3Index < Artists.Count))
                    {
                        // set the return value
                        artist = Artists[Artist3Index];
                    }
                    
                    // return value
                    return artist;
                }
            }
            #endregion
            
            #region Artist3Index
            /// <summary>
            /// This read only property returns the value for 'Artist3Index'.
            /// </summary>
            public int Artist3Index
            {
                get
                {
                    // return value
                    return (PageIndex * 5) + 2;
                }
            }
            #endregion

            #region Artist3Name
            /// <summary>
            /// This read only property returns the Name for Artist3, if Artist3 exists.
            /// </summary>
            public string Artist3Name
            {
                get
                {
                    // initial value
                    string artist3Name = "";
                    
                    // if the value for HasArtist3 is true
                    if (HasArtist3)
                    {
                        // set the return value
                        artist3Name = Artist3.Name;
                    }
                    
                    // return value
                    return artist3Name;
                }
            }
            #endregion

            #region Artist3ProfilePictureUrl
            /// <summary>
            /// This read only property returns the ProfilePicture for Artist3, if Artist3 exists.
            /// </summary>
            public string Artist3ProfilePictureUrl
            {
                get
                {
                    // initial value
                    string profilePictureUrl = "";
                    
                    // if the value for HasArtist3 is true
                    if (HasArtist3)
                    {
                        // set the return value
                        profilePictureUrl = Artist3.ProfilePicture;
                    }
                    
                    // return value
                    return profilePictureUrl;
                }
            }
            #endregion
            
            #region Artist4
            /// <summary>
            /// This read only property returns the Artist at Artist4Index, if the Artist4Index 
            /// is in range (greater than or equal to zero and less than Artists.Count).
            /// </summary>
            public Artist Artist4
            {
                get
                {
                    // initial value
                    Artist artist = null;
                    
                    // if the ArtistsCollection exists, and Artist4Index is in range
                    if ((HasArtists) && (Artist4Index >= 0) && (Artist4Index < Artists.Count))
                    {
                        // set the return value
                        artist = Artists[Artist4Index];
                    }
                    
                    // return value
                    return artist;
                }
            }
            #endregion
            
            #region Artist4Index
            /// <summary>
            /// This read only property returns the value for 'Artist4Index'.
            /// </summary>
            public int Artist4Index
            {
                get
                {
                    // return value
                    return (PageIndex * 5) + 3;
                }
            }
            #endregion

            #region Artist4Name
            /// <summary>
            /// This read only property returns the Name for Artist4, if Artist4 exists.
            /// </summary>
            public string Artist4Name
            {
                get
                {
                    // initial value
                    string artist4Name = "";
                    
                    // if the value for HasArtist4 is true
                    if (HasArtist4)
                    {
                        // set the return value
                        artist4Name = Artist4.Name;
                    }
                    
                    // return value
                    return artist4Name;
                }
            }
            #endregion

            #region Artist4ProfilePictureUrl
            /// <summary>
            /// This read only property returns the ProfilePicture for Artist4, if Artist4 exists.
            /// </summary>
            public string Artist4ProfilePictureUrl
            {
                get
                {
                    // initial value
                    string profilePictureUrl = "";
                    
                    // if the value for HasArtist4 is true
                    if (HasArtist4)
                    {
                        // set the return value
                        profilePictureUrl = Artist4.ProfilePicture;
                    }
                    
                    // return value
                    return profilePictureUrl;
                }
            }
            #endregion
            
            #region Artist5
            /// <summary>
            /// This read only property returns the Artist at Artist5Index, if the Artist5Index 
            /// is in range (greater than or equal to zero and less than Artists.Count).
            /// </summary>
            public Artist Artist5
            {
                get
                {
                    // initial value
                    Artist artist = null;
                    
                    // if the ArtistsCollection exists, and Artist5Index is in range
                    if ((HasArtists) && (Artist5Index >= 0) && (Artist5Index < Artists.Count))
                    {
                        // set the return value
                        artist = Artists[Artist5Index];
                    }
                    
                    // return value
                    return artist;
                }
            }
            #endregion
            
            #region Artist5Index
            /// <summary>
            /// This read only property returns the value for 'Artist5Index'.
            /// </summary>
            public int Artist5Index
            {
                get
                {
                    // return value
                    return (PageIndex * 5) + 4;
                }
            }
            #endregion

            #region Artist5Name
            /// <summary>
            /// This read only property returns the Name for Artist5, if Artist5 exists.
            /// </summary>
            public string Artist5Name
            {
                get
                {
                    // initial value
                    string artist5Name = "";
                    
                    // if the value for HasArtist5 is true
                    if (HasArtist5)
                    {
                        // set the return value
                        artist5Name = Artist5.Name;
                    }
                    
                    // return value
                    return artist5Name;
                }
            }
            #endregion

            #region Artist5ProfilePictureUrl
            /// <summary>
            /// This read only property returns the ProfilePicture for Artist5, if Artist5 exists.
            /// </summary>
            public string Artist5ProfilePictureUrl
            {
                get
                {
                    // initial value
                    string profilePictureUrl = "";
                    
                    // if the value for HasArtist5 is true
                    if (HasArtist5)
                    {
                        // set the return value
                        profilePictureUrl = Artist5.ProfilePicture;
                    }
                    
                    // return value
                    return profilePictureUrl;
                }
            }
            #endregion
            
            #region Artists
            /// <summary>
            /// This read only property returns the GalleryManager.Artists
            /// </summary>
            public List<Artist> Artists
            {
                get
                {
                    // initial value
                    List<Artist> artists = null;
                    
                    // if the value for HasGalleryManager is true
                    if (HasGalleryManager)
                    {
                        // set the return value
                        artists = GalleryManager.Artists;
                    }
                    
                    // return value
                    return artists;
                }
            }
            #endregion
            
            #region ArtistStyle1
            /// <summary>
            /// This property gets or sets the value for 'ArtistStyle1'.
            /// </summary>
            public string ArtistStyle1
            {
                get { return artistStyle1; }
                set { artistStyle1 = value; }
            }
            #endregion
            
            #region ArtistStyle2
            /// <summary>
            /// This property gets or sets the value for 'ArtistStyle2'.
            /// </summary>
            public string ArtistStyle2
            {
                get { return artistStyle2; }
                set { artistStyle2 = value; }
            }
            #endregion
            
            #region ArtistStyle3
            /// <summary>
            /// This property gets or sets the value for 'ArtistStyle3'.
            /// </summary>
            public string ArtistStyle3
            {
                get { return artistStyle3; }
                set { artistStyle3 = value; }
            }
            #endregion
            
            #region ArtistStyle4
            /// <summary>
            /// This property gets or sets the value for 'ArtistStyle4'.
            /// </summary>
            public string ArtistStyle4
            {
                get { return artistStyle4; }
                set { artistStyle4 = value; }
            }
            #endregion
            
            #region ArtistStyle5
            /// <summary>
            /// This property gets or sets the value for 'ArtistStyle5'.
            /// </summary>
            public string ArtistStyle5
            {
                get { return artistStyle5; }
                set { artistStyle5 = value; }
            }
            #endregion
            
            #region GalleryManager
            /// <summary>
            /// This parameter is available to all components 
            /// </summary>
            [CascadingParameter] GalleryManager GalleryManager { get; set; }
            #endregion
            
            #region HasArtist1
            /// <summary>
            /// This property returns true if this object has an 'Artist1'.
            /// </summary>
            public bool HasArtist1
            {
                get
                {
                    // initial value
                    bool hasArtist1 = (this.Artist1 != null);
                    
                    // return value
                    return hasArtist1;
                }
            }
            #endregion
            
            #region HasArtist2
            /// <summary>
            /// This property returns true if this object has an 'Artist2'.
            /// </summary>
            public bool HasArtist2
            {
                get
                {
                    // initial value
                    bool hasArtist2 = (this.Artist2 != null);
                    
                    // return value
                    return hasArtist2;
                }
            }
            #endregion
            
            #region HasArtist3
            /// <summary>
            /// This property returns true if this object has an 'Artist3'.
            /// </summary>
            public bool HasArtist3
            {
                get
                {
                    // initial value
                    bool hasArtist3 = (this.Artist3 != null);
                    
                    // return value
                    return hasArtist3;
                }
            }
            #endregion
            
            #region HasArtist4
            /// <summary>
            /// This property returns true if this object has an 'Artist4'.
            /// </summary>
            public bool HasArtist4
            {
                get
                {
                    // initial value
                    bool hasArtist4 = (this.Artist4 != null);
                    
                    // return value
                    return hasArtist4;
                }
            }
            #endregion
            
            #region HasArtist5
            /// <summary>
            /// This property returns true if this object has an 'Artist5'.
            /// </summary>
            public bool HasArtist5
            {
                get
                {
                    // initial value
                    bool hasArtist5 = (this.Artist5 != null);
                    
                    // return value
                    return hasArtist5;
                }
            }
            #endregion
            
            #region HasArtists
            /// <summary>
            /// This property returns true if this object has an 'Artists'.
            /// </summary>
            public bool HasArtists
            {
                get
                {
                    // initial value
                    bool hasArtists = (this.Artists != null);
                    
                    // return value
                    return hasArtists;
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

            #region PageIndex
            /// <summary>
            /// This property gets or sets the value for 'PageIndex'.
            /// </summary>
            public int PageIndex
            {
                get { return pageIndex; }
                set { pageIndex = value; }
            }
            #endregion

            #region PageDisplayText
            /// <summary>
            /// This read only property returns the current page text and items displayed.
            /// Example: Page 3 of 12
            /// </summary>
            public string PageDisplayText
            {
                get
                {
                    // locals
                    int min = 0;
                    int max = 0;
                    int total = 0;

                    // set the value
                    int totalPages = TotalPages;

                    // if we have more than one page
                    if (totalPages > 1)
                    {
                        // Show the PrevButton if we are not on the first page
                        ShowPrevButton = (PageNumber > 1);

                        // Show the NextButton if not on the last page
                        ShowNextButton = (PageNumber < totalPages);
                    }
                    else
                    {   
                        // if Prev or Next
                        ShowPrevButton = false;
                        ShowNextButton = false;
                    }
                    
                    // If have to use the GalleryManager.Artists, because the local Artists collection is only the current 5.

                    // If the Artists collection exists and has one or more items
                    if ((NullHelper.Exists(GalleryManager)) && (ListHelper.HasOneOrMoreItems(GalleryManager.Artists)))
                    {
                        // Set the min display value
                        min = ((PageIndex * 5) + 1);
                        max = min + 4;
                        total = GalleryManager.Artists.Count;

                        // if out of range
                        if (max > GalleryManager.Artists.Count)
                        {
                            // Set the value for Max
                            max = GalleryManager.Artists.Count;
                        }
                    }

                    // initial value
                    string displayText = "Viewing " + min.ToString() + " - " + max.ToString() + " of " + total.ToString();

                    // return value
                    return displayText;
                }
            }
            #endregion
            
            #region PageNumber
            /// <summary>
            /// This read only property returns the value for 'PageNumber'.
            /// </summary>
            public int PageNumber
            {
                get
                {
                    // initial value
                    int pageNumber = PageIndex + 1;
                    
                    // return value 
                    return pageNumber;
                }
            }
            #endregion
            
            #region SelectedArtist
            /// <summary>
            /// This read only property returns the GalleryManager.SelectedArtist
            /// </summary>
            public Artist SelectedArtist
            {
                get
                {
                    // initial value
                    Artist selectedArtist = null;
                    
                    // if the value for HasGalleryManager is true
                    if (HasGalleryManager)
                    {
                        // set the return value
                        selectedArtist = GalleryManager.SelectedArtist;
                    }
                    
                    // return value
                    return selectedArtist;
                }
            }
            #endregion

            #region ShowNextButton
            /// <summary>
            /// This property gets or sets the value for 'ShowNextButton'.
            /// </summary>
            public bool ShowNextButton
            {
                get { return showNextButton; }
                set { showNextButton = value; }
            }
            #endregion
            
            #region ShowPrevButton
            /// <summary>
            /// This property gets or sets the value for 'ShowPrevButton'.
            /// </summary>
            public bool ShowPrevButton
            {
                get { return showPrevButton; }
                set { showPrevButton = value; }
            }
            #endregion
            
            #region TotalPages
            /// <summary>
            /// This read only property returns the value for 'TotalPages'.
            /// </summary>
            public int TotalPages
            {
                get
                {
                    // initial value
                    int totalPages = 0;
                    
                    // if the value for HasArtists is true
                    if (HasArtists)
                    {
                        // get the total Pages
                        totalPages = GetTotalPages();                        
                    }

                    // return value 
                    return totalPages;
                }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
