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
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            gyroAvailable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {
            float xRotation = 0;
            float yRotation = 0;
            for (int i = 0; i < Input.touchCount; i++)
            {
                Vector2 positionTouch = Input.GetTouch(i).position;

                if (positionTouch.x < 20 || positionTouch.x > Screen.width - 20 ||
                    positionTouch.y < 20 || positionTouch.y > Screen.height - 20)
                    continue; // senza questo rischi di toccare il touch involontariamente (i telefoni moderni hanno veramente poca scocca)

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
            float myAccY = Mathf.Clamp(Input.acceleration.y, -tolleranceGyro, tolleranceGyro);
            float myAccX = Mathf.Clamp(Input.acceleration.x, -tolleranceGyro, tolleranceGyro);

            myAccX = map(-tolleranceGyro, tolleranceGyro, maxXRotation, -maxXRotation, myAccX);
            myAccY = map(-tolleranceGyro, tolleranceGyro, maxYRotation, -maxYRotation, myAccY);

            ApplyRotation(myAccX, myAccY);
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
