using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json; //Json Library

public class SessionManager : MonoBehaviour
{
    public static SessionManager Instance = null;

    private Session session;
    private ButtonPress buttonPress;
    //Session lasts for duration of a Level.
    private int trialNumber = 0;

    [SerializeField] private float timeBetweenButtonPresses;

    [SerializeField] private string ballColour;
    public string BallColour { get{ return ballColour; } set{ ballColour = value; } } 

    [SerializeField] private bool isBallDud;
    public bool IsBallDud { get{ return isBallDud; } set{ isBallDud = value; } }

    private string buttonChoice;

    private Coroutine timer;

    private void Awake()
    {
        if( Instance == null )
            Instance = this;
        else if( Instance  != this )
            Destroy( gameObject );
    }

    private void Start()
    {
        startTime = 0;
    }

    // Start is called before the first frame update
    public void StartSession()
    {
        session = new Session();

        session.TimeStamp = System.DateTime.Now.ToString( "yyyy_MM_dd_hh_mm_ss" );
        session.Level = 1;
        session.SessionID = "Test Session ID";
        session.UserID = SystemInfo.deviceUniqueIdentifier;
    }

    public void StartButtonPress()
    {
        buttonPress = new ButtonPress();
    }

    public void EndButtonPress()
    {
        if( session == null )
        {
             StartSession();
        }

        if( buttonPress != null )
        {    
            session.ButtonPress.Add( buttonPress );
        
        }
        
    }

    public void EndSession()
    {
        Debug.Log( "End Session...." );
        session.ButtonPress.Add( buttonPress );

        SaveSession();
    }


    private float startTime;
    private float endTime;
    //Record time between button presses
    public void StartTimer()
    {
        //StartCoroutine( "Timer" );
    }

    private bool isTimerRunning = false;
    private IEnumerator Timer()
    {
        while( true )
        {
            yield return null;
            timeBetweenButtonPresses += Time.time;
        }
    }

    public void TimeBetweenButtonPresses()
    {
        endTime = Time.time;
     
        Debug.Log( endTime - startTime );
        buttonPress.TimeToPressFireButton = ( endTime - startTime ) * 1000 ;
        startTime = endTime;
    }
    //Ends

    public void DetermineTransitionType( bool buttonPressed, bool ballColour )
    {
        if( buttonPressed ) //true = left false = right
        {
            if( ballColour ) //Ball is Green
            {
                buttonPress.Transition = "Common";
            }
            else
            {
                buttonPress.Transition = "Rare";
            }

        }
        else // Right button was pressed
        {
            if( ballColour ) //Ball is Green
            {
                buttonPress.Transition = "Rare";
            }
            else //Ball is Red.
            {
                buttonPress.Transition = "Common";
            }
        }
    }

    public void SetTrialNumber()
    { 
        trialNumber ++; //PlayerPref this value
        buttonPress.TrialNumber = trialNumber;
    }

    public void SetChoice( string choice )
    {
        Debug.Log( choice );
        buttonPress.Choice = choice;
        buttonChoice = choice;
    }

    public void SetBallColour()
    {
        Debug.Log( ballColour );
        buttonPress.BallColour = ballColour;
    }

    public void SetBallType()
    {
        if( isBallDud )
            buttonPress.Reward = 0;
        else
            buttonPress.Reward = 1;

        Debug.Log( "Reward: " + isBallDud );
    }

    public void SetTransition()
    {
        if( buttonChoice == "Left" && ballColour == "Green" )
            buttonPress.Transition = "Common";
        else if( buttonChoice == "Left" && ballColour == "Red" )
             buttonPress.Transition = "Rare";
        else if( buttonChoice == "Right" && ballColour == "Red" )
                buttonPress.Transition = "Common";
        else if( buttonChoice == "Right" && ballColour == "Green"  )
             buttonPress.Transition = "Rare";

        Debug.Log( buttonPress.Transition );
    }

    public void SetGreenProbability( float prob )
    {
        buttonPress.ProbabilityGreen = prob;
        Debug.Log( prob );
    }

    public void SetRedProbability( float prob)
    {
        buttonPress.ProbabilityRed = prob;
        Debug.Log( prob );
    }

    public void SaveSession()
    {
        //Serialize Session....
        
        string jsonString = JsonConvert.SerializeObject( session );
        
        Debug.Log( jsonString );
        
        Messenger.Broadcast<string>( "PUT" , jsonString );
    }
   
}
