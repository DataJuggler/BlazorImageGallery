
#region using statements

using System;

#endregion

namespace ObjectLibrary.BusinessObjects
{

    #region class Image
    [Serializable]
    public partial class Image
    {

        #region Private Variables
        private bool loadByOwnerId;        
        #endregion

        #region Constructor
        public Image()
        {

        }
        #endregion

        #region Methods

            #region Clone()
            public Image Clone()
            {
                // Create New Object
                Image newImage = (Image) this.MemberwiseClone();

                // Return Cloned Object
                return newImage;
            }
            #endregion

        #endregion

        #region Properties

            #region LoadByOwnerId
            /// <summary>
            /// This property gets or sets the value for 'LoadByOwnerId'.
            /// </summary>
            public bool LoadByOwnerId
            {
                get { return loadByOwnerId; }
                set { loadByOwnerId = value; }
            }
            #endregion
            
        #endregion

    }
    #endregion

}
