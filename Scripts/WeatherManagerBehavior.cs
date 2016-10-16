using UnityEngine;
using System.Collections;

public class WeatherManagerBehavior : MonoBehaviour
{
   public bool raining;
   public bool snowing;
   public bool dry;

   public bool day;
   public bool night;
   public bool dusk;

   public GameObject wallGenerator;

   public Material rockMaterial;
   public Material snowRockMaterial;

   public Light ambientLight1;
   public Light ambientLight2;
   public GameObject lightningLight;

   public GameObject rain;
   public GameObject blowingLeaves;
   public GameObject snow;
   public GameObject snowCloud;

   public GameObject bridge;

   private Color duskColor;

   void Awake()
   {
      duskColor = new Color(1.0f, 0.85f, 0.7f);

      if (raining)
      {
         wallGenerator.GetComponent<EndlessTerrain>().mapMaterial = rockMaterial;
         lightningLight.GetComponent<LightningBehavior>().enabled = true;
         rain.GetComponent<DigitalRuby.RainMaker.RainScript>().RainIntensity = 0.3f;
         blowingLeaves.GetComponent<EllipsoidParticleEmitterManagerBehavior>().playing = false;
         snowCloud.GetComponent<EllipsoidParticleEmitterManagerBehavior>().playing = false;
         snow.GetComponent<EllipsoidParticleEmitter>().enabled = false;
         bridge.GetComponent<BridgeBehavior>().snowing = false;
      }
      else if (snowing)
      {
         wallGenerator.GetComponent<EndlessTerrain>().mapMaterial = snowRockMaterial;
         lightningLight.GetComponent<LightningBehavior>().enabled = false;
         rain.GetComponent<DigitalRuby.RainMaker.RainScript>().RainIntensity = 0.0f;
         blowingLeaves.GetComponent<EllipsoidParticleEmitterManagerBehavior>().playing = false;
         snowCloud.GetComponent<EllipsoidParticleEmitterManagerBehavior>().playing = true;
         snow.GetComponent<EllipsoidParticleEmitter>().enabled = true;
         bridge.GetComponent<BridgeBehavior>().snowing = true;
      }
      else if (dry)
      {
         wallGenerator.GetComponent<EndlessTerrain>().mapMaterial = rockMaterial;
         lightningLight.GetComponent<LightningBehavior>().enabled = false;
         rain.GetComponent<DigitalRuby.RainMaker.RainScript>().RainIntensity = 0.0f;
         blowingLeaves.GetComponent<EllipsoidParticleEmitterManagerBehavior>().playing = true;
         snowCloud.GetComponent<EllipsoidParticleEmitterManagerBehavior>().playing = false;
         snow.GetComponent<EllipsoidParticleEmitter>().enabled = false;
         bridge.GetComponent<BridgeBehavior>().snowing = false;
      }

      if (day)
      {
         ambientLight1.intensity = 1.5f;
         ambientLight2.intensity = 0.3f;
         ambientLight1.color = Color.white;
      }
      else if (night)
      {
         ambientLight1.intensity = 0.25f;
         ambientLight2.intensity = 0.1f;
         ambientLight1.color = Color.white;
      }
      else if (dusk)
      {
         ambientLight1.intensity = 1.0f;
         ambientLight2.intensity = 0.25f;
         ambientLight1.color = duskColor;
      }
   }
}
