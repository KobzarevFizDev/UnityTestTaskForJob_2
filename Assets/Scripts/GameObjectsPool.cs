using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsPool
{
    private GameObject _prefab;
    private Queue<GameObject> _pool;
    private Transform _container;

    public GameObjectsPool(GameObject prefab, int initialSize)
    {
        _container = new GameObject("POOL").transform;
        _pool = new Queue<GameObject>();
        CreateObjects(prefab, initialSize);
    }

    private void CreateObjects(GameObject prefab, int needToCreate)
    {
        for (int i = 0; i < needToCreate; i++)
        {
            GameObject createdObject = GameObject.Instantiate(prefab);
            createdObject.SetActive(false);
            _pool.Enqueue(createdObject);

            createdObject.transform.SetParent(_container);
        }
    }

    public GameObject GetObject()
    {
        if (_pool.Count > 0)
        {
            GameObject objectFromPool = _pool.Dequeue();
            objectFromPool.SetActive(true);
            return objectFromPool;
        }
        else
        {
            return GameObject.Instantiate(_prefab);
        }
    }

    public void ReturnObject(GameObject returnObject)
    {
        returnObject.SetActive(false);
        _pool.Enqueue(returnObject);
    }
}
