using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSpawner : MonoBehaviour
{
    [SerializeField] private GameObject diamondContainer;
    [SerializeField] private GameObject diamondWall;

    [SerializeField] private GameObject diamond;

    [SerializeField] private float defaultRotation;

    [SerializeField] private List<float> rotations = new List<float>();
    private void Awake()
    {
        if( !diamondContainer )
        {
            Debug.Log( "Diamond Container not set" );
            return;
        }
    }
    private void OnEnable()
    {
        //Activate diamond Container
        diamondContainer.SetActive( true );
        diamond.SetActive( true );
        diamondWall.transform.localRotation = Quaternion.Euler( 0, 0, rotations[ Random.Range( 0, rotations.Count - 1 ) ] );
        //Set its rotation;
    }
    // Start is called before the first frame update

    private void OnDisable()
    {
        //Reset diamond container rotation
        diamondWall.transform.localRotation = Quaternion.Euler( 0, 0, defaultRotation );

        //Reset Blocks

        //Disable
        diamondContainer.SetActive( false );
    }
}
