using UnityEngine;
using System.Collections;

public class DestroyPlaneBehavior : MonoBehaviour
{
   void OnCollisionEnter(Collision collision)
   {
      if (collision.gameObject.tag != "Wall" && collision.gameObject.tag != "Player")
      {
         Destroy(collision.gameObject);
      }
   }
}
