using UnityEngine;
using System.Collections;

public class LightningBehavior : MonoBehaviour
{

    private float minTime = 0.5f;
    private float thresh = 0.7f;
    private float lastTime = 0.0f;

    void FixedUpdate()
    {
        if ((Time.time - lastTime) > minTime)
        {
            if (Random.value > thresh)
            {
                GetComponent<Light>().enabled = true;
            }
            else
            {
                GetComponent<Light>().enabled = false;
                lastTime = Time.time;
            }
        }
    }
}
