

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorImageGallery;
using ObjectLibrary.Models;

#endregion

namespace BlazorImageGallery.Models
{

    #region delegate LoginFinishedCallback(LoginResponse loginResponse)
    /// <summary>
    /// This is the delegate to call after the login is complete
    /// </summary>
    /// <returns></returns>
    public delegate void LoginFinishedCallback(LoginResponse loginResponse);
    #endregion

    #region class LoginModel
    /// <summary>
    /// This class is here so one object can be passed to another thread containing the login information
    /// </summary>
    public class LoginModel
    {

        #region Private Variables
        private string password;
        private string emailAddress;
        private string storedPasswordHash;
        private LoginFinishedCallback onLoginComplete;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of a LoginModel object and set its properties
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="password"></param>
        /// <param name="storedPasswordHash"></param>
        /// <param name="onLoginFinished"></param>
        public LoginModel(string emailAddress, string password, string storedPasswordHash, LoginFinishedCallback onLoginComplete)
        {
            // store the args
            EmailAddress = emailAddress;
            Password = password;
            StoredPasswordHash = storedPasswordHash;
            OnLoginComplete = onLoginComplete;
        }
        #endregion

        #region Properties
        
            #region EmailAddress
            /// <summary>
            /// This property gets or sets the value for 'EmailAddress'.
            /// </summary>
            public string EmailAddress
            {
                get { return emailAddress; }
                set { emailAddress = value; }
            }
            #endregion
            
            #region OnLoginComplete
            /// <summary>
            /// This property gets or sets the value for 'OnLoginComplete'.
            /// </summary>
            public LoginFinishedCallback OnLoginComplete
            {
                get { return onLoginComplete; }
                set { onLoginComplete = value; }
            }
            #endregion
            
            #region Password
            /// <summary>
            /// This property gets or sets the value for 'Password'.
            /// </summary>
            public string Password
            {
                get { return password; }
                set { password = value; }
            }
            #endregion
            
            #region StoredPasswordHash
            /// <summary>
            /// This property gets or sets the value for 'StoredPasswordHash'.
            /// </summary>
            public string StoredPasswordHash
            {
                get { return storedPasswordHash; }
                set { storedPasswordHash = value; }
            }
            #endregion
            
        #endregion

    }
    #endregion

}
