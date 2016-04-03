using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DominoManager : MonoBehaviour {

	public Texture dominoTexture;
	private List<GameObject> dominos;
	private List<Rigidbody> dominosRB;
	// Use this for initialization
	void Start () {
		dominos = new List<GameObject>();
		dominosRB = new List<Rigidbody>();
		foreach (Transform obj in transform) {
			if(obj.gameObject.name != "Plane"){
				obj.gameObject.GetComponent<Renderer>().material.mainTexture = dominoTexture;
				dominos.Add(obj.gameObject);
				dominosRB.Add(obj.gameObject.GetComponent<Rigidbody>());
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		foreach(Rigidbody obj in dominosRB) {
			if(obj.transform.localRotation.eulerAngles.x < 1f) {
				Debug.Log(obj.transform.rotation.eulerAngles);
				return;
			}
			else {
				Debug.Log(transform.rotation.z);
			}
		}
		FadeOut();
	}

	public void FadeOut() {
		Color fadedColor = dominos[0].GetComponent<Renderer>().material.color;
		fadedColor.a = 0.0f;
		foreach(GameObject obj in dominos) {
			Destroy(obj);
		}
	}
}
