using System;
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
        public int _infoIndex;

        public override PlayerInfo Info => _altInfo[_infoIndex];

        public PlayerInfo aliceObj;
        public PlayerInfo reimuObj;
        
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

        public void SwapInfo(Partners partner)
        {
            switch (partner)
            {
                case Partners.Alice:
                    print("please turn into reimu FUCK");
                    //_info = reimuObj;
                    SwitchplayerInfo(reimuObj);
                    LoveMeter.Instance.currentPartner = Partners.Reimu;
                    break;
                case Partners.Reimu:
                    print("please turn into alice FUCk");
                    //_info = aliceObj;
                    SwitchplayerInfo(aliceObj);
                    LoveMeter.Instance.currentPartner = Partners.Alice;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(partner), partner, null);
            }
        }
    }
}