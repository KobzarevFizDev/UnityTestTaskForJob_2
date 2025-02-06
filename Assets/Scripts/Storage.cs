using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum DeliveryResult { Correct, Incorrect }

public class Storage : MonoBehaviour
{
    public event Action<DeliveryResult> ResultResourcesDelivery;
    public event Action<int> ResourcesDelivered;

    public ResourceId ResourceId => _storageSettings.ResourceId;

    public Transform WorkerTargetPoint => _workerTrigger.transform;

    [SerializeField] private StorageCanvas _canvas;
    [SerializeReference] private StorageSettingsSO _storageSettings;
    [SerializeField] private WorkerTrigger _workerTrigger;

    private void Awake()
    {
        _workerTrigger.Entered += OnWorkerReachedStorage;
    }

    private void OnWorkerReachedStorage(Worker worker)
    {
        if(worker.CurrentResourceId != ResourceId)
        {
            ResultResourcesDelivery?.Invoke(DeliveryResult.Incorrect);
        }
        else
        {
            int numberOfDeliveredResourceElements = worker.GiveUpResources();
            ResultResourcesDelivery?.Invoke(DeliveryResult.Correct);
            ResourcesDelivered?.Invoke(numberOfDeliveredResourceElements);
        }

    }

    private void Start()
    {
        _canvas.Initialize(_storageSettings);
    }


    private void OnDestroy()
    {
        _workerTrigger.Entered -= OnWorkerReachedStorage;
    }
}
