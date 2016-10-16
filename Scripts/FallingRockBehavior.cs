using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class FallingRockBehavior : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
      /*
      if (!collision.gameObject.tag.Equals("Player") 
         && !collision.gameObject.tag.Equals("Wall")
         && !collision.gameObject.tag.Equals("FallingRock"))
      {
         Destroy(this.gameObject);
      }
      */
      /*
      if (collision.gameObject.tag.Equals("Water") || collision.gameObject.tag.Equals("Bridge"))
   {
         Destroy(this.gameObject);
      }
      */
      if (collision.gameObject.tag == "Bridge")
      {
         collision.gameObject.GetComponentInParent<BridgeBehavior>().BreakBridge();
      }
    }
}
