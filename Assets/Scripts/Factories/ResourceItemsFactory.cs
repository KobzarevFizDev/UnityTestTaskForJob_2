using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceItemsFactory
{
    private const string PATH_TO_WOOD_RESOURCE_ITEM_PREFAB = "WoodResourceItem";
    private const string PATH_TO_STONE_RESOURCE_ITEM_PREFAB = "StoneResourceItem";
    private const string PATH_TO_METAL_RESOURCE_ITEM_PREFAB = "MetalResourceItem";

    private const int POOL_SIZE = 20;

    private GameObjectsPool _pool;

    public ResourceItemsFactory(ResourceId resourceId)
    {
        GameObject prefab = LoadPrefabByResourceId(resourceId);
        _pool = new GameObjectsPool(prefab, POOL_SIZE);
    }

    public GameObject CreateResourceItem(Transform spawnPoint)
    {
        GameObject objectFromPool = _pool.GetObject();
        objectFromPool.transform.SetPositionAndRotation(spawnPoint.position, Quaternion.identity);
        return objectFromPool;

    }

    public void DestroyResourceItem(GameObject resourceItem)
    {
        _pool.ReturnObject(resourceItem);

    }

    private GameObject LoadPrefabByResourceId(ResourceId resourceId)
    {
        switch (resourceId)
        {
            case ResourceId.Wood:
                return (GameObject)Resources.Load(PATH_TO_WOOD_RESOURCE_ITEM_PREFAB);

            case ResourceId.Stone:
                return (GameObject)Resources.Load(PATH_TO_STONE_RESOURCE_ITEM_PREFAB);

            case ResourceId.Metal:
                return (GameObject)Resources.Load(PATH_TO_METAL_RESOURCE_ITEM_PREFAB);

            default:
                throw new System.ArgumentException($"Not found prefab for resourceId = {resourceId}");
        }
    }

}
