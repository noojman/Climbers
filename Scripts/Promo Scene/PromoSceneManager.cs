using UnityEngine;
using System.Collections;

public class PromoSceneManager : MonoBehaviour
{
   public GameObject leftHand;
   public GameObject rockParticles;

   private Vector3 point;

   private int scene;

   void Start()
   {
      scene = 0;
      StartCoroutine(HoldTitleScene());
   }

   void Update()
   {
      if (scene == 1)
      {
         RaycastHit hit;
         if (Physics.Raycast(leftHand.transform.position, Vector3.back, out hit))
         {
            Transform objectHit = hit.transform;
            if (objectHit.tag == "Wall" && objectHit.GetComponent<PromoMeshDeformBehavior>() != null)
            {
               objectHit.GetComponent<PromoMeshDeformBehavior>().DeformMesh(hit.point);
            }
         }
         rockParticles.GetComponent<FixedTimeParticleSystem>().Play();
         Destroy(leftHand.GetComponent<HingeJoint>());
         scene = 2;
      }
      
   }

   void IncrementScene()
   {
      scene++;
   }

   IEnumerator HoldTitleScene()
   {
      yield return new WaitForSeconds(8.0f);
      IncrementScene();
   }
}
