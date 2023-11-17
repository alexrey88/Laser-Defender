using System;
using UnityEngine;


public class AudioPlayer : Singleton<AudioPlayer>
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField][Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField][Range(0f, 1f)] float damageVolume = 1f;

    [Header("HealthBoost")]
    [SerializeField] AudioClip boostClip;
    [SerializeField][Range(0f, 1f)] float boostVolume = 1f;

    void PlayClip(AudioClip audioClip, float volume)
    {
        if (audioClip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(audioClip, cameraPos, volume);
        }
        else
        {
            Debug.LogWarning("Trying to play a null AudioClip.");
        }
    }

    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayDamageClip()
    {
        PlayClip(damageClip, damageVolume);
    }

    public void PlayBoostClip()
    {
        PlayClip(boostClip, boostVolume);
    }
}
