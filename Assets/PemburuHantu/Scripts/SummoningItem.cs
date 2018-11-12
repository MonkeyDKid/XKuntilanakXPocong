﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SummoningItem : MonoBehaviour {
	public GameObject Item;
	public GameObject InfoPanel;
	public string Atk, Def, Hp, Type, Name;
	public Text NameText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void SummonItem(string name)
	{	

		Item.SetActive(true);
		Item.GetComponent<Animator>().SetTrigger("Craft");
		Item.transform.FindChild("ImageSummon").GetComponent<Image>().sprite = Resources.Load<Sprite> ("icon_item/" + name);
		InfoPanel.SetActive(true);
		InfoPanel.transform.FindChild("HP").GetComponent<Text>().text = Hp;
		InfoPanel.transform.FindChild("Att").GetComponent<Text>().text = Atk;
		InfoPanel.transform.FindChild("Def").GetComponent<Text>().text = Def;
		InfoPanel.transform.FindChild("Type").GetComponent<Text>().text = Type;
		NameText.text = Name;
	}
}
