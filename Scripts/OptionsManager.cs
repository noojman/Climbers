using UnityEngine;
using System.Collections;

public class OptionsManager : MonoBehaviour
{
   public GameObject gameplayPanel;
   public GameObject audioPanel;
   public GameObject graphicsPanel;
   
   void Start ()
   {
      changeToGameplay();
   }
	
   public void changeToGameplay()
   {
      gameplayPanel.SetActive(true);
      audioPanel.SetActive(false);
      graphicsPanel.SetActive(false);
   }

   public void changeToAudio()
   {
      gameplayPanel.SetActive(false);
      audioPanel.SetActive(true);
      graphicsPanel.SetActive(false);
   }

   public void changeToGraphics()
   {
      gameplayPanel.SetActive(false);
      audioPanel.SetActive(false);
      graphicsPanel.SetActive(true);
   }
}
