using UnityEngine;

[System.Serializable]
public class Sound
{
    
    public string name;

    public AudioClip clip;

    [Range(0f,1f)]
    public float volume;
    [Range(0.03f, 5f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource audioSource;

    [System.Serializable]
    public enum SoundType { musica, efectoSonido, };
    public SoundType tipoDeSonido;

}
