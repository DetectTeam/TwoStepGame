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
            if( TutorialManager.Instance != null )
                TutorialManager.Instance.MoveNext();
            
            Messenger.Broadcast( "Reset" );
            Messenger.Broadcast( "Continue" );
            gameObject.SetActive( false );
        }
    }

   
}
