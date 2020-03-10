using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TabGroup tabGroup;
    public TabGroup MyTabGroup {
        set { tabGroup = value; }
    }

    public Image background;
    public int buttonId;



    // Start is called before the first frame update
    void Start()
    {
        buttonId = tabGroup.tabButtons.Count;
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.onTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.onTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.onTabExit(this);
    }
}
