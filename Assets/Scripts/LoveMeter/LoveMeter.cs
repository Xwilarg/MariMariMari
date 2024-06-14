using System.Collections.Generic;
using System.Linq;
using TouhouPride;
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
            else
            {
                print("no point");
            }

            if (currentContents == ActionRequirement)
            {
                print("we good");
                return true;
                break;
            }
        }
        return false;
    }

    // making this return a bool for now lolol.
    public bool UsePower(Partners partner)
    {
        /*
        int currentContents = 0;
        
        // check if the criteria is met
        for (int i = 0; i < pointList.Count; i++)
        {
            if (pointList[i] == partner)
            {
                currentContents++;
            }
        }

        if (currentContents >= ActionRequirement)
        {
            for (int i = 0; i < ActionRequirement; i++)
            {
                pointList.Remove(partner);
            }
            return true;
        }
        return false;
        */
        for (int i = 0; i < ActionRequirement; i++)
        {
            pointList.Remove(partner);
        }
        return true;
    }
}