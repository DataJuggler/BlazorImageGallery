

#region using statements

using ApplicationLogicComponent.Connection;
using DataJuggler.UltimateHelper.Core;
using DataGateway;
using ObjectLibrary.BusinessObjects;
using System.Collections.Generic;
using System.Threading.Tasks;
using ObjectLibrary.Models;
using DataJuggler.Core.Cryptography;
using System.Linq;

#endregion

namespace DataGateway.Services
{

    #region class ArtistService
    /// <summary>
    /// This is the Service class for managing Artist objects.
    /// </summary>
    public class ArtistService
    {

        #region Methods
            
            #region GetArtistList()
            /// <summary>
            /// This method is used to load the Site 
            /// </summary>
            /// <returns></returns>
            public static Task<List<Artist>> GetArtistList()
            {
                // initial value
                List<Artist> list = null;
                
                // Create a new instance of a 'Gateway' object, and set the connectionName
                Gateway gateway = new Gateway(Connection.Name);
                
                // load the sites
                list = gateway.LoadArtists();

                // If the list collection exists and has two or more items, else there is nothing to sort
                if (ListHelper.HasXOrMoreItems(list, 2))
                {
                    // order the list by name
                    list = list.OrderBy(x => x.Name).ToList();
                }
                
                // return the list
                return Task.FromResult(list);
            }
            #endregion

            #region Login(string emailAddress, string password, bool passwordIsHashed = false)
            /// <summary>
            /// This method is used to login to the Site 
            /// </summary>
            /// <returns></returns>
            public static Task<LoginResponse> Login(string emailAddress, string password, bool passwordIsHashed = false)
            {
                // initial value
                LoginResponse loginResponse = new LoginResponse();

                // local
                Artist artist = null;

                // If the emailAddress string exists
                if (TextHelper.Exists(emailAddress))
                {
                    // Create a new instance of a 'Gateway' object, and set the connectionName
                    Gateway gateway = new Gateway(Connection.Name);
                
                    // load the sites
                    artist = gateway.FindArtistByEmailAddress(emailAddress);

                    // if the player exists
                    if (NullHelper.Exists(artist))
                    {
                        // get the key
                        string key = EnvironmentVariableHelper.GetEnvironmentVariableValue("BlazorImageGallery");

                        // if the key was found
                        if (TextHelper.Exists(key))
                        {
                            // can this player be verified
                            bool isVerified = CryptographyHelper.VerifyHash(password, key, artist.PasswordHash, passwordIsHashed);
                            
                            // if the password hashes match
                            if (isVerified)
                            {
                                // The user did login
                                loginResponse.Success = true;

                                // Set the Player
                                loginResponse.Artist = artist;
                            }
                            else
                            {
                                // Set the message
                                loginResponse.Message = "The passwords do not match.";
                            }
                        }
                        else
                        {
                            // Set the message
                            loginResponse.Message = "The Environment BlazorImageGallery Key was not found.";
                        }
                    }
                }
                
                // return the list
                return Task.FromResult(loginResponse);
            }
            #endregion
            
            #region RemoveArtist(Artist artist)
            /// <summary>
            /// This method is used to delete a Artist
            /// </summary>
            /// <returns></returns>
            public static Task<bool> RemoveArtist(Artist artist)
            {
                // initial value
                bool deleted = false;
                
                // if the artist object exists
                if (NullHelper.Exists(artist))
                {
                    // Create a new instance of a 'Gateway' object, and set the connectionName
                    Gateway gateway = new Gateway(Connection.Name);
                    
                    // load the sites
                    deleted = gateway.DeleteArtist(artist.Id);
                }
                
                // return the value of deleted
                return Task.FromResult(deleted);
            }
        #endregion

            #region SaveArtist(ref Artist artist)
            /// <summary>
            /// This method is used to create Artist objects
            /// </summary>
            /// <param name="artist">Pass in an object of type Artist to save</param>
            /// <returns></returns>
            public static Task<bool> SaveArtist(ref Artist artist)
            {
                // initial value
                bool saved = false;
                
                // Create a new instance of a 'Gateway' object, and set the connectionName
                Gateway gateway = new Gateway(Connection.Name);
                
                // load the sites
                saved = gateway.SaveArtist(ref artist);
                
                // return the value of saved
                return Task.FromResult(saved);
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
