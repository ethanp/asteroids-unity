using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    [SerializeField] AudioSource bulletFiredClip;
    [SerializeField] AudioSource shipExplosionClip;
    [SerializeField] AudioSource asteroidExplosionClip;
    [SerializeField] AudioSource userLostClip;
    [SerializeField] AudioSource userWonClip;
    [SerializeField] AudioSource backgroundMusicClip;
    [SerializeField] AudioSource forwardThrusterClip;
    [SerializeField] AudioSource reverseThrusterClip;
    [SerializeField] AudioSource turnThursterClip;

    [SerializeField] bool startPaused;

    void Start()
    {
        StartMusicAtRandomPosition();
        if (startPaused)
            ToggleMusicPlaying();
    }

    void Update()
    {
        // GetKeyDown: Only registers one time, when key is initially pressed.
        if (Input.GetKeyDown("m"))
            ToggleMusicPlaying();
    }

    public void StartMusicAtRandomPosition()
    {
        backgroundMusicClip.time = backgroundMusicClip.clip.length * Random.value;
        backgroundMusicClip.Play();
    }

    public void ToggleMusicPlaying()
    {
        if (backgroundMusicClip.isPlaying)
            backgroundMusicClip.Pause();
        else backgroundMusicClip.UnPause();
    }

    public void PlayBulletFired() {
       if (bulletFiredClip)
           bulletFiredClip.Play();
       else Debug.Log("bulletFiredClip is not set.");
    }
    public void PlayShipExplosion() {
       if (shipExplosionClip)
           shipExplosionClip.Play();
       else Debug.Log("shipExplosionClip is not set.");
    }
    public void PlayAsteroidExplosion() {
       if (asteroidExplosionClip)
           asteroidExplosionClip.Play();
       else Debug.Log("asteroidExplosionClip is not set.");
    }
    public void PlayUserLost() {
       if (userLostClip)
           userLostClip.Play();
       else Debug.Log("userLostClip is not set.");
    }
    public void PlayUserWon() {
       if (userWonClip)
           userWonClip.Play();
       else Debug.Log("userWonClip is not set.");
    }
    public void PlayBackgroundMusic() {
       if (backgroundMusicClip)
           backgroundMusicClip.Play();
       else Debug.Log("backgroundMusicClip is not set.");
    }

    public void PlayForwardThruster() {
       if (forwardThrusterClip)
           forwardThrusterClip.Play();
       else Debug.Log("forwardThrusterClip is not set.");
    }
    public void PlayReverseThruster() {
        if (reverseThrusterClip)
            reverseThrusterClip.Play();
        else Debug.Log("reverseThrusterClip is not set.");
    }
    public void PlayTurnThruster() {
       if (turnThursterClip)
           turnThursterClip.Play();
       else Debug.Log("turnThursterClip is not set.");
    }

    public void StopForwardThruster() {
       if (forwardThrusterClip)
           forwardThrusterClip.Stop();
       else Debug.Log("forwardThrusterClip is not set.");
    }
    public void StopReverseThruster() {
       if (reverseThrusterClip)
           reverseThrusterClip.Stop();
       else Debug.Log("reverseThrusterClip is not set.");
    }
    public void StopTurnThruster() {
       if (turnThursterClip)
           turnThursterClip.Stop();
       else Debug.Log("turnThursterClip is not set.");
    }
}
