using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private IEnumerator triggerTrap;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D( Collider2D collider2d )
    {
        if( collider2d.CompareTag( "Ball" ) || collider2d.CompareTag( "Dud" ) )
        {
            Debug.Log( "Ball Passed Through here.....triggering trap" );
            triggerTrap = TriggerTrap();
            StartCoroutine( triggerTrap );
        }
    }

    private IEnumerator TriggerTrap()
    {
        yield return new WaitForSeconds( 0.1f );

        Messenger.Broadcast( "TriggerTrap" );
    }
}
