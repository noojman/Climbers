using UnityEngine;
using System.Collections;

public class MainMenuPanelBehavior : MonoBehaviour
{
   public Vector2 pos1;
   public Vector2 pos2;

   public float speed = 0.12f;
   public int stage = 1;
   
   void Start ()
   {
      transform.rotation = Quaternion.Euler(90.0f, 180.0f, Random.Range(-2.5f, 2.5f));
      pos1 = new Vector2(-1.0f, -4.0f);
      pos2 = new Vector2(-1.0f, -50.0f);
   }

   public void switchStage()
   {
      if (stage == 1)
      {
         stage = 2;
      }
      else
      {
         stage = 1;
      }
   }

   void Update ()
   {
      if (stage == 1)
      {
         GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(GetComponent<RectTransform>().anchoredPosition, pos1, Mathf.SmoothStep(0.0f, 1.0f, speed));
      }
      else
      {
         GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(GetComponent<RectTransform>().anchoredPosition, pos2, Mathf.SmoothStep(0.0f, 1.0f, speed));
      }
   }
}
