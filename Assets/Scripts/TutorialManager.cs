﻿using System.Collections;
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
        
        StartCoroutine( TutorialOne() );

        yield return new WaitUntil( () => isNextTutorial == true );

        Debug.Log( "Starting Tutorial Two" );
        isNextTutorial = false;

        StartCoroutine( TutorialTwo() );

        yield return new WaitUntil( () => isNextTutorial == true );

        Debug.Log( "Starting Tutorial Three" );
        isNextTutorial = false;

        yield return null;

        isNextTutorial = false;
        StartCoroutine( TutorialThree() );

        yield return new WaitUntil( () => isNextTutorial == true );

        isNextTutorial = false;
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
        Debug.Log( "Welcome to this tutorial...(Insert nice intro here...)" );
        //popUpMessage.text = "Welcome to this tutorial";
        TogglePopUp( );
        SetPopUpText( "Welcome to this tutorial (Insert nice intro here)"  );

        yield return new WaitUntil( () => isNext == true );
        

        isNext = false;

        Debug.Log( "The objective of this game is to get the diamond by shooting it" );
        SetPopUpText( "The objective of this game is to get the diamond"   );

         yield return new WaitUntil( () => isNext == true );

        isNext = false;
       
        Debug.Log( "Tap the screen in the direction you want to aim" );
        SetPopUpText( "Tap the screen in the direction you want to aim. In this case tap the golden circle."  );
        targetCircle.SetActive( true );

         yield return new WaitUntil( () => isNext == true );
        
        TogglePopUp();
        
        Debug.Log( "Waiting for user to aim...." );

        isNext = false;

        yield return new WaitUntil( () => isNext == true );

        Debug.Log( "Got this far...." );
        targetCircle.SetActive( false );
        gameController.HasAimed = true;
        Debug.Log( "Good job! . Now Press the fire button to launch a ball" );
        SetPopUpText( "Good job! . Now Press the fire button to launch a ball" ); 

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
        rightButton.interactable = false;

        //isLeft = true;

        Debug.Log( "Starting Tutorial 2" );

        tutDiamond.SetActive( true );

        TogglePopUp();
        SetPopUpText( "Now using the left button aim and hit all the diamonds..." ); 

        isNext = false;

        yield return new WaitUntil( () => isNext == true );

        TogglePopUp();

        //Tutorial 2 left Button test
        yield return new WaitUntil(() => continueTutorial == true);
        yield return new WaitForSeconds( 0.5f );

        PositionObj( new Vector2( 2.0f, 1.0f ) , -45.0f );
        rightButton.interactable = false;

        yield return new WaitUntil(() => continueTutorial == true);
        yield return new WaitForSeconds( 0.5f );

        PositionObj( new Vector2( -3.0f, 3.0f ) , 90.0f );
        rightButton.interactable = false;

        yield return new WaitUntil(() => continueTutorial == true);
        yield return new WaitForSeconds( 0.5f );

        PositionObj( new Vector2( -3.0f, 6.0f ) , 45.0f );
        rightButton.interactable = false;

        yield return new WaitUntil(() => continueTutorial == true);
        yield return new WaitForSeconds( 0.5f );

        PositionObj( new Vector2( -2.0f, 3.0f ) , 90.0f );
        rightButton.interactable = false;

        yield return new WaitUntil(() => continueTutorial == true);
        yield return new WaitForSeconds( 0.5f );
        rightButton.interactable = false;

        tutDiamond.SetActive( false );
        TogglePopUp();
        SetPopUpText( "Well done!. Now try with the right fire button " ); 
        leftButton.interactable = false;

        isNext = false;

        yield return new WaitUntil( () => isNext == true );
        TogglePopUp();

        //Right Trigger
         isLeft = false;

        leftButton.interactable = false;
        rightButton.interactable = true;

        tutDiamond.SetActive( true );

        //Tutorial 2 right button test
        
        PositionObj( new Vector2( -3.0f, 4.0f ) , -45.0f );

        yield return new WaitUntil(() => continueTutorial == true);
        yield return new WaitForSeconds( 0.5f );
        leftButton.interactable = false;

        PositionObj( new Vector2( -2.0f, 1.0f ) , 90.0f );
        leftButton.interactable = false;

        yield return new WaitUntil(() => continueTutorial == true);
        yield return new WaitForSeconds( 0.5f );

        PositionObj( new Vector2( 0.0f, 2.0f ) , 45.0f );
        leftButton.interactable = false;

        yield return new WaitUntil(() => continueTutorial == true);
        yield return new WaitForSeconds( 0.5f );

        TogglePopUp();
        SetPopUpText( "Well done!. " ); 

        isNext = false;

        yield return new WaitUntil( () => isNext == true );

        SetPopUpText( "As you’ll have noticed, there are two types of bullet; red and green." ); 
        isNext = false;

        yield return new WaitUntil( () => isNext == true );

        SetPopUpText( "The left container has more greens than red and the right container has more reds than greens." ); 
        isNext = false;

        yield return new WaitUntil( () => isNext == true );

        TogglePopUp();

        isNextTutorial = true;

        Debug.Log( "End Tutorial 3" );
        
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

        SetPopUpText( "Next we introduce breakable blocks. Aim and fire balls at the blue wall." ); 

        TogglePopUp();
       
        isNext = false;
        yield return new WaitUntil( () => isNext == true );
        TogglePopUp();

        leftButton.interactable = true;
        rightButton.interactable = true;

        tutDiamond.SetActive( true );
        gate.SetActive( true );
        PositionObj( new Vector2( 0.0f, 3.0f ) , 135f );

        continueTutorial = false;
        yield return new WaitUntil(() => continueTutorial == true);
        

        //Sometimes the red and green bullets will be hyper-charged.
     
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
        
        SetPopUpText( "There is one more detail to the game. Sometimes the red and green balls will be dud balls." ); 

        TogglePopUp();
       
        isNext = false;
        yield return new WaitUntil( () => isNext == true );
        TogglePopUp();


           
        SetPopUpText( "A dud ball does not glow and does not have a trail. Dud balls can not damage walls" ); 

        TogglePopUp();
       
        isNext = false;
        yield return new WaitUntil( () => isNext == true );
        TogglePopUp();


        //Red and Green balls can both be normal

        SetPopUpText( "Red and Green balls can both be duds" ); 

        TogglePopUp();
       
        isNext = false;
        yield return new WaitUntil( () => isNext == true );
        TogglePopUp();


        //Sometimes, most green balls will be duds...

        SetPopUpText( "Sometimes, most red balls will be duds " ); 

        TogglePopUp();
        redBallContainer.SetActive( true );


       
        isNext = false;
        yield return new WaitUntil( () => isNext == true );
        TogglePopUp();

        redBallContainer.SetActive( false );


         //...and sometimes, most red balls will be normal

        SetPopUpText( "...sometimes, most green balls will be duds " ); 
        redBallContainer.SetActive( false );
        greenBallContainer.SetActive( true );

        TogglePopUp();
       
        isNext = false;
        yield return new WaitUntil( () => isNext == true );
        TogglePopUp();


        //This changes over time so you need to pay attention!

        SetPopUpText( "The number of charged balls and duds changes over time so you need to pay attention!" ); 

        TogglePopUp();
       
        isNext = false;
        yield return new WaitUntil( () => isNext == true );
        TogglePopUp();

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