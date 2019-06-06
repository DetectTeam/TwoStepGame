using System;
using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour, IPoolable
{
    private Rigidbody2D enemyRigidBody2D;
    private Animator animator;
  
   [SerializeField] public float enemySpeed = -0.5f;
   private bool isMoving = false;
   [SerializeField] private bool isDeath = false;
   [SerializeField] private Transform target;
   public Transform Target { get{ return target; } set{ target = value; } }

   public event Action OnDestroyEvent = delegate { };
    
    private void OnEnable()
    {
       // enemySpeed = Random.Range( -0.4f, -2f );
        StartCoroutine( "ZombieWalk" );
    }

    private void OnDisable() { OnDestroyEvent(); }

    // Use this for initialization
    private void Awake()
    {
         enemyRigidBody2D = GetComponent<Rigidbody2D>(); 
         animator = GetComponent<Animator>();
    }

    private IEnumerator ZombieWalk()
    {
        yield return new WaitForSeconds( 0.5f );
        animator.SetBool( "Forward", true );
        isDeath = false;
        GetComponent<Collider2D>().enabled = true;
        yield return new WaitForSeconds( 0.5f );
      
        isMoving = true;
    }

    // Update is called once per frame
    public void Update()
    {  
        if( isDeath )
        {
            Debug.Log( "Stopping Movement" );
           
            StartCoroutine( "Death" );
           // isDeath = false;   
            isMoving = false;
        }
        
        if( isMoving  )
        {
            float step =  enemySpeed * Time.deltaTime;

            enemyRigidBody2D.velocity = new Vector2( 0.0f, enemySpeed );
            //enemyRigidBody2D.velocity = Vector3.MoveTowards( transform.position, target.position, step );
        } 
    }

    public IEnumerator Death()
    {
       
        Debug.Log( "Death" );
        animator.SetTrigger( "Death" );
        yield return new WaitForSeconds( 2.0f );
        gameObject.SetActive( false );  
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log( "Hit by" + col.name );

        if( col.tag == "Ball" )
        {
            isMoving = false;
            enemyRigidBody2D.velocity = Vector2.zero;
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine( "Death" ); 
        }
    }


    // public void Flip()
    // {
    //     transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    //     _isFacingRight = transform.localScale.x > 0;
    // }

}