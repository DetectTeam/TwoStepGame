using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TutorialManager : MonoBehaviour
{
    
    public static TutorialManager Instance;

    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject muzzle;

    [SerializeField] private GameObject popUp;

    [SerializeField] private GameObject targetCircle;
    [SerializeField] private TextMeshProUGUI popUpMessage;

    [SerializeField] private GameController gameController;

    [SerializeField] private bool isNext;

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

    public void Start()
    {
        StartCoroutine( TutorialOne() );
        targetCircle.SetActive( false );
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
        SetPopUpText( "Good job! . You have collected your first diamond" ); 
   
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

}
