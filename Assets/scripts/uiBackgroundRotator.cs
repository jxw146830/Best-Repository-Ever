using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiBackgroundRotator : MonoBehaviour
{

    bool spinningLeft;

    private void Start()
    {
        spinningLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.eulerAngles.y <= 340.4F && transform.rotation.eulerAngles.y > 330)
        {
            transform.rotation = Quaternion.Euler(15.7F, -19.5F, 0);
            spinningLeft = false;
        }

        else if (transform.rotation.eulerAngles.y >= 19.6F && transform.rotation.eulerAngles.y < 30)
        {
            transform.rotation = Quaternion.Euler(15.7F, 19.5F, 0);
            spinningLeft = true;
        }

        if (spinningLeft)
            transform.Rotate(Vector3.down, 10* Time.deltaTime, Space.World);
        else
            transform.Rotate(Vector3.up, 10* Time.deltaTime, Space.World);


    }
}
