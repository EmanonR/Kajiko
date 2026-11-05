using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer;

    [Range(0, 1)]
    public float masterVolume;
    [Range(0, 1)]
    public float bgmVolume;
    [Range(0, 1)] 
    public float sfxVolume;
    [Range(0, 1)] 
    public float environmentVolume;
    [Range(0, 1)] 
    public float voiceVolume;
    [Range(0, 1)]
    public float uiVolume;

    bool onBGMChannel1 = true; //True is 1, false is 2

    float bgm1Lerp = 1;
    float bgm2Lerp = 0;
    float currentBGMLength;
    bool lerpingBGMs;

    AudioSource BGM1;
    AudioSource BGM2;

    private void Awake()
    {
        GameObject bg1 = new();
        AudioSource ao1 = bg1.AddComponent<AudioSource>();
        ao1.outputAudioMixerGroup = mixer.FindMatchingGroups("BGM1")[0];

        BGM1 = ao1;

        GameObject bg2 = new();
        AudioSource ao2 = bg2.AddComponent<AudioSource>();
        ao2.outputAudioMixerGroup = mixer.FindMatchingGroups("BGM2")[0];

        BGM2 = ao2;

        ao1.clip = BGMPlayList.instance.GetSongAtIndex(0);
        ao1.Play(); 
        ao2.Play();
    }

    public void SwapBGM(AudioClip newClip)
    {
        lerpingBGMs = true;
        currentBGMLength = newClip.length;

        onBGMChannel1 = !onBGMChannel1;

        if (onBGMChannel1)
            BGM1.clip = newClip;
        else
            BGM2.clip = newClip;

        BGM1.Play();
        BGM2.Play();
    }

    private void Update()
    {
        if (lerpingBGMs)
            LerpBGM();

        UpdateVolumes();
    }

    void LerpBGM()
    {
        float lerpTime = .5f;

        if (onBGMChannel1)
        {
            bgm1Lerp = Mathf.Clamp(bgm1Lerp += Time.deltaTime / lerpTime, 0, 1);
            bgm2Lerp = Mathf.Clamp(bgm2Lerp -= Time.deltaTime / lerpTime, 0, 1);

            if (bgm1Lerp == 1 && bgm2Lerp == 0) lerpingBGMs = false;
        }
        else
        {
            bgm2Lerp = Mathf.Clamp(bgm2Lerp += Time.deltaTime / lerpTime, 0, 1);
            bgm1Lerp = Mathf.Clamp(bgm1Lerp -= Time.deltaTime / lerpTime, 0, 1);

            if (bgm1Lerp == 0 && bgm2Lerp == 1) lerpingBGMs = false;
        }
    }

    void UpdateVolumes()
    {
        mixer.SetFloat("MasterVolume", VolumeFix(masterVolume));
        mixer.SetFloat("BGMVolume", VolumeFix(bgmVolume));
        mixer.SetFloat("SFXVolume", VolumeFix(sfxVolume));
        mixer.SetFloat("EnvironmentVolume", VolumeFix(environmentVolume));
        mixer.SetFloat("VoiceVolume", VolumeFix(voiceVolume));
        mixer.SetFloat("UIVolume", VolumeFix(uiVolume));


        mixer.SetFloat("bgm1Volume", VolumeFix(bgm1Lerp));
        mixer.SetFloat("bgm2Volume", VolumeFix(bgm2Lerp));
    }

    float VolumeFix(float inp)
    {
        return Mathf.Lerp(-80, 0, Mathf.Log10(inp) + 1);
    }
}
