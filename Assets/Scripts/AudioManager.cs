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

    public AudioClip checkpointSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void muteUnmuteMusic()
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

    public void muteUnmuteSFX()
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


    public void playCheckpointSound()
    {
        sfxAudioSource.PlayOneShot(checkpointSound);
    }
}
