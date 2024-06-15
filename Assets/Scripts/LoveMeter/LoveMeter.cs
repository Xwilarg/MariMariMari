using System.Collections.Generic;
using System.Linq;
using TouhouPride.Manager;
using TouhouPride.Player;
using UnityEngine;

/**
 * forgive me if this is just a given, but we'll probably want to keep the UI logic for this seperate from here.
 */
public class LoveMeter : MonoBehaviour
{
    public static LoveMeter Instance { private set; get; }

    private int LoveMeterSize = 9;
    private int ActionRequirement = 3;
    [Tooltip("Amount of point we are starting wise")] private int BasePoint = 2;

    public string CurrentPartner { set; get; }

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Contains point for each character
    /// </summary>
    public Dictionary<string, int> pointList = new();

    public void Init(string partner)
    {
        pointList.Add(partner, BasePoint);
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
        if (fc.Info.Name != partner)
        {
            // switch partner here.
            PartnerSwitch(fc, partner);
        }
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
        return true;
    }
    
    public void PartnerSwitch(FollowerController fc, string newPartner)
    {
        GameObject followerRef = GameObject.Find("Follower");
        
        if ((followerRef!= null) && (followerRef.activeSelf))
        {
            followerRef.GetComponent<FollowerController>().SwapInfo(newPartner);
            CurrentPartner = newPartner;



            //LoveMeter.Instance.currentPartner = followerRef.GetComponent<FollowerController>()._info;
        }
    }
}