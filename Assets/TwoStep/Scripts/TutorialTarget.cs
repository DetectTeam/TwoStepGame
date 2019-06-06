using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTarget : MonoBehaviour
{
    private void OnMouseDown()
    {
        TutorialManager.Instance.MoveNext();
    }
}
