using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class StorageSelectButton : BaseActionButton
{
    public event Action<ResourceId> StorageSelected;
    public int StorageId => _storageSettings.StorageId;

    [SerializeField] private TextMeshProUGUI _storageIdText;
    [SerializeField] private StorageSettingsSO _storageSettings;
    protected override void Start()
    {
        base.Start();
        _storageIdText.SetText($"Склад {_storageSettings.StorageId}");
    }
    public override void InvokeClickEvent()
    {
        StorageSelected?.Invoke(_storageSettings.ResourceId);
    }
}
