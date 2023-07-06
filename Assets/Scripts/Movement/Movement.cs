using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private int _movement;
    // Start is called before the first frame update
    void Start()
    {
        //get instance of GameMaster
        _movement = GameMaster.Instance.getVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        //if the game isn't in pause
        if (!GameMaster.Instance.getPause()) {
            //move up 
            transform.Translate(Vector3.up * _movement * Time.deltaTime);
        }
    }
}
