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
	public AudioSource audioPlayer;
	// Use this for initialization
	void Start () {
		voiceClips = Resources.LoadAll<AudioClip>("Dialogue");
		johnAnimator = John.GetComponent<Animator>();
		conversationPoint = 0;
		isTalking = false;
		johnTalking = true;
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
			johnAnimator.SetBool("JohnTalking", johnTalking);
			if(johnTalking) {
				audioPlayer.clip = voiceClips[conversationPoint];
			}
			else {
				//audio.clip = voiceClips[conversationPoint ]
			}
			audioPlayer.Play();
			while(audioPlayer.isPlaying) {
				yield return null;
			}
			Debug.Log(conversationPoint);
			conversationPoint++;
			johnTalking = !johnTalking;
			isTalking = false;
		}
	}
}
