using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for starting the background music in the game.
/// It is attached to a GameObject as a MonoBehaviour script.
/// </summary>
public class StartMusic : MonoBehaviour
{
    void Start()
    {
        // Call the StartMusic method from the AudioSystem instance to begin playing the music.
        AudioSystem.Instance.StartMusic();
    }
}
