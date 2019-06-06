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
    public static ProbabilityManager Instance;
    
    [SerializeField] private TextMeshPro output;

    [SerializeField] private GreenProbability greenProb;
    [SerializeField] private RedProbability redProb;
        
    [SerializeField] private int greenListCount = 0;
    [SerializeField] private int redListCount = 0;

    [SerializeField] private string currentStage;

    [SerializeField] private TextMeshProUGUI greenProbText;
    [SerializeField] private TextMeshProUGUI redProbText;



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
        
        
        LoadProbabilityLists();
            
        //output.text = "" + greenProb.greenProbabilityList.Count;
        //output.text = "" + redProb.redProbabilityList.Count;
    }
        
        // Start is called before the first frame update


    private void LoadProbabilityLists(  )
    {
        //Get current stage
        currentStage = "stage" + StageManager.Instance.StageNumber;

        string path = "Probability/"+ currentStage;
        //Get Path based on current Stage
        greenProb = JsonUtility.FromJson<GreenProbability>( ReadJsonFromResources( path+ "/GreenProbability" ) );

        if( greenProb == null )
            Debug.LogError( "Green Probability Not Found" );

        redProb = JsonUtility.FromJson<RedProbability>( ReadJsonFromResources( path + "/RedProbability" ) );
        
        if( redProb == null )
             Debug.LogError( "Red Probability Not Found" );
        //Load Stage Probalities.
    }    

    private string ReadJsonFromResources( string path )
    {
        TextAsset jsonString = Resources.Load( path ) as TextAsset;

        if( jsonString == null )
        {
            Debug.LogError( "Json File not found..." );
            return null;
        }
        else
        {
            return jsonString.text;
        }
  
    } 

    public void IncrementGreenDrift()
    { 
        SessionManager.Instance.SetGreenProbability( greenProb.greenProbabilityList[ greenListCount ] );

        greenProbText.text = greenProb.greenProbabilityList[ greenListCount ].ToString();
        
        greenListCount ++;
        
        if( greenListCount > greenProb.greenProbabilityList.Count )
             greenListCount = 0;
 
    } 

    public void IncrementRedDrift()
    {
        SessionManager.Instance.SetRedProbability( redProb.redProbabilityList[ redListCount ] );

        redProbText.text = redProb.redProbabilityList[ redListCount ].ToString();
        
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
