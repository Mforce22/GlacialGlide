using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private int _movement = 0;
    // Start is called before the first frame update
    void Start()
    {
        //get instance of GameMaster
        GameMaster Gm = GameObject.Find("Master").GetComponent<GameMaster>();
        _movement = Gm.Velocity;
    }

    // Update is called once per frame
    void Update()
    {
        //move up 
        transform.Translate(Vector3.up * _movement * Time.deltaTime);
    }
}
