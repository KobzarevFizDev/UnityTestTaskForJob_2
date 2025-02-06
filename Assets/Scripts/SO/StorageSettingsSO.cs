using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Create storage settings", fileName = "StorageSettings")]
public class StorageSettingsSO : ScriptableObject
{
    [field: SerializeField] public ResourceId ResourceId { private set; get; }
    [field: SerializeField] public Sprite ResourceIcon { private set; get; }
    [field: SerializeField] public int StorageId { private set; get; }
}
