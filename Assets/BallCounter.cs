using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallCounter : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI ballCount;

    private int totalBalls = 10;

    private void OnEnable()
    {
        Messenger.AddListener( "DecrementBallCount", DecrementBallCount );
    }

    private void OnDisable()
    {
        Messenger.RemoveListener( "DecrementBallCount", DecrementBallCount );
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DecrementBallCount()
    {
        totalBalls --;

        if( totalBalls > 0 )
        {
            ballCount.text = totalBalls.ToString();
        }
        else if( totalBalls <= 0 )
        {
            ballCount.text = "0";
            Debug.Log( "Level Over..." );
        }
    }

    
}
