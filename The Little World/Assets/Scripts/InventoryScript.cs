using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class InventoryScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image slot;
    // Start is called before the first frame update
    void Start()
    {
        slot = GetComponent<Image>();
    }

    // Update is called once per frame
    public void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hovering");
        slot.GetComponent<Image>().color = new Color32(70, 70, 70, 255);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        slot.GetComponent<Image>().color = new Color32(58, 58, 58, 255);
        Debug.Log("Not Hovering");
    }
}
