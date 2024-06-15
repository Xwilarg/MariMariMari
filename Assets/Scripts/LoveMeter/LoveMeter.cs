using System.Collections.Generic;
using System.Linq;
using TouhouPride;
using TouhouPride.Player;
using UnityEngine;

/**
 * forgive me if this is just a given, but we'll probably want to keep the UI logic for this seperate from here.
 */
public class LoveMeter : MonoBehaviour
{
    public static LoveMeter Instance { private set; get; }

    public int LoveMeterSize = 9;
    public int ActionRequirement = 3;

    public Partners currentPartner = Partners.Reimu;
    
    // oops forgot to initialize the singleton lol
    private void Awake()
    {
        Instance = this;
    }
    
    // the list itself.
    public List<Partners> pointList = new List<Partners>();

    public void AddPoint(Partners partner)
    {
        print("added point: " + partner);
        
        pointList.Add(partner);
        
        if (pointList.Count == LoveMeterSize)
        {
            // remove first point
            pointList.RemoveAt(0);
        }
        
        // check if we need to do switch. 
        if (currentPartner != partner)
        {
            // switch partner here.
            PartnerSwitch();
        }
    }
    
    public bool CanBomb()
    {
        int currentContents = 0;
        
        // check if the criteria is met
        for (int i = 0; i < pointList.Count; i++)
        {
            if (pointList[i] == currentPartner)
            {
                print("point: " + i);
                currentContents++;
            }
            if (currentContents == ActionRequirement)
            {
                print("we good");
                // bombing condition has been met
                return true;
            }
        }
        return false;
    }

    // uses the designated amount of power for the love meter.
    public bool UsePower(Partners partner)
    {
        for (int i = 0; i < ActionRequirement; i++)
        {
            pointList.Remove(partner);
        }
        return true;
    }
    
    public void PartnerSwitch()
    {
        GameObject followerRef = GameObject.Find("Follower");
        
        if ((followerRef!= null) && (followerRef.activeSelf))
        {
            followerRef.GetComponent<FollowerController>().SwapInfo(currentPartner);
            
            

            //LoveMeter.Instance.currentPartner = followerRef.GetComponent<FollowerController>()._info;
        }
    }
}