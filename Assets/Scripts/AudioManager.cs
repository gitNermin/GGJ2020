using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] Sounds;
    public GameObject gameAudio;
    private bool gameEnding = false;
    private float fadeTime = 2f;

    private AudioSource gameMusic;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in Sounds)
        {
            s.Source= gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
            s.Source.playOnAwake = s.PlayOnAwake;
        }
    }
    private void Start()
    {
        gameMusic = gameAudio.GetComponent<AudioSource>();
    }
    public void PlayGameMusic()
    {
        gameMusic.Play();
    }
    public void StopGameMusic()
    {
        gameMusic.Stop();
    }
    public void play(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogError("there is no audio name: " + name);
            return;
        }
        s.Source.Play();
    }
    public void SlowMotion ()
    {
        gameEnding = true;

    }
    void Update()
    {
        if (gameEnding)
        {
            gameMusic.pitch -= 0.5f * Time.deltaTime / fadeTime;
            gameMusic.volume -= 1f * Time.deltaTime / fadeTime;
            if (gameMusic.pitch < 0.5f)
            {
                gameEnding = false;
                StopAndDisableAudio();
            }
        }
    }

    private void StopAndDisableAudio()
    {
        gameMusic.Stop();
        gameMusic.pitch = 1;
        gameMusic.volume = 1;
        //gameMusic.enabled = false;
    }
}
