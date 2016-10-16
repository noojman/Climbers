using UnityEngine;
using System.Collections;

public class PromoCameraBehavior : MonoBehaviour
{
   private int section;
   private float t;
   public float speedDivider;
   public float speedMultiplier;

   void Start()
   {
      section = 0;
      t = 0.0f;
      StartCoroutine(HoldTitleScene());
   }

   void FixedUpdate()
   {
      if (section == 1)
      {
         t += Time.deltaTime / speedDivider;
         transform.position = Vector3.Lerp(transform.position, new Vector3(0.0f, 153.5f, 15.0f), speedMultiplier * Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, t))));
      }
   }

   IEnumerator HoldTitleScene()
   {
      yield return new WaitForSeconds(8.0f);
      section = 1;
   }
}
