using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string characterName; // Name of the character speaking
    [TextArea(3, 10)]
    public string[] sentences;
}
