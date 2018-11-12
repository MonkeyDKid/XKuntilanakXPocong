using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;


public class GetOut : MonoBehaviour
{
    public GameObject ValidationError, vanny;
    private IEnumerator LoginOut()
    {
        string url = Link.url + "logout";
        WWWForm form = new WWWForm();
        form.AddField(Link.DEVICE_ID, PlayerPrefs.GetString(Link.DEVICE_ID));
        form.AddField(Link.EMAIL, PlayerPrefs.GetString(Link.EMAIL));
        form.AddField(Link.PASSWORD, PlayerPrefs.GetString(Link.PASSWORD));

        WWW www = new WWW(url, form);
        yield return www;
        if (www.error == null)
        {
            var jsonString = JSON.Parse(www.text);
            PlayerPrefs.DeleteAll();
            //PlayerPrefs.DeleteKey(Link.STATUS_LOGIN);
            //PlayerPrefs.DeleteKey("SummonTutor");
            Application.LoadLevel(Link.Register);
            //PlayerPrefs.DeleteKey("tutorhitung");
            //PlayerPrefs.DeleteKey(Link.LOGIN_BY);
        }
    }

    public void LogOut()
    {
        // PlayerPrefs.DeleteAll();
        StartCoroutine(LoginOut());
    }
    IEnumerator checkID()
    {
        string url = Link.url + "checkDID";
        WWWForm form = new WWWForm();
        form.AddField(Link.ID, PlayerPrefs.GetString(Link.ID));
        form.AddField("DID", PlayerPrefs.GetString(Link.DEVICE_ID));
        WWW www = new WWW(url, form);
        yield return www;
        if (www.error == null)
        {
            var jsonString = JSON.Parse(www.text);
            Debug.Log(www.text);
            PlayerPrefs.SetString(Link.FOR_CONVERTING, jsonString["code"]);
            if (PlayerPrefs.GetString(Link.FOR_CONVERTING) == "33")
            {
                if (vanny != null) {
                    vanny.SetActive(false);
                    ValidationError.SetActive(true);
                }
                else
                {
                    ValidationError.SetActive(true);
                }
               
            }
            else
            {
                Debug.Log("Nothing Happen");
            }
        }
    }
}
