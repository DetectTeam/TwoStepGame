using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionChecker : MonoBehaviour
{
    
    public enum Direction { UP, DOWN, LEFT, RIGHT }

    public delegate void OnDirectionChangeDelegate( string direction );
    public static event OnDirectionChangeDelegate DirectionChangeDelegate;


    [SerializeField] private Vector3 velocity;

    private Vector3 lastPosition; 

    [SerializeField] private Direction direction;
    [SerializeField] private Direction lastDirection;
    
    // Start is called before the first frame update
    private void Start()
    {
        lastPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        velocity = transform.position - lastPosition;
        lastPosition = transform.position;

        if( velocity.y > 0 && Mathf.Abs( velocity.x ) < 0.01f  )
            direction = Direction.UP;
        
        if( velocity.y < 0 && Mathf.Abs( velocity.x ) < 0.01f )
            direction = Direction.DOWN;
        
        if( velocity.x < 0 && Mathf.Abs( velocity.y ) < 0.01f )
            direction = Direction.LEFT;
        
        if( velocity.x > 0 && Mathf.Abs( velocity.y ) < 0.01f )
            direction = Direction.RIGHT;

        if( lastDirection != direction )
        {
            Debug.Log( "Direction has Changed ...." );
            //DirectionChangeDelegate();
        }

        lastDirection = direction;
    
    }

    
}
