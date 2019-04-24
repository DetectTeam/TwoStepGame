using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondHandler : MonoBehaviour
{

    [SerializeField] private GameObject diamond;
    // Start is called before the first frame update

    private void OnEnable()
    {
        Messenger.AddListener( "Reset" , Reset );
    }

    private void OnDisable()
    {
         Messenger.RemoveListener( "Reset" , Reset );
    }

    private void Awake()
    {
        if( !diamond )
        {
            Debug.LogError( "Diamond not assigned..." );
            return;
        } 
    }

    private void Reset()
    {
        if( !diamond.activeSelf )
            diamond.SetActive( true );
    }


}
