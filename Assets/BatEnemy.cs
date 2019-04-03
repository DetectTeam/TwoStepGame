using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 2;
    [SerializeField] private float frequency = 20f;
    [SerializeField] private float magnitude = .1f;
    [SerializeField] private bool facingRight = true;

    [SerializeField] private Vector3 pos;
    [SerializeField] private Vector3 localScale;


[SerializeField] private int hitPoints;

    private void Start()
    {
        pos = transform.position;
        localScale = transform.localScale;
    }

    private void Update()
    {
        CheckFacingDirection();

        if( facingRight )
            MoveRight();
        else
            MoveLeft();
    }

    private void CheckFacingDirection()
    {
        if( pos.x < - 7f )
        {
            facingRight = true;
        }
        else if( pos.x > 7f )
        {
            facingRight = false;
        }
        
        if( Mathf.Abs( pos.x ) >= 6f )
            MoveDown();

        // if( ( ( facingRight ) && (localScale.x < 0 ) ) || ( ( !facingRight ) && ( localScale.x > 0 ) ) )
        // {
        //     localScale *= -1;
        // }

        //transform.localScale *= -1;
    }

    private void MoveRight()
    {
        pos += transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Sin( Time.time * frequency ) * magnitude;
    }

    private void MoveDown()
    {
        pos -= transform.up * Time.deltaTime * moveSpeed;
    }


    private void MoveLeft()
    {
        pos -= transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Sin( Time.time * frequency ) * magnitude;
    }

    private void OnTriggerEnter2D( Collider2D collider2d )
    {
    
       if( collider2d.CompareTag( "Ball" ) )
            Destroy( gameObject );

        if( collider2d.CompareTag( "Dud" ) )
        {
            if( hitPoints == 0 )
                Destroy( gameObject );
            
            hitPoints --;
        }
   }


}
