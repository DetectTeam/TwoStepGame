using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TutorialManager : MonoBehaviour
{
    
    public static TutorialManager Instance;

    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;

    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject superBall;
    
    [SerializeField] private GameObject muzzle;

    [SerializeField] private GameObject popUp;
    [SerializeField] private GameObject dialog;
    [SerializeField] private GameObject gamePopUp;

    [SerializeField] private GameObject targetCircle;
    [SerializeField] private TextMeshProUGUI popUpMessage;

    [SerializeField] private GameController gameController;

    [SerializeField] private bool isNext;

    [SerializeField] private bool isLeft;
    [SerializeField] private bool isNextTutorial = false;
    [SerializeField] private bool isSuperBall = true;

    //Tutorial 2
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject tutDiamond;
    [SerializeField] private GameObject diamond;
    [SerializeField] private GameObject gate;

    [SerializeField] private Color red;
    [SerializeField] private Color green;

    [SerializeField] private GameObject greenBallContainer;
    [SerializeField] private GameObject redBallContainer;

    [SerializeField] private GameObject cannonTurret;
    [SerializeField] private GameObject demoTurret;
    

    [SerializeField] private List<GameObject> muzzleLists = new List<GameObject>();
    

    private void OnEnable()
    {
        Messenger.AddListener( "Continue" , ContinueTutorial );
        Messenger.MarkAsPermanent( "Continue" );
    }

    private void OnDisable()
    {
        Messenger.RemoveListener( "Continue" , ContinueTutorial );
    }

    private void Awake()
    {
        //Check if instance already exists
        if (Instance == null)
            //if not, set instance to this
            Instance = this;
        	//If instance already exists and it's not this:
        else if (Instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject); 
    }

    public IEnumerator Start()
    {  
        tutDiamond.SetActive( false );
        
        targetCircle.SetActive( false );

        leftButton.interactable = false;
        rightButton.interactable = false;
        
        gate.SetActive( false );
        
        // StartCoroutine( TutorialOne() );

        // yield return new WaitUntil( () => isNextTutorial == true );

        // Debug.Log( "Starting Tutorial Two" );
        // isNextTutorial = false;

        // StartCoroutine( TutorialTwo() );

        // yield return new WaitUntil( () => isNextTutorial == true );

        // Debug.Log( "Starting Tutorial Three" );
        // isNextTutorial = false;

        // yield return null;

        // isNextTutorial = false;
        // StartCoroutine( TutorialThree() );

       // yield return new WaitUntil( () => isNextTutorial == true );

       // isNextTutorial = false;
        StartCoroutine( TutorialFour() );

        yield return new WaitUntil( () => isNextTutorial == true );

        //Display Play Game Menu
        gamePopUp.SetActive( true );


    }

    public void Fire()
    {
        //Debug.Log( "Firing....." );
        Messenger.Broadcast( "DisableReloadButtons" );
        MoveNext();

       StartCoroutine( "IEFire" );
    }


    private int lCount = 0;
    private int rCount = 0;

    private IEnumerator IEFire()
    {

        yield return new WaitForSeconds( 0.5f );
        
        GameObject b = null;

        Color currentColor = green;
        
        if( !isSuperBall )
        {
            b = Instantiate( ball, muzzle.transform.position, muzzle.transform.rotation ) as GameObject;
        }
        else
        {
             b = Instantiate( superBall, muzzle.transform.position, muzzle.transform.rotation ) as GameObject;
        }
             

        //yield return new WaitUntil( () => isNext == true );
        //TogglePopUp();

        if( isLeft )
        {
            lCount ++;
            if( lCount <= 3  )
            {
                b.GetComponent<SpriteRenderer>().color = green;
                currentColor = green;
               
            }
            else
            {
                 b.GetComponent<SpriteRenderer>().color = red;
                 currentColor = red;
                 lCount = 0;
            }
        }
        else
        {
            rCount ++;
            if( rCount <= 3  )
            {
                b.GetComponent<SpriteRenderer>().color = red;
                currentColor = red;
            }
            else
            {
                 b.GetComponent<SpriteRenderer>().color = green;
                 currentColor = green;
                 rCount = 0;
            }
        }

         if( isSuperBall )
         {  
            
            b.GetComponent<TrailRenderer>().startColor = currentColor;
            b.transform.Find( "PlayerLight" ).GetComponent<Light>().color = currentColor;      
         }

        b.GetComponent<Rigidbody2D>().AddForce( muzzle.transform.up * 100 );
    }

    public void MoveNext()
    {
        isNext = true;
    }


    private bool continueTutorial;
    private void ContinueTutorial()
    {
        continueTutorial = true;
    }

    private IEnumerator TutorialOne()
    {
        isNext = false;
        Debug.Log( "Starting Tutorial 1...." );

        rightButton.interactable = false;

        
        PositionObj( new Vector2( 3f, 6f ) , -45.0f );
        tutDiamond.SetActive( true );

        //Display Dialog
        Debug.Log( "Welcome! In this game, we want to study how you make a choice betweentwo options. To get the science right, some parts of the game might annoyyou (sorry!), but to make up for that, we are letting you fire balls at stuff!" );
        //popUpMessage.text = "Welcome to this tutorial";
        TogglePopUp( );
        SetPopUpText( "Welcome! In this game, we want to study how you make a choice between two options. To get the science right, some parts of the game might annoy you (sorry!), but to make up for that, we are letting you fire balls at stuff!"  );

        yield return new WaitUntil( () => isNext == true );
        
        isNext = false;

        Debug.Log( "The objective of this game is to get the diamond by shooting it" );
        SetPopUpText( "Let’s get started. The objective of the game is to get the diamond."   );

        yield return new WaitUntil( () => isNext == true );

        isNext = false;
       
        Debug.Log( "Tap the screen in the direction you want to aim" );
        SetPopUpText( "Tap the screen in the direction you want to aim"  );
        targetCircle.SetActive( true );

         yield return new WaitUntil( () => isNext == true );
        
        TogglePopUp();
        
        Debug.Log( "Waiting for user to aim...." );

        isNext = false;

        yield return new WaitUntil( () => isNext == true );

        Debug.Log( "Got this far...." );
        targetCircle.SetActive( false );
        gameController.HasAimed = true;
        Debug.Log( "See how the cannon moved? . Now Press the fire button to launch a ball" );
        SetPopUpText( "See how the cannon moved? . Now press the left fire button to launch a ball" ); 

        leftButton.interactable = true;
        
        TogglePopUp();

        isNext = false;

         yield return new WaitUntil( () => isNext == true );
        
        Debug.Log( "......" );
        TogglePopUp();
        
        isNext = false;

        yield return new WaitUntil( () => isNext == true );

        Debug.Log( "Shot Taken...." );
        isNext = false;

        yield return new WaitUntil( () => isNext == true );

        yield return new WaitForSeconds( 0.5f );
         
        TogglePopUp();
        SetPopUpText( "Well done!. You have collected your first diamond." ); 

        isNext = false;

        yield return new WaitUntil( () => isNext == true );

         TogglePopUp();
         gameController.HasAimed = false;
         isNextTutorial = true;
   
    }

    private IEnumerator TutorialTwo()
    {
        yield return null;

        isNext = false;

        leftButton.interactable = true;
        rightButton.enabled = false;

        //isLeft = true;

        Debug.Log( "Starting Tutorial 2" );

        tutDiamond.SetActive( true );

        TogglePopUp();
        SetPopUpText( "Now using the left button to collect all the diamonds..." ); 

        isNext = false;

        yield return new WaitUntil( () => isNext == true );

        TogglePopUp();

        //Tutorial 2 left Button test
        yield return new WaitUntil(() => continueTutorial == true);
        yield return new WaitForSeconds( 0.5f );

        PositionObj( new Vector2( 2.0f, 1.0f ) , -45.0f );
        
        yield return new WaitUntil(() => continueTutorial == true);
        yield return new WaitForSeconds( 0.5f );

        PositionObj( new Vector2( -3.0f, 3.0f ) , 90.0f );
       
        yield return new WaitUntil(() => continueTutorial == true);
        yield return new WaitForSeconds( 0.5f );

        PositionObj( new Vector2( -3.0f, 6.0f ) , 45.0f );
     

        yield return new WaitUntil(() => continueTutorial == true);
        yield return new WaitForSeconds( 0.5f );

        PositionObj( new Vector2( -2.0f, 3.0f ) , 90.0f );
       

        yield return new WaitUntil(() => continueTutorial == true);
        yield return new WaitForSeconds( 0.5f );
       

        tutDiamond.SetActive( false );
        TogglePopUp();
        SetPopUpText( "Well done!. Now try with the right fire button " ); 
        leftButton.enabled = false;
        rightButton.enabled = true;

        isNext = false;

        yield return new WaitUntil( () => isNext == true );
        TogglePopUp();

        //Right Trigger
         isLeft = false;

      
        

        tutDiamond.SetActive( true );

        //Tutorial 2 right button test
        
        PositionObj( new Vector2( -3.0f, 4.0f ) , -90.0f );

        yield return new WaitUntil(() => continueTutorial == true);
        yield return new WaitForSeconds( 0.5f );
       

        PositionObj( new Vector2( -2.0f, 1.0f ) , 90.0f );
        

        yield return new WaitUntil(() => continueTutorial == true);
        yield return new WaitForSeconds( 0.5f );

        PositionObj( new Vector2( 0.0f, 2.0f ) , 45.0f );
        

        yield return new WaitUntil(() => continueTutorial == true);
        yield return new WaitForSeconds( 0.5f );

        TogglePopUp();
        SetPopUpText( "Well done!. " ); 

        isNext = false;

        yield return new WaitUntil( () => isNext == true );

        SetPopUpText( "As you'll have noticed there are two colours of ball; red and green." ); 
        isNext = false;

        yield return new WaitUntil( () => isNext == true );

        SetPopUpText( "The left container has more greens than red and the right container has more reds than greens." ); 
        isNext = false;

        yield return new WaitUntil( () => isNext == true );

        TogglePopUp();

        isNextTutorial = true;

        Debug.Log( "End Tutorial 3" );

        leftButton.enabled = true;
        
    }

    private void PositionObj( Vector2 position, float rotation )
    {
        continueTutorial = false;
        Debug.Log( "Repositioning Block" );

        diamond.SetActive(true);
        SetPosition(tutDiamond.transform, position );
        block.SetActive(false);

        block.SetActive(true);
        SetRotation( block.transform,  rotation );
    }

    //Tutorial 2

    private void SetPosition( Transform obj, Vector2 position )
    {
        obj.localPosition = position;
    }

    private void SetRotation( Transform obj, float rotationAmt )
    {
        //obj.eulerAngles = new Vector3( 0, 0, rotationAmt );
        obj.Rotate(0, 0, rotationAmt, Space.Self);
    }

    //Ends


    //Tutorial 3 breaking blocks

    private int dudCount = 0;

    private IEnumerator TutorialThree()
    {
        yield return null;

        //isLeft = true;

        SetPopUpText( "Now you will practice aiming and firing balls at a blue wall to break it. You need to break this wall before you can shoot the diamond" ); 

        TogglePopUp();
       
        isNext = false;
        yield return new WaitUntil( () => isNext == true );
        TogglePopUp();

        leftButton.interactable = true;
        rightButton.interactable = true;

        tutDiamond.SetActive( true );
        gate.SetActive( true );
        PositionObj( new Vector2( 0.0f, 3.0f ) , 180f );

        continueTutorial = false;
        yield return new WaitUntil(() => continueTutorial == true);
        

        //Sometimes the red and green bullets will be hyper-charged.
        tutDiamond.SetActive( true );
        SetPopUpText( "Good Job! It takes one ball to destroy a wall." ); 
        TogglePopUp();

        isNext = false;

        yield return new WaitUntil( () => isNext == true );
        TogglePopUp();

        isNextTutorial = true;

        Debug.Log( "End Tutorial 3" );

    }

    private IEnumerator TutorialFour()
    {
        Debug.Log( "Starting Tutorial 4" );

        tutDiamond.SetActive( false );

        //cannonButtons.SetActive( false );
        //cannonControls.SetActive( false );
        cannonTurret.SetActive( false );
        demoTurret.SetActive( true );
        
        SetPopUpText( "Here comes the annoying part. Sometimes the ball will be a dud!" ); 

        TogglePopUp();
       
        isNext = false;
        yield return new WaitUntil( () => isNext == true );
        TogglePopUp();

    
        SetPopUpText( "You’ll know it when you see it - a dud is a ball that just doesn’t work." ); 

        TogglePopUp();
       
        isNext = false;
        yield return new WaitUntil( () => isNext == true );
        TogglePopUp();


        //Trigger Dud ball animation here ......


        //Red and Green balls can both be normal

        SetPopUpText( "Both greens and reds can be duds and things tend to happen in streaks" ); 

     

        TogglePopUp();
       
        isNext = false;
        yield return new WaitUntil( () => isNext == true );
        TogglePopUp();


        //Sometimes, most green balls will be duds...

        SetPopUpText( "For a while, most of the greens could be duds..." ); 

        dialog.transform.position =  new Vector3( dialog.transform.position.x, dialog.transform.position.y + 400f, dialog.transform.position.z );
        TogglePopUp();
        
         muzzleLists[2].SetActive( true );


        isNext = false;
        yield return new WaitUntil( () => isNext == true );
        TogglePopUp();

        //...and sometimes, most red balls will be normal

        SetPopUpText( "...and then later green balls will become good. " ); 
        TogglePopUp();

        muzzleLists[2].SetActive( false );
        muzzleLists[0].SetActive( true );
        
       
        isNext = false;
        yield return new WaitUntil( () => isNext == true );
        TogglePopUp();


        SetPopUpText( "Likewise, the reds might start out great... " ); 
        TogglePopUp();

        muzzleLists[0].SetActive( false );
        muzzleLists[1].SetActive( true );
        
       
        isNext = false;
        yield return new WaitUntil( () => isNext == true );
        TogglePopUp();


        SetPopUpText( "...and then go through a stretch of being mostly duds." ); 
        TogglePopUp();

        muzzleLists[1].SetActive( false );
        muzzleLists[3].SetActive( true );
        
       
        isNext = false;
        yield return new WaitUntil( () => isNext == true );
        TogglePopUp();

        dialog.transform.position =  new Vector3( dialog.transform.position.x, dialog.transform.position.y - 400f, dialog.transform.position.z );
      

        muzzleLists[3].SetActive( false );
        demoTurret.SetActive( false );
        cannonTurret.SetActive( true );
        //This changes over time so you need to pay attention!

        SetPopUpText( "The proportion of Duds that are green and the proportion of Duds that are red changes over time so you need to pay attention!" ); 

        TogglePopUp();
       
        isNext = false;
        yield return new WaitUntil( () => isNext == true );
        TogglePopUp();

        cannonTurret.SetActive( true );

        isNextTutorial = true;

    }

    //Ends

    public void ToggleGamePopUp()
    {
        if( gamePopUp.activeSelf )
            gamePopUp.SetActive( false );
        else
            gamePopUp.SetActive( true );
    }

    public void TogglePopUp( )
    {
        if( popUp.activeSelf )
            popUp.SetActive( false );
        else
            popUp.SetActive( true );
    }

    

    private void SetPopUpText( string message )
    {
        popUpMessage.text = message;
    }

    public void IsLeft( bool b )
    {
        isLeft = b;
    }

}
