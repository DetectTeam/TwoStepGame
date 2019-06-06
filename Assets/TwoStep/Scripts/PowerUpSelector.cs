using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUpSelector : MonoBehaviour
{
    private IEnumerator cyclePowerUps;
    private IEnumerator coolDown;

    [SerializeField] private float lifeSpanCount;
    [SerializeField] private float delay;
    [SerializeField] private List<Sprite> icons = new List<Sprite>();

    [SerializeField] private SpriteRenderer backGroundSpriteRenderer; 
    [SerializeField] private SpriteRenderer iconSpriteRenderer; 

    [SerializeField] private Color coolDownColour;

    [SerializeField] private Color orignalBackgroundColour;

    [SerializeField] private bool wasHit = false;
    
    [SerializeField] private TextMeshPro countDown;
    
    // Start is called before the first frame update
    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void Start()
    {

        orignalBackgroundColour = backGroundSpriteRenderer.color;

       // lifeSpan = LifeSpan();
        //StartCoroutine( lifeSpan );
        
        cyclePowerUps = CyclePowerUps();
        StartCoroutine( cyclePowerUps );

        coolDown = CoolDown();
    }

    private IEnumerator LifeSpan()
    {
        float count = 0;
        
        while( count < lifeSpanCount )
        {
            yield return new WaitForSeconds( delay );
            count = count + delay ;
        }

        Destroy( gameObject );
    }

    private IEnumerator CyclePowerUps()
    {
        Debug.Log( "Starting Cycle Power Ups" );
        
        int index = 0;
        
        iconSpriteRenderer.gameObject.SetActive( true );
        
        while( true )
        {
            yield return new WaitForSeconds( delay );
            //Change Icon; 

            if( index >= icons.Count - 1 )
                index = 0;
            else 
                index ++;

            iconSpriteRenderer.sprite = icons[ index ];
        }
    }

    private IEnumerator CoolDown()
    {
        Debug.Log( "Cooling Down" );

        countDown.gameObject.SetActive( true );
        
        for( int x = 10; x > 0; x-- )
        {
            countDown.text = x.ToString();
            yield return new WaitForSeconds( 1.0f );
        }

        countDown.gameObject.SetActive( false );
        wasHit = false;
        backGroundSpriteRenderer.color = orignalBackgroundColour;

        cyclePowerUps = CyclePowerUps();
        StartCoroutine( cyclePowerUps );
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if( col.gameObject.CompareTag( "Ball" )  )
        {
            if( !wasHit )
            {
                Debug.Log( "Hit by Ball...." );
                StopCoroutine( cyclePowerUps );

                //Get the currently active sprite
            
                //Trigger power up activation. 

                //Greyout background Color
               // backGroundSpriteRenderer.color = coolDownColour;

                //Disable icon sprite
               // iconSpriteRenderer.gameObject.SetActive( false );

                //Trigger Cooldown
                //coolDown = CoolDown();
                //StartCoroutine( coolDown );
               
                wasHit = true;
            
            }

            //itween shake
            iTween.ShakePosition( gameObject, iTween.Hash( "x",0.3f, "y",0.3f, "time",0.5f ) ); // target game object ... t
         
        }
    }
   
}
