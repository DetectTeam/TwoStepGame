using System.Collections;
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
    [SerializeField] private TextMeshPro output;

    [SerializeField] private GreenProbability greenProb;
    [SerializeField] private RedProbability redProb;
        
    [SerializeField] private int greenListCount;
    [SerializeField] private int redListCount;



    private void Awake()
    { 
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
        greenListCount ++;
        
        if( greenListCount > greenProb.greenProbabilityList.Count )
             greenListCount = 0;

        SessionManager.Instance.SetGreenProbability( greenProb.greenProbabilityList[ greenListCount ] );
    } 

    public void IncrementRedDrift()
    {
        redListCount ++;
        
        if( redListCount > redProb.redProbabilityList.Count )
                redListCount = 0;

        SessionManager.Instance.SetRedProbability( redProb.redProbabilityList[ redListCount ] );
    }

    public float GetGreenDrift()
    {
        return greenProb.greenProbabilityList[ greenListCount ];
    }

    public float GetRedDrift()
    {
        return redProb.redProbabilityList[ redListCount ];
    }
}
