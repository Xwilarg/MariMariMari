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
    [field: SerializeField] public EventReference boss { get; private set; }
    [field: SerializeField] public EventReference bossCutscene { get; private set; }
    [field: SerializeField] public EventReference ending { get; private set; }
    
    [field: Header("Sound Effects")] 
    [field: SerializeField] public EventReference shoot { get; private set; }
    [field: SerializeField] public EventReference dash { get; private set; }
    [field: SerializeField] public EventReference hit { get; private set; }
    [field: SerializeField] public EventReference defeat { get; private set; }
    [field: SerializeField] public EventReference orb { get; private set; }
    [field: SerializeField] public EventReference bomb { get; private set; }
    [field: SerializeField] public EventReference menuMove { get; private set; }
}
