using UnityEngine;
using System.Collections;

public enum EmotionalState {Neutral, Happy, Sad};

public class Director : MonoBehaviour {

	public GameObject John;
	private Animator johnAnimator;
	public GameObject Roger;
	private Animator rogerAnimator;

	private int conversationPoint;
	private int conversationLength = 3;
	private bool isTalking;
	private bool johnTalking;
	private AudioClip[] johnVoiceClips;
	private AudioClip[] voiceClips;
	public AudioSource audioPlayer;
	// Use this for initialization
	void Start () {
		voiceClips = Resources.LoadAll<AudioClip>("Dialogue");
		johnVoiceClips = new AudioClip[3] {voiceClips[0], voiceClips[1], voiceClips[2]};
		johnAnimator = John.GetComponent<Animator>();
		rogerAnimator = Roger.GetComponent<Animator>();
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
		if(conversationPoint < conversationLength) {
			johnAnimator.SetBool("JohnTalking", johnTalking);
			if(johnTalking) {
				audioPlayer.clip = johnVoiceClips[conversationPoint];
			}
			else {
				conversationPoint++;
				Debug.Log(conversationPoint);
				rogerAnimator.SetInteger("conversationPoint", conversationPoint);
				rogerAnimator.SetInteger("mood", (int)EmotionalState.Sad);
				foreach(AudioClip clip in voiceClips){
					if(clip.name == string.Concat("Roger", conversationPoint.ToString(), 
						((int)EmotionalState.Sad + 1).ToString())) {
						audioPlayer.clip = clip;
						break;
					}
				}
			}
			audioPlayer.Play();
			while(audioPlayer.isPlaying) {
				yield return null;
			}
			johnTalking = !johnTalking;
			isTalking = false;
		}
	}
}
