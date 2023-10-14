using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "item", menuName = "data/Item", order = 0)]
public class Item : ScriptableObject
{
    public string itemName;
    public string description;
    public int itemId;

    public bool canStack;
    public bool canUse;
    public bool canEquip;
    public bool canDiscard;
    public bool canCombine;

    public GameObject previewMeshPrefab;
}
