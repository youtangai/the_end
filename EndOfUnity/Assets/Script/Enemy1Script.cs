using UnityEngine;
using System.Collections;

public class Enemy1Script : MonoBehaviour {

	Rigidbody2D rigidbody2D;
	public int speed = -3;
	public GameObject explosion;//爆発のエフェクト
	public GameObject item;//itemのオブジェクト
	public int attackPoint = 10;//ダメージ量
	private LifeScript lifeScript;

	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D> ();
		lifeScript = GameObject.FindGameObjectWithTag ("HP").GetComponent<LifeScript> ();
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

			//1/4の確立でアイテムを落とす
			if (Random.Range (0, 4) == 0) {
				Instantiate (item, transform.position, transform.rotation);
			}
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
