using UnityEngine;
using System.Collections;

public class PromoMeshDeformBehavior : MonoBehaviour
{
   private Mesh mesh;
   private Vector3[] vertices;
   private Mesh oldMesh;
   private GameObject debris;
   private GameObject upperNeighborChunk;
   private bool foundNeighbor = false;

   public void DeformMesh(Vector3 point)
   {
      oldMesh = mesh = this.GetComponent<MeshFilter>().mesh;
      vertices = mesh.vertices;
      this.GetComponent<MeshFilter>().mesh = mesh;
      for (int i = 0; i < vertices.Length; i++)
      {
         if (Mathf.Abs(Vector3.Distance(point, transform.TransformPoint(vertices[i]))) <= 1)
         {
            vertices[i] += new Vector3(0.0f, 0.2f, 0.0f);
         }
         else if (Mathf.Abs(Vector3.Distance(point, transform.TransformPoint(vertices[i]))) <= 2)
         {
            vertices[i] += new Vector3(0.0f, 0.175f, 0.0f);
         }
         else if (Mathf.Abs(Vector3.Distance(point, transform.TransformPoint(vertices[i]))) <= 3)
         {
            vertices[i] += new Vector3(0.0f, 0.15f, 0.0f);
         }
      }
      mesh.vertices = vertices;
      this.GetComponent<MeshFilter>().mesh.vertices = mesh.vertices;

      //debris = (GameObject)Instantiate(Resources.Load("RockParticles"), point, Quaternion.Euler(Vector3.zero));
      //debris.GetComponent<FixedTimeParticleSystem>().Play();
   }
}
