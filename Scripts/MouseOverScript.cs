using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class MouseOverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   public GameObject obj;
   public GameObject label;

   bool clicked;

   void Start()
   {
      obj.SetActive(false);
      clicked = false;
   }

   public void OnPointerEnter(PointerEventData data)
   {
      obj.SetActive(true);
   }

   public void OnPointerExit(PointerEventData data)
   {
      if (!clicked)
      {
         obj.SetActive(false);
      }
   }

   void Update()
   {
      if (Input.GetMouseButtonDown(0))
      {
         PointerEventData cursor = new PointerEventData(EventSystem.current);
         cursor.position = Input.mousePosition;
         List<RaycastResult> objectsHit = new List<RaycastResult>();
         EventSystem.current.RaycastAll(cursor, objectsHit);
         for (int i = 0; i < objectsHit.Count; i++)
         {
            if (objectsHit[i].gameObject == label)
            {
               clicked = true;
               i = objectsHit.Count;
            }
            else
            {
               clicked = false;
               obj.SetActive(false);
            }
         }
      }
   }
}
