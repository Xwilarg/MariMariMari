using System.Collections.Generic;
using System.Linq;
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

        public override PlayerInfo Info => _altInfo[_infoIndex];

        protected override void Awake()
        {
            base.Awake();
            _allPlayers.Add(_info);
            _allPlayers.AddRange(_altInfo);

            _infoIndex = _allPlayers.IndexOf(_allPlayers.First(x => x.Name == StaticData.CharacterName));
        }

        protected override void Start()
        {
            InputsManager.Instance.Register(this);
            GetComponent<Follower>().SetInfo(true);
        }
    }
}