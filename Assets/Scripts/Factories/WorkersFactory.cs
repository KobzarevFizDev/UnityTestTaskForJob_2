using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkersFactory
{
    private const string PATH_TO_WORKER_PREFAB = "Worker";
    private const string WORKER_SPAWN_POINT_TAG = "WorkerSpawn"; 

    private GameObject _workerPrefab;
    private Transform _workerSpawn;

    private Storage[] _storages;
    private Conveyour[] _conveyours;

    public WorkersFactory()
    {
        _workerSpawn = GameObject.FindWithTag(WORKER_SPAWN_POINT_TAG).transform;
        _storages = GameObject.FindObjectsOfType<Storage>();
        _conveyours = GameObject.FindObjectsOfType<Conveyour>();

        _workerPrefab = (GameObject)Resources.Load(PATH_TO_WORKER_PREFAB);
        if (_workerPrefab == null)
            throw new System.InvalidOperationException("Not found worker prefab in Resources");
    }

    public Worker CreateWorker()
    {
        Vector3 at = _workerSpawn.position;
        GameObject workerGO = GameObject.Instantiate(_workerPrefab, at, Quaternion.identity);
        Worker worker = workerGO.GetComponent<Worker>();
        worker.Initialize(_conveyours, _storages);
        return worker;
    }
}
