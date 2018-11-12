/*==============================================================================
Copyright (c) 2012-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using System.Collections;

namespace Vuforia
{
    /// <summary>
    /// This Script can be used to set a full screen error message if an error happens on startup.
    /// (such as no OpenGL ES 2.0 support that is required for some samples).
    /// </summary>
    public class GLErrorHandler : MonoBehaviour
    {
        #region PRIVATE_MEMBER_VARIABLES

        private static string mErrorText = "";
        private static bool mErrorOccurred = false;

        private const string WINDOW_TITLE = "Sample Error";

        #endregion // PRIVATE_MEMBER_VARIABLES



        #region PUBLIC_METHODS

        /// <summary>
        /// Sets an error text that is rendered every frame
        /// </summary>
        public static void SetError(string errorText)
        {
            mErrorText = errorText;
            mErrorOccurred = true;
        }

        #endregion // PUBLIC_METHODS



        #region UNTIY_MONOBEHAVIOUR_METHODS

        // In this method we draw an error window in case something happened.
        void OnGUI()
        {
            // On error, create a full screen window.
            if (mErrorOccurred)
            {
                
            }
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PRIVATE_METHODS

        // This method draws an error-dialog on the screen.
        private void DrawWindowContent(int id)
        {
            // Create text area with a 10 pixel distance from other controls and
            // window border.
           
        }

        #endregion // PRIVATE_METHODS
    }
}