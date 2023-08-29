using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : Singleton<AudioSystem>, ISystem {
    [SerializeField]
    private int _Priority; // Priority of the audio system.
    public int Priority { get => _Priority; }

    private AudioSource musicSource; // AudioSource component for playing music.

    #region GameEvents
    [Header("Game Events")]

    [Tooltip("Event listened when the player hit a shield")]
    [SerializeField]
    private GameEvent _ShieldHitEvent;

    [Tooltip("Event listened when the player take a coin")]
    [SerializeField]
    private GameEvent _CoinTakenEvent;

    [Tooltip("Event listened when the player take damage")]
    [SerializeField]
    private GameEvent _DamageTakenEvent;

    [Tooltip("Event listened when the player take damage from a bomb")]
    [SerializeField]
    private GameEvent _BombTakenEvent;

    [Tooltip("Event listened when the game is over")]
    [SerializeField]
    private GameEvent _GameOverEvent;
    #endregion

    #region Clips
    [Header("Clips")]

    [Tooltip("Audio clip when collected a shield")]
    [SerializeField]
    private AudioClip _ShieldClip;

    [Tooltip("Audio clip when collected a coin")]
    [SerializeField]
    private AudioClip _CoinClip;

    [Tooltip("Audio clip when taken damage")]
    [SerializeField]
    private AudioClip _DamageClip;

    [Tooltip("Audio clip when taken damage from a bomb")]
    [SerializeField]
    private AudioClip _BombClip;

    [Tooltip("Audio clip when the game is over")]
    [SerializeField]
    private AudioClip _GameOverClip;
    #endregion

    public void Setup() {
        _ShieldHitEvent.Subscribe(ShieldHit);
        _CoinTakenEvent.Subscribe(CoinTaken);
        _DamageTakenEvent.Subscribe(DamageTaken);
        _BombTakenEvent.Subscribe(bombTaken);
        _GameOverEvent.Subscribe(GameOver);

        musicSource = GetComponent<AudioSource>();
        musicSource.loop = false;
        musicSource.playOnAwake = false;
        musicSource.volume = 0.5f; // Default volume.
        // Signal that the system setup is finished.
        SystemCoordinator.Instance.FinishSystemSetup(this);
    }
    private void OnDisable() {
        _ShieldHitEvent.Unsubscribe(ShieldHit);
        _CoinTakenEvent.Unsubscribe(CoinTaken);
        _DamageTakenEvent.Unsubscribe(DamageTaken);
        _BombTakenEvent.Unsubscribe(bombTaken);
        _GameOverEvent.Unsubscribe(GameOver);
    }

    void ShieldHit(GameEvent evt) {
        musicSource.Stop();
        musicSource.clip = _ShieldClip;
        musicSource.Play();
    }
    void CoinTaken(GameEvent evt) {
        musicSource.Stop();
        musicSource.clip = _CoinClip;
        musicSource.Play();
    }
    void DamageTaken(GameEvent evt) {
        musicSource.Stop();
        musicSource.clip = _DamageClip;
        musicSource.Play();
    }

    void bombTaken(GameEvent evt) {
        musicSource.Stop();
        musicSource.clip = _BombClip;
        musicSource.Play();
    }

    void GameOver(GameEvent evt) {
        StartCoroutine(WaitAndPlay());
    }

    IEnumerator WaitAndPlay() {
        yield return new WaitForSeconds(0.2f);
        musicSource.Stop();
        musicSource.clip = _GameOverClip;
        musicSource.Play();
    }
}
