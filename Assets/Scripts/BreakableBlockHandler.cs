using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlockHandler : MonoBehaviour
{
    
    [SerializeField] private List<GameObject> blocks = new List<GameObject>();

    private void OnEnable()
    {
        Messenger.AddListener( "Reset" , ResetBlocks );
        Messenger.MarkAsPermanent( "Reset" );
        
    }

    private void OnDisable()
    {
         Messenger.RemoveListener( "Reset" , ResetBlocks );
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    private void EnableBlocks()
    {
        foreach( GameObject block in blocks )
            block.SetActive( true );
    }

    private void DisableBlocks()
    {
        foreach( GameObject block in blocks )
            block.SetActive( false );
    }

    private void ResetBlocks()
    {
        foreach( GameObject block in blocks )
        {
            if( !block.activeSelf )
                block.SetActive( true );
            
            block.GetComponent<Blocks>().ResetBlock();
        }
    }
}
