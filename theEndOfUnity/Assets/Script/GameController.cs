using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	// Use this for initialization
	enum State {
		Ready,
		Play,
		GameOver
	}

	State state;
	int score;

	public AzarashiController azarashi;
	public GameObject blocks;
	public Text scoreLabel;
	public Text stateLabel;
	private AudioSource pikon;

	void Start(){
		pikon = GetComponent<AudioSource>();
		Ready ();
	}

	void LateUpdate(){
		switch (state) {
			case State.Ready:
				if (Input.GetButtonDown ("Fire1"))
					GameStart ();
				break;

		case State.Play:
			if (azarashi.IsDead ())
				GameOver ();
			break;
		case State.GameOver:
			if (Input.GetButton ("Fire1"))
				Reload ();
			break;
		}
	}

	void Ready(){
		state = State.Ready;
		azarashi.SetSteerActive (false);
		blocks.SetActive (false);

		scoreLabel.text = "Score : " + 0;

		stateLabel.gameObject.SetActive (true);
		stateLabel.text = "Ready";
	}

	void GameStart(){
		state = State.Play;

		azarashi.SetSteerActive (true);
		blocks.SetActive (true);

		azarashi.Flap ();

		stateLabel.gameObject.SetActive (false);
		stateLabel.text = "";
	}

	void GameOver(){
		state = State.GameOver;

		ScrollObject[] scrollObject = GameObject.FindObjectsOfType<ScrollObject> ();

		foreach(ScrollObject so in scrollObject) so.enabled = false;

		stateLabel.gameObject.SetActive (true);
		stateLabel.text = "GameOver";
	}
	
	// Update is called once per frame
	void Reload () {
		Application.LoadLevel (Application.loadedLevel);
	}

	public void IncreaseScore(){
		score++;
		pikon.PlayOneShot (pikon.clip);
		scoreLabel.text = "Score : " + score;
	}
}
