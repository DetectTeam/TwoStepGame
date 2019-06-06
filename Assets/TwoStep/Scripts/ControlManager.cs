using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    private bool isSwitched;
    [SerializeField] private GameObject ballLauncher;
    [SerializeField] private GameObject manualControls;
    [SerializeField] private GameObject controlCanvas;
     
    // Start is called before the first frame update
    private void EnableBallLauncher()
    {
        //ballLauncher.SetActive( true );
        
        ballLauncher.GetComponent<SpriteRenderer>().enabled = true;
        ballLauncher.GetComponent<BallLauncher>().IsDraggable = true;
        controlCanvas.SetActive( true );
        manualControls.SetActive( false );
    }

    private void EnableManualControls()
    {
       // ballLauncher.SetActive( false );
        ballLauncher.GetComponent<SpriteRenderer>().enabled = false;
        ballLauncher.GetComponent<BallLauncher>().IsDraggable = false;
        controlCanvas.SetActive( false );
        manualControls.SetActive( true );
    }


    public void SwitchControls()
    {
            if( isSwitched )
            {
                EnableManualControls();
                isSwitched = false;
            }
            else
            {
                EnableBallLauncher();
                isSwitched = true;

            }
    }

  
}
