using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDiamond : MonoBehaviour
{
    
    public delegate void OnDestroyDelegate();

    private void OnTriggerEnter2D( Collider2D col )
    {
        Debug.Log( "Called ........" );
        
        if( col.tag == "Ball" || col.tag == "Dud" )
        {
            if( TutorialManager.Instance != null )
            {
                StartCoroutine( Death() );

            }
        }
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds( 0.1f );
        TutorialManager.Instance.MoveNext();
        Messenger.Broadcast( "Continue" );
        gameObject.SetActive( false );
    }

   
}
