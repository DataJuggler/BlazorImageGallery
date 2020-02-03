

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObjectLibrary.Models;

#endregion

namespace BlazorImageGallery.Models
{

    #region delegate SignUpFinishedCallback(LoginResponse loginResponse)
    /// <summary>
    /// This is the delegate to call after the login is complete
    /// </summary>
    /// <returns></returns>
    public delegate void SignUpFinishedCallback(LoginResponse loginResponse);
    #endregion

    #region class SignUpModel
    /// <summary>
    /// This class is here to be able to sign up in another thread.
    /// </summary>
    public class SignUpModel
    {
        
        #region Private Variables
        private string password;
        private string emailAddress;
        private string displayName;
        private string profilePictureUrl;
        private SignUpFinishedCallback signUpFinishedCallback;
        #endregion

        #region Properties

            #region DisplayName
            /// <summary>
            /// This property gets or sets the value for 'DisplayName'.
            /// </summary>
            public string DisplayName
            {
                get { return displayName; }
                set { displayName = value; }
            }
            #endregion
            
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
            
            #region HasSignUpFinishedCallback
            /// <summary>
            /// This property returns true if this object has a 'SignUpFinishedCallback'.
            /// </summary>
            public bool HasSignUpFinishedCallback
            {
                get
                {
                    // initial value
                    bool hasSignUpFinishedCallback = (this.SignUpFinishedCallback != null);
                    
                    // return value
                    return hasSignUpFinishedCallback;
                }
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
            
            #region ProfilePictureUrl
            /// <summary>
            /// This property gets or sets the value for 'ProfilePictureUrl'.
            /// </summary>
            public string ProfilePictureUrl
            {
                get { return profilePictureUrl; }
                set { profilePictureUrl = value; }
            }
            #endregion
            
            #region SignUpFinishedCallback
            /// <summary>
            /// This property gets or sets the value for 'SignUpFinishedCallback'.
            /// </summary>
            public SignUpFinishedCallback SignUpFinishedCallback
            {
                get { return signUpFinishedCallback; }
                set { signUpFinishedCallback = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
