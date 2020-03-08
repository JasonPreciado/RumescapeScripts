using UnityEngine;
using System;
[CreateAssetMenu][Serializable]
public class Item : ScriptableObject
{
    new public string name = "New name";
    public Sprite sprite;
    public bool idDefaultItem = false;
}
