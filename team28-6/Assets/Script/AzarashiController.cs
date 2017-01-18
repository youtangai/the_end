using UnityEngine;
using System.Collections;

public class AzarashiController : MonoBehaviour {
	Rigidbody2D rb2d;
	Animator animator;
	float angle;
	bool isDead;

	public float maxHeight;
	public float flapVelocity;
	public float relativeVelocityX;
	public GameObject sprite;
	private AudioSource jump;
	private AudioSource explosion;

	public bool IsDead(){
		return isDead;
	}
		
	void Awake (){
		rb2d = GetComponent<Rigidbody2D>();
		animator = sprite.GetComponent<Animator> ();
		AudioSource[] audioSources = GetComponents<AudioSource>();
		explosion = audioSources[0];
		jump = audioSources[1];
	}
	// Use this for initialization
//	void Start () {
//	
//	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1") && transform.position.y < maxHeight) {
			Flap();
		}

		ApplyAngle ();

		animator.SetBool ("flap", angle >= 0.0f);
	}

	public void Flap (){
		if (isDead) return;

		if (rb2d.isKinematic)
			return;

		jump.PlayOneShot (jump.clip);
		rb2d.velocity = new Vector2 (0.0f, flapVelocity);
	}

	void ApplyAngle(){
		float targetAngle;

		if (isDead) {
			targetAngle = -90.0f;
		} else {
			targetAngle = Mathf.Atan2 (rb2d.velocity.y, relativeVelocityX) * Mathf.Rad2Deg;
		}

		angle = Mathf.Lerp (angle, targetAngle, Time.deltaTime * 10.0f);

		sprite.transform.localRotation = Quaternion.Euler (0.0f, 0.0f, angle);
	}

	void OnCollisionEnter2D(){
		if (isDead)
			return;

		explosion.PlayOneShot (explosion.clip);
		Camera.main.SendMessage ("Clash");

		isDead = true;
	}

	public void SetSteerActive(bool active){
		rb2d.isKinematic = !active;
	}
}
