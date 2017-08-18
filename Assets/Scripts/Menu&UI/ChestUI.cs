﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChestUI : MonoBehaviour
{
    const int COINS_RANGE    = 70;
    const int CRYSTALS_RANGE = 99;
    const int ITEMS_RANGE    = 100;

    const int MIN_COIN_RANGE = 50;
    const int MID_COIN_RANGE = 70;
    const int BIG_COIN_RANGE = 100;

    const int MIN_CRYSTAL_RANGE = 65;
    const int MID_CRYSTAL_RANGE = 85;
    const int BIG_CRYSTAL_RANGE = 100;


    [SerializeField]
    Image lightCircle;
    [SerializeField]
    GameObject chest;
    [SerializeField]
    Sprite chestOpen;
    [SerializeField]
    Sprite chestClose;
    [SerializeField]
    GameObject activateButton;
    [SerializeField]
    Image loot;
    [SerializeField]
    GameObject chestFade;
    [SerializeField]
    Sprite[] lootArray;

    Image chestImage;
    bool isSpined;
    Quaternion rotationVector;
    Animator lootAnimator;
    bool isOpened;
    bool isStarsCollected;

    private void Start()
    {
        chestImage = chest.GetComponent<Image>();
        lootAnimator = loot.gameObject.GetComponent<Animator>();

        isOpened = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_chest") > 0;
        isStarsCollected = (Player.Instance.stars >= 3) || (PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_collects") >= 3);

        if (isOpened)
        {
            chestImage.sprite = chestOpen;
            activateButton.SetActive(false);
        }
        else
        {
            chestImage.sprite = chestClose;
            if (isStarsCollected)
            {
                lightCircle.gameObject.SetActive(true);
                isSpined = true;
                activateButton.SetActive(true);
            }
            else
            {
                lightCircle.gameObject.SetActive(false);
                isSpined = false;
                chestImage.color = new Color(chestImage.color.r, chestImage.color.g, chestImage.color.b, 0.55f);
                activateButton.SetActive(false);
            }
        }

        
    }

    private void Update()
    {
        if (isSpined)
        {
            rotationVector = lightCircle.rectTransform.rotation;
            rotationVector.z -= 0.0005f;
            lightCircle.rectTransform.rotation = rotationVector;
        }

        if (AdsManager.Instance.isRewardVideoWatched)
        {
            AdsManager.Instance.isRewardVideoWatched = false;
            if (!PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_chest"))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_chest", 1);
            }
            GiveLoot();

            AppMetrica.Instance.ReportEvent("#CHEST 3Stars chest activate");
        }
    }

    public void OpenChestButton()
    {
        AdsManager.Instance.ShowRewardedVideo();
    }

    public void GiveLoot()
    {
        Randomize();
        chestImage.sprite = chestOpen;
        chestFade.SetActive(true);
        loot.gameObject.SetActive(true);
        activateButton.SetActive(false);
    }

    public void CollectLoot()
    {
        chestFade.SetActive(false);
        loot.gameObject.SetActive(false);
    }

    public void Randomize()
    {
        int random = UnityEngine.Random.Range(1, 100);

        if (random <= COINS_RANGE)
        {
            RandomizeCoins();
        }
        if (random > COINS_RANGE && random <= CRYSTALS_RANGE)
        {
            RandomizeCrystals();
        }
        if (random > CRYSTALS_RANGE && random <= ITEMS_RANGE)
        {
            RandomizeItems();
        }
    }

    void RandomizeCoins()
    {
        int random = UnityEngine.Random.Range(1, 100);

        if (random <= MIN_COIN_RANGE)
        {
            loot.sprite = lootArray[0];
            AddCoins(50);
        }
        if (random > MIN_COIN_RANGE && random <= MID_COIN_RANGE)
        {
            loot.sprite = lootArray[1];
            AddCoins(150);
        }
        if (random > MID_COIN_RANGE && random <= BIG_COIN_RANGE)
        {
            loot.sprite = lootArray[2];
            AddCoins(300);
        }
    }

    void RandomizeCrystals()
    {
        int random = UnityEngine.Random.Range(1, 100);

        if (random <= MIN_CRYSTAL_RANGE)
        {
            loot.sprite = lootArray[3];
            AddCrystals(2);
        }
        if (random > MIN_CRYSTAL_RANGE && random <= MID_CRYSTAL_RANGE)
        {
            loot.sprite = lootArray[4];
            AddCrystals(5);
        }
        if (random > MID_CRYSTAL_RANGE && random <= BIG_CRYSTAL_RANGE)
        {
            loot.sprite = lootArray[5];
            AddCrystals(10);
        }
    }

    void RandomizeItems()
    {

    }

    void AddCoins(int value)
    {
        loot.gameObject.GetComponentInChildren<Text>().text = value.ToString();
        GameManager.collectedCoins += value;
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + value);
    }

    void AddCrystals(int value)
    {
        loot.gameObject.GetComponentInChildren<Text>().text = value.ToString();
        PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") + value);
        GameManager.crystalTxt.text = PlayerPrefs.GetInt("Crystals").ToString();
    }

    void AddItem(string itemName)
    {

    }
}
