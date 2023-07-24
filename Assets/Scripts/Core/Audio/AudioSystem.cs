using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSystem : Singleton<AudioSystem>, ISystem {

    [SerializeField]
    private int _Priority;
    public int Priority { get => _Priority; }

    //Music
    private AudioSource musicSource;


    public void Setup() {
        musicSource = gameObject.GetComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.playOnAwake = false;
        musicSource.volume = 0.5f; // Set volume

        SystemCoordinator.Instance.FinishSystemSetup(this);
    }


    public void SetVolume(float volume) {
        //volume = Mathf.Clamp01(volume);
        //Debug.Log("Volume: " + volume.ToString());
        musicSource.volume = volume;
    }

    public void StartMusic() {
        // start music
        musicSource.Play();
    }

    public void StopMusic() {
        musicSource.Stop();
    }
}
