﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;


[System.Serializable]
public class GreenProbability
{
        public List<float> greenProbabilityList;
        //public List<float> GreenProbabilityList { get{ return greenProbabilityList; } set{ greenProbabilityList = value; } }
}

[System.Serializable]
public class RedProbability
{
        public List<float> redProbabilityList;
        //public List<float> RedProbabilityList { get{ return redProbabilityIsHyper; } set{ redProbabilityIsHyper = value; } }
}

public class ProbabilityManager : MonoBehaviour
{
    public static ProbabilityManager Instance;
    
    [SerializeField] private TextMeshPro output;

    [SerializeField] private GreenProbability greenProb;
    [SerializeField] private RedProbability redProb;
        
    [SerializeField] private int greenListCount = 0;
    [SerializeField] private int redListCount = 0;



    private void Awake()
    { 
        
          //Check if instance already exists
        if (Instance == null)
            //if not, set instance to this
            Instance = this;
        	//If instance already exists and it's not this:
        else if (Instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject); 
        
        
        greenProb = JsonUtility.FromJson<GreenProbability>( ReadJsonFromResources( "Probability/GreenProbabilityValues" ) );
            
        redProb = JsonUtility.FromJson<RedProbability>( ReadJsonFromResources( "Probability/RedProbabilityValues" ) );
            
        //output.text = "" + greenProb.greenProbabilityList.Count;
        //output.text = "" + redProb.redProbabilityList.Count;
    }
        
        // Start is called before the first frame update

    private string ReadJsonFromResources( string path )
    {
        TextAsset jsonString = Resources.Load( path ) as TextAsset;
        return jsonString.text;
    } 

    public void IncrementGreenDrift()
    { 
        SessionManager.Instance.SetGreenProbability( greenProb.greenProbabilityList[ greenListCount ] );
        
        greenListCount ++;
        
        if( greenListCount > greenProb.greenProbabilityList.Count )
             greenListCount = 0;
 
    } 

    public void IncrementRedDrift()
    {
        SessionManager.Instance.SetRedProbability( redProb.redProbabilityList[ redListCount ] );
        
        redListCount ++;
        
        if( redListCount > redProb.redProbabilityList.Count )
                redListCount = 0;
    }

    public float GetCurrentGreenDrift()
    {
        Debug.Log( "Current Green Probability Drift: " + greenProb.greenProbabilityList[ greenListCount ] );
        return greenProb.greenProbabilityList[ greenListCount ];
    }

    public float GetCurrentRedDrift()
    {
        Debug.Log( "Current Red Probability Drift: " + redProb.redProbabilityList[ redListCount ] );
        return redProb.redProbabilityList[ redListCount ];
    }

}
