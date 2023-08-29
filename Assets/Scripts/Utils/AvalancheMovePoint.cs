using System.Collections;
using UnityEngine;

public class AvalancheMovePoint : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(MovePoint());
    }

    IEnumerator MovePoint() {
        while (true) {
            transform.position = new Vector3((transform.position.x * (-1)), transform.position.y, transform.position.z);
            yield return new WaitForSeconds(10.0f);
        }
    }
}
