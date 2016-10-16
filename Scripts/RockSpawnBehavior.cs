using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RockSpawnBehavior : MonoBehaviour
{
    public Vector3 left;
    public Vector3 right;
    public float speed;

    public GameObject rock;

    private bool shiftingRight;

    private float time;

    void Start()
    {
        transform.position = left;
        shiftingRight = true;
        time = 0.0f;
        StartCoroutine(SpawnRock());
    }

    void FixedUpdate()
    {
        time = Random.Range(5, 30) / 10.0f;
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x - right.x) < 10.0f)
        {
            shiftingRight = false;
        }
        if (Mathf.Abs(transform.position.x - left.x) < 10.0f)
        {
            shiftingRight = true;
        }
        
        if (shiftingRight)
        {
            transform.position = Vector3.Lerp(transform.position, right, speed);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, left, speed);
        }
    }

    IEnumerator SpawnRock()
    {
        while (true)
        {
            Instantiate(rock, transform.position, transform.rotation);
            yield return new WaitForSeconds(time);
        }
    }
}
