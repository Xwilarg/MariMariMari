using FMODUnity;
using UnityEngine;

public class FModReferences : MonoBehaviour
{
    public static FModReferences instance { get; private set; }
    
    private void Awake()
    {
        instance = this;
    }
    
    // define references for commonly needed sound effects and music
    // depending on the size, we might wanna reorganize this / do something else.
    [field: SerializeField] public EventReference stage { get; private set; }
    [field: SerializeField] public EventReference partnerSelect { get; private set; }
}
