using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class DespawnCollectibles : MonoBehaviour
{
    [SerializeField]
    private GameObject _slopeEnd;

    [SerializeField]
    private IdContainer _idPoolManager;
    void Update()
    {
        //check if the y position is >= of slope end
        if (transform.position.y >= _slopeEnd.transform.position.y) {
            //Destroying game object
            PoolManager poolManager = PoolingSystem.Instance.getPoolManagerInstance(_idPoolManager);
            poolManager.ReturnPoolableObject(gameObject.GetComponent<PoolableObject>());
        }
    }
}
