using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    [SerializeField] Animator phoneAnimator;

    [SerializeField] bool open;

    public AudioSource AudioSource;

    [Header("Song List")]
    public Transform content;
    public Transform viewPort;
    public GameObject SongListPrefab;

    List<SongListItem> songsList = new();

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
        List<AudioClip> clips = MusicPlaylist.Instance.GetPlayList();

        foreach (AudioClip clip in clips)
        {
            SongListItem con = Instantiate(SongListPrefab, content).GetComponent<SongListItem>();

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

        if (phonePressedThisFrame) return;

        if (phoneInp.y < 0)
        {
            currentSelectedSong++;

            if (currentSelectedSong >= MusicPlaylist.Instance.GetPlayList().Count)
                currentSelectedSong = 0;
        }
        else if (phoneInp.y > 0)
        {
            currentSelectedSong--;


            if (currentSelectedSong < 0)
                currentSelectedSong = MusicPlaylist.Instance.GetPlayList().Count - 1;
        }

        UpdateSelected();

        phonePressedThisFrame = true;
    }

    void UpdatePlaying()
    {
        int ind = MusicPlaylist.Instance.currentSongIndex;

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

        SoundManager.Instance.SwapBGM(MusicPlaylist.Instance.GetSongAtIndex(currentSelectedSong));
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
