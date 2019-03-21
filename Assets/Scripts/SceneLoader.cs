using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

	
	[SerializeField] private string sceneToLoad;
	// Use this for initialization

	public void LoadScene(  )
	{
		Debug.Log( "Loading Scene" );
		SceneManager.LoadSceneAsync( sceneToLoad );
	}
}
