using UnityEngine;

public class InstantiatePools : MonoBehaviour {
    [SerializeField]
    private string SceneName;

    [ContextMenu("DebugInstantiatePools")]

    public void DebugInstantiatePools() {
        PoolingSystem.Instance.SceneManagerSetup(SceneName);
    }
}
