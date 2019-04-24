using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private List<Blocks> blocks = new List<Blocks>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ResetBlocks()
    {
        Messenger.Broadcast( "Reset" );
    }
}
