using UnityEngine;
using System.Collections;

public class MouseTestBehavior : MonoBehaviour
{
   public Camera camera;
   
   void Update ()
   {
      if (Input.GetMouseButtonDown(0))
      {
         RaycastHit hit;
         Ray ray = camera.ScreenPointToRay(Input.mousePosition);

         if (Physics.Raycast(ray, out hit))
         {
            Transform objectHit = hit.transform;
            if (objectHit.tag == "Wall" && objectHit.GetComponent<MeshDeformBehavior>() != null)
            {
               objectHit.GetComponent<MeshDeformBehavior>().DeformMesh(hit.point);
            }
            else if (objectHit.tag == "Wall" && objectHit.GetComponent<PromoMeshDeformBehavior>() != null)
            {
               objectHit.GetComponent<PromoMeshDeformBehavior>().DeformMesh(hit.point);
            }
         }
      }
   }
}
