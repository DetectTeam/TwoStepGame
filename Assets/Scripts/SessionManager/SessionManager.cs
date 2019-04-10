using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionManager : MonoBehaviour
{
    public static SessionManager Instance = null;

    private Session session;
    private ButtonPress buttonPress;
    //Session lasts for duration of a Level.
    private int trialNumber;

    [SerializeField] private float timeBetweenButtonPresses;

    private IEnumerator timer;

    private void Awake()
    {
        if( Instance == null )
            Instance = this;
        else if( Instance  != this )
            Destroy( gameObject );
    }

    private void Start()
    {
        
    }

    // Start is called before the first frame update
    public void StartSession()
    {
        session = new Session();
        
    }

    public void StartButtonPress()
    {
        buttonPress = new ButtonPress();
    }

    public void EndSession()
    {
        
        session.ButtonPress.Add( buttonPress );
        
        SaveSession();
    }


    //Record time between button presses
    private void StartTimer()
    {
        timer = Timer();
        StartCoroutine( timer );
    }

    private IEnumerator Timer()
    {
        yield return null;
        timeBetweenButtonPresses += Time.time;
    }

    private void StopTimer()
    {
        if( timer != null )
            StopCoroutine( timer );
        
        //Record Time
       buttonPress.TimeToPressFireButton = timeBetweenButtonPresses;
    }
    //Ends

    private void DetermineTransitionType( bool buttonPressed, bool ballColour )
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

    private void SetTrialNumber()
    { 
        trialNumber ++; //PlayerPref this value
        buttonPress.TrialNumber = trialNumber;
    }

    private void SaveSession()
    {
        //Serialize Session....
    }
   
}
