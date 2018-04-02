using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject CarPrefarb;

	//public int SpawnerType;

	public Transform[] LPath;
	public Transform[] RPath;
	public Transform[] SPath;

	[Space]
	public Transform LRotPoint;
	public Transform RRotPoint;

	[Space]
	public bool IsSpawn;
	private int NumType;

	public Transform Parent;



	void Update () {
		Spawn ();
	}

	void Spawn (){
		if (IsSpawn == true){

			Randomize ();

			switch (NumType) 
			{
			case 0:
				for (int i = 0; i < LPath.Length; i++) {
					CarPrefarb.GetComponent<Car> ().PathArray [i] = SPath [i];
				}

				CarPrefarb.GetComponent<Car> ().TurnType = 0;
				break;
			
			case 1:
				for (int i = 0; i < LPath.Length; i++) {
					CarPrefarb.GetComponent<Car> ().PathArray [i] = LPath [i];
				}

				CarPrefarb.GetComponent<Car> ().RotPoint = LRotPoint;
				CarPrefarb.GetComponent<Car> ().TurnType = 1;
				break;
			
			case 2:
				for (int i = 0; i < RPath.Length; i++) {
					CarPrefarb.GetComponent<Car>().PathArray[i] = RPath[i];
				}

				CarPrefarb.GetComponent<Car> ().RotPoint = RRotPoint;
				CarPrefarb.GetComponent<Car> ().TurnType = 2;
				break;
			}

			Instantiate (CarPrefarb, transform.position, Quaternion.Euler(new Vector3(0, Parent.transform.localEulerAngles.y, 0)),Parent.transform);
			IsSpawn = false;
		}
		else if (IsSpawn == false){
			return;
		}
	}

	void Randomize(){
		int index = Random.Range (0,3);
		NumType = index;
	}
}
