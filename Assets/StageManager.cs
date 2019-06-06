using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;
    
    [SerializeField] private int stageNumber;

    public int StageNumber { get{ return stageNumber; } } 

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
    }
    
    // Start is called before the first frame update
    void Start()
    {
        LoadStage();
    }

    private void LoadStage()
    {
        if( PlayerPrefs.HasKey( "stage" ) )
            stageNumber = PlayerPrefs.GetInt( "stage" );
    }

    private void SaveStage()
    {
        PlayerPrefs.SetInt( "stage" , stageNumber );
    }

    public void IncrementStage()
    {
        stageNumber ++;
        SaveStage();
    }

    public void ResetStage()
    {
        stageNumber = 1;
    }
}
