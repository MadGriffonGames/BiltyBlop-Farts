﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordsSwipeMenu : SwipeMenu {

	[SerializeField]
	GameObject swordCard;
	[SerializeField]
	GameObject unlockSwordWindow;
	[SerializeField]
	GameObject fade;
	[SerializeField]
	GameObject closeBuyWindowButton;
	[SerializeField]
	Sprite equipButton;
	[SerializeField]
	Sprite equipedButton;

	private const float DISTANCE = 175f;

	public override void Start () 
	{
		buttons = new GameObject[SkinManager.Instance.swordPrefabs.Length];
		Debug.Log (SkinManager.Instance.swordPrefabs.Length);
		SetSwordCards();
		distance = new float[buttons.Length];
		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i] = panel.GetChild(i).gameObject;
		}
		buttonDistance = (int)DISTANCE;
		minButtonsNumber = 1;
		panel.anchoredPosition = new Vector2(buttons[1].transform.position.x, panel.anchoredPosition.y);
	}

	public override void Update () {
		base.Update ();	
	}

	void SetSwordCards()
	{
		for (int i = 0; i < SkinManager.Instance.swordPrefabs.Length; i++)
		{
			for (int j = 0; j < SkinManager.Instance.swordPrefabs.Length; j++)
			{
				if (SkinManager.Instance.swordPrefabs[j].GetComponent<SwordPrefab>().orderNumber == i)
				{
					GameObject swordCardObj = Instantiate(swordCard, new Vector3(buttonDistance * i, 0, 0),Quaternion.identity) as GameObject;
					SwordPrefab sword = SkinManager.Instance.swordPrefabs[j].GetComponent<SwordPrefab>();
					sword.SetPlayerPrefsParams ();			

					swordCardObj.transform.SetParent(panel);
					swordCardObj.transform.localPosition = new Vector3(i * DISTANCE, 0, 0);
					swordCardObj.transform.localScale = new Vector3(1, 1, 1);
					swordCardObj.gameObject.GetComponentsInChildren<Text>()[0].text = sword.shopName;
					swordCardObj.gameObject.GetComponentsInChildren<Image>()[1].sprite = sword.swordSprite;

					if (PlayerPrefs.GetString(sword.name) == "Unlocked")
					{
						if (PlayerPrefs.GetString ("Sword") == sword.name)
						{
							swordCardObj.gameObject.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text> ().text = "EQUIPED";
							swordCardObj.gameObject.GetComponentsInChildren<Image> () [2].sprite = equipedButton;
						} 
						else  
						{
							swordCardObj.gameObject.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text> ().text = "EQUIP";
							swordCardObj.gameObject.GetComponentsInChildren<Image> () [2].sprite = equipButton;
						}
						swordCardObj.gameObject.GetComponentsInChildren<Button> () [0].onClick.AddListener (() => ApplySword (sword.orderNumber));
						swordCardObj.gameObject.GetComponentsInChildren<Button> () [1].onClick.AddListener (() => ApplySword (sword.orderNumber));
					}
					else
					{
						swordCardObj.gameObject.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => ShowUnlockSwordWindow(SkinManager.Instance.NumberOfSkinPrefabBySkinOrder(sword.orderNumber))); // wdfsdf
						swordCardObj.gameObject.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => ShowUnlockSwordWindow(SkinManager.Instance.NumberOfSkinPrefabBySkinOrder(sword.orderNumber)));
					}
					//swordCardObj.GetComponentInChildren<SkinStatsPanel>().SetAttackIndicators(sword.attackStat);
					//swordCardObj.GetComponentInChildren<SkinStatsPanel>().SetDefendIndicators(sword.armorStat);

					buttons[i] = swordCardObj;
					break;
				}
			}

		}
	}

	public void ShowUnlockSwordWindow(int swordNumber)
	{
		base.OnButtonClickLerp (SkinManager.Instance.swordPrefabs[swordNumber].GetComponent<SwordPrefab>().orderNumber);
		unlockSwordWindow.gameObject.SetActive(true);
		fade.gameObject.SetActive(true);
		closeBuyWindowButton.gameObject.SetActive(true);

		unlockSwordWindow.GetComponent<UnlockSwordWindow>().SetWindowWithSwordNumber(swordNumber);
	}

	public void CloseUnlockSwordWindow()
	{
		unlockSwordWindow.gameObject.SetActive(false);
		fade.gameObject.SetActive(false);
		closeBuyWindowButton.gameObject.SetActive(false);
	}


	public void UpdateSwordCards()
	{
		for (int i = 0; i < SkinManager.Instance.swordPrefabs.Length; i++)
		{
			for (int j = 0; j < SkinManager.Instance.swordPrefabs.Length; j++) 
			{
				if (SkinManager.Instance.swordPrefabs[j].GetComponent<SwordPrefab>().orderNumber == i)
				{
					SwordPrefab sword = SkinManager.Instance.swordPrefabs[j].GetComponent<SwordPrefab>();
					if (PlayerPrefs.GetString(sword.name) == "Unlocked")
					{
						buttons[i].gameObject.GetComponentsInChildren<Button>()[0].onClick.RemoveAllListeners();
						buttons[i].gameObject.GetComponentsInChildren<Button>()[1].onClick.RemoveAllListeners();
						if (PlayerPrefs.GetString ("Sword") == sword.name) {
							buttons [i].gameObject.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text> ().text = "EQUIPED";
							buttons [i].gameObject.GetComponentsInChildren<Image> () [2].sprite = equipedButton;
						} else 
						{
							buttons [i].gameObject.GetComponentsInChildren<Button> () [1].GetComponentInChildren<Text> ().text = "EQUIP";
							buttons[i].gameObject.GetComponentsInChildren<Image> () [2].sprite = equipButton;
						}
						buttons[i].gameObject.GetComponentsInChildren<Button>()[0].onClick.AddListener(() => ApplySword(sword.orderNumber));
						buttons[i].gameObject.GetComponentsInChildren<Button>()[1].onClick.AddListener(() => ApplySword(sword.orderNumber));
					}
				}
			}
		}
	}
	public void ApplySword(int swordOrderNumber) // writing to player prefs current skin
	{
		base.OnButtonClickLerp(swordOrderNumber);
		SkinManager.Instance.ApplySword(SkinManager.Instance.NameOfSwordPrefabBySwordOrder(swordOrderNumber), SkinManager.Instance.IndexOfSwordByOrderNumber(swordOrderNumber));
		UpdateSwordCards();
	}


}
