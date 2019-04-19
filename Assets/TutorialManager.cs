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
    [SerializeField] private GameObject muzzle;

    [SerializeField] private GameObject popUp;

    [SerializeField] private GameObject targetCircle;
    [SerializeField] private TextMeshProUGUI popUpMessage;

    [SerializeField] private GameController gameController;

    [SerializeField] private bool isNext;
    [SerializeField] private bool isNextTutorial = false;

    //Tutorial 2
    [SerializeField] private GameObject block;
    [SerializeField] private GameObject tutDiamond;
    [SerializeField] private GameObject diamond;
    [SerializeField] private GameObject gate;

    private void OnEnable()
    {
        Messenger.AddListener( "Continue" , ContinueTutorial );
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
        targetCircle.SetActive( false );

        leftButton.interactable = false;
        rightButton.interactable = false;
        
        gate.SetActive( false );
        
        StartCoroutine( TutorialTwo() );

        yield return new WaitUntil( () => isNextTutorial == true );

        Debug.Log( "Starting Tutorial Two" );

        //StartCoroutine( TutorialTwo() );
 
    }

    public void Fire()
    {
        Debug.Log( "Firing....." );
        Messenger.Broadcast( "DisableReloadButtons" );
        MoveNext();

       StartCoroutine( "IEFire" );
    }

    private IEnumerator IEFire()
    {
        yield return new WaitForSeconds( 0.5f );
        
        var b = Instantiate( ball, muzzle.transform.position, muzzle.transform.rotation ) as GameObject;

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
        Debug.Log( "Starting Tutrial...." );

        //Display Dialog
        Debug.Log( "Welcome to this tutorial..." );
        //popUpMessage.text = "Welcome to this tutorial";
        TogglePopUp( );
        SetPopUpText( "Welcome to this tutorial"  );

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
        SetPopUpText( "Well done!. You have collected your first diamond and completed this tutorial." ); 

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

        Debug.Log("Starting Tutorial 2");

        yield return new WaitUntil(() => continueTutorial == true);

        PositionObj( new Vector2( 2.0f, 3.0f ) , -45.0f );

        yield return new WaitUntil(() => continueTutorial == true);

        PositionObj( new Vector2( -3.0f, 3.0f ) , 90.0f );

        yield return new WaitUntil(() => continueTutorial == true);

        PositionObj( new Vector2( 3.0f, 6.0f ) , -45.0f );

        yield return new WaitUntil(() => continueTutorial == true);

        PositionObj( new Vector2( -3.0f, 4.0f ) , 90.0f );

        yield return new WaitUntil(() => continueTutorial == true);

        tutDiamond.SetActive( false );
        TogglePopUp();
        SetPopUpText( "Well done!. You have mastered aiming" ); 

        isNext = false;


        yield return new WaitUntil( () => isNext == true );
        TogglePopUp();


    }

    private void PositionObj( Vector2 position, float rotation )
    {
        continueTutorial = false;
        Debug.Log("Repositioning Block");

        diamond.SetActive(true);
        SetPosition(tutDiamond.transform, position );
        block.SetActive(false);

        block.SetActive(true);
        SetRotation(block.transform,  rotation );
        Messenger.Broadcast("Reset");
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

}
