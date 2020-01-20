

#region using statements

using ObjectLibrary.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;

#endregion


namespace DataAccessComponent.DataManager.Readers
{

    #region class ArtistReader
    /// <summary>
    /// This class loads a single 'Artist' object or a list of type <Artist>.
    /// </summary>
    public class ArtistReader
    {

        #region Static Methods

            #region Load(DataRow dataRow)
            /// <summary>
            /// This method loads a 'Artist' object
            /// from the dataRow passed in.
            /// </summary>
            /// <param name='dataRow'>The 'DataRow' to load from.</param>
            /// <returns>A 'Artist' DataObject.</returns>
            public static Artist Load(DataRow dataRow)
            {
                // Initial Value
                Artist artist = new Artist();

                // Create field Integers
                int activefield = 0;
                int createdDatefield = 1;
                int emailAddressfield = 2;
                int folderPathfield = 3;
                int idfield = 4;
                int imagesCountfield = 5;
                int isAdminfield = 6;
                int lastUpdatedfield = 7;
                int namefield = 8;
                int passwordHashfield = 9;
                int profilePicturefield = 10;

                try
                {
                    // Load Each field
                    artist.Active = DataHelper.ParseBoolean(dataRow.ItemArray[activefield], false);
                    artist.CreatedDate = DataHelper.ParseDate(dataRow.ItemArray[createdDatefield]);
                    artist.EmailAddress = DataHelper.ParseString(dataRow.ItemArray[emailAddressfield]);
                    artist.FolderPath = DataHelper.ParseString(dataRow.ItemArray[folderPathfield]);
                    artist.UpdateIdentity(DataHelper.ParseInteger(dataRow.ItemArray[idfield], 0));
                    artist.ImagesCount = DataHelper.ParseInteger(dataRow.ItemArray[imagesCountfield], 0);
                    artist.IsAdmin = DataHelper.ParseBoolean(dataRow.ItemArray[isAdminfield], false);
                    artist.LastUpdated = DataHelper.ParseDate(dataRow.ItemArray[lastUpdatedfield]);
                    artist.Name = DataHelper.ParseString(dataRow.ItemArray[namefield]);
                    artist.PasswordHash = DataHelper.ParseString(dataRow.ItemArray[passwordHashfield]);
                    artist.ProfilePicture = DataHelper.ParseString(dataRow.ItemArray[profilePicturefield]);
                }
                catch
                {
                }

                // return value
                return artist;
            }
            #endregion

            #region LoadCollection(DataTable dataTable)
            /// <summary>
            /// This method loads a collection of 'Artist' objects.
            /// from the dataTable.Rows object passed in.
            /// </summary>
            /// <param name='dataTable'>The 'DataTable.Rows' to load from.</param>
            /// <returns>A Artist Collection.</returns>
            public static List<Artist> LoadCollection(DataTable dataTable)
            {
                // Initial Value
                List<Artist> artists = new List<Artist>();

                try
                {
                    // Load Each row In DataTable
                    foreach (DataRow row in dataTable.Rows)
                    {
                        // Create 'Artist' from rows
                        Artist artist = Load(row);

                        // Add this object to collection
                        artists.Add(artist);
                    }
                }
                catch
                {
                }

                // return value
                return artists;
            }
            #endregion

        #endregion

    }
    #endregion

}
