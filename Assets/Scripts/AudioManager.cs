using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    const string MIXER_MUSIC = "MusicVolume";

    const string MIXER_SFX = "SFXVolume";


    public AudioMixer audioMixer;

    public AudioSource musicAudioSource;

    public AudioSource sfxAudioSource;

    public AudioClip bgMusic;

    public AudioClip bgMusicLowPass;

    public AudioClip checkpointSound;

    public AudioClip jumpSound;

    public AudioClip[] runSound;

    //public AudioClip[] enterSlimeSound;

    //public AudioClip[] exitSlimeSound;

    public AudioClip enterSlimeSound;

    public AudioClip pushBackSound;

    private AudioMixerSnapshot normal;

    private AudioMixerSnapshot inSlime;

    // Start is called before the first frame update
    void Start()
    {
        normal = audioMixer.FindSnapshot("NormalSound");

        inSlime = audioMixer.FindSnapshot("SlimeSound"); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OutSlimeSoundEffect()
    {
        float time;

        //audioMixer.FindSnapshot

        //normal.TransitionTo(0.5f);

        /*
        while (musicAudioSource.pitch < 1)
        {
            musicAudioSource.pitch = musicAudioSource.pitch + Time.deltaTime;
        }
        */

        /*
        if (musicAudioSource.pitch < 1)
        {
            musicAudioSource.pitch = musicAudioSource.pitch + Time.deltaTime;

            if(Mathf.Abs(musicAudioSource.pitch - 1) < 0.01f)
            {
                musicAudioSource.pitch = 1;
            }
        }
        */

        

        if(musicAudioSource.clip != bgMusic)
        {
            time = musicAudioSource.time;

            musicAudioSource.clip = bgMusic;

            musicAudioSource.time = time;

            musicAudioSource.Play();
        }
    }

    public void InSlimeSoundEffect()
    {
        float time;

        //audioMixer.FindSnapshot

        //inSlime.TransitionTo(0.25f);

        /*
        while(musicAudioSource.pitch > 0.95f)
        {
            musicAudioSource.pitch = musicAudioSource.pitch - Time.deltaTime;
        }
        */

        /*
        if(musicAudioSource.pitch > 0.8f)
        {
            musicAudioSource.pitch = musicAudioSource.pitch - (Time.deltaTime / 10);
        }
        */

        if (musicAudioSource.clip != bgMusicLowPass)
        {
            time = musicAudioSource.time;

            musicAudioSource.clip = bgMusicLowPass;

            musicAudioSource.time = time;

            musicAudioSource.Play();
        }
    }

    public void MuteUnmuteMusic()
    {
        float volume;

        audioMixer.GetFloat(MIXER_MUSIC, out volume);

        //Debug.Log("volume = " + volume);

        
        if (volume != -80)
        {
            audioMixer.SetFloat(MIXER_MUSIC, -80);
        }
        else
        {
            audioMixer.SetFloat(MIXER_MUSIC, 0);
        }
        
    }

    public void MuteUnmuteSFX()
    {
        float volume;

        audioMixer.GetFloat(MIXER_SFX, out volume);

        //Debug.Log("volume = " + volume);


        if (volume != -80)
        {
            audioMixer.SetFloat(MIXER_SFX, -80);
        }
        else
        {
            audioMixer.SetFloat(MIXER_SFX, 0);
        }
    }


    public void PlayCheckpointSound()
    {
        sfxAudioSource.PlayOneShot(checkpointSound);
    }

    public void PlayJumpSound()
    {
        sfxAudioSource.PlayOneShot(jumpSound);
    }

    public void PlayPushBackSound()
    {
        sfxAudioSource.PlayOneShot(pushBackSound);
    }

    public void RunSound()
    {
        AudioClip clip = GetRandomClip(runSound);

        sfxAudioSource.PlayOneShot(clip);
    }

    public void EnterSlimeSound()
    {
        //AudioClip clip = GetRandomClip(enterSlimeSound);

        sfxAudioSource.PlayOneShot(enterSlimeSound);
    }

    /*
    public void ExitSlimeSound()
    {
        AudioClip clip = GetRandomClip(exitSlimeSound);

        sfxAudioSource.PlayOneShot(clip);
    }
    */

    private AudioClip GetRandomClip(AudioClip[] clip)
    {
        return clip[UnityEngine.Random.Range(0, clip.Length)];
    }
}
