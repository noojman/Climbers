using UnityEngine;
using System.Collections;

public class HideOnPlay : MonoBehaviour
{
   void Start()
   {
      gameObject.SetActive(false);
   }
}
