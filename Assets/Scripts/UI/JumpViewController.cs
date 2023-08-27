using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpViewController : MonoBehaviour
{
    #region Events
    [Tooltip("Event to invoke when the jump is completed with success")]
    [SerializeField]
    private GameEvent _JumpCompleted;

    [Tooltip("Event to invoke when the jump is failed")]
    [SerializeField]
    private GameEvent _JumpFailed;
    #endregion
    #region Variables
    [SerializeField]
    private GameObject _RedCircle;

    private Vector3 _vect = new Vector3(0.04f, 0.04f, 0);
    #endregion

    private void Start()
    {
        StartCoroutine(Reduce());
    }

    private IEnumerator Reduce()
    {
        while (_RedCircle.transform.localScale.x >= 0.5f)
        {
            _RedCircle.transform.localScale = _RedCircle.transform.localScale - _vect;
            if (GameMaster.Instance.getEasyJump()) {
                yield return new WaitForSeconds(0.1f);
            }
            else if(GameMaster.Instance.getMediumJump()) {
                yield return new WaitForSeconds(0.08f);
            }
            else if (GameMaster.Instance.getHardJump()) {
                yield return new WaitForSeconds(0.06f);
            }
        }
        Debug.Log("JUMP FAILED");
        _JumpFailed.Invoke();
        Destroy(gameObject);
    }

    [ContextMenu("Click debug")]
    public void Click()
    {
        if (_RedCircle.transform.localScale.x >= 0.6f && _RedCircle.transform.localScale.x <= 1.1f)
        {
            Debug.Log("JUMP COMPLETED");
            _JumpCompleted.Invoke();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("JUMP FAILED");
            _JumpFailed.Invoke();
            Destroy(gameObject);
        }
    }
}
