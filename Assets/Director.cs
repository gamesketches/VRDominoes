using UnityEngine;
using System.Collections;

public enum EmotionalState {Neutral, Happy, Sad};

public class Director : MonoBehaviour {

	public GameObject John;
	private Animator johnAnimator;
	public GameObject Roger;

	private int conversationPoint;
	private bool isTalking;
	private bool johnTalking;
	private AudioClip[] voiceClips;
	public AudioSource audio;
	// Use this for initialization
	void Start () {
		voiceClips = Resources.LoadAll<AudioClip>("Dialogue");
	}
	
	// Update is called once per frame
	void Update () {
		if(!isTalking) {
			StartCoroutine(PlayAudio());
		}
		if(Input.GetKeyDown(KeyCode.R)){
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	IEnumerator PlayAudio() {
		isTalking = true;
		if(conversationPoint < voiceClips.Length) {
			audio.clip = voiceClips[conversationPoint];
			audio.Play();
			while(audio.isPlaying) {
				yield return null;
			}
			conversationPoint++;
			isTalking = false;
		}
	}
}
