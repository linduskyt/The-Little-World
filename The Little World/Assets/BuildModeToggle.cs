using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class BuildModeToggle : MonoBehaviour
{
    public bool builderMode;

    [SerializeField] private Button myButton = null;
    [SerializeField] private Player player = null;
    [SerializeField] private TextMeshProUGUI text = null;
    private Button btn;
    private Text btnText;

    private void Start()
    {
        builderMode = false;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        btn = myButton.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
        text.text = "Gamemode: None";
        btnText = GetComponentInChildren<Text>();
        btnText.text = "Build Mode Disabled";
    }

    private void OnClick()
    {

        builderMode = !builderMode;
        if (builderMode)
        {
            text.text = "Gamemode: Build Mode";
            btnText.text = "Build Mode Enabled";
            player.mode = 1;
        }
        else
        {
            text.text = "Gamemode: None";
            btnText.text = "Build Mode Disabled";
            player.mode = 0;
        }
    }
}
