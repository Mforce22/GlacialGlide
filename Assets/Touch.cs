using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{
	private Vector2 movement;

	[SerializeField]
	private Rigidbody2D rb;

	[SerializeField]
	private float moveSpeed = 5f;

	void Start()
    {

		
    }

	// Update is called once per frame
	void Update()
	{
		movement.x = Input.GetAxisRaw("Horizontal");

        if (Input.touchCount > 0)
        {
			Touch touch = Input.GetTouch(0);
			if(touch.phase == TouchPhase.Moved)
            {
				movement.x = touch.position;
            }
		}
		




	}

    private void FixedUpdate()
    {
		rb.MovePosition(rb.position + moveSpeed * Time.fixedDeltaTime * movement);
    }
}
