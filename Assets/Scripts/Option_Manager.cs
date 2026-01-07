using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;


public class Option_Manager : MonoBehaviour
{
    [Header("---Component---")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioMixer mixer;

    [Header("---Setting / Sound---")]
    [SerializeField] private bool isMasterOn;
    [SerializeField] private bool isBGMOn;
    [SerializeField] private bool isSFXOn;
    [SerializeField] private float masterVolume;
    [SerializeField] private float bgmVolume;
    [SerializeField] private float sfxVolume;
    public enum SoundType { Master, BGM, SFX };


    #region Sound Option
    /// <summary>
    /// 사운드의 On Off 여부 체크
    /// </summary>
    /// <param name="isOn"></param>
    /// <param name="type"></param>
    public void SoundOnOff(bool isOn, SoundType type)
    {
        switch (type)
        {
            case SoundType.Master:
                isMasterOn = isOn;
                break;

            case SoundType.BGM:
                isBGMOn = isOn;
                break;

            case SoundType.SFX:
                isSFXOn = isOn;
                break;
        }

        ApplySound(type);
    }

    /// <summary>
    /// 사운드의 크기 체크
    /// </summary>
    /// <param name="value"></param>
    /// <param name="type"></param>
    public void SoundSetting(float value, SoundType type)
    {
        float volume = value <= 0.0001f ? -80f : Mathf.Log10(value) * 20f;
        switch (type)
        {
            case SoundType.Master:
                masterVolume = volume;
                break;

            case SoundType.BGM:
                bgmVolume = volume;
                break;

            case SoundType.SFX:
                sfxVolume = volume;
                break;
        }

        ApplySound(type);
    }

    /// <summary>
    /// 변경된 사운드를 실제로 적용하는 함수
    /// </summary>
    /// <param name="type"></param>
    private void ApplySound(SoundType type)
    {
        switch (type)
        {
            case SoundType.Master:
                mixer.SetFloat("MasterVolume", isMasterOn ? masterVolume : -80f);
                break;

            case SoundType.BGM:
                mixer.SetFloat("BGMVolume", isBGMOn ? bgmVolume : -80f);
                break;

            case SoundType.SFX:
                mixer.SetFloat("SFXVolume", isSFXOn ? sfxVolume : -80f);
                break;
        }
    }
    #endregion


    #region FPS
    #endregion
}
