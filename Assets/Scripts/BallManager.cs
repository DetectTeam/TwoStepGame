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

    [SerializeField] private bool isWhiteBullet = true;

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


    public bool CheckForDud( float percentage )
    {
        bool b;
        var rand = Random.value;

        //Debug.Log( "Check For Dud: " + rand + " " + ( percentage / 100 ) );

        //Weighted Random goes here
       if( rand < ( percentage / 100 ) )
        b = true;
       else
        b = false;

        return b;
    }

    public void BallColourProbability( float percentage )
    {  
        var rand = Random.value;
        bool b = false;

        //Debug.Log( "Rand: " + rand + " " + percentage / 100 );
        
        if ( rand < ( percentage / 100 ) )
            isWhiteBullet = true;
        else 
            isWhiteBullet = false;       
        
    }

    public GameObject CreateBall( bool b )
    {
        GameObject ball = null;
        SessionManager.Instance.IsBallDud = b;

        if( b )
        { 
            ball = Instantiate( dudBallPrefab ) as GameObject;  
        }
        else
        {
            ball = Instantiate( ballPrefab ) as GameObject;
            
            if( !isWhiteBullet )
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
    public void BuildBall()
    {
        activeBall = new BallInfo();
       
        if ( isWhiteBullet )
        {
            activeBall.IsWhite = true;
            isDud = CheckForDud( percentageWhiteBallIsDud);  
        }
        else
        {
            activeBall.IsWhite = false;
            isDud = CheckForDud( percentageRedBallIsDud );
        }

        activeBall.IsDud = isDud;

        ball = CreateBall( isDud  );

        // if( isDud )
        //     currentBallIcon.SetActive( true );
        // else
        //     currentBallIcon.SetActive( false );   

        if( isWhiteBullet )
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

    private void Fire()
    {

    }

    
}
