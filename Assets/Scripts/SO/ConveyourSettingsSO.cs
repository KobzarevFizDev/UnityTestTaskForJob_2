using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ResourceId { None, Wood, Stone, Metal }

[CreateAssetMenu(fileName = "ConveyourSettings", menuName = "Data/Create conveyour settings")]
public class ConveyourSettings : ScriptableObject
{
    [field: SerializeField] public Sprite ResourceItemIcon { private set; get; }
    [field: SerializeField] public ResourceId ResourceId { private set; get; }
    [field: SerializeField] public int TimeCreationOneItemRecource { private set; get; }
    [field: SerializeField] public int StorageCapacity { private set; get; }
    [field: SerializeField] public float TapeSpeed { private set; get; }
}
