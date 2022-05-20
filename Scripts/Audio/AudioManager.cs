using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public bool muteMusic = false, muteSFX = false;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound sonido in sounds)
        {
            sonido.audioSource = gameObject.AddComponent<AudioSource>();
            sonido.audioSource.clip = sonido.clip;
            sonido.audioSource.volume = sonido.volume;
            sonido.audioSource.loop = sonido.loop;
            sonido.audioSource.pitch = sonido.pitch;
            if (sonido.tipoDeSonido == Sound.SoundType.musica)
            {
                sonido.audioSource.mute = muteMusic;
            }
            else { sonido.audioSource.mute = muteSFX; }      
        }
    }

    private void Start()
    {
       // Play("MusicMenu");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.audioSource.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.audioSource.Stop();
    }

    public void StopAll()
    {
        foreach (Sound s in sounds)
        {
            s.audioSource.Stop();
        }
    }

    public void SetVolume(string name, float volume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.audioSource.volume = volume;
    }

    public Sound GetSoundByName(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s;
    }

    public void SetVolumenMusica(float volume)
    {
        foreach (Sound s in sounds)
        {
            if (s.tipoDeSonido == Sound.SoundType.musica)
            {
                s.audioSource.volume = volume;
            }
        }
    }

    public void SetVolumenSFX(float volume)
    {
        foreach (Sound s in sounds)
        {
            if (s.tipoDeSonido == Sound.SoundType.efectoSonido)
            {
                s.audioSource.volume = volume;
            }
        }
    }

    public bool MuteUnmuteMusic()
    {
        if (muteMusic)
        {
            foreach (Sound s in sounds)
            {
                if (s.tipoDeSonido == Sound.SoundType.musica)
                {
                    s.audioSource.mute = false;
                }                
            }
            muteMusic = false;
            
        }
        else
        {
            foreach (Sound s in sounds)
            {
                if (s.tipoDeSonido == Sound.SoundType.musica)
                {
                    s.audioSource.mute = true;
                }
            }
            muteMusic = true;
        }
        return muteMusic;
    }

    public bool MuteUnmuteSFX()
    {
        if (muteSFX)
        {
            foreach (Sound s in sounds)
            {
                if (s.tipoDeSonido == Sound.SoundType.efectoSonido)
                {
                    s.audioSource.mute = false;
                }
            }
            muteSFX = false;

        }
        else
        {
            foreach (Sound s in sounds)
            {
                if (s.tipoDeSonido == Sound.SoundType.efectoSonido)
                {
                    s.audioSource.mute = true;
                }
            }
            muteSFX = true;
        }
        return muteSFX;
    }

    [ContextMenu("Actualizar datos de sonidos")]
    public void ActualizarDatosSonidos()
    {
        foreach (Sound sonido in sounds)
        {
            sonido.audioSource.clip = sonido.clip;
            sonido.audioSource.volume = sonido.volume;
            sonido.audioSource.loop = sonido.loop;
            sonido.audioSource.pitch = sonido.pitch;
            if (sonido.tipoDeSonido == Sound.SoundType.musica)
            {
                sonido.audioSource.mute = muteMusic;
            }
            else { sonido.audioSource.mute = muteSFX; }
        }
    }

}
