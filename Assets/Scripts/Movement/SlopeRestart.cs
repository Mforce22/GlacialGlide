using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeRestart : MonoBehaviour
{
    [SerializeField]
    private GameObject _targetSlope;

    [SerializeField]
    private GameObject _slopeEnd;
    // Update is called once per frame
    void Update()
    {
        //check if the y position is >= of slope end
        if (transform.position.y >= _slopeEnd.transform.position.y)
        {
            //move to slope start
            transform.position = new Vector3(transform.position.x, (_targetSlope.transform.position.y - 9.6f), transform.position.z);
        }
    }
}
