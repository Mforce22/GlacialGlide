using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathViewController : MonoBehaviour
{
    [SerializeField]
    private int _Points;

    [SerializeField]
    private TextMeshProUGUI _PointsValue;

    private void Start() {
        //Set text for points
        _Points = GameMaster.Instance.getPoints();
        _PointsValue.SetText(_Points.ToString());
        GameMaster.Instance.setPause(true);
    }
    public void ChangeScene(string scene) {
        //Game reset
        GameMaster.Instance.setHearts(3);
        GameMaster.Instance.setPause(false);
        GameMaster.Instance.setPoints(0);
        GameMaster.Instance.setVelocity(2);

        AudioSystem.Instance.StopMusic();
        TravelSystem.Instance.SceneLoad(scene);
    }
}
