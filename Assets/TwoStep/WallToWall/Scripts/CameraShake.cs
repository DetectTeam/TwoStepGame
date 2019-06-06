using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private IEnumerator cameraShake;
    Vector3 originalPos;
    private void Awake()
    {
        originalPos = transform.localPosition;
    }

    private void OnEnable()
    {
        Messenger.AddListener( "ShakeCamera", ShakeCamera );
        Messenger.MarkAsPermanent( "ShakeCamera" );
    }

    private void OnDisable()
    {
        Messenger.RemoveListener( "ShakeCamera", ShakeCamera );
    }

    public void ShakeCamera()
    {
        cameraShake = Shake( 0.25f, 0.25f );
        StartCoroutine( cameraShake );
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }

}
