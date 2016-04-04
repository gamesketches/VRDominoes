using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public enum EmotionalState {Neutral, Happy, Sad};

public class Director : MonoBehaviour {

	public GameObject John;
	private Animator johnAnimator;
	public GameObject Roger;
	public GameObject walkingRoger;
	private Animator rogerAnimator;

	private int conversationPoint;
	private int conversationLength = 3;
	private bool isTalking;
	private bool johnTalking;
	private AudioClip[] johnVoiceClips;
	private AudioClip[] voiceClips;
	private EmotionalState mood;
	public AudioSource audioPlayer;
	private bool rogerArrives = false;
	// Use this for initialization
	void Start () {
		VRSettings.renderScale = 0.5f;
		Roger.gameObject.SetActive(false);
		voiceClips = Resources.LoadAll<AudioClip>("Dialogue");
		johnVoiceClips = new AudioClip[3] {voiceClips[0], voiceClips[1], voiceClips[2]};
		johnAnimator = John.GetComponent<Animator>();
		rogerAnimator = Roger.GetComponent<Animator>();
		conversationPoint = 0;
		isTalking = false;
		johnTalking = true;
		mood = EmotionalState.Neutral;
	}
	
	// Update is called once per frame
	void Update () {
		if(rogerArrives) {
			if(!isTalking) {
				StartCoroutine(PlayAudio());
			}
			if(Input.GetKeyDown(KeyCode.R)){
				Application.LoadLevel(Application.loadedLevel);
			}
		}
		else {
			if(walkingRoger.gameObject.transform.position.x < 0.3f){
				walkingRoger.SetActive(false);
				Roger.gameObject.SetActive(true);
				rogerArrives = true;
			}
		}
	}

	public void ChangeMood(EmotionalState newMood) {
		mood = newMood;
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
				rogerAnimator.SetInteger("mood", (int)mood);
				foreach(AudioClip clip in voiceClips){
					if(clip.name == string.Concat("Roger", conversationPoint.ToString(), 
						((int)mood + 1).ToString())) {
						audioPlayer.clip = clip;
						break;
					}
				}
				mood = EmotionalState.Neutral;
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
