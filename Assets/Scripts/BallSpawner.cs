using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    
    [SerializeField] private GameObject ball;
    //[SerializeField] private GameController gameController;
    [SerializeField] private BallManager ballManager;
    // Start is called before the first frame update
    private void Start()
    {
        if( !ball )
            Debug.Log( "Ball prefab not set..." );
    }

    public void CreateDummyBall()
    { 
       // ball = gameController.Ball;
       ball = ballManager.Ball;
        
        var clone = Instantiate( ball , transform.position, transform.rotation );
        clone.name = "leftBallIcon";
        clone.GetComponent<Rigidbody2D>().gravityScale = 3.0f;
        clone.transform.localScale = new Vector3( 1.0f, 1.0f, 1.0f );
        clone.layer = 11;
        clone.SetActive( true );
    }




}
