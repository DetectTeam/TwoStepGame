using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Switch : MonoBehaviour
{ 
    [SerializeField] private float delay;
    [SerializeField] private GameObject gate;
    [SerializeField] private TextMeshPro counter;
    // Start is called before the first frame update
    private void Start()
    {
        if( !gate )
            Debug.Log( "Gate Not set.." );

        if( !counter )
            Debug.Log( "Counter not set..." );
    }

    private void OnTriggerEnter2D( Collider2D collider )
    {
      if( collider.CompareTag( "Ball" ) )
      {
          StartCoroutine( GateControl() );
      }
    }

    private IEnumerator GateControl()
    {
        gate.SetActive( false );
        GetComponent<Collider2D>().enabled = false;

        for( int x = 0; x < delay; x++ )
        {
            //counter.text = ( delay - x ).ToString();
            yield return new WaitForSeconds( 1f );
           
        }

        counter.text = "";

        gate.SetActive( true );
         GetComponent<Collider2D>().enabled = true;
    }
 
}
