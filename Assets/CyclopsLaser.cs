using UnityEngine;
using System.Collections;

public class CyclopsLaser : MonoBehaviour {

	public float rayLength;
	GameObject lastLookedAt;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit hit;
		Ray PsychicRay = new Ray(transform.position, transform.forward);

		if(Physics.Raycast(PsychicRay, out hit, rayLength)){
			if(hit.rigidbody) {
				hit.rigidbody.AddForceAtPosition(PsychicRay.direction * 100f, hit.point);
				Debug.Log(PsychicRay.direction * 100f);
			}
		}
		Debug.DrawRay(transform.position, transform.forward * rayLength);
	}
}
