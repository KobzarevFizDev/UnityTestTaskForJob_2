using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WorkerCommander : MonoBehaviour
{
    [SerializeField] private GameObject[] _buttonsContainer;

    [Header("Resources buttons")]
    [SerializeField] private ResourceSelectButton _selectWoodButton;
    [SerializeField] private ResourceSelectButton _selectMetalButton;
    [SerializeField] private ResourceSelectButton _selectStoneButton;


    [Header("Storages buttons")]
    [SerializeField] private StorageSelectButton _selectStorageOneButton;
    [SerializeField] private StorageSelectButton _selectStorageTwoButton;
    [SerializeField] private StorageSelectButton _selectStorageThreeButton;

    private WorkersFactory _workersFactory;

    private Worker _currentWorker;

    private ResourceId _currentSelectedResourceId = ResourceId.None;

    private void Awake()
    {
        _workersFactory = new WorkersFactory();

        _selectWoodButton.ResourceSelected += OnResourceSelected;
        _selectMetalButton.ResourceSelected += OnResourceSelected;
        _selectStoneButton.ResourceSelected += OnResourceSelected;

        _selectStorageOneButton.StorageSelected += OnStorageSelected;
        _selectStorageTwoButton.StorageSelected += OnStorageSelected;
        _selectStorageThreeButton.StorageSelected += OnStorageSelected;
    }


    private void OnResourceSelected(ResourceId resourceId)
    {
        var buttons = new List<ResourceSelectButton>();
        buttons.Add(_selectWoodButton);
        buttons.Add(_selectMetalButton);
        buttons.Add(_selectStoneButton);
        
        foreach (var button in buttons)
        {
            if (button.ResourceId != resourceId)
                button.ResetState();
        }

        _currentSelectedResourceId = resourceId;
    }

    private void OnStorageSelected(ResourceId storageResourceId)
    {
        var selectResourcesButtons = new List<ResourceSelectButton>();
        selectResourcesButtons.Add(_selectWoodButton);
        selectResourcesButtons.Add(_selectMetalButton);
        selectResourcesButtons.Add(_selectStoneButton);

        var selectStoragesButtons = new List<StorageSelectButton>();
        selectStoragesButtons.Add(_selectStorageOneButton);
        selectStoragesButtons.Add(_selectStorageTwoButton);
        selectStoragesButtons.Add(_selectStorageThreeButton);

        foreach (var button in selectResourcesButtons)
            button.ResetState();

        foreach (var button in selectStoragesButtons)
            button.ResetState();

        if (_currentSelectedResourceId != ResourceId.None)
        {
            CreateWorkerWithTask(_currentSelectedResourceId, storageResourceId);
        }

        _currentSelectedResourceId = ResourceId.None;
    }

    private void CreateWorkerWithTask(ResourceId resourceId, ResourceId storageResourceId)
    {
        _currentWorker = _workersFactory.CreateWorker();
        _currentWorker.DeliveryResource(resourceId, storageResourceId);
        _currentWorker.Delivered += OnWorkerDeliveredResourcesToStorage;

        HideUI();
    }

    private void OnWorkerDeliveredResourcesToStorage()
    {
        ShowUI();
        _currentWorker.Delivered -= OnWorkerDeliveredResourcesToStorage;
        Destroy(_currentWorker.gameObject);
    }

    private void HideUI()
    {
        foreach (GameObject container in _buttonsContainer)
            container.SetActive(false);
    }

    private void ShowUI()
    {
        foreach(GameObject container in _buttonsContainer)
            container.SetActive(true);
    }

    private void OnDestroy()
    {
        _selectWoodButton.ResourceSelected -= OnResourceSelected;
        _selectMetalButton.ResourceSelected -= OnResourceSelected;
        _selectStoneButton.ResourceSelected -= OnResourceSelected;

        _selectStorageOneButton.StorageSelected -= OnStorageSelected;
        _selectStorageTwoButton.StorageSelected -= OnStorageSelected;
        _selectStorageThreeButton.StorageSelected -= OnStorageSelected;
    }

}
