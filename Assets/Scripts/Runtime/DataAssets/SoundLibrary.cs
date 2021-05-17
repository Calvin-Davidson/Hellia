using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SoundLibrary", order = 1)]
public class SoundLibrary : ScriptableObject
{
    public SoundSurface surface;
    public AudioClip[] clips;
}
