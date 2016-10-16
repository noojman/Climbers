using UnityEngine;
using System.Collections;

public class MapBehavior : MonoBehaviour {

   public bool snowing;
   public Material rock;
   public Material snowRock;

   private Renderer[] renderers;

   void Start ()
   {
      renderers = transform.GetComponentsInChildren<Renderer>();
      if (snowing)
      {
         foreach (Renderer r in renderers)
         {
            r.material = snowRock;
         }
      }
      else
      {
         foreach (Renderer r in renderers)
         {
            r.material = rock;
         }
      }
   }
}
