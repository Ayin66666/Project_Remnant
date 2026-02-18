using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;


public class OptionManager : MonoBehaviour
{
    [Header("---Component---")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private GameObject optionUI;

    [Header("---Setting / Sound---")]
    [SerializeField] private bool isMasterOn;
    [SerializeField] private bool isBGMOn;
    [SerializeField] private bool isSFXOn;
    [SerializeField] private float masterVolume;
    [SerializeField] private float bgmVolume;
    [SerializeField] private float sfxVolume;
    public enum SoundType 
    { 
        Master, BGM, SFX 
    };

    [Header("---FPS & V-Sync---")]
    [SerializeField]  private List<int> fps = new List<int> { 30, 60, 90, 120, -1 };
    [SerializeField] private bool isVSyncOn;

    [Header("---Post Processing---")]
    [SerializeField] private bool isPostOn;
    [SerializeField] private bool isPost_BloomOn;
    [SerializeField] private bool isPost_VignetteOn;

    [Header("---Anti Aliasing---")]
    [SerializeField] private AntiAliasingType antiAliasing;
    [SerializeField] private UniversalAdditionalCameraData cam;
    public enum AntiAliasingType
    {
        Off, FXAA, SMAA,
    }


    public void OptionOnOff()
    {
        optionUI.SetActive(!optionUI.activeSelf);
    }


    #region Sound Option
    public void ToggleMaster(bool isOn)
    {
        SoundOnOff(isOn, SoundType.Master);
    }

    public void ToggleBGM(bool isOn)
    {
        SoundOnOff(isOn, SoundType.BGM);
    }

    public void ToggleSFX(bool isOn)
    {
        SoundOnOff(isOn, SoundType.SFX);
    }

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

    public void SetMasterVolume(float value)
    {
        SoundSetting(value, SoundType.Master);
    }

    public void SetBGMVolume(float value)
    {
        SoundSetting(value, SoundType.BGM);
    }

    public void SetSFXVolume(float value)
    {
        SoundSetting(value, SoundType.SFX);
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
    /// <summary>
    /// 프레임 조절 30 ~ 120
    /// </summary>
    /// <param name="value"></param>
    public void SetFPS(int value)
    {
        if (value < 0 || value >= fps.Count) 
            Debug.Log($"프레임레이트 사이즈 오버 / 체크 필요 {value}");
        else 
            Application.targetFrameRate = fps[value];
    }

    public void VSync(bool isOn)
    {
        // 1 = On
        // 2 = Off
        isVSyncOn = isOn;
        QualitySettings.vSyncCount = isOn ? 1 : 0;
    }
    #endregion


    #region Post Processing & Anti Aliasing
    
    // 포스트 프로세싱 On/Off 는 여기에서는 값만 제어함!
    
    // 로드 과정에서 Stage_Manager 같은 해당 씬을 제어하는 매니저에게 알림을 주고
    // 거기서 자기들이 가지고 있는 volume 컴포넌트의 값을 바꾸게 하도록 구현
    // 이때 제어는 컴포넌트나 오브젝트 OnOff 대신 weight 값을 0~1로 제어하도록 변경

    // On/Off 의 경우 연산 과정에서 일시적으로 프레임이 튀어버림
    // 단, 성능 부분에서 최소 계산값이 있긴 함! 다만 무시해도 되는 수준

    /// <summary>
    /// 안티 얼라이싱 조절
    /// </summary>
    /// <param name="value"></param>
    public void AntiAliasing_Setting(int value)
    {
        antiAliasing = (AntiAliasingType)value;
        ApplyAntiAliasing(antiAliasing);
    }

    public void ApplyAntiAliasing(AntiAliasingType type)
    {
        // 여기 사용된 기능은 스위치식 C# 8.0 이상부터 사용 가능
        cam.antialiasing = type switch
        {
            AntiAliasingType.Off => AntialiasingMode.None,
            AntiAliasingType.FXAA => AntialiasingMode.FastApproximateAntialiasing,
            AntiAliasingType.SMAA => AntialiasingMode.SubpixelMorphologicalAntiAliasing,
            _ => AntialiasingMode.None
        };
    }

    /// <summary>
    /// 포스트 프로세싱 On/Off
    /// </summary>
    /// <param name="isOn"></param>
    public void Post_Postprocessing(int value)
    {
        isPostOn = value == 0 ? false : true;
        // stage_Manager의 OnOff 함수 호출
    }

    /// <summary>
    /// 포스트 프로세싱 - 볼륨 On/Off
    /// </summary>
    /// <param name="isOn"></param>
    public void Post_Bloom(int value)
    {
        isPost_BloomOn = value == 0 ? false : true;
        // stage_Manager의 OnOff 함수 호출
    }

    /// <summary>
    /// 포스트 프로세싱 - 모션블러 On/Off
    /// </summary>
    /// <param name="isOn"></param>
    public void Post_Vignette(int value)
    {
        isPost_VignetteOn = value == 0 ? false : true;
        // stage_Manager의 OnOff 함수 호출
    }
    #endregion


    #region Save & Load
    /// <summary>
    /// 세이브에 필요한 데이터 전달
    /// </summary>
    public void SaveData()
    {

    }

    /// <summary>
    /// 저장된 옵션 설정 값 로드 후 적용
    /// </summary>
    public void LoadData()
    {

    }
    #endregion
}
