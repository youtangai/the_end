using UnityEngine;
using System.Collections;

public class Enemy1Script : MonoBehaviour {

	Rigidbody2D rigidbody2D;
	public int speed = -3;
	public GameObject explosion;//爆発のエフェクト

	public int attackPoint = 10;//ダメージ量
	public LifeScript lifeScript;

	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		rigidbody2D.velocity = new Vector2 (speed, rigidbody2D.velocity.y);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if (col.tag == "Bullet") {
			Destroy (gameObject);
			Instantiate (explosion, transform.position, transform.rotation);
		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		//UnityChanとぶつかった時
		if (col.gameObject.tag == "UnityChan") {
			//LifeScriptのLifeDownメソッドを実行
			lifeScript.LifeDown(attackPoint);
		}
	}
}
