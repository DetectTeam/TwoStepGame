using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionChecker : MonoBehaviour
{
    
    private Vector3 velocity;

    private Vector3 lastPosition; 
    
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

        if( velocity.y > 0.1f )
            Debug.Log( "UP" );
        
        if( velocity.y < 0.0f )
            Debug.Log( "Down" );
        
        if( velocity.x > 0.1f  )
             Debug.Log( "Right" );
        
        if( velocity.x < 0.0f  )
             Debug.Log( "left" );


    }
}
