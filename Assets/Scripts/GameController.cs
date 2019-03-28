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


    public enum Direction
    {
        LEFT,
        RIGHT
    }

    private void Start()
    {
        compass.transform.rotation = Quaternion.identity;
    }


    [SerializeField] private float angle;

	private void Update()
    {
        //rotation:
		//Vector3 mousePos = new Vector3( Input.mousePosition.x,Input.mousePosition.y,Camera.main.transform.position.y );
		//Vector3 worldPos = Camera.main.ScreenToWorldPoint( mousePos );
		///iTween.LookUpdate( compass ,iTween.Hash( "looktarget", worldPos, "time", 2, "axis", "x" ) );
      
        //Get the Screen positions of the object
         Vector2 positionOnScreen = Camera.main.WorldToViewportPoint ( compass.transform.position );
         
         //Get the Screen position of the mouse
         Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
         
         //Get the angle between the points
         angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
         
        angle+= 80;
       

         if( angle >= 80 )
            angle = 80;

         if( angle <= -80 )
            angle = -80;  
 
         //Ta Daaa
         compass.transform.rotation =  Quaternion.Euler ( new Vector3( 0f,0f,angle ) );

         if ( Input.GetMouseButtonUp(0)  && !IsOverUI() && isReloaded )
         {
            Fire();
            isReloaded = false;
         }

		//fire:
		// if(Input.GetMouseButtonDown(0)){
		// 	Fire();
		// }
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

    private void OnMouseDrag()
    {
        

       //CalculateClickPosition();
       //CalculateDirection();

       float dist = Vector3.Distance(initialPosition, cursorPosition);
       
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

    private void Fire()
    {
        //var ball = Instantiate( ballPrefab, muzzle.transform.position, muzzle.transform.rotation ) as GameObject;
        //ball.gameObject.SetActive(true);

        if ( isWhiteBullet )
                isDud = CheckForDud( 20 );
            else
                isDud = CheckForDud( 80 );

        GameObject ball = CreateBall( isDud  );

        ball.transform.position = muzzle.transform.position;
        ball.transform.rotation = muzzle.transform.rotation;

         if( isWhiteBullet )
            ball.GetComponent<SpriteRenderer>().color = white;
        else
            ball.GetComponent<SpriteRenderer>().color = red;
        
        ball.gameObject.SetActive(true);
        
       // ball.GetComponent<Rigidbody2D>().AddForce(-direction);
        ball.GetComponent<Rigidbody2D>().AddForce( muzzle.transform.up * 100 );
    }

    public void Reload()
    {
        isReloaded = true;
        Messenger.Broadcast( "DisableReloadButtons" );
        StartCoroutine( EnableReloadButtons( ) );
    }

    private IEnumerator EnableReloadButtons()
    {
        yield return new WaitForSeconds( 1.0f );
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

    public bool CheckForDud( float percentage )
    {
        bool b;
        var rand = Random.value;

        Debug.Log( "Check For Dud: " + rand + " " + ( percentage / 100 ) );

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

        Debug.Log( "Rand: " + rand + " " + percentage / 100 );
        
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
    
   
}
