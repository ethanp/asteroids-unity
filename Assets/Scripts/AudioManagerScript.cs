using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    [SerializeField] private AudioSource bulletFiredClip;
    [SerializeField] private AudioSource shipExplosionClip;
    [SerializeField] private AudioSource asteroidExplosionClip;
    [SerializeField] private AudioSource userLostClip;
    [SerializeField] private AudioSource userWonClip;
    [SerializeField] private AudioSource backgroundMusicClip;
    [SerializeField] private AudioSource forwardThrusterClip;
    [SerializeField] private AudioSource reverseThrusterClip;
    [SerializeField] private AudioSource turnThursterClip;

    // TODO Actually I want the AudioSource to be on the Ship so that the
    //      directionality is right.
    public void PlayBulletFired() { bulletFiredClip.Play(); }
    public void PlayShipExplosion() { shipExplosionClip.Play(); }
    public void PlayAsteroidExplosion() { asteroidExplosionClip.Play(); }
    public void PlayUserLost() { userLostClip.Play(); }
    public void PlayUserWon() { userWonClip.Play(); }
    public void PlayBackgroundMusic() { backgroundMusicClip.Play(); }

    public void PlayForwardThruster() { forwardThrusterClip.Play(); }
    public void PlayReverseThruster() { reverseThrusterClip.Play(); }
    public void PlayTurnThruster() { turnThursterClip.Play(); }

    public void StopForwardThruster() { forwardThrusterClip.Stop(); }
    public void StopReverseThruster() { reverseThrusterClip.Stop(); }
    public void StopTurnThruster() { turnThursterClip.Stop(); }
}
