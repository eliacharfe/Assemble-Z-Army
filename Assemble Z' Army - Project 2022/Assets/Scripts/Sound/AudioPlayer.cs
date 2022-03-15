using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Damage")]
    [SerializeField] AudioClip damageClip;
    [SerializeField] [Range(0f, 1f)] float damageVolume = 1f;

    [Header("Healing")]
    [SerializeField] AudioClip healingClip;
    [SerializeField] [Range(0f, 1f)] float healingVolume = 1f;

    [Header("Death")]
    [SerializeField] AudioClip deadClip;
    [SerializeField] [Range(0f, 1f)] float deadVolume = 1f;

    [Header("Horse Gallop")]
    [SerializeField] AudioClip horseGallopClip;
    [SerializeField] [Range(0f, 1f)] float horseGallopVolume = 1f;

    [Header("ButtonClick Error")]
    [SerializeField] AudioClip btnClickErrorClip;
    [SerializeField] [Range(0f, 1f)] float btnClickErrorVolume = 1f;


    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayDamageClip()
    {
        PlayClip(damageClip, damageVolume);
    }

    public void PlayHealingClip()
    {
        PlayClip(healingClip, healingVolume);
    }

    public void PlayDeathClip()
    {
        PlayClip(deadClip, deadVolume);
    }

    public void PlayHorseGallopClip()
    {
        PlayClip(horseGallopClip, horseGallopVolume);
    }

    public void PlayBtnClickErrorClip()
    {
        PlayClip(btnClickErrorClip, btnClickErrorVolume);
    }

    public void StopHorseGallopClip()
    {
      //GetComponent<AudioSource>().Stop();
    }

    private void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}
