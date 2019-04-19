using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDiamond : MonoBehaviour
{
    
    public delegate void OnDestroyDelegate();

    private void OnTriggerEnter2D( Collider2D col )
    {
        if( col.tag == "Ball" || col.tag == "Dud" )
        {
            Debug.Log( "Im Hit." );
            TutorialManager.Instance.MoveNext();
            Messenger.Broadcast( "Continue" );
            gameObject.SetActive( false );
 
        }
    }
}
