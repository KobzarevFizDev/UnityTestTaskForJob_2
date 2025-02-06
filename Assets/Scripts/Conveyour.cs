using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyour : MonoBehaviour
{
    public ResourceId ResourceId => _settings.ResourceId;

    public Transform WorkerTargetPoint => _workerTrigger.transform;

    [SerializeField] private WorkerTrigger _workerTrigger;
    [SerializeField] private ParticleSystem _smokeVFX;
    [SerializeField] private Transform _spawnResourcePoint;
    [SerializeField] private ResourceDepositeTrigger _dropTrigger;
    [SerializeField] private ConveyourCanvas _conveyourCanvas;

    [SerializeReference] private ConveyourSettings _settings;
    
    private ResourceItemsFactory _resourceItemsFactory;
    private List<Transform> _itemsOnTape;
    
    private int _resourceItemCount;

    private int ResourceItemsCount
    {
        get
        {
            return _resourceItemCount;
        }

        set
        {
            if (value < 0)
            {
                _resourceItemCount = 0;
                _smokeVFX.Stop();
            }
            else if (value > _settings.StorageCapacity)
            {
                _resourceItemCount = _settings.StorageCapacity;
                _smokeVFX.Play();
            }
            else
            {
                _resourceItemCount = value;
                _smokeVFX.Stop();
            }

            _conveyourCanvas.UpdateFullness(_resourceItemCount, _settings.StorageCapacity);
        }
    }

    private void Awake()
    {
        _resourceItemsFactory = new ResourceItemsFactory(_settings.ResourceId);
        _itemsOnTape = new List<Transform>();

        _dropTrigger.OnResourcesDeposited += DropResourceToStorage;
        _workerTrigger.Entered += OnWorkerReachedConveyor;
    }
    private void Start()
    {
        StartCoroutine(SpawnResourceItemsRoutine());
        _conveyourCanvas.UpdateFullness(_resourceItemCount, _settings.StorageCapacity);
        _conveyourCanvas.SetResourceItemIcon(_settings.ResourceItemIcon);
    }

    private void OnWorkerReachedConveyor(Worker worker)
    {
        int transportLimit = worker.GetTransportLimitByResourceId(ResourceId);
        int resourceElementsTaken = ResourceItemsCount > transportLimit ? transportLimit : ResourceItemsCount;
        ResourceItemsCount -= resourceElementsTaken;
        worker.TakeResources(ResourceId, resourceElementsTaken);
    }

    private void Update()
    {
        foreach(Transform itemOnTape in _itemsOnTape)
            itemOnTape.Translate(transform.forward * _settings.TapeSpeed * Time.deltaTime);
    }

    private IEnumerator SpawnResourceItemsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_settings.TimeCreationOneItemRecource);
            GameObject resourceItem = _resourceItemsFactory.CreateResourceItem(_spawnResourcePoint);
            _itemsOnTape.Add(resourceItem.transform);
        }
    }

    private void DropResourceToStorage(GameObject resourceItem)
    {
        _itemsOnTape.Remove(resourceItem.transform);
        _resourceItemsFactory.DestroyResourceItem(resourceItem);
        ResourceItemsCount++;
    }

    private void OnDestroy()
    {
        _dropTrigger.OnResourcesDeposited -= DropResourceToStorage;
        _workerTrigger.Entered -= OnWorkerReachedConveyor;
    }
}
