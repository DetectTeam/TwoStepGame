using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Switch : MonoBehaviour
{ 
    [SerializeField] private float delay;
    [SerializeField] private GameObject gate;
    [SerializeField] private GameObject halfGate;
    [SerializeField] private TextMeshPro counter;
    [SerializeField] private SpriteRenderer foreGround;

    private IEnumerator gateControl;
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
      if( collider.CompareTag( "Ball" ) || collider.CompareTag( "Dud" ) )
      {
          gateControl = GateControl( collider.tag );
          StopCoroutine( gateControl );
          StartCoroutine( gateControl );
      }
    }

    private IEnumerator GateControl( string ballType )
    {
        float H, S, V;

        Color switchColor = foreGround.color;
        
    

        if( ballType == "Dud" )
        {
            Debug.Log( "DUD HIT ME" );
            if( !halfGate.activeSelf && gate.activeSelf )
            {    
                Debug.Log( "Got this far" );
                gate.SetActive( false );
                halfGate.SetActive( true );
            }
            else
            {
                halfGate.SetActive( false );
                GetComponent<Collider2D>().enabled = false;
            }
        }
        else
        {
            halfGate.SetActive( false );
            gate.SetActive( false );
            GetComponent<Collider2D>().enabled = false;
        }
    
        

        Color.RGBToHSV(  switchColor , out H, out S, out V );
        ChangeHue( H + 0.025f );

        for( int x = 0; x < delay; x++ )
        {
            //counter.text = ( delay - x ).ToString();
            yield return new WaitForSeconds( 1f );
        }

        ChangeHue( H );

        counter.text = "";

        gate.SetActive( true );
        GetComponent<Collider2D>().enabled = true;
    }

    private void ChangeHue( float hueValue )
    {
        foreGround.color = Color.HSVToRGB(hueValue, 0.6f, 0.8f); 
    }
 
}
