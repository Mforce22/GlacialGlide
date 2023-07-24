using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathViewController : MonoBehaviour
{
    private void Start() {
        GameMaster.Instance.setPause(true);
        //TODO: GameMaster.Instance.reset();
    }
    public void ChangeScene(string scene) {
        AudioSystem.Instance.StopMusic();
        TravelSystem.Instance.SceneLoad(scene);
    }
}
