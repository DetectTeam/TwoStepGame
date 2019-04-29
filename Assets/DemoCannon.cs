using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCannon : MonoBehaviour
{
   [SerializeField] private List<string> ballList  = new List<string>();
   [SerializeField] private GameObject dudBall;
   [SerializeField] private GameObject normalBall;
   [SerializeField] private bool isGreen = false;

   [SerializeField] private Color green;
   [SerializeField] private Color red;
    [SerializeField] private Color black;

   [SerializeField] private SpriteRenderer ballIndicator;


   private void Awake()
   {
        if( !isGreen )
        {
            dudBall.GetComponent<SpriteRenderer>().color = red;
            normalBall.GetComponent<SpriteRenderer>().color = red;
            normalBall.transform.Find( "PlayerLight" ).GetComponent<Light>().color = red;
        }
        else
        {
            dudBall.GetComponent<SpriteRenderer>().color = green;
            normalBall.GetComponent<SpriteRenderer>().color = green;
            normalBall.transform.Find( "PlayerLight" ).GetComponent<Light>().color = green;
        }
   }

   private void OnEnable()
   {
        StartCoroutine( Fire() );
   }

   private void OnDisable()
   {

   }

   private IEnumerator Fire()
   {
       GameObject b = null;
       
       while( true )
       {
          foreach( string ball in ballList )
          {
              
             
              
            if( !isGreen )
                ballIndicator.color = red;
            else
                ballIndicator.color = green;

             yield return new WaitForSeconds( 0.4f );

             ballIndicator.color = black;

             yield return new WaitForSeconds( 0.1f );

              Debug.Log( "Launching " + ball ); 

              if( ball == "normal" )
              {
                b = Instantiate( normalBall, transform.position, transform.rotation ) as GameObject;

              }
              else
              {
                b = Instantiate( dudBall, transform.position, transform.rotation ) as GameObject;
              }

             b.GetComponent<Rigidbody2D>().AddForce( Vector2.up );

          }

          yield return new WaitForSeconds( 1f );
       }
   }

}
