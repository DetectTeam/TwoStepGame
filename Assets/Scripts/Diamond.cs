using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    private void OnTriggerEnter2D( Collider2D col )
    {
        if( col.tag == "Ball" || col.tag == "Dud" )
        {
           StartCoroutine( Death() );
        }
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds( 0.3f );
    
        Messenger.Broadcast( "Continue" );
        Messenger.Broadcast( "Reset" );
        SessionManager.Instance.EndSession();
        gameObject.SetActive( false );
    }
}
