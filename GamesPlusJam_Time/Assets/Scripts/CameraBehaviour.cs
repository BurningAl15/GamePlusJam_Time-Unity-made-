using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public GameObject target;
    public Vector3 offset;
    public float speed;
    Vector3 newPos;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if (target.transform.position.y < 0)
        {
            newPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPos + offset, speed * Time.deltaTime);
        }
        else if(target.transform.position.y >=0)
        {
            newPos = target.transform.position;
            transform.position = Vector3.Lerp(transform.position, newPos + offset, speed * Time.deltaTime);
        }
        //transform.position = Vector3.Lerp(transform.position, newPos + offset, speed*Time.deltaTime);
        //transform.LookAt(target.transform);
    }
}
