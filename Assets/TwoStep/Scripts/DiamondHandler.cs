using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondHandler : MonoBehaviour
{

    [SerializeField] private GameObject diamond;
    [SerializeField] private bool isTutorial = false;
    // Start is called before the first frame update

    private void OnEnable()
    {
        Messenger.AddListener( "Reset" , Reset );
        Messenger.MarkAsPermanent( "Reset" );
      
    }

    private void OnDisable()
    {
         Messenger.RemoveListener( "Reset" , Reset );
         Reset();
    }

    private void Awake()
    {
        if( !diamond )
        {
            Debug.LogError( "Diamond not assigned..." );
            return;
        } 

        // if( !isTutorial )
        // {
        //     diamond.GetComponent<TutorialDiamond>().enabled = false;
        // }
    }

    private void Reset()
    {
        if( !diamond.activeSelf )
            diamond.SetActive( true );
    }


}
