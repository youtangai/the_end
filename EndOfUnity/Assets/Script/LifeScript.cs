using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeScript : MonoBehaviour {

	RectTransform rt;


	public GameObject unityChan;
	public GameObject explosion;
	public Text gameOverText;
	private bool gameOver = false;

	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform> ();
	}

	void Update(){
		if (rt.sizeDelta.y <= 0) {
			if (gameOver == false) {
				Instantiate (explosion, unityChan.transform.position + new Vector3(0, 1, 0), unityChan.transform.rotation);

			}
			GameOver ();
		}

		if (gameOver) {
			gameOverText.enabled = true;

			if (Input.GetKeyDown("space")) {
				Application.LoadLevel ("Title");
			}
		}
	}
	
	public void LifeDown(int ap){
		//RectTransformのサイズを取得し、マイナスする
		rt.sizeDelta -= new Vector2 (0, ap);
	}

	public void LifeUp(int hp){
		//RectTransformのサイズを取得、プラスする
		rt.sizeDelta += new Vector2(0, hp);
		//最大値を超えたら、最大値で上書きする
		if (rt.sizeDelta.y > 240f) {
			rt.sizeDelta = new Vector2 (51f, 240f);
		}
	}

	public void GameOver(){
		gameOver = true;
		Destroy (unityChan);
	}
}
