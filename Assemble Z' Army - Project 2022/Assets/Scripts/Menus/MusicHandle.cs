using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicHandle : MonoBehaviour
{
    [SerializeField] private Sprite spriteOff;
    [SerializeField] private Sprite spriteOn;

    [SerializeField] private Button button = null;

    [SerializeField] private AudioSource audioSource = null;

    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private TMP_Text volumeTextUI = null;

    bool state = false;

    public void VolumeSlider(float volume)
    {
        volumeTextUI.text = volume.ToString("0.0");
        float volumeValue = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeValue", volumeValue);

        audioSource.volume = volumeValue;

        if (volumeValue == 0)
        {
            button.image.sprite = spriteOff;
        }
        else
        {
            button.image.sprite = spriteOn;
        }
    }

    public void ChangeStateMusic()
    {
        if (!state)
        {
            PauseMusic();
            state = true;
            button.image.sprite = spriteOff;
        }
        else
        {
            PlayMusic();
            state = false;
            button.image.sprite = spriteOn;
        }
    }

    public void PauseMusic()
    {
        audioSource.Pause();
    }

    public void PlayMusic()
    {
        audioSource.Play();
    }
}
