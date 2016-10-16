using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {
   public float targetTime = 0.2f; // Время на один шаг в секундах
   public float Smooth = 10; // Мягкость
   public float AmplitudeHeight = 0.1f; // Амлитуда покачивания вверх-вниз 
   public float AmplitudeRot = 1.5f; // Амплитуда поворота
   private float Progress; // Прогресс
   private int PassedStep = 1; // Шаг
   private float DefCamPos = 0; // Изначальная позиция камеры
   private float DefCamRot = 0; // Изначальный поворот камеры
   private Transform MyTransform; // Наш трансформ


   void Start()
   {
      MyTransform = transform; // Ну, я где-то прочитал что так будет работать быстрей
      DefCamPos = MyTransform.localPosition.y; // Изначальная позиция камеры
      DefCamRot = MyTransform.localEulerAngles.z; // Изначальный поворот камеры
   }


   void Update()
   {
      float Pssd = Passed(); // Наш прогресс

      // Позиция в Vector3, к которой мы стримимся
      Vector3 GoalPos = new Vector3(MyTransform.localPosition.x, Pssd * AmplitudeHeight + DefCamPos, MyTransform.localPosition.z);
      // Position interpolation (smooth)
      MyTransform.localPosition = Vector3.Lerp(MyTransform.localPosition, GoalPos, Time.deltaTime * Smooth);


      // Vector3 rotation in way we need to go
      if (Mathf.Abs(Input.GetAxis("Horizontal")) == 1 && Mathf.Abs(Input.GetAxis("Vertical")) == 0)
      {
         Pssd = 0; // Equals to zero if we would go to side ways
      }
      Vector3 GoalRot = new Vector3(MyTransform.localPosition.x, MyTransform.localPosition.y, Pssd * AmplitudeRot + DefCamRot);
      // Rotation smooth
      MyTransform.localEulerAngles = Vector3.Lerp(MyTransform.localPosition, GoalRot, Time.deltaTime * Smooth);
   }


   private float Passed()
   {

      // Если мы вообще никуда не двигаемся (право, лево, вперед, назад)
      // То возвращаем ноль
      if (Mathf.Abs(Input.GetAxis("Horizontal")) == 0 && Mathf.Abs(Input.GetAxis("Vertical")) == 0)
      {
         PassedStep = 1; // Сбрасываем шаг
         return (Progress = 0); // Прогресс сводим к нулю и возвращаем его
      }

      // Умножаем прогресс на шаг (PassedStep)
      // Если step = 1, то тогда значение не меняется. 
      // А если step = -1, то тогда значение формулы становится отрицательным и мы начинаем вычитать из Progress
      Progress += (Time.deltaTime * (1f / targetTime)) * PassedStep;
      if (Mathf.Abs(Progress) >= 1)
      { // Если Progress больше или равно 1, или меньше или равно -1
         PassedStep *= -1; // Инвертируем шаг
      }

      //Return progress, it's about from 0 to 1
      return Progress;
   }
}
