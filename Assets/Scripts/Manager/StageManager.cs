using System.Collections;
using TMPro;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public TMP_Text ReadyText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // play stage music
        AudioManager.instance.PlayMusic(FModReferences.instance.stage);
        
        // disable ready text
        StartCoroutine(coroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator coroutine()
    {
        yield return new WaitForSeconds(6f);
        ReadyText.enabled = false;
    }
}
