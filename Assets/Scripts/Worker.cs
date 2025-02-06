using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System;


public class Worker : MonoBehaviour
{
    public event Action Delivered;

    [SerializeReference] private WorkerSettingsSO _settings;
    [SerializeField] private NavMeshAgent _agent;

    private Conveyour[] _conveyours;
    private Storage[] _storages;

    public ResourceId CurrentResourceId { private set; get; }
    private int _numberResourceItemsTaken;

    public void Initialize(Conveyour[] conveyours, Storage[] storages)
    {
        _conveyours = conveyours;
        _storages = storages;
    }

    public int GetTransportLimitByResourceId(ResourceId resourceId)
    {
        switch (resourceId)
        {
            case ResourceId.Wood:
                return _settings.WoodTransportLimit;

            case ResourceId.Stone:
                return _settings.StoneTransportLimit;

            case ResourceId.Metal:
                return _settings.MetalTransportLimit;

            default:
                throw new System.ArgumentException($"Not defined max limit for resourceId = {resourceId}");
        }
    }

    public int GiveUpResources()
    {
        int res = _numberResourceItemsTaken;
        _numberResourceItemsTaken = 0;
        return res;
    }

    public void TakeResources(ResourceId resourceId, int numberOfResourceItems)
    {
        _numberResourceItemsTaken = numberOfResourceItems;
        CurrentResourceId = resourceId;
        Debug.Log($"Рабочий взял {numberOfResourceItems} ресурсов типа {resourceId}");
    }

    public void DeliveryResource(ResourceId resourceId, ResourceId storageResourceId)
    {
        Transform conveyourPoint = GetConveyourTargetPointByResourceId(resourceId);
        Transform storagePoint = GetStorageTargetPointByStorageId(storageResourceId);
        StartCoroutine(DeliveryResourceRoutine(conveyourPoint, storagePoint));
    }

    private IEnumerator DeliveryResourceRoutine(Transform conveyourPoint, Transform storagePoint)
    {
        _agent.SetDestination(conveyourPoint.position);

        yield return null;

        while (_agent.remainingDistance > 1f)
        {
            yield return new WaitForSeconds(0.1f);
        }

        _agent.isStopped = true;
        yield return new WaitForSeconds(2f);
        _agent.isStopped = false;

        _agent.SetDestination(storagePoint.position);

        yield return null;

        while(_agent.remainingDistance > 1f)
        {
            yield return new WaitForSeconds(0.1f);
        }

        Delivered?.Invoke();
    }

    private Transform GetConveyourTargetPointByResourceId(ResourceId resourceId)
    {
        Conveyour targetConveyour = _conveyours.FirstOrDefault(c => c.ResourceId == resourceId);
        if (targetConveyour == null)
            throw new System.ArgumentException($"Not found target conveyour for resourceId = {resourceId}");

        return targetConveyour.WorkerTargetPoint;
    }

    private Transform GetStorageTargetPointByStorageId(ResourceId storageResourceId)
    {
        Storage targetStorage = _storages.FirstOrDefault(s => s.ResourceId == storageResourceId);
        if (targetStorage == null)
            throw new System.ArgumentException($"Not found target storage for resourceId = {storageResourceId}");

        return targetStorage.WorkerTargetPoint;
    }

}
