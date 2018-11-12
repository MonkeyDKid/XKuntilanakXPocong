using UnityEngine;
using System;
using UnityEngine.UI;
using SimpleJSON;
using System.Collections;

public class Home : MonoBehaviour
{
    [Serializable]
    public class ShopMall
    {
        public GameObject Shop;
        public GameObject ShopButton, CreditImage, ImageSelected;
    }
    public GameObject quitter, FirstTimerGO, FirstTimerGO2, GOSHOP, Story, errorhandle,validationerror,RedeemCodeHandle;
    [Header("User Info")]
	public InputField RedeemCodeIF;
    public AudioClip[] MusicClip;
    public AudioSource Music;
    public Text Energy, EnergyShop;
    public Text Coin, CoinShop;
    public Text CrystalShop;
    public Text SoulStone, levelP, exptext;
    public Text userName, settingusername, settingemail, settingWuser, settingWpas, settingUID;
    public GameObject  SoundBG, aniON, aniOFF, aniON2, aniOFF2;
    public Slider MusicSlide, SoundFXSlide;
    public Image levelbar, Profile;
    public ShopMall[] ShopMarket;
    bool Onshop;
    public GameObject Camera, test;
    void Start()
    {
       
		if (PlayerPrefs.GetString("Base64PictureProfile") != "")
        {
            byte[] b64_bytes = System.Convert.FromBase64String(PlayerPrefs.GetString("Base64PictureProfile"));
            Texture2D tex = new Texture2D(1, 1);
            tex.LoadImage(b64_bytes);

            var rect = new Rect(0, 0, tex.width, tex.height);

            if (tex.height != 8)
            {
                Profile.overrideSprite = Sprite.Create(tex, rect, Vector2.zero, 128.0f);
            }
            else {
                //anotherpopup.SetActive (true);
            }
        }

       StartCoroutine(CheckConnectionToMasterServer());

      
        if (PlayerPrefs.HasKey("NoAni"))
        {
            if (PlayerPrefs.GetString("NoAni") == "ON")
            {
                aniOFF.SetActive(false);
                aniOFF2.SetActive(false);
                aniON.SetActive(true);
                aniON2.SetActive(true);
            }
            else {
                aniOFF.SetActive(true);
                aniOFF2.SetActive(true);
                aniON.SetActive(false);
                aniON2.SetActive(false);
            }


        }
        else
        {
            PlayerPrefs.SetString("NoAni", "ON");
        }
        Debug.Log(PlayerPrefs.GetString("PLAY_TUTORIAL"));
        PlayerPrefs.SetString("berburu", "tidak");
        FirstTimerGO.SetActive(false);
        FirstTimerGO2.SetActive(false);
        MusicSlide.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        SoundFXSlide.onValueChanged.AddListener(delegate { ValueChangeCheck2(); });
        if (PlayerPrefs.HasKey("Music")&& PlayerPrefs.HasKey("SoundsFX"))
        {
            MusicSlide.value = PlayerPrefs.GetFloat("Music");
            SoundFXSlide.value = PlayerPrefs.GetFloat("SoundsFX");
        }
        else {
            PlayerPrefs.SetFloat("Music", 1.0f);
            PlayerPrefs.SetFloat("SoundsFX", 1.0f);
            SoundFXSlide.value = 1;
            MusicSlide.value = 1;
        }
        StartCoroutine(GetDataUser());

        //PlayerPrefs.DeleteAll ();
        //		PlayerPrefs.SetInt ("FT", 0);
        //	PlayerPrefs.SetString ("PLAY_TUTORIAL", "TRUE");

      

        if (PlayerPrefs.GetString("GoShop") == "yes")
        {
            GOSHOP.SetActive(true);
            StartCoroutine(GetDataUser());
            PlayerPrefs.SetString("GoShop", "no");
        }


        if (PlayerPrefs.HasKey("PLAY_TUTORIAL"))
        {

            if (PlayerPrefs.GetString("PLAY_TUTORIAL") == "FALSE")
            {
                FirstTimerGO.SetActive(false);
                Story.SetActive(false);
                if (PlayerPrefs.GetString("lewat") == "ya")
                {
                    FirstTimerGO2.SetActive(true);
                }
            }
            else {
                //	Story.SetActive(true);
                //FirstTimerGO.SetActive(true);
				Camera.GetComponent<Animator>().Play("HomeCameraAnimation");
            }

            if (PlayerPrefs.GetString("SummonTutor") == "UDAH")
			{if (PlayerPrefs.GetString ("PLAY_TUTORIAL") == "TRUE") {
					SceneManagerHelper.LoadTutorial ("Home_2");
				}
                else{SceneManagerHelper.LoadTutorial ("Home_3");}
                Camera.GetComponent<Animator>().Play("HomeCameraAnimation");
                Story.SetActive(false);
            }
           else if (PlayerPrefs.GetString("PLAY_TUTORIAL") == "TRUE")
            {

                StartCoroutine(WaitTime());
            }
        }
        //        }
        //        else {
        //          FirstTimerGO.SetActive(false);
        //			Story.SetActive(false);
        //        }
        //		if (PlayerPrefs.GetString("PLAY_TUTORIAL") == "FALSE")
        //		{
        //			FirstTimerGO.SetActive(false);
        //			Story.SetActive(false);
        //			if (PlayerPrefs.GetString ("lewat") == "ya") {
        //				FirstTimerGO2.SetActive(true);
        //			}
        //		}

    }
	public void StartDialog(string name)
	{
		SceneManagerHelper.LoadTutorial (name);
	}

        public void LoadScene(string name)
    {
        SceneManagerHelper.LoadScene (name);
    }
    public void PlayMusic(int whichMusic)
    {
        Music.clip = MusicClip[whichMusic];
		Music.GetComponent<AudioSource> ().Play ();
    }
    public void inetcheck()
    {
        StartCoroutine(CheckConnectionToMasterServer());
    }

    public void Non()
    {

        if (PlayerPrefs.HasKey("NoAni"))
        {
            if (PlayerPrefs.GetString("NoAni") == "ON")
            {
                aniOFF.SetActive(false);
                aniOFF2.SetActive(false);
                aniON.SetActive(true);
                aniON2.SetActive(true);
            }
            else {
                aniOFF.SetActive(true);
                aniOFF2.SetActive(true);
                aniON.SetActive(false);
                aniON2.SetActive(false);
            }


        }
    }

    public void AniONm()
    {
        PlayerPrefs.SetString("NoAni", "ON");
    }
    public void AniOFFm()
    {
        PlayerPrefs.SetString("NoAni", "OFF");
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        PlayerPrefs.SetFloat("Music", MusicSlide.value);
        SoundBG.GetComponent<AudioSource>().volume = MusicSlide.value;
    }
    public void ValueChangeCheck2()
    {
        PlayerPrefs.SetFloat("SoundsFX", SoundFXSlide.value);
       // SoundBG.GetComponent<AudioSource>().volume = MusicSlide.value;
    }

    public void GDUShop()
    {
        StartCoroutine(GetDataUser());
    }



    private IEnumerator WaitTime()
    {
        Story.SetActive(true);
        Story.transform.FindChild("0").gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        Story.transform.FindChild("0").gameObject.SetActive(false);
        Story.transform.FindChild("1").gameObject.SetActive(true);

    }

    private IEnumerator GetDataUser()
    {
        string url = Link.url + "getDataUser";
        WWWForm form = new WWWForm();
        form.AddField(Link.ID, PlayerPrefs.GetString(Link.ID));
		form.AddField("DID", PlayerPrefs.GetString(Link.DEVICE_ID));
        WWW www = new WWW(url, form);
        yield return www;
        if (www.error == null)
        {
            var jsonString = JSON.Parse(www.text);
            Debug.Log(jsonString);
            PlayerPrefs.SetString(Link.FOR_CONVERTING, jsonString["code"]);
            if (PlayerPrefs.GetString(Link.FOR_CONVERTING) == "0")
            {
				PlayerPrefs.SetString ("GMode",jsonString["data"]["GMode"]) ;
                Coin.text = jsonString["data"]["coin"];
                CoinShop.text = jsonString["data"]["coin"];
                CrystalShop.text = jsonString["data"]["crystal"];
                SoulStone.text = jsonString["data"]["soulstone"];
                //levelP.text = "LV "+jsonString["data"]["PlayerLevel"];
                userName.text = jsonString["data"]["user_name"];
                settingusername.text = jsonString["data"]["user_name"];
                settingemail.text = jsonString["data"]["email"];
                settingWpas.text =  "Web Pass			: "+jsonString["Wpass"];
                settingWuser.text = "Web Username	: "+jsonString["Wuser"];
                settingUID.text =    "Id : "+jsonString["data"]["UniqueID"];
                PlayerPrefs.SetString("curexp", jsonString["data"]["xpp"]);
                PlayerPrefs.SetString("tarexp", jsonString["data"]["targetexplevel"]);
                PlayerPrefs.SetString(Link.AR, jsonString["data"]["ar"]);
                PlayerPrefs.SetString("PlayerLevel", jsonString["data"]["PlayerLevel"]);
                var energy = int.Parse(jsonString["data"]["energy"]) + int.Parse(jsonString["data"]["BonusEnergy"]);
                PlayerPrefs.SetString("BonusEnergy", jsonString["data"]["BonusEnergy"]);
                PlayerPrefs.SetString("EnergyCombo", energy.ToString());
				Energy.text = energy.ToString();
                EnergyShop.text = energy.ToString();
                PlayerPrefs.SetString(Link.ENERGY, jsonString["data"]["energy"]);
                PlayerPrefs.SetString(Link.GOLD, jsonString["data"]["coin"]);
                PlayerPrefs.SetString("Crystal", jsonString["data"]["crystal"]);
                PlayerPrefs.SetString(Link.SOUL_STONE, jsonString["data"]["soulstone"]);
                exptext.text = jsonString["data"]["xpp"] + "/" + jsonString["data"]["targetexplevel"];
                PlayerPrefs.SetString("MaxE", jsonString["data"]["MaxEnergy"]);
                PlayerPrefs.SetString(Link.IBURU, jsonString["data"]["iklan_buru"]);
                PlayerPrefs.SetString(Link.IGOLD, jsonString["data"]["iklan_gold"]);
                PlayerPrefs.SetString(Link.IENERGY, jsonString["data"]["iklan_energy"]);
                PlayerPrefs.SetString(Link.COMMON, jsonString["data"]["common"]);
                PlayerPrefs.SetString(Link.RARE, jsonString["data"]["rare"]);
                PlayerPrefs.SetString(Link.LEGENDARY, jsonString["data"]["legendary"]);
                yield return new WaitForSeconds(1);
                datauser();
				GetComponent<lifeless> ().updatedata ();
            }
            else if (PlayerPrefs.GetString(Link.FOR_CONVERTING) == "33")
            {
                validationerror.SetActive(true);
            }
        }
        else {
			errorhandle.SetActive(true);
		//	errorhandle.transform.FindChild("Dialog").gameObject.SetActive(true);
            Debug.Log("GAGAL");
			print (www.text);
        }

    }
    private void datauser()
    {
		Energy.text = PlayerPrefs.GetString("EnergyCombo");
        EnergyShop.text = PlayerPrefs.GetString("EnergyCombo");
        Coin.text = PlayerPrefs.GetString(Link.GOLD);
        CoinShop.text = PlayerPrefs.GetString(Link.GOLD);
        CrystalShop.text = PlayerPrefs.GetString("Crystal");
        SoulStone.text = PlayerPrefs.GetString("Crystal");
        levelP.text = "LV " + PlayerPrefs.GetString("PlayerLevel");
        userName.text = PlayerPrefs.GetString(Link.USER_NAME);
        settingusername.text = PlayerPrefs.GetString(Link.USER_NAME);
        settingemail.text = PlayerPrefs.GetString(Link.EMAIL);
        if (float.Parse(PlayerPrefs.GetString("tarexp")) >= 999999)
        {
            levelbar.fillAmount = 1;
            exptext.text = "MAX";
        }
        else {
            levelbar.fillAmount = float.Parse(PlayerPrefs.GetString("curexp")) / float.Parse(PlayerPrefs.GetString("tarexp"));
        }
    }
    void Update()
    {          
       
        if (Input.GetKey(KeyCode.Escape) && Onshop)
        {
           
           GOSHOP.SetActive(false);
           outshop();
        }

        
        
       
      



    }
    public void outshop()
    {
         Onshop = false;
    }
    public void OnShop(int shopnumber)
    {
        Onshop = true;
        GOSHOP.SetActive(true);
        if(shopnumber==0)
        {
            ShopMarket[0].Shop.SetActive(true);
            ShopMarket[0].ShopButton.SetActive(true);
            ShopMarket[0].CreditImage.SetActive(true);
            ShopMarket[0].ImageSelected.SetActive(true);
            GOSHOP.GetComponent<Shop>().SpecialShopCek();
             for(int i=1;i<3;i++)
        {
            ShopMarket[i].Shop.SetActive(false);
            ShopMarket[i].ShopButton.SetActive(false);
            ShopMarket[i].CreditImage.SetActive(false);
            ShopMarket[i].ImageSelected.SetActive(false);
        }
        }
        else if(shopnumber==33)
        {
            ShopMarket[0].Shop.SetActive(true);
            ShopMarket[0].ShopButton.SetActive(true);
            ShopMarket[0].CreditImage.SetActive(true);
            ShopMarket[0].ImageSelected.SetActive(true);
            GOSHOP.GetComponent<Shop>().SpecialShopCek();
        }
         
        else
        {
         
          
            if(shopnumber==1)
            {
            ShopMarket[1].Shop.SetActive(true);
            ShopMarket[1].ShopButton.SetActive(true);
            ShopMarket[1].CreditImage.SetActive(true);
            ShopMarket[1].ImageSelected.SetActive(true);

            ShopMarket[2].ImageSelected.SetActive(false);
            ShopMarket[2].ShopButton.SetActive(true);

            ShopMarket[0].Shop.SetActive(false);
            ShopMarket[0].ShopButton.SetActive(false);
            ShopMarket[0].CreditImage.SetActive(false);
            ShopMarket[0].ImageSelected.SetActive(false);
            }
               GOSHOP.GetComponent<Shop>().CrystalShopCek();
        }
       
        
           
        

    }
    private IEnumerator CheckConnectionToMasterServer()
	{ 		string url = Link.url + "CheckInternet";
			WWWForm form = new WWWForm();
			form.AddField(Link.ID, PlayerPrefs.GetString(Link.ID));
			WWW www = new WWW(url,form);
			yield return www;
		if (www.error == null) {
			
			print (www.text);

				errorhandle.SetActive (false);
			 

		}

        else
        {
			print (www.text);
			print ("fafal niam");
            errorhandle.SetActive(true);
           errorhandle.transform.FindChild("Dialog").gameObject.SetActive(true);
        }
    }
	public void RedeemCodeP(){
		StartCoroutine(RedeemCode());
	}
	
	  private IEnumerator RedeemCode()
	{ 		string url = Link.url + "RedeemPlayerCode";
			WWWForm form = new WWWForm();
			form.AddField("PID", PlayerPrefs.GetString(Link.ID));
			form.AddField("Code", RedeemCodeIF.text);
			WWW www = new WWW(url,form);
			yield return www;
		if (www.error == null) {
			
			print (www.text);
			var jsonString = JSON.Parse(www.text);
            Debug.Log(jsonString);
            if(int.Parse(jsonString["code"])==1)
			{
				RedeemCodeHandle.SetActive(true);
				RedeemCodeHandle.transform.FindChild ("Text").GetComponent<Text> ().text = "Redeem code success,\n Check your inbox.";
					RedeemCodeIF.text="";
			}
			else if(int.Parse(jsonString["code"])==22){
				RedeemCodeHandle.SetActive(true);
				RedeemCodeHandle.transform.FindChild ("Text").GetComponent<Text> ().text = "Check the code.";
								
			}
			else{
				RedeemCodeHandle.SetActive(true);
				RedeemCodeHandle.transform.FindChild ("Text").GetComponent<Text> ().text = " The code already redeemed by this user";
				RedeemCodeIF.text="";
			}
			
				errorhandle.SetActive (false);
			 

		}

        else
        {
			print (www.text);
			print ("fafal niam");
            errorhandle.SetActive(true);
           errorhandle.transform.FindChild("Dialog").gameObject.SetActive(true);
        }
    }
	
	
	
	
    public void OnStorageClick()
    {
        SceneManagerHelper.LoadScene(Link.Storage);
    }

    public void OnFusionClick()
    {
       SceneManagerHelper.LoadScene(Link.Fusion);
    }

    public void OnSummonnClick()
    {
        SceneManagerHelper.LoadScene(Link.Summon);
    }

    public void OnBattleClick()
    {
        SceneManagerHelper.LoadScene(Link.Map);
    }

    public void OnPracticeClick()
    {
        SceneManagerHelper.LoadScene(Link.Practice);
    }
    public void OnHuntingClick()
    {
        SceneManagerHelper.LoadScene("berburuhantu");
    }
    public void quit()
    {
        Application.Quit();
    }
    public void EndFirstimer()
    {
        PlayerPrefs.SetString("TutorialMission", "TRUE");
        StartCoroutine(Finish());
    }

    public void LogOut()
    {
        // PlayerPrefs.DeleteAll();
        StartCoroutine(LoginOut());
    }

    private IEnumerator Finish()
    {
        string url = Link.url + "tutorialfinish";
        WWWForm form = new WWWForm();
        form.AddField(Link.DEVICE_ID, PlayerPrefs.GetString(Link.DEVICE_ID));
        form.AddField(Link.EMAIL, PlayerPrefs.GetString(Link.EMAIL));
        form.AddField(Link.PASSWORD, PlayerPrefs.GetString(Link.PASSWORD));

        WWW www = new WWW(url, form);
        yield return www;
        if (www.error == null)
        {
            var jsonString = JSON.Parse(www.text);
            PlayerPrefs.SetInt("FT", 1);
            PlayerPrefs.SetString("lewat", "tidak");
            PlayerPrefs.SetString("Tutorgame", "FALSE");
            PlayerPrefs.DeleteKey("PLAY_TUTORIAL");
            PlayerPrefs.DeleteKey("SummonTutor");
        }
    }
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
           SceneManagerHelper.LoadScene(Link.Register);
            //PlayerPrefs.DeleteKey("tutorhitung");
            //PlayerPrefs.DeleteKey(Link.LOGIN_BY);
        }
    }
}
