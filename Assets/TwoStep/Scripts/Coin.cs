using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D( Collider2D collider2D )
    {
        if( collider2D.CompareTag( "Ball" ) || collider2D.CompareTag( "Dud" ) )
        {
            Destroy( gameObject );
        }
    }
}
