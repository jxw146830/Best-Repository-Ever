using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraController : NetworkBehaviour {
    public GameObject player;
    private Vector3 offset;


	// Use this for initialization
	void Start () {
        if(isLocalPlayer)
            offset = transform.position - player.transform.position;
	}
	
	public void LateUpdate () {
        if (isLocalPlayer)
            transform.position = player.transform.position + offset;
    }
}
