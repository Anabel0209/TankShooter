using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //coroutine used to shake the camera
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            //generate a random offset
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            //add the offset to the camera position
            transform.localPosition = new Vector3(x, y, originalPos.z);

            //update elapsed time
            elapsed += Time.deltaTime;

            yield return null;
        }

        //put back the previous position
        transform.localPosition = originalPos;
    }
}
