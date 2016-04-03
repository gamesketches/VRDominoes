using UnityEngine;
using System.Collections;

public class dominoTrigger : MonoBehaviour {

	public DominoManager mrManager;
	public Director mrDirector;
	void OnTriggerEnter(Collider other) {
		Debug.Log(other.gameObject.name);
		if(other.gameObject.name != "Plane") {
			mrManager.FadeOut();
			mrDirector.ChangeMood(other.gameObject.name == "Happy Domino" ? EmotionalState.Happy : EmotionalState.Sad);
		}
	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log(collision.gameObject.name);
	}


}
