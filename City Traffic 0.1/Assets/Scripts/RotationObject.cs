using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationObject : MonoBehaviour {

	public GameObject test;
	public Vector3 point;

	private bool StartRotating = true;

	// Update is called once per frame
	void Update () {
		if (StartRotating == true)
			transform.RotateAround (point, Vector3.up, 50 * Time.deltaTime);
		else if (StartRotating == false)
			return;


		//Debug.Log (transform.position);

		//transform.Rotate(Vector3.up , 10 * Time.deltaTime);
		//transform.Rotate(new Vector3 (0, 10 , 0), 10 * Time.deltaTime);
		if (transform.position.x >= 2.5f) {
			//Debug.Log (true);
			StartRotating = false;
		}
	}
}
