using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BuilderModeToggleScript : MonoBehaviour
{
    public bool builderMode;

    [SerializeField] private Button myButton = null;
    [SerializeField] private Player player = null;
    private TextMeshProUGUI text = null;
    private Button btn;

    private void Start()
    {
        builderMode = false;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        text = player.GetComponentInChildren<TextMeshProUGUI>();
        btn = myButton.GetComponent<Button>();
        btn.onClick.AddListener(onClick);
        text.text = "Build Mode Disabled";
    }

    private void onClick()
    {

        builderMode = !builderMode;
        player.mode = Convert.ToInt32(builderMode);
        if (builderMode)
            text.text = "Build Mode Enabled";
        else
            text.text = "Build Mode Disabled";
    }
}

