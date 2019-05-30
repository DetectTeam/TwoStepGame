using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallCounter : MonoBehaviour
{
    private static BallCounter instance;

    public static BallCounter Instance { get { return instance; } }
    [SerializeField] private TextMeshProUGUI ballCount;

    [SerializeField] private int totalBalls = 15;
    [SerializeField] private int numBallsLeft;
    [SerializeField] private GameObject endLevelMessage;

    private void OnEnable()
    {
       // Debug.Log( "Enabled...." );
       // Messenger.AddListener( "DecrementBallCount", DecrementBallCount );
    }

    private void OnDisable()
    {
        //Messenger.RemoveListener( "DecrementBallCount", DecrementBallCount );
    }

    private void Awake()
    {
        if ( instance != null &&  instance != this )
        {
            Destroy( this.gameObject );
        } 
        else 
        {
             instance = this;
        }
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        ballCount.text = totalBalls.ToString();
        numBallsLeft = totalBalls;
    }

    public void DecrementBallCount()
    {
        numBallsLeft --;

        if( numBallsLeft > 0 )
        {
            ballCount.text = numBallsLeft.ToString();
        }
        else if( numBallsLeft <= 0 )
        {
            ballCount.text = "0";
            endLevelMessage.SetActive( true );
            StartCoroutine( CountDown() );
        }
    }

    private IEnumerator CountDown()
    {

        yield return new WaitForSeconds( 3.0f );
        endLevelMessage.SetActive( false );
        numBallsLeft = totalBalls;
        ballCount.text = numBallsLeft.ToString();

        Messenger.Broadcast( "Continue" ); //Spawn new Diamond Block
        Messenger.Broadcast( "Reset" ); //Reset the walls
        SessionManager.Instance.EndSession(); //Save data to server
    }

    
}
