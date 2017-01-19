using UnityEngine;
using System.Collections;

public class CallStage1Script : MonoBehaviour {

	void Update ()
	{
		if(Input.GetKeyDown ("space")){
			Application.LoadLevel("Stage1");
		}
	}
}