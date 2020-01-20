
#region using statements

using System;
using System.Collections.Generic;

#endregion

namespace ObjectLibrary.BusinessObjects
{

    #region class Artist
    [Serializable]
    public partial class Artist
    {

        #region Private Variables
        private bool findByEmailAddress;
        private List<Image> images;
        #endregion

        #region Constructor
        public Artist()
        {

        }
        #endregion

        #region Methods

            #region Clone()
            public Artist Clone()
            {
                // Create New Object
                Artist newArtist = (Artist) this.MemberwiseClone();

                // Return Cloned Object
                return newArtist;
            }
            #endregion

        #endregion

        #region Properties

            #region FindByEmailAddress
            /// <summary>
            /// This property gets or sets the value for 'FindByEmailAddress'.
            /// </summary>
            public bool FindByEmailAddress
            {
                get { return findByEmailAddress; }
                set { findByEmailAddress = value; }
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
            
            #region Images
            /// <summary>
            /// This property gets or sets the value for 'Images'.
            /// </summary>
            public List<Image> Images
            {
                get { return images; }
                set { images = value; }
            }
            #endregion
            
        #endregion

    }
    #endregion

}
