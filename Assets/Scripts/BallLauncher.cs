using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class BallLauncher : MonoBehaviour
{
    private Vector3 startDragPosition;
    private Vector3 endDragPosition;
    private BlockSpawner blockSpawner;
    [SerializeField] private LaunchPreview launchPreview;
    private List<Ball> balls = new List<Ball>();
    private int ballsReady;

    private bool isDraggable = true;

   [SerializeField] private Button leftButton;
   [SerializeField] private Button rightButton;
 
    private bool isDud;
    [SerializeField] private bool needReload;
   
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject dudBallPrefab;

    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;

    [SerializeField] private bool isWhiteBullet = true;

    [SerializeField] private Color white;
    [SerializeField] private Color red;


    

    private void Awake()
    {
        blockSpawner = FindObjectOfType<BlockSpawner>();
        launchPreview = GetComponent<LaunchPreview>();

        //CreateBall();
    }

    private void start()
    {
        launchPreview.SetStartPoint(transform.position);
    }

    public void ReturnBall()
    {
        ballsReady++;
        if (ballsReady == balls.Count)
        {
            blockSpawner.SpawnRowOfBlocks();
            //CreateBall();
        }
    }

    private GameObject CreateBall( bool b )
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
        //balls.Add(ball);
       // ballsReady++;

        return ball;
    }

    private void Update()
    {
        if( !isDraggable )
            return;
       
        // if( !EventSystem.current.IsPointerOverGameObject() )
        // {
        //     return;
        // }

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.back * -10;
                
        if (Input.GetMouseButtonDown(0))
        {
            StartDrag(worldPosition);
        }
        else if (Input.GetMouseButton(0))
        {
            ContinueDrag(worldPosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            EndDrag(worldPosition);
        }       
    }

    private void EndDrag( Vector3 worldPosition )
    {
        Debug.Log( "End drag" );
        
        float distance = Vector3.Distance( startPosition , worldPosition );
        Debug.Log( "Distance : " + distance );
        if( distance > 1.0f )
        {
            StartCoroutine(LaunchBalls());
            Messenger.Broadcast( "DisableReloadButtons" );
        }
    }

    private IEnumerator LaunchBalls()
    {
        Vector3 direction = endDragPosition - startDragPosition;
        direction.Normalize();

        if( !needReload )
        {     
            if ( isWhiteBullet )
                isDud = CheckForDud( 50 );
            else
                isDud = CheckForDud( 80 );
                
            GameObject ball = CreateBall( isDud  );

            ball.transform.position = transform.position;

            if( isWhiteBullet )
                ball.GetComponent<SpriteRenderer>().color = white;
            else
                ball.GetComponent<SpriteRenderer>().color = red;
              
            ball.gameObject.SetActive(true);
            ball.GetComponent<Rigidbody2D>().AddForce(-direction);

            needReload = true;

        }

        yield return new WaitForSeconds(0.25f);

        DisableDrag();

            
        
       // ballsReady = 0;
    }

    private void ContinueDrag(Vector3 worldPosition)
    {
        endDragPosition = worldPosition;

        Vector3 direction = endDragPosition - startDragPosition;

        launchPreview.SetEndPoint(transform.position - direction);
    }

    private void StartDrag(Vector3 worldPosition)
    {
        startDragPosition = worldPosition;
        launchPreview.SetStartPoint(transform.position);
        startPosition = worldPosition;
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

    public void Reload()
    {
        Debug.Log( "Reloading ......" );
        Messenger.Broadcast( "DisableReloadButtons" );
        needReload = false;
        EnableDrag();
    }

    private void DisableDrag()
    {
        Debug.Log( "Disable Collider" );
        isDraggable = false;
       // GetComponent<Collider2D>().enabled = false;
    }

    private void EnableDrag()
    {
        isDraggable = true;
        //GetComponent<Collider2D>().enabled = true;
    }

    public void BulletColourProbability( float percentage )
    {  
       
        var rand = Random.value;

        Debug.Log( "Rand: " + rand + " " + percentage / 100 );
        
        if ( rand < ( percentage / 100 ) )
            isWhiteBullet = true;
        else 
            isWhiteBullet = false;             
    }
}
