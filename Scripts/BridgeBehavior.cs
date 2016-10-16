using UnityEngine;
using System.Collections;

public class BridgeBehavior : MonoBehaviour {

   public bool snowing;
   public Material wood;
   public Material snowWood;

   private Renderer[] renderers;

   public GameObject point1;
   public GameObject point2;
   public GameObject point3;
   public GameObject point4;

   void Start ()
   {
      renderers = transform.GetComponentsInChildren<Renderer>();
      if (snowing)
      {
         foreach (Renderer r in renderers)
         {
            r.material = snowWood;
         }
      }
      else
      {
         foreach (Renderer r in renderers)
         {
            r.material = wood;
         }
      }
   }

   public void BreakBridge()
   {
      HingeJoint[] joints = GetComponentsInChildren<HingeJoint>();
      for (int i = 0; i < joints.Length; i++)
      {
         if (i % 2 != 0)
         {
            Destroy(joints[i]);
         }
      }
   }
}
