using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StorageCanvas : MonoBehaviour
{
    [SerializeField] private Image _resourceIcon;
    [SerializeField] private TextMeshProUGUI _idText;

    public void Initialize(StorageSettingsSO storageSettings)
    {
        _resourceIcon.sprite = storageSettings.ResourceIcon;
        _idText.SetText($"Склад {storageSettings.StorageId}");
    }
}
