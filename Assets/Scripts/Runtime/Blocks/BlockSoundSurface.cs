using UnityEngine;

public class BlockSoundSurface : MonoBehaviour
{
    [SerializeField] private SoundSurface surface;
    public SoundSurface GetSoundSurface()
    {
        return surface;
    }
}
