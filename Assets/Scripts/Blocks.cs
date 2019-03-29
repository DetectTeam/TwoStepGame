using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
   
    [SerializeField] private bool isBreakable;
    [SerializeField] private float hitTotal;


    private void OnCollisionEnter2D(Collision2D col)
    {
        if( col.gameObject.tag == "Ball" && isBreakable )
        {
            hitTotal  = hitTotal - 3f;
        }
        else if( col.gameObject.CompareTag( "Dud" ) && isBreakable )
        {
             hitTotal  = hitTotal - 1.5f; 
        }

        if( hitTotal <= 0 )
        {
            Destroy( gameObject );
        }

    }
}
