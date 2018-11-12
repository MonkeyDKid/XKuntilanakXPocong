using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour {
	public Image GhostMap1, GhostMap2, GhostMap3;
	public Image GhostSelect1, GhostSelect2, GhostSelect3;
	public string LevelName;
	public Button StartButton, LevelButton;
	public Text LevelNameText;
	public int StageChoosen;
	// Use this for initialization
	void Start () {
		this.GetComponent<Button>().onClick.AddListener(OnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	 private void OnClick()
	{
		int mystage = int.Parse(PlayerPrefs.GetString(Link.Stage));
		if (PlayerPrefs.GetString ("PLAY_TUTORIAL") == "TRUE")
		{
			if(StageChoosen==99)
			{
				StartButton.interactable = true;
				StartButton.onClick = LevelButton.onClick;
			}
			else
			{
				StartButton.interactable = false;
			}
			GhostSelect1.sprite = GhostMap1.sprite;
			if(GhostMap2!=null)
			GhostSelect2.sprite = GhostMap2.sprite;
			if(GhostMap2!=null)
			GhostSelect3.sprite = GhostMap3.sprite;
			LevelNameText.text = LevelName;

		}

		else
		{
		if(mystage >= StageChoosen)
		{
			StartButton.interactable = true;
			StartButton.onClick = LevelButton.onClick;
		}
		else
		{
			StartButton.interactable = false;
		}
	
		GhostSelect1.sprite = GhostMap1.sprite;
		if(GhostMap2!=null)
		GhostSelect2.sprite = GhostMap2.sprite;
		if(GhostMap2!=null)
		GhostSelect3.sprite = GhostMap3.sprite;
		LevelNameText.text= LevelName;
	}
	}
	
}
