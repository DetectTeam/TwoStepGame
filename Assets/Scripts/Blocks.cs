using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
   [SerializeField] private SpriteRenderer blockSprite;
   [SerializeField] private Sprite normalBlock;
   [SerializeField] private Sprite damagedBlock;
    [SerializeField] private bool isBreakable;
    [SerializeField] private float hitTotal;

    private void OnEnable()
    {
        Messenger.AddListener( "Reset", ResetBlock );
        blockSprite = GetComponent<SpriteRenderer>();
    }

    private void OnDisable()
    {
        Messenger.RemoveListener( "Reset", ResetBlock );
    }


    private void Start()
    {
        blockSprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        
        if( col.CompareTag( "Ball" ) && isBreakable )
        {
            hitTotal  = hitTotal - 3f;
            Messenger.Broadcast( "ShakeCamera" );
        }
        else if( col.CompareTag( "Dud" ) && isBreakable )
        {  
             Messenger.Broadcast( "ShakeCamera" );
             blockSprite.sprite = damagedBlock;
             hitTotal  = hitTotal - 1.5f; 
             
             if( hitTotal >= 0 )
                Destroy( col.gameObject );

           
        }

        if( hitTotal <= 0  )
        {
            //Destroy( gameObject );
            gameObject.SetActive( false );
        }

    }

    public void ResetBlock()
    {
        blockSprite.sprite = normalBlock;
    }
}
