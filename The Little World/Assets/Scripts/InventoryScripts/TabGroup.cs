using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    [SerializeField] private Sprite tabIdle;
    [SerializeField] private Sprite tabHover;
    [SerializeField] private Sprite tabSelected;
    [SerializeField] private DisplayHotbar hotbar;
    public TabButton selectedTab = null;
    public bool IsHover = false;

    private void Start()
    {
        hotbar = GameObject.FindWithTag("Hotbar").GetComponent<DisplayHotbar>();
    }

    public void Subscribe(TabButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void onTabEnter(TabButton button)
    {
        IsHover = true;
        hotbar.slotDisplay(button.buttonId);
    }

    public void onTabExit(TabButton button)
    {
        IsHover = false;
        hotbar.slotDisplay(-1);
    }

    public void onTabSelected(TabButton button)
    {
        selectedTab = button;
        hotbar.slotSelected(selectedTab.buttonId);
    }
}
