using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhoneNavigator : MonoBehaviour
{
    [SerializeField] Animator phoneAnimator;

    [SerializeField] bool open;

    public InputAction phoneKey;
    public InputAction playKey;
    public InputAction phoneNav;
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
        phoneKey.Enable();
        phoneNav.Enable();
        playKey.Enable();
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
        if (phoneKey.triggered)
        {
            open = !open;
            PhoneFlip();
        }

        if (playKey.triggered)
            PlaySelected();


        Vector2 phoneInp = phoneNav.ReadValue<Vector2>();

        if (phoneInp.y == 0)
        {
            phonePressedThisFrame = false;
            return;
        }

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
