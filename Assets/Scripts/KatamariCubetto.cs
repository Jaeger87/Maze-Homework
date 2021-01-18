using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatamariCubetto : MonoBehaviour
{
    private bool firstTimeSphere = true;
    public GameObject sphere;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!firstTimeSphere)
            return;
        if(collision.gameObject.Equals(sphere))
        {
            this.transform.parent = sphere.transform;
            firstTimeSphere = true;
            Destroy(this.GetComponent<Rigidbody>());
        }
    }
}
