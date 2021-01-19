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
        if(SystemInfo.supportsGyroscope)
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
            

        }

        if (Input.acceleration.sqrMagnitude >= sqrShakeDetectionThreshold
           && Time.unscaledTime >= timeSinceLastShake + MinShakeInterval)
        {
            sphere.GetComponent<Rigidbody>().AddForce(new Vector3(0, 4.5f, 0), ForceMode.Impulse);
            timeSinceLastShake = Time.unscaledTime;
            print("zompato");
        }
    


        if (gyroAvailable)
        {

            float myGyroY = Mathf.Clamp(Input.gyro.attitude.x, -tolleranceGyro, tolleranceGyro);
            float myGyroX = Mathf.Clamp(Input.gyro.attitude.y, -tolleranceGyro, tolleranceGyro);

            myGyroX = scale(-tolleranceGyro, tolleranceGyro, -maxXRotation, maxXRotation, myGyroX);
            myGyroY = scale(-tolleranceGyro, tolleranceGyro, maxYRotation, -maxYRotation, myGyroY);


            transform.Rotate(myGyroX - xCurrentRotation, 0, myGyroY - yCurrentRotation); 
            xCurrentRotation = myGyroX;
            yCurrentRotation = myGyroY;


            transform.Rotate(0, 0 - transform.rotation.y, 0);

        }

        else
        {
            //accellerometro
        }

        
        //print(Input.acceleration.x);


    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }


    private float scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {

        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }

}
