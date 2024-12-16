using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPool : MonoBehaviour
{
    public GameObject objectPrefab; // The prefab to pool
    public int initialPoolSize = 10; // Initial size of the pool

    private Queue<GameObject> objectPool;

    void Awake()
    {
        objectPool = new Queue<GameObject>();

        // Initialize the pool with inactive objects
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab, gameObject.transform);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (objectPool.Count > 0)
        {
            GameObject obj = objectPool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(objectPrefab, gameObject.transform);
            obj.SetActive(true);
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        objectPool.Enqueue(obj);
    }
}
