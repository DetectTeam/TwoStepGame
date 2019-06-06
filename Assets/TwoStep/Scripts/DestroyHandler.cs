using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHandler : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad( this.gameObject );
    }
}
