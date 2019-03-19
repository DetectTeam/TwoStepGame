using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	[SerializeField] private bool isLevelOver;
	[SerializeField] private int count;

	[SerializeField] private Transform target;

	[SerializeField] private float delay;
	// Use this for initialization
	private IEnumerator Start () {
		
		while( !isLevelOver  )
		{
			yield return new WaitForSeconds( delay );
			GameObject go = ObjectPooler.SharedInstance.GetPooledObject( 0 );

			//spawn at a random location on screen within top third of screen.
			go.transform.position =  new Vector2( Random.Range( -2f, 2f ),Random.Range( 4.0f, 2.0f ) );
			go.transform.rotation = Quaternion.identity;
			Zombie zombie = go.GetComponent<Zombie>();
			//zombie.Target = target;
			go.SetActive( true );
			
		}
	}	
}
