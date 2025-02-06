using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WorkerSettigs", menuName = "Data/Create worker settings")]
public class WorkerSettingsSO : ScriptableObject
{

    [field: SerializeField] public int WoodTransportLimit { private set; get; }
    [field: SerializeField] public int StoneTransportLimit { private set; get; }
    [field: SerializeField] public int MetalTransportLimit { private set; get; }
}
