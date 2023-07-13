using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {
    [SerializeField]
    private IdContainer _ID;
    [SerializeField]
    private PoolableObject _PoolableObjectPrefab;
    [SerializeField]
    private int _Quantity;
    [SerializeField]
    private int _ObjectsPerFrame;

    public string ID => _ID.Id;
    private Queue<PoolableObject> _poolQueue;

    public void Setup() {
        if (this._poolQueue == null){
            this._poolQueue = new Queue<PoolableObject>();
        }
        StartCoroutine(AsyncInstantiate());
    }

    private IEnumerator AsyncInstantiate() {  
        PoolableObject poolableObject;
        for (int i = 1; i <= _Quantity; ++i) {
            poolableObject = Instantiate(_PoolableObjectPrefab, gameObject.transform);
            poolableObject.gameObject.SetActive(false);
            this._poolQueue.Enqueue(poolableObject);
            if(i % _ObjectsPerFrame == 0) {
                yield return null;
            }
        }
     
    }

    public T GetPoolableObject<T>() where T : PoolableObject {
        if(this._poolQueue.Count == 0) {
            Debug.LogError("QUEUE ENDED: " + gameObject.name);
            return null;
        }
        T poolableObject = this._poolQueue.Dequeue() as T;
        poolableObject.gameObject.SetActive(true);
        poolableObject.Setup();
        return poolableObject;
    }

    public void ReturnPoolableObject(PoolableObject poolableObject) {
        poolableObject.Clear();
        poolableObject.gameObject.SetActive(false);
        this._poolQueue.Enqueue(poolableObject);
    }
}
