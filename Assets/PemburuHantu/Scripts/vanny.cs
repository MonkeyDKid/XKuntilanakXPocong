using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vanny : MonoBehaviour {
	public Texture[] txrSkin;
	public GameObject[] Mesh;
	int choosen;
	// Use this for initialization
	void Start () {
		GetComponent<SkinnedMeshRenderer> ().material.mainTexture = txrSkin [PlayerPrefs.GetInt("Skin")];
		if (Application.loadedLevelName == "wardrobe") {
			StartCoroutine (idling ());
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.loadedLevelName=="wardrobe"){
			if (GetComponent<Animation> ().isPlaying == false) {
				StartCoroutine (idling ());
			}
		}
	}
	public void GantiTexture(int txrnumber){
		choosen = txrnumber;
		GetComponent<SkinnedMeshRenderer> ().material.mainTexture = txrSkin [txrnumber];
	}
	public void applycustom(){
		PlayerPrefs.SetInt ("Skin", choosen);
		GetComponent<SkinnedMeshRenderer> ().material.mainTexture = txrSkin [choosen];

	}
	IEnumerator idling(){
		GetComponent<Animation> ().PlayQueued ("idle", QueueMode.PlayNow);
		GetComponent<Animation> ().PlayQueued ("idle", QueueMode.PlayNow);
		GetComponent<Animation> ().PlayQueued ("liat", QueueMode.CompleteOthers);
		yield return null;
	}
}
