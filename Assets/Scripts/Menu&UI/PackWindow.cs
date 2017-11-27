﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class PackWindow : MonoBehaviour
{
    [SerializeField]
    Button buyButton;
    [SerializeField]
    int productId;
    [SerializeField]
    Text timerText;

    TimeSpan timer; 
    TimeSpan hours48;
    DateTime lastOpenDate;

    private void Awake()
    {
        hours48 = new TimeSpan(48,0,0);
        timer = new TimeSpan();
        lastOpenDate = new DateTime();
    }

    private void Update()
    {
        if (timerText != null)
        {
            if (timer > TimeSpan.Zero)
            {
                ConverDateTimeToText();
            }
        }
    }

    private void OnEnable()
    {
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => PurchaseManager.Instance.BuyNonConsumable(productId));
        if (timerText != null)
        {
            if (!PlayerPrefs.HasKey("StarterPackOpenDate"))
            {
                PlayerPrefs.SetString("StarterPackOpenDate", DateTime.Now.ToString());
                lastOpenDate = DateTime.Parse(PlayerPrefs.GetString("StarterPackOpenDate"));
                ConverDateTimeToText();
            }
            else
            {
                lastOpenDate = DateTime.Parse(PlayerPrefs.GetString("StarterPackOpenDate"));
                ConverDateTimeToText();
            }
        }
    }

    void ConverDateTimeToText()
    {
        string tmp;
        timer = hours48 + (lastOpenDate - DateTime.Now);
        tmp = "" + (timer.Hours + timer.Days * 24).ToString() + ":" + timer.Minutes.ToString() + ":" + timer.Seconds.ToString();
        timerText.text = tmp;
    }
}
