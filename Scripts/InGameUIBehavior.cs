using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameUIBehavior : MonoBehaviour
{
   public Text timerText;
   private float timer;
   public Slider playerSlider;

   void Start()
   {
      timer = 0.0f;
   }

   void Update()
   {
      timer += Time.deltaTime;
      timerText.text = Mathf.Floor(timer / 60).ToString("00") + ":" + Mathf.Floor(timer % 60).ToString("00");
   }
}
