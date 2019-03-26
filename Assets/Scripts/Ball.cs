using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;

    [SerializeField] private bool isDud;
    [SerializeField] private float lifeSpan;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    
   
    [SerializeField] private float moveSpeed = 10;


    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if( isDud )
        {
            moveSpeed = 3;
            StartCoroutine( BallLifeSpan() );
        }
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = rigidbody2D.velocity.normalized * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {    
        if( isDud )
        {
            if( col.tag == "Enemy" || col.tag == "Wall"  )
            {
                Debug.Log( "is dud" );
               // Messenger.Broadcast( "EnableReloadButtons" );
                Destroy( gameObject );  

            }
        }
        else
        {
            if( col.tag == "Enemy"  )
            {
                //Messenger.Broadcast( "EnableReloadButtons" );
                Destroy( gameObject );
            }
        } 
    }

    private void OnCollisionEnter2D( Collision2D col )
    {
        lifeSpan --;

        if( lifeSpan == 0 )
            Destroy( gameObject );
    }

    private IEnumerator BallLifeSpan()
    {
        Debug.Log( "Ball Life Span..." );
        yield return new WaitForSeconds( lifeSpan );
        
        //Messenger.Broadcast( "EnableReloadButtons" );
        Destroy( gameObject );
   
    }

    private void OnBecameInvisible() 
    {
         //Messenger.Broadcast( "EnableReloadButtons" );
         Destroy(gameObject);
     }
}