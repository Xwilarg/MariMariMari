using System.Collections.Generic;
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
            _allPlayers.Add(Info);
            _allPlayers.AddRange(_altInfo);
        }

        protected override void Start()
        {
            base.Start();
            GetComponent<Follower>().SetInfo(true);
        }
    }
}