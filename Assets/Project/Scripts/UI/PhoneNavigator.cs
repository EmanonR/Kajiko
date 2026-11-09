using System.Collections.Generic;
using UnityEngine;

public class PhoneNavigator : MonoBehaviour
{
    [SerializeField] Animator phoneAnimator;

    [SerializeField] bool open;

    public AudioSource AudioSource;

    [Header("Song List")]
    public Transform content;
    public Transform viewPort;
    public GameObject SongListPrefab;

    List<SongItemController> songsList = new();

    int currentSelectedSong = 0;
    bool phonePressedThisFrame;

    private void Awake()
    {
        PhoneFlip();
    }

    private void Start()
    {
        CreateList();
        UpdatePlaying();
        UpdateSelected();
    }

    void CreateList()
    {
        List<AudioClip> clips = BGMPlayList.instance.GetPlayList();

        foreach (AudioClip clip in clips)
        {
            SongItemController con = Instantiate(SongListPrefab, content).GetComponent<SongItemController>();

            con.UpdateSong(clip.name);
            songsList.Add(con);
        }
    }

    private void Update()
    {
        if (KeyBinds.Instance.phoneOpenInput.triggered)
        {
            open = !open;
            PhoneFlip();
        }

        if (!open) return;

        if (KeyBinds.Instance.interactInput.triggered)
            PlaySelected();

        Vector2 phoneInp = KeyBinds.Instance.phoneNavigationInput.ReadValue<Vector2>();

        if (phoneInp.y == 0)
        {
            phonePressedThisFrame = false;
            return;
        }

        if (!open) return;

        if (phonePressedThisFrame) return;

        if (phoneInp.y < 0)
        {
            currentSelectedSong++;

            if (currentSelectedSong >= BGMPlayList.instance.GetPlayList().Count)
                currentSelectedSong = 0;
        }
        else if (phoneInp.y > 0)
        {
            currentSelectedSong--;


            if (currentSelectedSong < 0)
                currentSelectedSong = BGMPlayList.instance.GetPlayList().Count - 1;
        }

        UpdateSelected();

        phonePressedThisFrame = true;
    }

    void UpdatePlaying()
    {
        int ind = BGMPlayList.instance.currentSongIndex;

        songsList[ind].UpdatePlaying(true);
    }

    void UpdateSelected()
    {
        foreach (var song in songsList)
        {
            song.UpdateSelected(false);
        }

        songsList[currentSelectedSong].UpdateSelected(true);
    }

    void PlaySelected()
    {
        foreach (var song in songsList)
        {
            song.UpdatePlaying(false);
        }

        songsList[currentSelectedSong].UpdatePlaying(true);

        SoundManager.instance.SwapBGM(BGMPlayList.instance.GetSongAtIndex(currentSelectedSong));
    }

    void PhoneFlip()
    {
        if (open)
            phoneAnimator.Play("PhoneOpen");
        else
            phoneAnimator.Play("PhoneClosed");

        AudioSource.Play();
    }
}
