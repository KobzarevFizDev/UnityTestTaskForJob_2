using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceDepositeTrigger : MonoBehaviour
{
    public event Action<GameObject> OnResourcesDeposited;
    private void OnTriggerEnter(Collider other)
    {
        OnResourcesDeposited?.Invoke(other.gameObject);
    }
}
