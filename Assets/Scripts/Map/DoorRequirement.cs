using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TouhouPride.Map
{
    public class DoorRequirement : MonoBehaviour, IRequirement<ACharacter>
    {
        [SerializeField]
        private ACharacter[] _requirements;
        private List<ACharacter> _requirementList;

        private void Awake()
        {
            _requirementList = _requirements.ToList();
            foreach (var r in _requirementList)
            {
                r.AddRequirement(this);
            }
        }

        public void Unlock(ACharacter o)
        {
            _requirementList.Remove(o);
            if(!_requirementList.Any())
            {
                Destroy(gameObject);
            }
        }
    }
}