using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
public class LinkUser : MonoBehaviour {
	public InputField EmailText, PasswordText;
	public GameObject Error,Succeed;
	public FBholder FBGo;
	public Text UsernameA,UsernameB,Email;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void LUser(int which){
		
			StartCoroutine (LinkEmail (which));

	}
	private IEnumerator LinkEmail(int which)
	{
		string url = Link.url + "LinkEmail";
		WWWForm form = new WWWForm();
		form.AddField ("ID", PlayerPrefs.GetString (Link.ID));
		form.AddField ("fbstats", which);
		if (which == 1) {
			form.AddField ("Email", EmailText.text);
			form.AddField ("Password", PasswordText.text);
		} else {
			form.AddField ("Email", FBGo.id + "@fb.com");
			form.AddField ("Password",  FBGo.id + "Fb");
			form.AddField ("Username",  FBGo.name);


		}
		WWW www = new WWW(url, form);
		yield return www;
		if (www.error == null)
		{
			var jsonString = JSON.Parse(www.text);
			Debug.Log(jsonString);
			PlayerPrefs.SetString(Link.FOR_CONVERTING, jsonString["code"]);
			if (PlayerPrefs.GetString(Link.FOR_CONVERTING) == "1")
			{
				PlayerPrefs.SetString ("GMode","0");
				if (which == 1) {
					PlayerPrefs.SetString (Link.EMAIL, EmailText.text);
					PlayerPrefs.SetString (Link.PASSWORD, PasswordText.text);
					Email.text = EmailText.text;
				} 
				else {
					Email.text = EmailText.text;
					UsernameA.text = FBGo.name;
					UsernameB.text = FBGo.name;
					PlayerPrefs.SetString (Link.EMAIL,FBGo.id + "@fb.com");
					PlayerPrefs.SetString(Link.PASSWORD, FBGo.id + "Fb");
					PlayerPrefs.SetString("Base64PictureProfile", PlayerPrefs.GetString("Base64PictureProfileFB"));

					PlayerPrefs.SetString (Link.LOGIN_BY, "FB");
				}
				Debug.Log (www.text);
				Succeed.SetActive (true);
			}
			else if (PlayerPrefs.GetString(Link.FOR_CONVERTING) == "4")
			{
				Error.SetActive(true);
				Error.transform.FindChild("ErrorText").GetComponent<Text>().text="Email Already Used";
			}
			else if (PlayerPrefs.GetString(Link.FOR_CONVERTING) == "5")
			{
				Error.SetActive(true);
				Error.transform.FindChild("ErrorText").GetComponent<Text>().text="Insert Your Email Correctly";
			}
		}
		else {
			Error.SetActive(true);
			Error.transform.FindChild("ErrorText").GetComponent<Text>().text="Submit Failed";
			Debug.Log (www.text);
		}

	}
}
