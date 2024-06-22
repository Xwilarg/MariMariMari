using System.Collections.Generic;
using System.Linq;
using TouhouPride.Manager;
using TouhouPride.Player;
using UnityEngine;
using UnityEngine.UI;

namespace TouhouPride.Love
{
    // forgive me if this is just a given, but we'll probably want to keep the UI logic for this seperate from here.
    public class LoveMeter : MonoBehaviour
    {
        [SerializeField] private RectTransform _loveContainer;
        [SerializeField] private GameObject _lovePrefab;

        public static LoveMeter Instance { private set; get; }

        [SerializeField] private int LoveMeterSize = 7;
        [SerializeField] private int ActionRequirement = 3;
        [Tooltip("Amount of point we are starting wise")][SerializeField] private int BasePoint = 2;

        public string CurrentPartner { set; get; }

        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// Contains point for each character
        /// </summary>
        public Dictionary<string, int> pointList = new();
        public Dictionary<string, RectTransform> _loveUI = new();

        public void Init(string partner, Color color)
        {
            pointList.Add(partner, 0);
            var ui = Instantiate(_lovePrefab, _loveContainer);
            ui.GetComponent<Image>().color = color;
            _loveUI.Add(partner, (RectTransform)ui.transform);

            UpdateUI();
        }

        private void UpdateUI()
        {
            var ttWidth = _loveContainer.sizeDelta.x;
            var keys = pointList.Keys;
            for (int i = 0; i < keys.Count; i++)
            {
                var partner = keys.ElementAt(i);
                var score = pointList[partner];
                var perc = score * ttWidth / LoveMeterSize;

                _loveUI[partner].sizeDelta = new(perc, _loveUI[partner].sizeDelta.y);
            }
        }

        public void AddPoint(string partner)
        {
            if (pointList.Sum(x => x.Value) == LoveMeterSize) // Bar is already full
            {
                return;
            }

            Debug.Log($"[LOVE] ({partner}) {pointList[partner]} +1");
            pointList[partner]++;

            // check if we need to do switch.
            var fc = PlayerManager.Instance.Follower;
            if (!fc.gameObject.activeInHierarchy)
            {
                fc.gameObject.SetActive(true);
                fc.ResetFollowing();
            }
            if (fc.Info.Name != partner)
            {
                // switch partner here.
                PartnerSwitch(fc, partner);
            }

            UpdateUI();
        }

        public bool CanBomb(string partner)
        {
            Debug.Log($"[BOMB] ({partner}) {pointList[partner]} >= {ActionRequirement}");
            return pointList[partner] >= ActionRequirement;
        }

        // uses the designated amount of power for the love meter.
        public bool UsePower(string partner)
        {
            pointList[partner] -= ActionRequirement;
            UpdateUI();
            return true;
        }

        /// <returns>Is the partner dead</returns>
        public bool LooseHealth(string partner)
        {
            if (pointList[partner] == 0) return true;
            pointList[partner]--;
            UpdateUI();
            return false;
        }

        public void PartnerSwitch(FollowerController fc, string newPartner)
        {
            GameObject followerRef = GameObject.Find("Follower");

            if ((followerRef != null) && (followerRef.activeSelf))
            {
                followerRef.GetComponent<FollowerController>().SwapInfo(newPartner);
                CurrentPartner = newPartner;



                //LoveMeter.Instance.currentPartner = followerRef.GetComponent<FollowerController>()._info;
            }
        }
    }
}