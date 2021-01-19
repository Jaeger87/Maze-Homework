using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    private bool gyroAvailable;
    public GameObject sphere;
    private const float tolleranceGyro = 0.5f;
    private const float maxYRotation = 25f;
    private const float maxXRotation = 22f;
    private float xCurrentRotation = 0;
    private float yCurrentRotation = 0;

    private float ShakeDetectionThreshold = 3.6f;
    private float MinShakeInterval = 1;

    private float sqrShakeDetectionThreshold;
    private float timeSinceLastShake;


    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            gyroAvailable = true;
        }

        sqrShakeDetectionThreshold = Mathf.Pow(ShakeDetectionThreshold, 2);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {
            float xRotation = 0;
            float yRotation = 0;
            for (int i =0; i < Input.touchCount; i++)
            {
                Vector2 positionTouch = Input.GetTouch(i).position;

                xRotation += map(0, Screen.width, maxXRotation, -maxXRotation, positionTouch.x);
                yRotation += map(0, Screen.height, maxYRotation, -maxYRotation, positionTouch.y);
                
            }
            ApplyRotation(xRotation, yRotation);
        }
        else if (gyroAvailable)
        {
            float myGyroY = Mathf.Clamp(Input.gyro.attitude.x, -tolleranceGyro, tolleranceGyro);
            float myGyroX = Mathf.Clamp(Input.gyro.attitude.y, -tolleranceGyro, tolleranceGyro);

            myGyroX = map(-tolleranceGyro, tolleranceGyro, -maxXRotation, maxXRotation, myGyroX);
            myGyroY = map(-tolleranceGyro, tolleranceGyro, maxYRotation, -maxYRotation, myGyroY);

            ApplyRotation(myGyroX, myGyroY);

        }


        else
        {
            //accellerometro
        }


        //print(Input.acceleration.x);


        if (Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold
            && Time.unscaledTime >= timeSinceLastShake + MinShakeInterval)
        {
            sphere.GetComponent<Rigidbody>().AddForce(new Vector3(0, 4.5f, 0), ForceMode.Impulse);
            timeSinceLastShake = Time.unscaledTime;
        }

    }


    private void ApplyRotation(float xRotation, float yRotation)
    {
        transform.Rotate(xRotation - xCurrentRotation, 0, yRotation - yCurrentRotation);
        xCurrentRotation = xRotation;
        yCurrentRotation = yRotation;


        transform.Rotate(0, 0 - transform.rotation.y, 0);
    }



    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }


    private float map(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {

        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }

}
