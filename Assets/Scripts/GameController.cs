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
    [SerializeField] private GameObject dudBallPrefab;

    [SerializeField] private GameObject muzzle;

    

    [SerializeField] private TextMeshProUGUI sliderSensitivity;

    [SerializeField] private float speed;

     private bool mouseDrag;

     private bool isReloaded = true;

    [SerializeField] private bool isWhiteBullet = true;
    [SerializeField] private bool isDud;

    [SerializeField] private Color white;
    [SerializeField] private Color red;
    [SerializeField] private Color grey;

    [SerializeField] private GameObject currentBall;
    [SerializeField] private SpriteRenderer ballContainer;
    [SerializeField] private GameObject currentBallIcon;

    [SerializeField] private float percentageWhiteBallIsDud;
    [SerializeField] private float percentageRedBallIsDud;

    [SerializeField] private float ballSpeed = 20f;
    private float originalBallSpeed;


     [SerializeField] private float mouseXThreshold = 0.01f;
     [SerializeField] private float mouseYThreshold = 0.01f;
     [SerializeField] private bool isMoving;

     [SerializeField] private GameObject speedometer;
     private TextMeshPro speedometerText;


    public enum Direction
    {
        LEFT,
        RIGHT
    }

    private void Start()
    {
        compass.transform.rotation = Quaternion.identity;
        BulletColourProbability( 70 );
        BuildBullet();
        isReloaded = true;

        originalBallSpeed = ballSpeed;

        speedometerText = speedometer.GetComponent<TextMeshPro>();
        
    }


    [SerializeField] private float angle;

	private void Update()
    {
            
        //rotation:
        //Vector3 mousePos = new Vector3( Input.mousePosition.x,Input.mousePosition.y,Camera.main.transform.position.y );
        //Vector3 worldPos = Camera.main.ScreenToWorldPoint( mousePos );
        ///iTween.LookUpdate( compass ,iTween.Hash( "looktarget", worldPos, "time", 2, "axis", "x" ) );

        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.ScreenToWorldPoint(compass.transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = new Vector2(mouseOnScreen.x - compass.transform.position.x,
                                         mouseOnScreen.y - compass.transform.position.y);

        //compass.transform.up = direction;

        //  //Get the angle between the points
        angle = AngleBetweenTwoPoints(compass.transform.position, mouseOnScreen);

        // angle+= 80;


        if (!IsOverUI())
            compass.transform.up = direction;

        //  //Ta Daaa
        //  compass.transform.rotation =  Quaternion.Euler ( new Vector3( 0f,0f,angle ) );
       
      if( Input.GetMouseButton(0) && !IsOverUI() && isReloaded )
       {

            if ( !HasMouseMoved() )
            {
                ChangeBallSpeed( mouseOnScreen );   
            }
            else
            {     
                speedometer.SetActive( false );
                isMoving = true;
                ballSpeed = 20;
                s = 20;
            }
       }

        if (Input.GetMouseButtonUp(0) && !IsOverUI() && isReloaded)
        {
            Fire();
            isReloaded = false;
            speedometer.SetActive( false );
            timeCount = 0;
            s = 20;
            speedometerText.SetText("");

        }
    }

    private int count = 0;
    private int s = 20;

    private float speedDelay = 1.0f;
    private float timeCount = 0;
    
    private void ChangeBallSpeed( Vector2 mouseOnScreen )
    {   
            timeCount += Time.deltaTime;
            if( timeCount < speedDelay )
                return;

            speedometer.SetActive( true );

            ballSpeed += 0.1f;
            count ++;

            isMoving = false;
                 
            if (ballSpeed >= 40)
                ballSpeed = 40; 

            if( count == 10 )
            {
                  s = s + 1;
                if( s > 40 )
                        s = 40;
                    speedometerText.SetText( s.ToString() );
                    count = 0;
            }

                speedometer.transform.position = new Vector2( mouseOnScreen.x,  mouseOnScreen.y + 1.0f );          
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
         return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
     }


    // private void Update()
    // {
    //      if( mouseDrag )
    //      {
    //         Quaternion targetRotation = Quaternion.identity;
    //         targetRotation *=  Quaternion.AngleAxis(z, Vector3.forward);

    //         compass.transform.rotation = Quaternion.Lerp ( compass.transform.rotation, targetRotation , ( Time.time * speed ) ); 
    //      }
    // }

    [SerializeField] private Direction currentDirection;

    private void OnMouseDown()
    {
        cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y,     screenPoint.z);
        initialPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
    }

    [SerializeField] private float dist;
    private void OnMouseDrag()
    {
        

       //CalculateClickPosition();
       //CalculateDirection();

       dist = Vector3.Distance(initialPosition, cursorPosition);

       if( dist > 0.25f )
       {
         ballSpeed = ballSpeed  + 0.15f;
             
             if( ballSpeed >= 40 )
                ballSpeed = 40;
       }
       
       //if( dist > 0.5f )
        //RotateCompass();
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

    public void Fire()
    {
        ballContainer.color = grey;
        currentBallIcon.SetActive( false );

        currentBall.transform.position = muzzle.transform.position;
        currentBall.transform.rotation = muzzle.transform.rotation;

        currentBall.GetComponent<Ball>().MoveSpeed = ballSpeed;

        currentBall.SetActive(true);

        // ball.GetComponent<Rigidbody2D>().AddForce(-direction);
        currentBall.GetComponent<Rigidbody2D>().AddForce( muzzle.transform.up * 100 );
        StartCoroutine( EnableReloadButtons( ) );

        ballSpeed = originalBallSpeed;
    }

    public void Reload()
    {
        isReloaded = true;
        Messenger.Broadcast( "DisableReloadButtons" );
       
    }

    private IEnumerator EnableReloadButtons()
    {
        yield return new WaitForSeconds( 0.25f );
         Messenger.Broadcast( "EnableReloadButtons" );
    }

    public void SetSliderSensitivity( float value )
    {
        sliderSensitivity.text = value.ToString();
        numDegrees = value;
    }

    public static bool IsOverUI()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);

        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        if (results.Count > 0)
        {
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.GetComponent<CanvasRenderer>())
                {
                    return true;
                }
            }
        }

        return false;
    } 

    bool HasMouseMoved()
     {
         //I feel dirty even doing this 
         return (Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0);
     }

    public bool CheckForDud( float percentage )
    {
        bool b;
        var rand = Random.value;

        //Debug.Log( "Check For Dud: " + rand + " " + ( percentage / 100 ) );

        //Weighted Random goes here
       if( rand < ( percentage / 100 ) )
        b = true;
       else
        b = false;

        return b;
    }

    public void BulletColourProbability( float percentage )
    {  
        var rand = Random.value;
        bool b = false;

        //Debug.Log( "Rand: " + rand + " " + percentage / 100 );
        
        if ( rand < ( percentage / 100 ) )
            isWhiteBullet = true;
        else 
            isWhiteBullet = false;       
        
    }

      public GameObject CreateBall( bool b )
    {
        GameObject ball = null;

        if( b )
        { 
            ball = Instantiate( dudBallPrefab ) as GameObject;

        }
        else
        {
             ball = Instantiate( ballPrefab ) as GameObject;
        }
     
        return ball;
    }


    public void BuildBullet()
    {
        if ( isWhiteBullet )
            isDud = CheckForDud( percentageWhiteBallIsDud);
        else
            isDud = CheckForDud( percentageRedBallIsDud );

        GameObject ball = CreateBall( isDud  );

        if( isDud )
            currentBallIcon.SetActive( true );
        else
            currentBallIcon.SetActive( false );   

        if( isWhiteBullet )
        {
            ball.GetComponent<SpriteRenderer>().color = white;
            ballContainer.color = white;
        }
        else
        {
            ball.GetComponent<SpriteRenderer>().color = red;
            ballContainer.color = red;
        }

        currentBall = ball;
        currentBall.SetActive( false );
    }
}
