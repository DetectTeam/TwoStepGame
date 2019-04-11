using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestroyer : MonoBehaviour
{
   
    [SerializeField] private SpriteRenderer currentBullet;
    [SerializeField] private GameController gameController;

    private void OnTriggerEnter2D( Collider2D collider2d )
    {
    
       if( collider2d.CompareTag( "Ball" ) || collider2d.CompareTag( "Dud" ) )
       {
           StartCoroutine( Fire() );
           currentBullet.color = collider2d.GetComponent<SpriteRenderer>().color;

            //If not dud then add glow effect....

           Destroy( collider2d.gameObject );

       }
         
   }

   private IEnumerator Fire()
   {
       yield return new WaitForSeconds( 0.2f) ;
       gameController.Fire();

   }

}
