using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerCanvasBehavior : MonoBehaviour
{
   public GameObject loc;
   public int playerType;
   
   void Update()
   {
      if (playerType == 0)
      {
         transform.position = new Vector3(loc.transform.position.x, loc.transform.position.y + 6.0f, -8.0f);
      }
      else
      {
         transform.position = loc.transform.position + new Vector3(0.0f, 3.5f, 0.0f);
      }
   }
}
