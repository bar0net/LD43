using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Use it to spawn bullets

public class Store : MonoBehaviour{
    private static Store _instance = null;

    public GameObject prefab;
    public int count = 20;

    Stack<GameObject> store;
    static Vector3 store_pos = new Vector3 (100, 100, -100);

    public static Store Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("Creating an instance of BulletStore");
                _instance = new Store();
                _instance.SetUp();
            }

            return _instance;
        }
    }

    void SetUp()
    {
        for (int i = 0; i < count; i++)
        {
            store.Push(GameObject.Instantiate(prefab, store_pos, Quaternion.identity, null));
        }
    }

    public static GameObject Create(Vector3 pos, Quaternion rot)
    {
        if (Store.Instance.store.Count == 0)
            return GameObject.Instantiate(Store.Instance.prefab, pos, rot, null);

        GameObject go = Store.Instance.store.Pop();
        go.transform.position = pos;
        go.transform.rotation = rot;
        go.SetActive(true);
        return go;
    }

    public static void Delete(GameObject go)
    {
        go.transform.position = store_pos;
        go.SetActive(false);
        Store.Instance.store.Push(go);
    }
}
