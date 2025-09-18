using UnityEngine;
using UnityEngine.Audio;

public class Settings : ScriptableObject
{
    public AudioMixer audioMixer;

    public int MasterVolume
    {
        get
        {
            audioMixer.GetFloat("Master", out var val);
            return (int)(val + 10) * 5;
        } 
        set => audioMixer.SetFloat("Master", (value/5f) - 10);
    }

    public int MusicVolume
    {
        get
        {
            audioMixer.GetFloat("Music", out var val);
            return (int)(val + 10) * 5;
        }
        set => audioMixer.SetFloat("Music", (value/5f) - 10);
    }

    public int SfxVolume
    {
        get
        {
            audioMixer.GetFloat("SFX", out var val);
            return (int)(val + 10) * 5;
        }
        set => audioMixer.SetFloat("SFX", (value/5f) - 10);
    }
}
