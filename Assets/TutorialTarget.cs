using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTarget : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log( "CLICK>>>>" );
        TutorialManager.Instance.MoveNext();
    }
}
