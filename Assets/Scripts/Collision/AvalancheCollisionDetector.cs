using UnityEngine;

public class AvalancheCollisionDetector : MonoBehaviour
{

    [Header("Events")]
    [Tooltip("Event to invoke when the object hit the player")]
    [SerializeField]
    private GameEvent _EventToInvoke;


    // [Header("Pool Manager Id")]
    // [Tooltip("Id of the pool manager to use")]
    // [SerializeField]
    // private IdContainer _idPoolManager;

    private bool _alreadyTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_alreadyTriggered)
        {
            return;
        }
        Debug.Log("Trigger detected " + gameObject.name);
        _EventToInvoke.Invoke();
        _EventToInvoke.Invoke();//avalanche do 2 damage
        _alreadyTriggered = true;
        //PoolManager poolManager = PoolingSystem.Instance.getPoolManagerInstance(_idPoolManager);
        //poolManager.ReturnPoolableObject(gameObject.GetComponent<PoolableObject>());
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     Debug.Log("Collision detected");
    // }
}
