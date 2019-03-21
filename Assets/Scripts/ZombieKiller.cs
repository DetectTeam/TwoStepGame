using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieKiller : MonoBehaviour
{
    private void OnTriggerEnter2D( Collider2D col )
    {
        
        Debug.Log( "Hit by" + col.name );
    }
}
