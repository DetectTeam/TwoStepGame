using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

	[SerializeField] private Button leftButton;
	[SerializeField] private Button rightButton;

	private void OnEnable()
	{
		Messenger.AddListener( "EnableReloadButtons" , EnableReloadButton );
		Messenger.AddListener( "DisableReloadButtons" , DisableReloadButton );

		Messenger.MarkAsPermanent( "EnableReloadButtons" );
		Messenger.MarkAsPermanent( "DisableReloadButtons" );
	}

	private void OnDisable()
	{
		Messenger.RemoveListener( "EnableReloadButtons" , EnableReloadButton );
		Messenger.RemoveListener( "DisableReloadButtons" , DisableReloadButton );
	}

	private void EnableReloadButton()
	{
		leftButton.interactable = true;
		rightButton.interactable = true;
	}

	private void DisableReloadButton()
	{
		leftButton.interactable = false;
		rightButton.interactable = false;
	}

	public void LoadScene()
	{
		SceneManager.LoadScene( "Game" );
	}

}
