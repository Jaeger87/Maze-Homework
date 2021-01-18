using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    private bool gyroAvailable;
    public GameObject sphere;

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
        
        if(Input.touchCount > 0)
        {
            Vector3 oldPosition = transform.position;
            //transform.RotateAround(transform.position, Vector3.right, 20 * Time.deltaTime);
            transform.Rotate(0, 0, -00.1f);
            Vector3 newPosition = transform.position;
            Vector3 difference = new Vector3(newPosition.x - oldPosition.x, newPosition.y - oldPosition.y,
                newPosition.z - oldPosition.z);
            sphere.transform.Translate(difference);

            
        }
        //print(SystemInfo.supportsGyroscope && Input.gyro.enabled);
        if (gyroAvailable) // non ho un gyroscopio quindi non posso testare :(
        {
            print(Input.gyro.attitude);
           // transform.rotation = GyroToUnity(Input.gyro.attitude);
        }
          

        //print(Input.acceleration.x);


    }

    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.y, q.x, 0, 0);
    }
}
