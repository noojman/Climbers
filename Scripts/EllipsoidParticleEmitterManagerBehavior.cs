using UnityEngine;
using System.Collections;

public class EllipsoidParticleEmitterManagerBehavior : MonoBehaviour
{
   public bool playing;

   void Start()
   {
      if (playing)
      {
         foreach (EllipsoidParticleEmitter emitter in GetComponentsInChildren<EllipsoidParticleEmitter>())
         {
            emitter.enabled = true;
         }
      }
      else
      {
         foreach (EllipsoidParticleEmitter emitter in GetComponentsInChildren<EllipsoidParticleEmitter>())
         {
            emitter.enabled = false;
         }
      }
   }
}
