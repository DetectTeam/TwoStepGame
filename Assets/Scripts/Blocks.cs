using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
   
    [SerializeField] private bool isBreakable;
    [SerializeField] private int hitTotal;


    private void OnCollisionEnter2D(Collision2D col)
    {
        if( col.gameObject.tag == "Ball" && isBreakable )
        {
            Debug.Log( "HIT" );
            hitTotal --;
            if( hitTotal == 0 )
            {
                Destroy( gameObject );
            }
        }

    }
}
