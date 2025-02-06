using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorkerTrigger : MonoBehaviour
{
    public event Action<Worker> Entered;
    private void OnTriggerEnter(Collider other)
    {
        Worker worker = other.gameObject.GetComponent<Worker>();
        if (worker == null)
            throw new InvalidOperationException("Incorrect level settings for worker <-> storage trigger");
        Entered?.Invoke(worker);
    }
}
