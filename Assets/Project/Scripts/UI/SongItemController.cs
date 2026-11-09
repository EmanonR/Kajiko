using TMPro;
using UnityEngine;

public class SongItemController : MonoBehaviour
{
    public TMP_Text songTitle;
    public GameObject playingIcon;
    public GameObject selectedIcon;

    public void UpdateSong(string title)
    {
        songTitle.text = title;
    }

    public void UpdatePlaying(bool playing)
    {
        playingIcon.SetActive(playing);
    }

    public void UpdateSelected(bool selected)
    {
        selectedIcon.SetActive(selected);
    }
}
