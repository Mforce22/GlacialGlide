using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnCollectibles : MonoBehaviour
{
    [SerializeField]
    private GameObject _slopeEnd;
    void Update()
    {
        //check if the y position is >= of slope end
        if (transform.position.y >= _slopeEnd.transform.position.y) {
            //Destroying game object
            Destroy(gameObject);
        }
    }
}
