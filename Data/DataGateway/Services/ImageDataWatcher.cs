
#region using statements

using DataJuggler.Net.Core.Enumerations;
using DataJuggler.UltimateHelper.Core;
using ObjectLibrary.BusinessObjects;
using System.Collections.Generic;

#endregion

namespace DataGateway.Services
{

    #region class ImageDataWatcher
    /// <summary>
    /// This class is used to hold a delegate so when changes occur in a 
    /// Image item, the delegate is notified so the values are saved.
    /// </summary>
    public class ImageDataWatcher
    {

        #region Methods

            #region ItemChanged(object itemChanged, ListChangeTypeEnum listChangeType)
            /// <summary>
            /// This method Item Changed
            /// </summary>
            public async void ItemChanged(object itemChanged, ChangeTypeEnum listChangeType)
            {
                // cast the item as a ToDo object
                Image image = itemChanged as Image;

                // If the image object exists
                if (NullHelper.Exists(image))
                {
                    // perform the saved
                    bool saved = await ImageService.SaveImage(ref image);
                }
            }
            #endregion

            #region Watch(List<Image> image)
            /// <summary>
            /// This method watches the current list by setting a delegate for each item.
            /// </summary>
            /// <param name="images">The list of Image objects to set a delegate on.</param>
            public void Watch(List<Image> images)
            {
                // If the images collection exists and has one or more items
                if (ListHelper.HasOneOrMoreItems(images))
                {
                    // Iterate the collection of Image objects
                    foreach (Image image in images)
                    {
                        // Setup the Callback for each item
                       image.Callback = ItemChanged;
                    }
                }
            }
            #endregion

        #endregion

    }
    #endregion

}
