using UnityEngine;
using System.Collections;

public class PromoPlayerBehavior : MonoBehaviour
{
   public Camera myCamera;

   public float timeMultiplier;

   private float defaultTimeScale;
   private float defaultFixedDeltaTime;

   private GameObject splash;
   private bool hitWater;

   void Start()
   {
      defaultTimeScale = Time.timeScale;
      defaultFixedDeltaTime = Time.fixedDeltaTime;
      hitWater = false;
   }

   void FixedUpdate()
   {
      if (transform.position.y > -40.0f)
      {
         myCamera.transform.position = new Vector3(myCamera.transform.position.x, transform.position.y + 5.0f, myCamera.transform.position.z);
      }
   }

   void Update()
   {
      if (transform.position.y < -52.5f)
      {
         if (!hitWater)
         {
            splash = (GameObject)Instantiate(Resources.Load("WaterSplashParticles"), transform.position, Quaternion.Euler(new Vector3(-90.0f, 2.5f, 0.0f)));
            splash.GetComponent<FixedTimeParticleSystem>().Play();
            hitWater = true;
         }
         GetComponent<Rigidbody>().velocity = Vector3.zero;
         GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
         GetComponent<Rigidbody>().AddForce(Vector3.left * 700.0f);
         GetComponent<Rigidbody>().AddForce(Vector3.up * 700.0f);
      }

      if ((transform.position.y < 72.5f && transform.position.y > 60.0f) || (transform.position.y < 25.0f && transform.position.y > 5.0f))
      {
         Time.timeScale = Mathf.Lerp(Time.timeScale, defaultTimeScale * 0.01f, timeMultiplier * Time.deltaTime);
         Time.fixedDeltaTime = Mathf.Lerp(Time.fixedDeltaTime, defaultFixedDeltaTime * 0.01f, timeMultiplier * Time.deltaTime);
      }
      else
      {
         Time.timeScale = Mathf.Lerp(Time.timeScale, defaultTimeScale, timeMultiplier * Time.deltaTime);
         Time.fixedDeltaTime = Mathf.Lerp(Time.fixedDeltaTime, defaultFixedDeltaTime, timeMultiplier * Time.deltaTime);
      }

      GetComponent<Rigidbody>().AddForce(Vector3.left * 13.0f);
      GetComponent<Rigidbody>().AddForce(Vector3.back * 4.0f);
   }
}
