

#region using statements

using ApplicationLogicComponent.Connection;
using DataJuggler.UltimateHelper.Core;
using DataGateway;
using ObjectLibrary.BusinessObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace DataGateway.Services
{

    #region class ImageService
    /// <summary>
    /// This is the Service class for managing Image objects.
    /// </summary>
    public class ImageService
    {

        #region Methods
            
            #region GetImageList(int artistId)
            /// <summary>
            /// This method is used to load the Site 
            /// </summary>
            /// <returns></returns>
            public static Task<List<Image>> GetImageList(int artistId)
            {
                // initial value
                List<Image> list = null;
                
                // Create a new instance of a 'Gateway' object, and set the connectionName
                Gateway gateway = new Gateway(Connection.Name);
                
                // load the sites
                list = gateway.LoadImagesForOwnerId(artistId);

                
                
                // return the list
                return Task.FromResult(list);
            }
            #endregion
            
            #region RemoveImage(Image image)
            /// <summary>
            /// This method is used to delete a Image
            /// </summary>
            /// <returns></returns>
            public static Task<bool> RemoveImage(Image image)
            {
                // initial value
                bool deleted = false;
                
                // if the image object exists
                if (NullHelper.Exists(image))
                {
                    // Create a new instance of a 'Gateway' object, and set the connectionName
                    Gateway gateway = new Gateway(Connection.Name);
                    
                    // load the sites
                    deleted = gateway.DeleteImage(image.Id);
                }
                
                // return the value of deleted
                return Task.FromResult(deleted);
            }
        #endregion

            #region SaveImage(ref Image image)
            /// <summary>
            /// This method is used to create Image objects
            /// </summary>
            /// <param name="image">Pass in an object of type Image to save</param>
            /// <returns></returns>
            public static Task<bool> SaveImage(ref Image image)
            {
                // initial value
                bool saved = false;
                
                // Create a new instance of a 'Gateway' object, and set the connectionName
                Gateway gateway = new Gateway(Connection.Name);
                
                // load the sites
                saved = gateway.SaveImage(ref image);
                
                // return the value of saved
                return Task.FromResult(saved);
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
