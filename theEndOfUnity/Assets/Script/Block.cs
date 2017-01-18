using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
	public float minHeight;
	public float maxHeight;
	public GameObject pivot;
	float height;
	float accel = 0.05f;

	// Use this for initialization
	void Start () {
		ChangeHeight ();
	}

	// Update is called once per frame
	void Update () {
		if (height >= maxHeight)
			accel *= -1;
		else if (height <= minHeight)
			accel *= -1;
		height += accel;
		pivot.transform.localPosition = new Vector3 (0.0f, height, 0.0f);
	}

	void ChangeHeight(){
		height = Random.Range (minHeight, maxHeight);
		pivot.transform.localPosition = new Vector3 (0.0f, height, 0.0f);
	}

	void OnScrollEnd(){
		ChangeHeight ();
	}
}