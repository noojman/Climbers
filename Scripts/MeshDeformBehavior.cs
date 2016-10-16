using UnityEngine;
using System.Collections;

public class MeshDeformBehavior : MonoBehaviour
{
   private Mesh mesh;
   private Vector3[] vertices;
   private Mesh oldMesh;
   private GameObject debris;
   private GameObject upperNeighborChunk;
   private bool foundNeighbor = false;

   void Update()
   {
      if (!foundNeighbor)
      {
         Collider[] colliders;
         if ((colliders = Physics.OverlapSphere(transform.position + new Vector3(0.0f, 96.0f, 0.0f), 20.0f)).Length > 1)
         {
            Debug.Log(transform.position + "!");
            for (int i = 0; i < colliders.Length; i++)
            {
               if (colliders[i].gameObject.tag == "Wall")
               {
                  Debug.Log("Found it!");
                  upperNeighborChunk = colliders[i].gameObject;
                  foundNeighbor = true;
                  i = colliders.Length;
               }
               else
               {
                  upperNeighborChunk = null;
               }
            }
         }
      }
   }

   public void DeformMesh(Vector3 point)
   {
      oldMesh = mesh = this.GetComponent<MeshFilter>().mesh;
      vertices = mesh.vertices;
      this.GetComponent<MeshFilter>().mesh = mesh;
      for (int i = 0; i < vertices.Length; i++)
      {
         if (Mathf.Abs(Vector3.Distance(point, transform.TransformPoint(vertices[i]))) <= 3)
         {
            vertices[i] += new Vector3(0.0f, 0.0f, -8.0f);
         }
         else if (Mathf.Abs(Vector3.Distance(point, transform.TransformPoint(vertices[i]))) <= 5)
         {
            vertices[i] += new Vector3(0.0f, 0.0f, -6.0f);
         }
         else if (Mathf.Abs(Vector3.Distance(point, transform.TransformPoint(vertices[i]))) <= 7)
         {
            vertices[i] += new Vector3(0.0f, 0.0f, -3.0f);
         }
      }
      mesh.vertices = vertices;
      this.GetComponent<MeshFilter>().mesh.vertices = mesh.vertices;
      
      if (foundNeighbor)
      {
         oldMesh = mesh = upperNeighborChunk.GetComponent<MeshFilter>().mesh;
         vertices = mesh.vertices;
         upperNeighborChunk.GetComponent<MeshFilter>().mesh = mesh;
         for (int i = 0; i < vertices.Length; i++)
         {
            if (Mathf.Abs(Vector3.Distance(point, transform.TransformPoint(vertices[i]))) <= 3)
            {
               vertices[i] += new Vector3(0.0f, 0.0f, -8.0f);
            }
            else if (Mathf.Abs(Vector3.Distance(point, transform.TransformPoint(vertices[i]))) <= 5)
            {
               vertices[i] += new Vector3(0.0f, 0.0f, -6.0f);
            }
            else if (Mathf.Abs(Vector3.Distance(point, transform.TransformPoint(vertices[i]))) <= 7)
            {
               vertices[i] += new Vector3(0.0f, 0.0f, -3.0f);
            }
         }
         mesh.vertices = vertices;
         upperNeighborChunk.GetComponent<MeshFilter>().mesh.vertices = mesh.vertices;
      }

      debris = (GameObject)Instantiate(Resources.Load("RockParticles"), point, Quaternion.Euler(Vector3.zero));
      debris.GetComponent<FixedTimeParticleSystem>().Play();
   }
}
