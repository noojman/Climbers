using UnityEngine;
using System.Collections;

public class MapObjectsManagerBehavior : MonoBehaviour
{
   public GameObject weatherManager;
   public GameObject bridgeManager;

   public GameObject rocks;
   public GameObject poles;

   public Material rockMaterial;
   public Material snowRockMaterial;

   public Material woodMaterial;
   public Material snowWoodMaterial;

   void Start()
   {
      if (weatherManager.GetComponent<WeatherManagerBehavior>().snowing)
      {
         bridgeManager.GetComponent<BridgeBehavior>().snowing = true;
         
         foreach (Renderer childRenderer in rocks.GetComponentsInChildren<Renderer>())
         {
            childRenderer.material = snowRockMaterial;
         }
         foreach (Renderer childRenderer in poles.GetComponentsInChildren<Renderer>())
         {
            childRenderer.material = snowWoodMaterial;
         }
      }
      else
      {
         bridgeManager.GetComponent<BridgeBehavior>().snowing = false;

         foreach (Renderer childRenderer in rocks.GetComponentsInChildren<Renderer>())
         {
            childRenderer.material = rockMaterial;
         }
         foreach (Renderer childRenderer in poles.GetComponentsInChildren<Renderer>())
         {
            childRenderer.material = woodMaterial;
         }
      }
   }
}
