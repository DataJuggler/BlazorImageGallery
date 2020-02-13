

#region using statements

using ObjectLibrary.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;

#endregion


namespace DataAccessComponent.DataManager.Readers
{

    #region class ImageReader
    /// <summary>
    /// This class loads a single 'Image' object or a list of type <Image>.
    /// </summary>
    public class ImageReader
    {

        #region Static Methods

            #region Load(DataRow dataRow)
            /// <summary>
            /// This method loads a 'Image' object
            /// from the dataRow passed in.
            /// </summary>
            /// <param name='dataRow'>The 'DataRow' to load from.</param>
            /// <returns>A 'Image' DataObject.</returns>
            public static Image Load(DataRow dataRow)
            {
                // Initial Value
                Image image = new Image();

                // Create field Integers
                int createdDatefield = 0;
                int extensionfield = 1;
                int fileSizefield = 2;
                int fullPathfield = 3;
                int heightfield = 4;
                int idfield = 5;
                int imageNumberfield = 6;
                int imageUrlfield = 7;
                int namefield = 8;
                int ownerIdfield = 9;
                int sitePathfield = 10;
                int visiblefield = 11;
                int widthfield = 12;

                try
                {
                    // Load Each field
                    image.CreatedDate = DataHelper.ParseDate(dataRow.ItemArray[createdDatefield]);
                    image.Extension = DataHelper.ParseString(dataRow.ItemArray[extensionfield]);
                    image.FileSize = DataHelper.ParseInteger(dataRow.ItemArray[fileSizefield], 0);
                    image.FullPath = DataHelper.ParseString(dataRow.ItemArray[fullPathfield]);
                    image.Height = DataHelper.ParseInteger(dataRow.ItemArray[heightfield], 0);
                    image.UpdateIdentity(DataHelper.ParseInteger(dataRow.ItemArray[idfield], 0));
                    image.ImageNumber = DataHelper.ParseInteger(dataRow.ItemArray[imageNumberfield], 0);
                    image.ImageUrl = DataHelper.ParseString(dataRow.ItemArray[imageUrlfield]);
                    image.Name = DataHelper.ParseString(dataRow.ItemArray[namefield]);
                    image.OwnerId = DataHelper.ParseInteger(dataRow.ItemArray[ownerIdfield], 0);
                    image.SitePath = DataHelper.ParseString(dataRow.ItemArray[sitePathfield]);
                    image.Visible = DataHelper.ParseBoolean(dataRow.ItemArray[visiblefield], false);
                    image.Width = DataHelper.ParseInteger(dataRow.ItemArray[widthfield], 0);
                }
                catch
                {
                }

                // return value
                return image;
            }
            #endregion

            #region LoadCollection(DataTable dataTable)
            /// <summary>
            /// This method loads a collection of 'Image' objects.
            /// from the dataTable.Rows object passed in.
            /// </summary>
            /// <param name='dataTable'>The 'DataTable.Rows' to load from.</param>
            /// <returns>A Image Collection.</returns>
            public static List<Image> LoadCollection(DataTable dataTable)
            {
                // Initial Value
                List<Image> images = new List<Image>();

                try
                {
                    // Load Each row In DataTable
                    foreach (DataRow row in dataTable.Rows)
                    {
                        // Create 'Image' from rows
                        Image image = Load(row);

                        // Add this object to collection
                        images.Add(image);
                    }
                }
                catch
                {
                }

                // return value
                return images;
            }
            #endregion

        #endregion

    }
    #endregion

}
