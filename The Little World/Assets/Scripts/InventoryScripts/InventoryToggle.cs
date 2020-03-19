using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryToggle : MonoBehaviour
{
    public bool inventoryToggled;

    [SerializeField] private GameObject inventoryScreen = null;
    [SerializeField] private Button myButton = null;

    private Button btn;

    // Start is called before the first frame update
    void Start()
    {
        inventoryToggled = false;
        btn = myButton.GetComponent<Button>();
        btn.onClick.AddListener(onClick);
    }

    private void onClick()
    {
        inventoryToggled = !inventoryToggled;
        inventoryScreen.SetActive(!inventoryScreen.activeInHierarchy);
    }
}
