using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "item", menuName = "data/items/item", order = 0)]
public class item : ScriptableObject
{
    public string item_name;
    public int id;
    public string description;
    public Sprite image;

    [Header("flags")] 
    public bool usable;
    public bool stackable;
    public bool equippable;
    public bool discardable;
}
