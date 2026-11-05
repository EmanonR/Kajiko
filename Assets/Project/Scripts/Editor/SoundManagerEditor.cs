using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SoundManager))]
public class SoundManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (Application.isPlaying)
        {
            if (GUILayout.Button("Prev Song"))
            {
                SoundManager manager = (SoundManager)target;
                manager.SwapBGM(BGMPlayList.instance.GetPrevSong());
            }

            if (GUILayout.Button("Next Song"))
            {
                SoundManager manager = (SoundManager)target;
                manager.SwapBGM(BGMPlayList.instance.GetNextSong());
            }
        }
    }
}
