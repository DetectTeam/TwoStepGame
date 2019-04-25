using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
     private void OnTriggerEnter2D( Collider2D col )
    {
        if( col.tag == "Ball" || col.tag == "Dud" )
        {
           gameObject.SetActive( false );
           
           SessionManager.Instance.EndSession();
          
        }
    }
}
