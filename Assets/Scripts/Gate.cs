using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private GameObject gateBlocks;
    [SerializeField] private float delay;

    private void OnEnable()
    {


    }

    private void OnDisable()
    {
        
    }

    private void Start()
    {
        if( !gateBlocks )
        {
            Debug.Log( "Gate Blocks not set" );
        }
    }

    private void OpenGate()
    {
        StartCoroutine( IEOpenGate() );
    }

    private IEnumerator IEOpenGate()
    {
        gateBlocks.SetActive( false );
        yield return new WaitForSeconds( delay );
        gateBlocks.SetActive( true );
    }
}
