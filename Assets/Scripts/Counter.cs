using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    
    [SerializeField] private int count;
    [SerializeField] private TextMeshPro countText; 
    
    // Start is called before the first frame update
    private void Start()
    {
        if( !countText )
        {
            Debug.Log( "CountText not set" );
            return;
        }
    }

    public void IncrementCount()
    {
        count++;
        countText.text = count.ToString();
    }


   
}
