using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Dialogue : MonoBehaviour
{
    public string name;

    [TextArea(2, 10)]
    public string[] sentences = new string[1];
}
