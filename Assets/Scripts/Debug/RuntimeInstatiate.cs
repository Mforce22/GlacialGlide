using UnityEngine;

public class RuntimeInstatiate : MonoBehaviour {
    [SerializeField]
    private GameObject prefab;

    [ContextMenu("InstantiateRuntime")]

    public void InstantiateRuntime() {
        for (int i = 0; i < 1000; i++) {
            Instantiate(prefab, gameObject.transform);
        }

    }
}
