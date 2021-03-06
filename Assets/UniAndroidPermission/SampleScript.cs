﻿using UnityEngine;
using UnityEngine.UI;

public class SampleScript : MonoBehaviour
{

    [SerializeField]
    Text text;
    [SerializeField]
    GameObject GO;
    [SerializeField]
    Home Home;
    [SerializeField]
    lifeless Home2;
    public void RequestPermission()
    {
        if (UniAndroidPermission.IsPermitted(AndroidPermission.ACCESS_FINE_LOCATION))
        {
           // text.text = "WRITE_EXTERNAL_STORAGE is already permitted!!";
            if (UniAndroidPermission.IsPermitted(AndroidPermission.CAMERA))
            {
                Home2.pindah();
                Home.OnHuntingClick();
               
               // text.text = "CAMERA is already permitted!!";
                return;
            }
            UniAndroidPermission.RequestPermission(AndroidPermission.CAMERA, OnAllow2, OnDeny2, OnDenyAndNeverAskAgain2);
            return;
        }

        UniAndroidPermission.RequestPermission(AndroidPermission.ACCESS_FINE_LOCATION, OnAllow, OnDeny, OnDenyAndNeverAskAgain);
    }

    private void OnAllow()
    {
       // text.text = "WRITE_EXTERNAL_STORAGE is permitted NOW!!";
        UniAndroidPermission.RequestPermission(AndroidPermission.CAMERA, OnAllow2, OnDeny2, OnDenyAndNeverAskAgain2);
    }

    private void OnDeny()
    {
        GO.SetActive(true);
        text.text = "WARNING\n\nYou can't use this mode yet, enable your Location permission.";
    }

    private void OnDenyAndNeverAskAgain()
    {
        GO.SetActive(true);
        text.text = "WARNING\n\nYou can't use this mode yet, enable your Location permission.";
    }
    private void OnAllow2()
    {
        Home2.pindah();
        Home.OnHuntingClick();
        
        // text.text = "CAMERA is permitted NOW!!";
    }

    private void OnDeny2()
    {
        GO.SetActive(true);
        text.text = "WARNING\n\nYou can't use this mode yet, enable your Camera permission.";
    }

    private void OnDenyAndNeverAskAgain2()
    {
        GO.SetActive(true);
        text.text = "WARNING\n\nYou can't use this mode yet, enable your Camera permission.";
    }
}
