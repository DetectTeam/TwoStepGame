using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Vector3 cursorPoint;
    [SerializeField] private Vector3 initialPosition;
    [SerializeField] private Vector3 cursorPosition;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 screenPoint;
    [SerializeField] private Vector3 heading;
    [SerializeField] private Vector3 direction;

    [SerializeField] private GameObject compass;

    [SerializeField] private Transform toRotation;

    [SerializeField] private float z;

    [SerializeField] private float numDegrees;

    [SerializeField] private float movementLimt;


    [SerializeField] private GameObject ballPrefab;

    [SerializeField] private GameObject muzzle;

    

    [SerializeField] private TextMeshProUGUI sliderSensitivity;

    [SerializeField] private float speed;

    public enum Direction
    {
        LEFT,
        RIGHT
    }


    private bool mouseDrag;
    private void Update()
    {
         if( mouseDrag )
         {
            Quaternion targetRotation = Quaternion.identity;
            targetRotation *=  Quaternion.AngleAxis(z, Vector3.forward);

            compass.transform.rotation = Quaternion.Lerp ( compass.transform.rotation, targetRotation , ( Time.time * speed ) ); 
         }
    }

    [SerializeField] private Direction currentDirection;

    private void OnMouseDown()
    {
        cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y,     screenPoint.z);
        initialPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
    }

    private void OnMouseDrag()
    {
        

       CalculateClickPosition();
       CalculateDirection();

       float dist = Vector3.Distance(initialPosition, cursorPosition);
       
       if( dist > 0.5f )
        RotateCompass();
    }

    private void OnMouseUp()
    {
        mouseDrag = false;
        float dist = Vector3.Distance(initialPosition, cursorPosition);

        if( dist < 0.3f ) 
            Fire();
    }

    private void CalculateClickPosition()
    {
        cursorPoint = new Vector3( Input.mousePosition.x, Input.mousePosition.y, screenPoint.z );
        cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;       
    }

    private void CalculateDirection()
    {
         heading = cursorPosition - initialPosition;
        direction = heading / heading.magnitude;

        if( direction.x > 0 )
            currentDirection = Direction.RIGHT;
        else if( direction.x < 0 )
            currentDirection = Direction.LEFT;  
    }

    private void RotateCompass()
    {
        
        
        //z = compass.transform.rotation.z;
       // Debug.Log( z );
       
        if( currentDirection == Direction.LEFT )
        {
//           transform.rotation = Quaternion.Lerp( compass.transform.rotation, -90f, 1);
            z = z + numDegrees;
            if( z >= movementLimt )
            z = movementLimt;
        }
        else if( currentDirection == Direction.RIGHT )
        {
            z= z - numDegrees;
            if( z <= -movementLimt)
                z = -movementLimt;
        }

        mouseDrag = true;

       // compass.transform.eulerAngles = new Vector3(0, 0, z);
       // compass.transform.rotation = new Quaternion(0, 0, z, 0);
        //Vector3 rotationVector = new Vector3(0, 0, z);
       // compass.transform.localRotation = Quaternion.Euler( 0f, 0f, z );
       // var desiredRotation =  Quaternion.Euler( 0f, 0f, z );

        //  Quaternion targetRotation = Quaternion.identity;
        //  targetRotation *=  Quaternion.AngleAxis(z, Vector3.forward);

        //  compass.transform.rotation = Quaternion.Lerp ( compass.transform.rotation, targetRotation , ( Time.time * speed ) ); 
    }

    private void Fire()
    {
        var ball = Instantiate( ballPrefab, muzzle.transform.position, muzzle.transform.rotation ) as GameObject;
        ball.gameObject.SetActive(true);
        
        
       // ball.GetComponent<Rigidbody2D>().AddForce(-direction);
        ball.GetComponent<Rigidbody2D>().AddForce( muzzle.transform.up * 100 );
    }

    public void SetSliderSensitivity( float value )
    {
        sliderSensitivity.text = value.ToString();
        numDegrees = value;
    }
   
}
