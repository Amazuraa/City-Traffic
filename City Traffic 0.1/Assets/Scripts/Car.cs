using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour 
{

	[Header("Path Settings")]
	public Transform[] PathArray;
	private Vector3[] PathPos;
	private short PathNumber = 0;


	[Space]
	[Header("Rotation Control")]
	public Transform RotPoint;

	[Range(1, 100)]
	public short RotSpeed = 10;
	[Range(1f, 10f)]
	public float RotAccelR = 1f;
	[Range(1f, 10f)]
	public float RotAccelL = 1f;
	private bool StartRotating = false;

	public GameObject LTire;
	public GameObject RTire;
	private string TireRotating = "Rotating";
	private float Tire_degree = 0;

	//[HideInInspector]
	public short RotMax;
	//[HideInInspector]
	public short TurnType = 0;
	/*
	 * Turn Type: 0=Stright ; 1=Left ; 2=Right
	 */


	[Space]
	[Header("Movement Control")]

	[Range(1f, 5f)]
	public float MoveSpeed = 1f;
	[Range(0f, 3f)]
	public float AccelSpeed = 1f;

	private float AccelSpeedStart = 0f;
	private float AccelSpeedCurrent = 0f;
	private bool StartMoving = true;


	[Space]
	[Header("Touch Control")]
	public bool IsTouchAble = true;
	private bool IsCrash = false;
	public bool PreStart = false;


	void Start () {
		PathPos = new Vector3[PathArray.Length];
		transform.position = new Vector3 (PathArray[0].position.x, transform.position.y, PathArray[0].position.z);
	}



	void Update () 
	{
		Movement ();
		Rotation ();
	}



	void Movement(){
		// Path Initialization
		for (int i = 0; i < PathArray.Length; i++)
			PathPos[i] = new Vector3 (PathArray[i].position.x, transform.position.y, PathArray[i].position.z);

		// Statement
		if (StartMoving == true) {
			if (transform.position == PathPos [PathNumber])
				PathNumber++;

			switch (PathNumber) {
			case 2:
				StartRotating = true;
				IsTouchAble = false;
				break;
			
			case 3:
				IsTouchAble = false;
				AccelSpeedCurrent += Time.deltaTime * 1f;
				if (AccelSpeedCurrent > AccelSpeed)
					AccelSpeedCurrent = AccelSpeed;

				transform.position = Vector3.MoveTowards (transform.position, PathPos [PathNumber], (MoveSpeed * Time.fixedDeltaTime * AccelSpeedCurrent));
				break;

			case 4:
				Destroy (transform.gameObject);
				break;

			default:
				AccelSpeedCurrent += Time.deltaTime * 1f;
				if (AccelSpeedCurrent > AccelSpeed)
					AccelSpeedCurrent = AccelSpeed;

				transform.position = Vector3.MoveTowards (transform.position, PathPos [PathNumber], (MoveSpeed * Time.fixedDeltaTime * AccelSpeedCurrent));
				break;
			}
		} 
		else if (StartMoving == false) {
			switch (PathNumber) {
			case 2:
				if (IsCrash == false) {
					AccelSpeedCurrent = AccelSpeed;
					StartMoving = true;
				} 
				else
					return;
				break;

			default :
				AccelSpeedCurrent -= Time.deltaTime * 1.5f;
				if (AccelSpeedCurrent < AccelSpeedStart)
					AccelSpeedCurrent = AccelSpeedStart;

				transform.position = Vector3.MoveTowards (transform.position, PathPos [PathNumber], (MoveSpeed * Time.fixedDeltaTime * AccelSpeedCurrent));
				break;
			}
		}
	}



	void Rotation(){

		if (StartRotating == true) 
		{
			IsTouchAble = false;
			AccelSpeedCurrent = AccelSpeed;
			switch(TurnType)
			{
			case 0: //Stright
				PathNumber = 3;
				StartMoving = true;
				StartRotating = false;
				break;


			case 1: //Left
				transform.RotateAround (RotPoint.position, Vector3.up, -Time.fixedDeltaTime * RotSpeed * RotAccelL);

				if ((transform.localEulerAngles.y*-1) > -270f){ 
					transform.localRotation = Quaternion.Euler (new Vector3 (transform.rotation.x, 270, transform.rotation.z));
					StartMoving = true;
					PathNumber = 3;
					StartRotating = false;
				}
					
				if (TireRotating == "Rotating") {
					Tire_degree -= Time.fixedDeltaTime * RotSpeed * RotAccelL;
					if (Tire_degree < (-35f))
						TireRotating = "Reverse";
				} 
				else if (TireRotating == "Reverse") {
					Tire_degree += Time.fixedDeltaTime * RotSpeed * RotAccelL;
					if (Tire_degree > (0f))
						TireRotating = "Null";
				}
				break;


			case 2: //Right
				transform.RotateAround (RotPoint.position, Vector3.up, Time.fixedDeltaTime * RotSpeed * RotAccelR);

				if (transform.localEulerAngles.y > 90f){ // edited
					transform.localRotation = Quaternion.Euler (new Vector3 (transform.rotation.x, 90, transform.rotation.z));
					StartMoving = true;
					PathNumber = 3;
					StartRotating = false;
				}
					
				if (TireRotating == "Rotating") {
					Tire_degree += Time.fixedDeltaTime * RotSpeed * RotAccelR;
					if (Tire_degree > (35f)) //edited
						TireRotating = "Reverse";
				}
				else if (TireRotating == "Reverse") {
					Tire_degree -= Time.fixedDeltaTime * RotSpeed * RotAccelR;
					if (Tire_degree < (0f))
						TireRotating = "Null";
				}
				break;
			}

			LTire.transform.localRotation = Quaternion.Euler (new Vector3 (0, Tire_degree, 0));
			RTire.transform.localRotation = Quaternion.Euler (new Vector3 (0, Tire_degree, 0));
		}
		else if (StartRotating == false)
			return;
	}

	void OnMouseDown(){
		if (IsTouchAble == true) {
			if (StartMoving == true)
				StartMoving = false;
			else if (StartMoving == false)
				StartMoving = true;
		} 
		else
			return;
			
	}

	public void TouchControl(){
		IsTouchAble = true;
	}

	void OnCollisionEnter(Collision col){
		if (PreStart == false) {
			if (col.transform.tag == "Car") {
				StartMoving = false;
				StartRotating = false;
				IsTouchAble = false;
				IsCrash = true;

				GameObject[] spawnerContainer;
				GameObject[] Car;

				spawnerContainer = GameObject.FindGameObjectsWithTag ("Spawner Container");
				Car = GameObject.FindGameObjectsWithTag ("Car");

				foreach (GameObject r in spawnerContainer)
					r.GetComponent<Spawner_Manager> ().IsCrash = true;
			
				foreach (GameObject r in Car)
					r.GetComponent<Car> ().IsTouchAble = false;

				FindObjectOfType<GameManager> ().EndGame ();
			}
		} 
		else
			return;
	}
}
