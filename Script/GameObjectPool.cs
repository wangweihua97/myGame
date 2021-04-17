using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameObjectPool : MonoBehaviour
{
    public static GameObjectPool instance;
    public GameObject gameObjectPool;
    private Dictionary<string, List<GameObject>> pools;

    private void Awake()
    {
        instance = this;
        pools = new Dictionary<string, List<GameObject>>();
    }

    public GameObject GetGameObject(string objectName)
    {
        if (!pools.ContainsKey(objectName))
            return null;
        List<GameObject> list = pools[objectName];
        if (list.Count == 0)
            return null;
        GameObject go = list[0];
        list.RemoveAt(0);
        go.transform.SetParent(null);
        return go;
    }

    public void RemoveGameObject(string objectName, GameObject go)
    {
        if (!pools.ContainsKey(objectName))
        {
            pools.Add(objectName, new List<GameObject>());
        }

        go.transform.SetParent(gameObjectPool.transform);
        go.transform.localPosition =
            new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f), go.transform.position.z);
        pools[objectName].Add(go);
    }
}
