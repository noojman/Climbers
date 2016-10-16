using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CameraBehavior : MonoBehaviour
{
   public float shakeDuration;
   public float shakeMagnitude;
   public float offset;
   private Vector3 startPos;

   void Start ()
   {
      startPos = transform.position;
   }

   void Update ()
   {
      Camera.main.transform.position = new Vector3(startPos.x, transform.position.y + offset, startPos.z + 35.0f);
   }

   IEnumerator Shake()
   {
      float elapsed = 0.0f;

      Vector3 originalCamPos = Camera.main.transform.position;

      while (elapsed < shakeDuration)
      {
         elapsed += Time.deltaTime;

         float percentComplete = elapsed / shakeDuration;
         float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

         // map value to [-1, 1]
         float x = Random.value * 2.0f - 1.0f;
         float y = Random.value * 2.0f - 1.0f;
         x *= shakeMagnitude * damper;
         y *= shakeMagnitude * damper;

         Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);

         yield return null;
      }

      Camera.main.transform.position = originalCamPos;
   }
}
