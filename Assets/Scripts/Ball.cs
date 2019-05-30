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
    [SerializeField] private GameObject deadEffect;
    [SerializeField] private float moveSpeed = 10;

    [SerializeField] private bool isDemo = false;

    

    public float MoveSpeed { get{ return moveSpeed; } set{ moveSpeed = value; } }

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if( isDud )
        {
            //moveSpeed = 3;
            StartCoroutine( BallLifeSpan() );

             Physics2D.IgnoreLayerCollision( 8, 11, true );
        }
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = rigidbody2D.velocity.normalized * moveSpeed;
    }


    private bool isDead = false;

    private void OnTriggerEnter2D(Collider2D col)
    {    
        if( isDud )
        {
            if( col.tag == "Enemy"  )
            {
                Debug.Log( "is dud" );
               // Messenger.Broadcast( "EnableReloadButtons" );
                //gameObject.SetActive( false );
                Destroy( gameObject );
              
                StartCoroutine( BallLifeSpan() );
            }
        }
        else
        {
            if( col.tag == "Enemy"  )
            {
                //Messenger.Broadcast( "EnableReloadButtons" );
                //gameObject.SetActive( false );
                BallCounter.Instance.DecrementBallCount();
                Destroy( gameObject );   
            }
        }

        if( !isDud && col.CompareTag( "BreakableWall" ) || col.CompareTag( "Enemy" ) )
        {
            GameObject effectObj = Instantiate( deadEffect, col.transform.position, Quaternion.identity );
            var main = effectObj.transform.Find( "Red" ).GetComponent<ParticleSystem>().main;
             main.startColor = gameObject.GetComponent<SpriteRenderer>().color;

            //Destroy( effectObj, 0.25f );
        } 

       
    }

    private void OnCollisionEnter2D( Collision2D col )
    {
        Debug.Log( col.gameObject.name );
        
        lifeSpan --;

        if( lifeSpan == 0 )
        {
            if( !isDud && col.gameObject.name != "LeftGround" && col.gameObject.name != "RightGround"  )
                BallCounter.Instance.DecrementBallCount();

            gameObject.SetActive( false );
        }

        if( col.gameObject.CompareTag( "Slot" ) || col.gameObject.CompareTag( "Crusher" )  )
        {
            Messenger.Broadcast( "EnableReloadButtons" );
            //gameObject.SetActive( false );
            Destroy( gameObject );
        }
    }


    private IEnumerator BallLifeSpan()
    {
        Debug.Log( "Ball Life Span..." );
        yield return new WaitForSeconds( lifeSpan );

        DeathEffect();
        //Messenger.Broadcast( "EnableReloadButtons" );
        gameObject.SetActive( false );
   
    }

    // private void OnBecameInvisible() 
    // {
    //     if( !isDemo ) 
    //     {
    //         Messenger.Broadcast( "EnableReloadButtons" );
    //         //BallCounter.Instance.DecrementBallCount();

    //      //gameObject.SetActive( false );
    //      Destroy( gameObject );
    //     }
    // }

     private void DeathEffect()
     {
        GameObject effectObj = Instantiate( deadEffect, gameObject.transform.position, Quaternion.identity );
        var main = effectObj.transform.Find( "Red" ).GetComponent<ParticleSystem>().main;
        main.startColor = gameObject.GetComponent<SpriteRenderer>().color;
        BallCounter.Instance.DecrementBallCount();
        Destroy( effectObj, 0.25f );
         
     }
}