using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSelection : MonoBehaviour
{
    private GameObject border = GameObject.Find("Border");

    private void OnMouseDown()
    {
        border.GetComponent<Image>().sprite = (Sprite)Resources.Load("/Images/HotbarBlock_1");
    }
}   