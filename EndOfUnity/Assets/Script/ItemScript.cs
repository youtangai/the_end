using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour {

	public int healPoint = 20;
	//Prefab化するとInspectorから指定できないためにprivate化
	private LifeScript lifeScript;

	void Start(){
		//HPタグの付いているオブジェクトのLifeScriptを取得
		lifeScript = GameObject.FindGameObjectWithTag("HP").GetComponent<LifeScript>();
	}


	void OnCollisionEnter2D (Collision2D col){
		//ユニティちゃんと衝突したとき
		if (col.gameObject.tag == "UnityChan") {
			//LifeUpメソッドを呼び出す 引数はhealPoint
			lifeScript.LifeUp(healPoint);
			//アイテムを削除する
			Destroy(gameObject);
		}
	}
}
