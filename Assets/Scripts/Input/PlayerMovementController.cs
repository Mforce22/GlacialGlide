using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

    [SerializeField]
    private float _Velocity; // The speed at which the player moves.

    private int _direction = 1; // 1 = left, -1 = right, used to determine the player's facing direction.

    void Update() {
        // Check if there is at least one touch input.
        if (Input.touchCount > 0) {
            // Get the first touch input.
            Touch touch = Input.GetTouch(0);

            // Convert the touch position to world coordinates.
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            // Check if the touch is on the left side of the screen.
            if (touchPosition.x < 0) {
                // Move the player to the left.
                transform.position -= new Vector3(_Velocity * Time.deltaTime, 0, 0);

                // Check if the player was previously facing right and update the direction.
                if (_direction == -1) {
                    _direction = 1;
                    transform.localScale = new Vector3(_direction, 1, 1); // Flip the player's sprite to face left.
                }
            } else if (touchPosition.x > 0) {
                // Move the player to the right.
                transform.position += new Vector3(_Velocity * Time.deltaTime, 0, 0);

                // Check if the player was previously facing left and update the direction.
                if (_direction == 1) {
                    _direction = -1;
                    transform.localScale = new Vector3(_direction, 1, 1); // Flip the player's sprite to face right.
                }
            }
        }
    }
}
