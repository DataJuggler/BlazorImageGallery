

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObjectLibrary.BusinessObjects;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Web;
using DataJuggler.UltimateHelper.Core;

#endregion

namespace BlazorImageGallery.Components
{

    #region class ImageButton
    /// <summary>
    /// This class creates properties for a BlazorStyled CSS class that derive
    /// from an ObjectLibrary.BusinessObject.Image class. This saves setting
    /// many properties from the component, they all just pull the values from 
    /// the underlying image object.
    /// </summary>
    public partial class ImageButton 
    {
        
        #region Private Variables
        private Image image;
        private int height;
        private string heightPixels;
        private int width;
        private string widthPixels;
        private string imageUrl;
        private string imageStyle;
        private double scale;
        private int rowNumber;
        private int columnNumber;
        private string columnLeftPixels;
        private string rowTopPixels;
        public const int ImagesPerRow = 5;
        private const int RowHeight = 88;
        private const int ColumnWidth = 172;
        #endregion
        
        #region Methods
            
            #region Button_Clicked()
            /// <summary>
            /// This method Button _ Clicked
            /// </summary>
            public void Button_Clicked()
            {
                
            }
            #endregion
            
            #region SetColumnLeftPixels()
            /// <summary>
            /// This method Set Column Left Pixels
            /// </summary>
            public void SetColumnLeftPixels()
            {
                // Get the left value
                int columnLeft = (ColumnNumber * ColumnWidth) - 160;

                // initial value
                ColumnLeftPixels = columnLeft.ToString() + "px";
            }
            #endregion
            
            #region SetColumnNumber(int imageNumber)
            /// <summary>
            /// This method returns the Column Number
            /// </summary>
            public int SetColumnNumber(int imageNumber)
            {
                // initial value
                int columnNumber = imageNumber % 5;

                // if zero
                if (columnNumber == 0)
                {
                    // Set to 5
                    columnNumber = 5;
                }

                // return value
                return columnNumber;
            }
            #endregion
            
            #region SetImageAttributes()
            /// <summary>
            /// This method Sets Image Properties
            /// </summary>
            public void SetImageAttributes()
            {
                try
                {
                    // initial value
                    int height = 0;
                    int width = 0;
                    string imageUrl = "";
                    int imageNumber = 0;
                
                    // if the value for HasImage is true
                    if (HasImage)
                    {
                        height = image.Height;
                        width = image.Width;
                        imageUrl = image.ImageUrl;
                        imageNumber = image.ImageNumber;
                    }
                
                    // Set the values
                    this.ColumnNumber = SetColumnNumber(imageNumber);
                    this.RowNumber = SetRowNumber(imageNumber);
                    this.Height = height;
                    this.Width = width;
                    this.ImageUrl = imageUrl;
                    this.Scale = .1;
                }
                catch (Exception error)
                {
                    // for debugging only
                    DebugHelper.WriteDebugError("SetImageAttributes", "ImageButton.razor.cs", error);
                }
            }            
            #endregion
            
            #region SetRowNumber(int imageNumber)
            /// <summary>
            /// This method returns the Row Number
            /// </summary>
            public int SetRowNumber(int imageNumber)
            {
                // initial value
                int rowNumber = 1;

                // if the imageNumber is lessage than 5
                if (imageNumber >= 11)
                {
                    // Set to 3
                    rowNumber = 3;
                }
                else if (imageNumber >= 6)
                {
                    // Set to 2
                    rowNumber = 2;
                }
                
                // return value
                return rowNumber;
            }
            #endregion
            
        #endregion
        
        #region Properties

            #region ColumnLeftPixels
            /// <summary>
            /// This property gets or sets the value for 'ColumnLeftPixels'.
            /// </summary>
            public string ColumnLeftPixels
            {
                get { return columnLeftPixels; }
                set { columnLeftPixels = value; }
            }
            #endregion

            #region ColumnNumber
            /// <summary>
            /// This property gets or sets the value for 'ColumnNumber'.
            /// </summary>
            [Parameter]
            public int ColumnNumber
            {
                get { return columnNumber; }
                set 
                { 
                    // set the value
                    columnNumber = value;

                    // Set the value for ColumnLeftPixels
                    SetColumnLeftPixels();
                }
            }
            #endregion
            
            #region HasImage
            /// <summary>
            /// This property returns true if this object has an 'Image'.
            /// </summary>
            public bool HasImage
            {
                get
                {
                    // initial value
                    bool hasImage = (this.Image != null);
                    
                    // return value
                    return hasImage;
                }
            }
            #endregion
            
            #region Height
            /// <summary>
            /// This property gets or sets the value for 'Height'.
            /// </summary>
            public int Height
            {
                get { return height; }
                set 
                { 
                    // set the value
                    height = value;

                    // Set the value for HeightPixels
                    HeightPixels = height.ToString() + "px";
                }
            }
            #endregion
            
            #region HeightPixels
            /// <summary>
            /// This property gets or sets the value for 'HeightPixels'.
            /// </summary>
            public string HeightPixels
            {
                get { return heightPixels; }
                set { heightPixels = value; }
            }
            #endregion
            
            #region Image
            /// <summary>
            /// This property gets or sets the value for 'Image'.
            /// </summary>
            [Parameter]
            public Image Image
            {
                get { return image; }
                set 
                { 
                    // set the value
                    image = value;
                    
                    // set the image properties
                    SetImageAttributes();
                }
            }
            #endregion
            
            #region ImageStyle
            /// <summary>
            /// This property gets or sets the value for 'ImageStyle'.
            /// </summary>
            public string ImageStyle
            {
                get { return imageStyle; }
                set { imageStyle = value; }
            }
            #endregion
            
            #region ImageUrl
            /// <summary>
            /// This property gets or sets the value for 'ImageUrl'.
            /// </summary>
            public string ImageUrl
            {
                get { return imageUrl; }
                set { imageUrl = value; }
            }
            #endregion

            #region RowNumber
            /// <summary>
            /// This property gets or sets the value for 'RowNumber'.
            /// </summary>
            [Parameter]
            public int RowNumber
            {
                get { return rowNumber; }
                set 
                { 
                    // set the value
                    rowNumber = value;

                    // Set the value for up
                    int up = (rowNumber * RowHeight) - 400;

                    // Set the value
                    RowTopPixels = up.ToString() + "px";
                }
            }
            #endregion
            
            #region RowTopPixels
            /// <summary>
            /// This property gets or sets the value for 'RowTopPixels'.
            /// </summary>
            public string RowTopPixels
            {
                get { return rowTopPixels; }
                set { rowTopPixels = value; }
            }
            #endregion
            
            #region Scale
            /// <summary>
            /// method [Enter Method Description]
            /// </summary>
            public double Scale
            {
                get { return scale; }
                set { scale = value; }
            }
            #endregion
            
            #region Width
            /// <summary>
            /// This property gets or sets the value for 'Width'.
            /// </summary>
            public int Width
            {
                get { return width; }
                set 
                { 
                    // set the values
                    width = value;

                    // set the value
                    WidthPixels = width.ToString() + "px";
                }
            }
            #endregion
            
            #region WidthPixels
            /// <summary>
            /// This property gets or sets the value for 'WidthPixels'.
            /// </summary>
            public string WidthPixels
            {
                get { return widthPixels; }
                set { widthPixels = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
