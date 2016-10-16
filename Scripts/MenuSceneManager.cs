using UnityEngine;
using System.Collections;

public class MenuSceneManager : MonoBehaviour
{
   public int stage;

   public GameObject optionsCanvas;
   
   void Start()
   {
      optionsCanvas.SetActive(false);
      stage = 0;
      goToMenu();
   }

   public void goToOptions()
   {
      optionsCanvas.SetActive(true);
      stage = 2;
   }

   public void goToMenu()
   {
      stage = 1;
   }

   public void goToLobby()
   {
      stage = 3;
   }
}
