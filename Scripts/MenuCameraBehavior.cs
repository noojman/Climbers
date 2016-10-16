using UnityEngine;
using System.Collections;

public class MenuCameraBehavior : MonoBehaviour
{
   public GameObject sceneManager;

   Vector3 startPos;
   Quaternion startAngle;
   Vector3 menuPos;
   Quaternion menuAngle;
   Vector3 optionsPos;
   Quaternion optionsAngle;

   public float speedDivider;
   public float speedMultiplier;

   float t;
   bool startingUp;
   
   void Start ()
   {
      t = 0.0f;
      startingUp = true;
      startPos = new Vector3(0.0f, 80.0f, 800.0f);
      startAngle = Quaternion.Euler(new Vector3(0.0f, -180.0f, 0.0f));
      menuPos = new Vector3(-5.0f, 125.0f, -124.0f);
      menuAngle = Quaternion.Euler(new Vector3(70.0f, -180.0f, 0.0f));
      optionsPos = new Vector3(0.0f, 138.0f, -131.0f);
      optionsAngle = Quaternion.Euler(new Vector3(28.0f, -180.0f, 0.0f));
      transform.position = startPos;
      transform.rotation = startAngle;
   }
	
   void Update ()
   {
      t += Time.deltaTime / speedDivider;
	  if (startingUp)
      {
         transform.position = Vector3.Lerp(transform.position, menuPos, Mathf.SmoothStep(0.0f, 1.0f, t));
         transform.rotation = Quaternion.Lerp(transform.rotation, menuAngle, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, t) * 4.5f));
      }

      if (sceneManager.GetComponent<MenuSceneManager>().stage != 2 && !startingUp)
      {
         transform.position = Vector3.Lerp(transform.position, menuPos, speedMultiplier * Mathf.SmoothStep(0.0f, 1.0f, t));
         transform.rotation = Quaternion.Lerp(transform.rotation, menuAngle, speedMultiplier * Mathf.SmoothStep(0.0f, 1.0f, t));
      }

      if (sceneManager.GetComponent<MenuSceneManager>().stage == 2)
      {
         startingUp = false;
         transform.position = Vector3.Lerp(transform.position, optionsPos, speedMultiplier * Mathf.SmoothStep(0.0f, 1.0f, t));
         transform.rotation = Quaternion.Lerp(transform.rotation, optionsAngle, speedMultiplier * Mathf.SmoothStep(0.0f, 1.0f, t));
      }
   }

   public void resetCounter()
   {
      t = 0.0f;
   }
}
