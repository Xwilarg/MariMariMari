using System.Collections.Generic;
using System.Linq;
using TouhouPride.Love;
using TouhouPride.Manager;
using TouhouPride.SO;
using UnityEngine;

namespace TouhouPride.Player
{
	public class FollowerController : PlayerController
	{
        [SerializeField]
        private PlayerInfo[] _altInfo;

        private List<PlayerInfo> _allPlayers = new();
        private int _infoIndex;
        public int InfoIndex
        {
            set
            {
                Debug.Log($"[FLLW] Changed from {_allPlayers[_infoIndex].Name} to {_allPlayers[value].Name}");
                _infoIndex = value;
                _follower.UpdateProperties();
                _anim.runtimeAnimatorController = Info.CharacterAnimator;
            }
            get
            {
                return _infoIndex;
            }
        }

        public override PlayerInfo Info => _allPlayers[InfoIndex];
        
        protected override void Awake()
        {
            base.Awake();
            _allPlayers.Add(_info);
            _allPlayers.AddRange(_altInfo);

            _follower = GetComponent<Follower>();

            InfoIndex = _allPlayers.IndexOf(_allPlayers.First(x => x.Name == StaticData.CharacterName));
            _anim.runtimeAnimatorController = Info.CharacterAnimator;
        }

        protected override void Start()
        {
            base.Start();
            PlayerManager.Instance.Register(this);
            _follower.SetInfo(true);

            foreach (var p in _allPlayers)
            {
                LoveMeter.Instance.Init(p.Name, p.Color);
            }
        }

        protected override void StartInternal()
        { }

        public void SwapInfo(string target)
        {
            InfoIndex = _allPlayers.IndexOf(_allPlayers.First(x => x.Name == target));
        }
    }
}