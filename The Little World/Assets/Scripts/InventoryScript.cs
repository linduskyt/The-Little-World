using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {

    }

    public void OnGUI()
    {
        // Make a group on the center of the screen
        GUI.BeginGroup(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 150, 300, 300));
        // All rectangles are now adjusted to the group. (0,0) is the topleft corner of the group.

        // We'll make a box so you can see where the group is on-screen.
        GUI.Box(new Rect(0, 0, 200, 200), "Inventory");
        GUI.Button(new Rect(20, 40, 160, 150), "Click me");

        // End the group we started above. This is very important to remember!
        GUI.EndGroup();
    }
}
