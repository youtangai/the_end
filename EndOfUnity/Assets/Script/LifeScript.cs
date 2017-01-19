using UnityEngine;
using System.Collections;

public class LifeScript : MonoBehaviour {

	RectTransform rt;

	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform> ();
	}
	
	public void LifeDown(int ap){
		//RectTransformのサイズを取得し、マイナスする
		rt.sizeDelta -= new Vector2 (0, ap);
	}
}
