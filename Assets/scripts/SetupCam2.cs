using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupCam2 : NetworkBehaviour
{


    public Camera cam;
    public Camera cam2;
    public Camera cam3;
    private Vector3 offset;
    public GameObject player;
    // Use this for initialization
    public void Update()
    {
        if (!isLocalPlayer)
            return;
        if(isServer)
            cam.transform.position = player.transform.localPosition + offset;
        else
            cam3.transform.position = player.transform.localPosition + offset;
    }



    // Use this for initialization
    void Start()
    {
        if (isServer)
        {
            cam = Camera.main.GetComponent<Camera>();
            offset = cam.transform.position - player.transform.position;
                        
        }
        else
        {
            if (!isLocalPlayer)
                return;
            GameObject.Find("MainCameraOnline").SetActive(false);

            cam3 = GameObject.Instantiate(cam2, new Vector3(0,3,10), cam2.transform.rotation);

            offset = cam3.transform.position - player.transform.position;

        }

       

    }

}
