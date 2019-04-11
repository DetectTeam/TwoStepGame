using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    
    [SerializeField] private List<Sprite> leverStates = new List<Sprite>();
    [SerializeField] private List<SpriteRenderer> states = new List<SpriteRenderer>();

    private SpriteRenderer spriteRenderer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer  = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D( Collider2D col )
    {
        if( col.CompareTag( "Ball" ) )
        {
            spriteRenderer.sprite = leverStates[2];
            ResetStates();
            states[2].gameObject.SetActive( true );
        }
        
        if(  col.CompareTag( "Dud" ) )
        {
             if( spriteRenderer.sprite == leverStates[0] )
             {
                spriteRenderer.sprite = leverStates[1];
                 ResetStates();
                 states[1].gameObject.SetActive( true );
             }
             else if( spriteRenderer.sprite == leverStates[1] )
             {
                 spriteRenderer.sprite = leverStates[2];
                  ResetStates();
                 states[2].gameObject.SetActive( true );
             }
        }
    }

    public void Reset()
    {
        spriteRenderer.sprite = leverStates[0];
        ResetStates();
        states[0].gameObject.SetActive( true );
    }

    public void ResetStates()
    {
        foreach( SpriteRenderer state in states )
            state.gameObject.SetActive( false );
    }
}
