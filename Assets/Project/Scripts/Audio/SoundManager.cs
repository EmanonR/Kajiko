using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioMixerSnapshot[] snapshots;

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
    float currentBGMTime;
    float crossFadeTime = 1f;
    bool lerpingBGMs;

    AudioSource BGM1;
    AudioSource BGM2;

    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    public void SwapSnapShot(bool paused)
    {
        float[] weights = paused
        ? new float[] { 0f, 1f }
        : new float[] { 1f, 0f };

        mixer.TransitionToSnapshots(snapshots, weights, 0f);
    }

    private void Start()
    {
        GameObject bg1 = new();
        AudioSource ao1 = bg1.AddComponent<AudioSource>();
        ao1.outputAudioMixerGroup = mixer.FindMatchingGroups("BGM1")[0];

        BGM1 = ao1;

        GameObject bg2 = new();
        AudioSource ao2 = bg2.AddComponent<AudioSource>();
        ao2.outputAudioMixerGroup = mixer.FindMatchingGroups("BGM2")[0];

        BGM2 = ao2;

        ao1.clip = MusicPlaylist.Instance.GetSongAtIndex(0);
        currentBGMLength = ao1.clip.length;
        ao1.Play(); 
        ao2.Play();
        ao1.loop = true;
        ao2.loop = true;
    }

    public void SwapBGM(AudioClip newClip)
    {
        currentBGMTime = 0;
        lerpingBGMs = true;
        currentBGMLength = newClip.length;

        onBGMChannel1 = !onBGMChannel1;

        if (onBGMChannel1)
        {
            BGM1.clip = newClip;
            BGM1.Play();
        }
        else
        {
            BGM2.clip = newClip;
            BGM2.Play();
        }
    }

    private void Update()
    {
        if (lerpingBGMs)
            LerpBGM();

        currentBGMTime += Time.unscaledDeltaTime;

        if (currentBGMTime > currentBGMLength - crossFadeTime)
            SwapBGM(MusicPlaylist.Instance.GetNextSong());

        UpdateVolumes();
    }

    void LerpBGM()
    {
        if (onBGMChannel1)
        {
            bgm1Lerp = Mathf.Clamp(bgm1Lerp += Time.unscaledDeltaTime / crossFadeTime, 0, 1);
            bgm2Lerp = Mathf.Clamp(bgm2Lerp -= Time.unscaledDeltaTime / crossFadeTime, 0, 1);

            if (bgm1Lerp == 1 && bgm2Lerp == 0) lerpingBGMs = false;
        }
        else
        {
            bgm2Lerp = Mathf.Clamp(bgm2Lerp += Time.unscaledDeltaTime / crossFadeTime, 0, 1);
            bgm1Lerp = Mathf.Clamp(bgm1Lerp -= Time.unscaledDeltaTime / crossFadeTime, 0, 1);

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
        inp = Mathf.Max(inp, 0.0001f);
        return Mathf.Lerp(-80, 0, Mathf.Log10(inp) + 1);
    }
}
