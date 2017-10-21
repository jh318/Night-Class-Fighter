using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public static AudioManager master;

    public AudioClip[] music;
    public AudioMixerGroup musicMixer;
    public int musicSourcesCount = 5;
    private int currentMusicSource = 0;
    private AudioSource[] musicSources;

    public AudioClip[] ambient;
    public AudioMixerGroup ambientMixer;
    public int ambientSourcesCount = 5;
    private int currentAmbientSource = 0;
    private AudioSource[] ambientSource;

    public AudioClip[] clips;
    public AudioMixerGroup sfxMixer;
    public int sfxSourcesCount = 5;
    private int currentSFXSource = 0;
    private AudioSource[] sfxSources;

    void Awake()
    {
        if (master == null)
        {
            master = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        musicSources = new AudioSource[musicSourcesCount];
        for (int i = 0; i < musicSourcesCount; i++)
        {
            GameObject g = new GameObject("musicSource" + 1);
            g.transform.parent = transform;
            AudioSource s = g.AddComponent<AudioSource>();
            s.loop = true;
            s.outputAudioMixerGroup = musicMixer;
            musicSources[i] = s;
        }
        sfxSources = new AudioSource[sfxSourcesCount];
        for (int i = 0; i < sfxSourcesCount; ++i)
        {
            GameObject g = new GameObject("sfxSources" + 1);
            g.transform.parent = transform;
            AudioSource s = g.AddComponent<AudioSource>();
            s.outputAudioMixerGroup = sfxMixer;
            sfxSources[i] = s;
        }
        ambientSource = new AudioSource[ambientSourcesCount];
        for (int i = 0; i < ambientSourcesCount; ++i)
        {
            GameObject g = new GameObject("ambient" + 1);
            g.transform.parent = transform;
            AudioSource s = g.AddComponent<AudioSource>();
            s.loop = true;
            s.outputAudioMixerGroup = ambientMixer;
            ambientSource[i] = s;
        }
    }

    public static void AmbientSounds(string soundName, float pitch =1, float volume =1, float pan = 0)
    {
        if (master == null) return;
        AudioClip sound = null;
        for (int i = 0; i < master.clips.Length; ++i)
        {
            if (master.clips[i].name == soundName) sound = master.clips[i];
        }

        if (sound == null) return;

        AudioSource source = master.sfxSources[master.currentSFXSource];
        master.currentSFXSource = (master.currentSFXSource + 1) % master.sfxSourcesCount;

        source.clip = sound;
        source.pitch = pitch;
        source.volume = volume;
        source.panStereo = pan;
        source.Play();
    }

    //SFX group
    public static void PlayVariedEffect(string clipName, float variation = 0.1f, float pan = 0)
    {
        PlayEffect(
            clipName,
            Random.Range(1 - variation, 1 + variation),
            Random.Range(1 - variation, 1 + variation),
            pan);
    }

    public static void PlayEffect(string clipName, float pitch = 1, float volume = 1, float pan = 0)
    {
        if (master == null) return;
        AudioClip clip = null;
        for (int i = 0; i < master.clips.Length; ++i)
        {
            if (master.clips[i].name == clipName) clip = master.clips[i];
        }

        if (clip == null) return;

        AudioSource source = master.sfxSources[master.currentSFXSource];
        master.currentSFXSource = (master.currentSFXSource + 1) % master.sfxSourcesCount;

        source.clip = clip;
        source.pitch = pitch;
        source.volume = volume;
        source.panStereo = pan;
        source.Play();
    }

    //Music Group
    public static void PlayMusic(string songName)
    {
        if (master == null || master.musicSources == null) return;
        AudioClip song = null;
        for (int i = 0; i < master.music.Length; ++i)
        {
            if (master.music[i].name == songName) song = master.music[i];
        }

        if (song == null) return;

        AudioSource source = master.musicSources[master.currentMusicSource];
        source.clip = song;
        source.Play();
    }

    public static void CrossfadeMusic(AudioClip clip, float duration)
    {
        AudioSource nextSource = master.musicSources[(master.currentMusicSource + 1) % 2];
        nextSource.clip = clip;
        master.StartCoroutine("CrossfadeMusic", duration);
    }

    IEnumerator CorssfadeMusic(float duration)
    {
        AudioSource a = musicSources[currentMusicSource];
        currentMusicSource = (currentMusicSource + 1) % 2;
        AudioSource b = musicSources[currentMusicSource];
        b.volume = 0;
        b.Play();

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float frac = t / duration;
            a.volume = 1 - frac;
            b.volume = frac;

            yield return new WaitForEndOfFrame();
        }

        a.volume = 0;
        a.Stop();
        b.volume = 1;
    }

    public static void StopPlaying(string songName)
    {
        AudioClip song = null;
        for (int i = 0; i < master.music.Length; ++i)
        {
            if (master.music[i].name == songName) song = master.music[i];
        }

        if (song == null) return;

        AudioSource source = master.musicSources[master.currentMusicSource];
        source.clip = song;
        source.Stop();
    }

}
