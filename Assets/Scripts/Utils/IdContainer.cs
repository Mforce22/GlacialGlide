using UnityEngine;

[CreateAssetMenu(fileName = "IdContainer", menuName = "ScriptableObjects/IdContainer", order = 1)]
public class IdContainer : ScriptableObject {
    /// <summary>
    /// A container for holding a unique identifier (ID) represented as a string.
    /// </summary>
    public string Id;
}