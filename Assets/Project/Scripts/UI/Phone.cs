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

    public static bool IsOpen { get; private set; }

    List<SongListItem> songsList = new();

    int currentSelectedSong = 0;

    private void Awake()
    {
        IsOpen = false;
        PhoneFlip();

        MusicPlaylist.OnSongChanged += SongChanged;
    }

    private void OnDestroy()
    {
        MusicPlaylist.OnSongChanged -= SongChanged;
    }

    private void Start()
    {
        CreateList();
        UpdatePlaying();
        UpdateSelected();
    }

    private void Update()
    {
        if (GameManager.Instance.gameState != GameManager.GameState.Playing) return;

        if (KeyBinds.Instance.phoneOpenInput.triggered)
        {
            open = !open;
            PhoneFlip();
        }

        if (!open) return;

        if (KeyBinds.Instance.interactInput.triggered)
            PlaySelected();

        float scroll = KeyBinds.Instance.scrollInput.ReadValue<float>();

        if (scroll == 0) return;

        if (scroll < 0)
        {
            currentSelectedSong++;

            if (currentSelectedSong >= MusicPlaylist.Instance.GetPlayList().Count)
                currentSelectedSong = 0;
        }
        else if (scroll > 0)
        {
            currentSelectedSong--;

            if (currentSelectedSong < 0)
                currentSelectedSong = MusicPlaylist.Instance.GetPlayList().Count - 1;
        }

        UpdateSelected();
    }


    void SongChanged(int index)
    {
        if (index >= songsList.Count) return;

        foreach (SongListItem song in songsList)
            song.UpdatePlaying(false);

        songsList[index].UpdatePlaying(true);
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
        SoundManager.Instance.SwapBGM(MusicPlaylist.Instance.GetSongAtIndex(currentSelectedSong));
    }

    void PhoneFlip()
    {
        IsOpen = open;

        if (open)
            phoneAnimator.Play("PhoneOpen");
        else
            phoneAnimator.Play("PhoneClosed");

        AudioSource.Play();
    }
}
