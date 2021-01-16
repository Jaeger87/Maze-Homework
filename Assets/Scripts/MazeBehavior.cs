using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    private bool gyroAvailable;

    void Start()
    {
        gyroAvailable = Input.gyro.enabled;
    }

    // Update is called once per frame
    void Update()
    {
        if(gyroAvailable) // non ho un gyroscopio quindi non posso testare :(
          transform.rotation = GyroToUnity(Input.gyro.attitude);

        print(Input.acceleration.x); // delta

    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
