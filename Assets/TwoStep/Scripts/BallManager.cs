using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallInfo
{
        public bool IsDud{get; set;}
        public bool IsWhite{get; set;}
}

public class BallManager : MonoBehaviour
{
    [SerializeField] private GameObject ball;
     public GameObject Ball { get{ return ball; } set{ ball = value; } }
    [SerializeField] private GameObject muzzle;

    [SerializeField] private bool isGreenBall = true;

    [SerializeField] private bool isDud;
 

    [SerializeField] private GameObject ballPrefab;

    [SerializeField] private GameObject dudBallPrefab;

    [SerializeField] private Color white;
    [SerializeField] private Color red;
    [SerializeField] private Color grey;
    [SerializeField] private float percentageWhiteBallIsDud;
    [SerializeField] private float percentageRedBallIsDud;
    [SerializeField] private GameObject currentBall;

    [SerializeField] private BallInfo activeBall;
     public BallInfo ActiveBall { get{ return activeBall; } set{ activeBall = value; } }

     [SerializeField] private List<float> ballColourRatios = new List<float>();
 

    public bool CheckForDud( bool isLeft, bool isGreen)
    {
        bool b = false;
        var rand = Random.value;

        float probability = 0;

        if( isGreen )
            probability = ProbabilityManager.Instance.GetCurrentGreenDrift();
        else
            probability = ProbabilityManager.Instance.GetCurrentRedDrift();

        //if left button pressed and ball is green

        if( isLeft && isGreen )
        {
              //Weighted Random goes here
              if( rand < probability )
                b = false;
              else
                b = true;
        }
        else if( isLeft && !isGreen )
        {
            if( rand > probability )
                b = true;
            else
                b = false;
        }
        else if( !isLeft && !isGreen )
        {
            //Weighted Random goes here
            if( rand < probability  )
                b = false;
            else
                b = true;
        }
        else if( !isLeft && isGreen )
        {
            if( rand < probability  )
            {
                b = false;
            }
            else
            {
                b = true;
            }
        }

        return b;
    }

    public void BallColourProbability( bool isLeft )
    {  
        var rand = Random.value;
        bool b = false;

        float percentage = ballColourRatios[ StageManager.Instance.StageNumber - 1 ];
        
        if( !isLeft )
        {
            percentage = 100 - percentage;
        }
  
        if ( rand < ( percentage / 100 ) )
            isGreenBall = true;
        else 
            isGreenBall = false;        
    }

    public GameObject CreateBall( bool b )
    {
        //Get the Ball Colour probability based on the current stage
      
        
        GameObject ball = null;
        SessionManager.Instance.IsBallDud = b;

        if( b )
        { 
            ball = Instantiate( dudBallPrefab ) as GameObject;  
        }
        else
        {
            ball = Instantiate( ballPrefab ) as GameObject;
            
            if( !isGreenBall )
            {
                var light = ball.transform.Find( "PlayerLight" );
                if( light != null )
                {
                   light.GetComponent<Light>().color = red;
                }
            }
        }
     
        return ball;
    }

      //BallHandler
    public void BuildBall( bool b )
    {
        activeBall = new BallInfo();
       
        if ( isGreenBall )
        {
            activeBall.IsWhite = true;
            isDud = CheckForDud( b, isGreenBall );  
        }
        else
        {
            activeBall.IsWhite = false;
            isDud = CheckForDud( b, isGreenBall );
        }

        activeBall.IsDud = isDud;

        ball = CreateBall( isDud  );

        // if( isDud )
        //     currentBallIcon.SetActive( true );
        // else
        //     currentBallIcon.SetActive( false );   

        if( isGreenBall )
        {
            ball.GetComponent<SpriteRenderer>().color = white;
            //ballContainer.color = white;
            SessionManager.Instance.BallColour = "Green";
        }
        else
        {
            ball.GetComponent<SpriteRenderer>().color = red;
           // ballContainer.color = red;
            SessionManager.Instance.BallColour = "Red";
        }

        SetCurrentBall( ball );
    }

    public void SetCurrentBall( GameObject ball )
    {
        currentBall = ball;
        currentBall.SetActive( false );
    }

    public void SetCurrentBallTutorial( GameObject b )
    {
        ball = b;
    }

    public GameObject GetCurrentBall()
    {
        return currentBall;
    }

    public void DisableFireButtons()
    {
        Messenger.Broadcast( "DisableReloadButtons" );
    }

    private void Fire()
    {

    }

    
}
