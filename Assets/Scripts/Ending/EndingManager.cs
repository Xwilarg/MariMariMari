using UnityEngine;

public class EndingManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.instance.PlayMusic(FModReferences.instance.partnerSelect);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
