using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LeftFootBehavior : MonoBehaviour
{
    public bool hanging;

    public GameObject player;

   private GameObject collidedObject;

   public float timeLimit;
   private float timer;

   void Start()
   {
      timer = 0.0f;
      hanging = false;
   }

   void Update()
    {
      timer += Time.deltaTime;
      if (hanging)
      {
         GetComponent<Rigidbody>().velocity = Vector3.zero;
         GetComponent<Light>().color = Color.Lerp(Color.green, Color.red, timer / timeLimit);
      }
      if ((Input.GetKeyUp(KeyCode.A) || timer > timeLimit) && hanging)
      {
         hanging = false;
         GetComponent<Light>().intensity = 0.0f;
         Destroy(GetComponent<HingeJoint>());
         if (player.GetComponent<PlayerBehavior>().isServer)
         {
            player.GetComponent<PlayerBehavior>().RpcDestroyJoint(3);
         }
         else
         {
            player.GetComponent<PlayerBehavior>().CmdDestroyJoint(3);
         }
         if (timer > timeLimit && collidedObject != null)
         {
            collidedObject.GetComponent<MeshDeformBehavior>().DeformMesh(transform.position);
            //TODO - call this function on the same mesh for other players in the game... probably use PlayerBehavior
         }
      }
      if (GetComponent<HingeJoint>() == null && player.GetComponentInChildren<HingeJoint>() != null)
      {
         if (Input.GetAxis("Mouse Y") > 0 && !hanging)
         {
            GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 250.0f, 0.0f));
            GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 0.0f, -250.0f));
         }
      }
   }

   void OnCollisionEnter(Collision collision)
   {
      if (this.GetComponent<LeftFootBehavior>().enabled == false)
      {
         return;
      }
      if (collision.gameObject.tag.Equals("Wall"))
      {
         collidedObject = collision.gameObject;
      }
      if (collision.gameObject.tag.Equals("Wall") || collision.gameObject.tag.Equals("Player"))
      {
         if (Input.GetKey(KeyCode.A) && !hanging && GetComponent<HingeJoint>() == null)
         {
            timer = 0.0f;
            if (player.GetComponent<PlayerBehavior>().isServer)
            {
               player.GetComponent<PlayerBehavior>().RpcCreateJoint(3, transform.position);
            }
            else
            {
               player.GetComponent<PlayerBehavior>().CmdCreateJoint(3, transform.position);
            }
            gameObject.AddComponent<HingeJoint>();
            GetComponent<HingeJoint>().enablePreprocessing = false;
            GetComponent<Light>().intensity = 10.0f;
            GetComponent<Light>().color = Color.green;
            hanging = true;
         }
      }
   }
}
