﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class LoomTest : MonoBehaviour
{

    private Text _text;
	// Use this for initialization
	void Start ()
	{
        gameObject.AddComponent<ColaLoom>();
        _text = this.transform.Find("Text").GetComponent<Text>();
        ColaLoom.RunAsync(UpdateUI);
    }

    private void UpdateUI()
    {
        ColaLoom.QueueOnMainThread(() =>
        {
            this._text.text = Time.realtimeSinceStartup.ToString();
            TimeHelper.SetRepeatTimer(() =>
            {
                this._text.text = Time.realtimeSinceStartup.ToString();
            }, 1);
        });

    }
}
