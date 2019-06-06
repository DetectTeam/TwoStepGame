using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace twostep
{
    public class RandomLevelGenerator : MonoBehaviour
    {
        [SerializeField] private List<GameObject> spawners = new List<GameObject>();

        private void OnEnable()
        {
            Messenger.AddListener( "Continue" , SpawnBlock );
            Messenger.MarkAsPermanent( "Continue" );
        }

        private void OnDisable()
        {
            Messenger.RemoveListener( "Continue" , SpawnBlock );
        }
        // Start is called before the first frame update
        private void Start()
        {
           SpawnBlock(); 
        }

        private void DisableSpawners()
        {
            foreach( GameObject spawner in spawners )
                spawner.SetActive( false );
        }


        private int lastRand;
        private void PickRandomSpawner()
        {
            int rand = Random.Range( 0 , spawners.Count -1 );

            while( rand == lastRand )
            {
                rand = Random.Range( 0 , spawners.Count -1 );
                Debug.Log( "Match" );
            }

            spawners[ rand ].SetActive( true );

            lastRand = rand;
        }

        private void SpawnBlock()
        {

            //Disable all spawners 
            DisableSpawners();
            
            //Pick a random spawner and activate it.
            PickRandomSpawner();
        }  
    }
}
