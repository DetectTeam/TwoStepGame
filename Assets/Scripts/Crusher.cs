using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Crusher : MonoBehaviour
{
    [SerializeField] private float targetPosition;
    [SerializeField] private float timeToTarget;
    [SerializeField] private float delay;
    // Start is called before the first frame update

    [SerializeField] private float originalPositionX; 

    private void OnEnable()
    {
        Messenger.AddListener( "TriggerTrap" , ActivateTrap );
    }
    private void Start()
    {
        originalPositionX = gameObject.transform.position.x;

    }

    private void ActivateTrap()
    {
        StartCoroutine( IEActivateTrap() );
    }
    private IEnumerator IEActivateTrap()
    {  
        iTween.MoveTo (gameObject, iTween.Hash ("x", targetPosition,"easetype",iTween.EaseType.easeInExpo,"time",timeToTarget,"delay",delay,"oncomplete","CameraShake"));

        yield return new WaitForSeconds( 1.0f );

		iTween.MoveTo (gameObject, iTween.Hash ("x", originalPositionX,"easetype",iTween.EaseType.easeInExpo,"time",0.25f,"delay",0.1f));
    }

    // Update is called once per frame
    private void CameraShake()
    {
        iTween.ShakePosition(Camera.main.gameObject,new Vector3(.4f,.2f,0),.4f);
    }
}
