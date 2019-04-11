using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundsChecker : MonoBehaviour
{
    [SerializeField] private GameObject dot;
    [SerializeField] private Vector3 p;
    [SerializeField] private Camera cam;

    [SerializeField] private Transform topWall;
    [SerializeField] private Transform bottomWall;
    [SerializeField] private Transform leftWall;
    [SerializeField] private Transform rightWall;

    private void Start()
    {
       CheckScreenBoundaries();

        if( !topWall || !bottomWall || !leftWall || !rightWall )  
            Debug.Log( "One or more boundary walls have not been set..." );
    }

    // private void Update()
    // {
    //     CheckScreenBoundaries();
    // }

    private void CheckScreenBoundaries()
    {
        Vector3 screenDimensions = Camera.main.ScreenToWorldPoint( new Vector3( Screen.width, Screen.height,0 ) );
        
        topWall.position = new Vector3( 0, screenDimensions.y, 0 );
       // bottomWall.position = new Vector3( 0, -screenDimensions.y, 0 );
        leftWall.position = new Vector3( -screenDimensions.x, 0, 0 );
        rightWall.position = new Vector3( screenDimensions.x, 0, 0 );
    }

    
}
