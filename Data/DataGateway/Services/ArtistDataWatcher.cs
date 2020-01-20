
#region using statements

using DataJuggler.Net.Core.Enumerations;
using DataJuggler.UltimateHelper.Core;
using ObjectLibrary.BusinessObjects;
using System.Collections.Generic;

#endregion

namespace DataGateway.Services
{

    #region class ArtistDataWatcher
    /// <summary>
    /// This class is used to hold a delegate so when changes occur in a 
    /// Artist item, the delegate is notified so the values are saved.
    /// </summary>
    public class ArtistDataWatcher
    {

        #region Methods

            #region ItemChanged(object itemChanged, ListChangeTypeEnum listChangeType)
            /// <summary>
            /// This method Item Changed
            /// </summary>
            public async void ItemChanged(object itemChanged, ChangeTypeEnum listChangeType)
            {
                // cast the item as a ToDo object
                Artist artist = itemChanged as Artist;

                // If the artist object exists
                if (NullHelper.Exists(artist))
                {
                    // perform the saved
                    bool saved = await ArtistService.SaveArtist(ref artist);
                }
            }
            #endregion

            #region Watch(List<Artist> artist)
            /// <summary>
            /// This method watches the current list by setting a delegate for each item.
            /// </summary>
            /// <param name="artists">The list of Artist objects to set a delegate on.</param>
            public void Watch(List<Artist> artists)
            {
                // If the artists collection exists and has one or more items
                if (ListHelper.HasOneOrMoreItems(artists))
                {
                    // Iterate the collection of Artist objects
                    foreach (Artist artist in artists)
                    {
                        // Setup the Callback for each item
                       artist.Callback = ItemChanged;
                    }
                }
            }
            #endregion

        #endregion

    }
    #endregion

}
