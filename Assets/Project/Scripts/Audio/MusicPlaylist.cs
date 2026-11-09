using System.Collections.Generic;
using UnityEngine;

public class MusicPlaylist : MonoBehaviour
{
    public List<AudioClip> PlayList = new();

    public int currentSongIndex;

    public static MusicPlaylist Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
            return;
        }
    }

    public List<AudioClip> GetPlayList()
    {
        return PlayList;
    }

    public AudioClip GetNextSong()
    {
        currentSongIndex++;
        return GetSongAtIndex(currentSongIndex);
    }

    public AudioClip GetPrevSong()
    {
        currentSongIndex--;
        return GetSongAtIndex(currentSongIndex);
    }

    public AudioClip GetSongAtIndex(int i)
    {
        currentSongIndex = i;

        if (currentSongIndex >= PlayList.Count)
            currentSongIndex = 0;

        if (currentSongIndex < 0)
            currentSongIndex = PlayList.Count - 1;

        return PlayList[currentSongIndex];
    }
}
