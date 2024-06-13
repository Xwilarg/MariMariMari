using System;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> _eventInstances;
    private EventInstance music;
    
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Audio Manager in the scene!");
        }
        instance = this;

        _eventInstances = new List<EventInstance>();
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);  
    }

    public void PlayMusic(EventReference music)
    {
        // RuntimeManager.PlayOneShot();
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
        music = this.CreateEventInstance(FModReferences.instance.stage);
        music.start();
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

    private void OnDestroy()
    {
        CleanUp();
    }
}
