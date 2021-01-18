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

    void Start()
    {
        if(SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            gyroAvailable = true;
        }

        transform.Rotate(0, 0, -00.1f);

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {
            transform.Rotate(0.01f, 0, 0);

        }


        if (gyroAvailable)
        {
            //print(Input.gyro.attitude);

            float myGyroY = Mathf.Clamp(Input.gyro.attitude.x, -tolleranceGyro, tolleranceGyro);
            float myGyroX = Mathf.Clamp(Input.gyro.attitude.y, -tolleranceGyro, tolleranceGyro);

            myGyroX = scale(-tolleranceGyro, tolleranceGyro, -maxXRotation, maxXRotation, myGyroX);
            myGyroY = scale(-tolleranceGyro, tolleranceGyro, maxYRotation, -maxYRotation, myGyroY);


            transform.Rotate(myGyroX - xCurrentRotation, 0, myGyroY - yCurrentRotation); 
            xCurrentRotation = myGyroX;
            yCurrentRotation = myGyroY;
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
