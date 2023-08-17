using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{

    [SerializeField]
    private float _Velocity;


    private int _direction = 1;//1 = left, -1 = right

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            if (touchPosition.x < 0)
            {
                transform.position -= new Vector3(_Velocity * Time.deltaTime, 0, 0);
                if (_direction == -1)
                {
                    _direction = 1;
                    transform.localScale = new Vector3(_direction, 1, 1);
                }
            }
            else if (touchPosition.x > 0)
            {
                transform.position += new Vector3(_Velocity * Time.deltaTime, 0, 0);
                if (_direction == 1)
                {
                    _direction = -1;
                    transform.localScale = new Vector3(_direction, 1, 1);
                }
            }
        }
    }
}
