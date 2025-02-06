using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceCounterView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _counterValueText;
    [SerializeField] private Storage _storage;

    private int _counter;

    private void Awake()
    {
        _storage.ResourcesDelivered += OnResourcesDelivered;

        _counterValueText.SetText(0.ToString());
    }

    private void OnResourcesDelivered(int numberOfResourceDelivered)
    {
        _counter += numberOfResourceDelivered;
        _counterValueText.SetText(_counter.ToString());
    }

    private void OnDestroy()
    {
        _storage.ResourcesDelivered -= OnResourcesDelivered;
    }
}
