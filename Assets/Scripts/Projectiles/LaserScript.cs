using System;
using FMOD.Studio;
using UnityEditor;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    private EventInstance instance;
    
    // sound effect stuff.
    void Start()
    {
        instance = AudioManager.instance.CreateEventInstance(FModReferences.instance.shoot);
        instance.setParameterByName("SHOOT", 2);
        instance.start();
    }
    
    private void OnDestroy()
    {
        instance.stop(STOP_MODE.ALLOWFADEOUT);
        instance.release();
    }
}
