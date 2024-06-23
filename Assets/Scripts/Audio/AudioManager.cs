using System;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> _eventInstances;
    private EventInstance music;

    private EventInstance sound;
    
    private Bus masterBus;
    private Bus musicBus;
    private Bus sfxBus;
    
    public static AudioManager instance { get; private set; }

    public float masterVolume = 1;
    public float sfxVolume = 1;
    public float musicVolume = 1;

    private void Awake()
    {
        FMODUnity.RuntimeManager.LoadBank("SOUND", true);
        FMODUnity.RuntimeManager.LoadBank("MUSIC", true);
        
        // singleton stuff
        if (instance == null)
        {
            instance = this;
        }
        
        else if (instance != this)
        {
            print("More than one Audio Manager in the scene!");
            Destroy(this.gameObject);
            //Debug.LogError("More than one Audio Manager in the scene!");
        }
        
        _eventInstances = new List<EventInstance>();
        
        // bus configuration stuff
        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/MUSIC");
        sfxBus = RuntimeManager.GetBus("bus:/SOUND");
        
        // we want to keep this persistent i think
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        if (FMODUnity.RuntimeManager.HasBankLoaded("SOUND"))
        {
            RuntimeManager.PlayOneShot(sound, worldPos);  
        }
    }

    public void PlayOneShotParam(EventReference sound, Vector3 worldPos, String parameterName, int parameterValue)
    {
        if (FMODUnity.RuntimeManager.HasBankLoaded("SOUND"))
        {
            this.sound = this.CreateEventInstance(sound);
            this.sound.setParameterByName(parameterName, parameterValue);
            this.sound.start();
            this.sound.release();
        }
    }

    public void PlayMusic(EventReference musicRef)
    {
        PLAYBACK_STATE playbackstate;
        music.getPlaybackState(out playbackstate);

        if (playbackstate != FMOD.Studio.PLAYBACK_STATE.PLAYING && FMODUnity.RuntimeManager.HasBankLoaded("MUSIC"))
        {
            music = this.CreateEventInstance(musicRef);
            music.start();
        }
    }
    
    public void PauseMusic()
    {
        music.setPaused(true);
    }

    public void UnPauseMusic()
    {
        music.setPaused(false);
    }

    public void ChangeMusicParameter(String parameterName, int parameterValue)
    {
        music.setParameterByName(parameterName, parameterValue);
    }

    public void StopMusic()
    {
        music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        _eventInstances.Add(eventInstance);
        return eventInstance;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // start playing the stage theme
        // once we have code for initializing the stage itself we'll probably want to move it there.
        // PlayMusic(FModReferences.instance.stage);
    }

    private void CleanUp()
    {
        // stop and release any created event instances.
        for (int i = 0; i < _eventInstances.Count; i++)
        {
            _eventInstances[i].stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            _eventInstances[i].release();
        }
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
        masterBus.setVolume(masterVolume);
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicBus.setVolume(musicVolume);
    }

    public void SetSfxVolume(float volume)
    {
        sfxVolume = volume;
        sfxBus.setVolume(sfxVolume);
    }

    /*
    private void Update()
    {
        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolume);
        sfxBus.setVolume(sfxVolume);
    }
    */
    

    private void OnDestroy()
    {
        music.release();
        sound.release();
        CleanUp();
    }
}
