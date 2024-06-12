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
        
        if (pointList.Count == LoveMeterSize)
        {
            print("DOOR STUCK!");
            
            // add new love point to end of list
            pointList.Add(partner);
            
            pointList.RemoveAt(0);
            
            //pointList.Insert(0, partner);
            //pointList.Sort();

            
            
        }
        else
        {
            // add love point
            // sort array by love points
            pointList.Add(partner);
            //pointList.Sort();
        }
    }

    // making this return a bool for now lolol.
    public bool UsePower(Partners partner)
    {
        int currentContents = 0;
        
        // check if the criteria is met
        for (int i = 0; i < LoveMeterSize; i++)
        {
            if (pointList[i] == partner)
            {
                currentContents++;
            }
        }

        if (currentContents > ActionRequirement)
        {
            for (int i = 0; i < ActionRequirement; i++)
            {
                pointList.Remove(partner);
            }
            
            return true;
        }
        
        // 
        
        // todo; this functionality.
        return false;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // debug
        if (Input.GetKey("r"))
        {
            AddPoint(Partners.Reimu);
        }
        
        if (Input.GetKey("a"))
        {
            AddPoint(Partners.Alice);
        }
    }
}