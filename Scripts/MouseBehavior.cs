using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MouseBehavior : MonoBehaviour
{
    public GameObject player;

   public float thrust;
    
	void Update () {
      if (player.GetComponentInChildren<HingeJoint>() != null)
      {
         if (Input.GetAxis("Mouse X") < 0)
         {
            GetComponent<Rigidbody>().AddForce(new Vector3(thrust, 0.0f, 0.0f));
         }
         if (Input.GetAxis("Mouse X") > 0)
         {
            GetComponent<Rigidbody>().AddForce(new Vector3(-thrust, 0.0f, 0.0f));
         }
         if (Input.GetAxis("Mouse Y") > 0)
         {
            GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, thrust / 2.0f, 0.0f));
         }
         if (Input.GetAxis("Mouse Y") < 0)
         {
            GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, thrust / 4.0f));
         }
      }
      else
      {
         if (Input.GetAxis("Mouse X") < 0)
         {
            GetComponent<Rigidbody>().AddForce(new Vector3(thrust / 4.0f, 0.0f, 0.0f));
         }
         if (Input.GetAxis("Mouse X") > 0)
         {
            GetComponent<Rigidbody>().AddForce(new Vector3(-thrust / 4.0f, 0.0f, 0.0f));
         }
         if (Input.GetAxis("Mouse Y") > 0)
         {
            GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, -thrust / 4.0f));
         }
         if (Input.GetAxis("Mouse Y") < 0)
         {
            GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, thrust / 4.0f));
         }
      }
   }
}
