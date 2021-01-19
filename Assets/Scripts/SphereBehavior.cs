using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBehavior : MonoBehaviour
{

    private float ShakeDetectionThreshold = 3;
    private float MinShakeInterval = 1;

    private float sqrShakeDetectionThreshold;
    private float timeSinceLastShake;
    // Start is called before the first frame update
    void Start()
    {
        sqrShakeDetectionThreshold = Mathf.Pow(ShakeDetectionThreshold, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold
            && Time.unscaledTime >= timeSinceLastShake + MinShakeInterval)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 4.5f, 0), ForceMode.Impulse);
            GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), Random.Range(-2, 2)), ForceMode.Impulse);
            timeSinceLastShake = Time.unscaledTime;
        }
    }
}
