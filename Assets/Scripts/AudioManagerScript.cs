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

    public void PlayBulletFired() { bulletFiredClip.Play(); }

    public void PlayShipExplosion() { shipExplosionClip.Play(); }

    public void PlayAsteroidExplosion() { asteroidExplosionClip.Play(); }

    public void PlayUserLost() { userLostClip.Play(); }

    public void PlayUserWon() { userWonClip.Play(); }

    public void PlayBackgroundMusic() { backgroundMusicClip.Play(); }
}
