﻿using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float speed = 4f; //歩くスピード
	public float jumpPower = 700; //ジャンプ力
	public LayerMask groundLayer; //Linecastで判定するLayer
	public GameObject mainCamera;//カメラオブジェクト
	public GameObject bullet;//弾丸
	private Rigidbody2D rigidbody2D;
	private Animator anim;
	private bool isGrounded;

	void Start () {
		//各コンポーネントをキャッシュしておく
		anim = GetComponent<Animator>();
		rigidbody2D = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		//LInecastでユニティちゃんの足元に地面があるかどうか
		isGrounded = Physics2D.Linecast(
			transform.position + transform.up * 1,
			transform.position - transform.up * 0.05f,
			groundLayer);
		//スペースキーを押し、
		if (Input.GetKeyDown ("space")) {
			//着地していた時、
			if (isGrounded) {
				//Dashアニメーションをとめて、Jumpアニメーションを実行
				anim.SetBool("Dash", false);
				anim.SetTrigger ("Jump");
				//着地判定をfalse
				isGrounded = false;
				//AddForceにて上方向へ力を加える
				rigidbody2D.AddForce(Vector2.up * jumpPower);
			}
		}
		//上下への移動速度を取得
		float velY = rigidbody2D.velocity.y;
		//移動速度が0.1より大きければ上昇
		bool isJumping = velY > 0.1f ? true:false;
		//移動速度が-0.1より小さければ下降
		bool isFalling = velY < -0.1f ? true:false;
		//結果をアニメータービューの変数へ反映する
		anim.SetBool("isJumping", isJumping);
		anim.SetBool("isFalling", isFalling);
		if (Input.GetKeyDown ("left ctrl")){
			anim.SetTrigger("Shot");
			Instantiate(bullet, transform.position + new Vector3(0.8f,1.2f,0f), transform.rotation);
		}
	}

	void FixedUpdate ()
	{
		//左キー: -1、右キー: 1
		float x = Input.GetAxisRaw ("Horizontal");
		//左か右を入力したら
		if (x != 0) {
			//入力方向へ移動
			rigidbody2D.velocity = new Vector2 (x * speed, rigidbody2D.velocity.y);
			//localScale.xを-1にすると画像が反転する
			Vector2 temp = transform.localScale;
			temp.x = x;
			transform.localScale = temp;
			//Wait→Dash
			anim.SetBool ("Dash", true);
			//左も右も入力していなかったら

			//画面中央から左に4移動した位置お画面中央にする
			if (transform.position.x > mainCamera.transform.position.x - 4) {
				//カメラの位置を取得
				Vector3 cameraPos = mainCamera.transform.position;

				//ユニティちゃんの位置から右に4移動した位置を画面中央にする
				cameraPos.x = transform.position.x + 4;
				mainCamera.transform.position = cameraPos;
			}
			//カメラ表示領域の左下をワールド座標に変換
			Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

			//カメラ表示領域の右上をワールド座標に変換
			Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

			//ユニティちゃんのポジションを取得
			Vector2 pos = transform.position;

			//ユニティちゃんのx座標の移動範囲をClampメソッドで制限
			pos.x = Mathf.Clamp(pos.x, min.x + 0.5f, max.x);
			transform.position = pos;
		} else {
			//横移動の速度を0にしてピタッと止まるようにする
			rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
			//Dash→Wait
			anim.SetBool ("Dash", false);
		}
	}
}