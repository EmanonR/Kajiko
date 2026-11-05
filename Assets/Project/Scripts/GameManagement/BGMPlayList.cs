using System.Collections.Generic;
using UnityEngine;

public class BGMPlayList : MonoBehaviour
{
    public List<AudioClip> PlayList = new();

    public static BGMPlayList instance;

    public int currentSongIndex;

    private void Awake()
    {
        if (instance == null)
            instance = this;
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
