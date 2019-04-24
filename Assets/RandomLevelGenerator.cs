using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace twostep
{
    public class RandomLevelGenerator : MonoBehaviour
    {
         [SerializeField] private List<GameObject> spawners = new List<GameObject>();
        // Start is called before the first frame update
        private IEnumerator Start()
        {
           while( true )
           {
               
               DisableSpawners();
               PickRandomSpawner();


               yield return new WaitForSeconds( 3.0f );
               

                //Disable all spawners

               //Pick a random spawner and activate it.
           }
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

       
    }
}
