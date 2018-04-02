using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Manager : MonoBehaviour {

	public GameObject Spawner0;
	public GameObject Spawner1;

	public int SpawnDelay;
	public int DelayCount = 0;

	public bool StartSpawn = false;

	public bool IsCrash = false;



	void Start () {
	}

	void Update () {
		if (IsCrash == false) {
			DelayCount++;
			if (DelayCount == SpawnDelay) {
				StartSpawn = true;
				Spawn ();
				DelayCount = 0;
			}
		} 
		else if (IsCrash == true) 
			return;
	}

	void Spawn (){
		if (StartSpawn == true) {
			int index = Random.Range (0, 4);
			switch (index) 
			{
				case 0:
				Spawner0.GetComponent<Spawner> ().IsSpawn = true;
				break;

				case 1:
				Spawner1.GetComponent<Spawner> ().IsSpawn = true;
				break;

				case 2:
				Spawner0.GetComponent<Spawner> ().IsSpawn = true;
				break;

				case 3:
				Spawner1.GetComponent<Spawner> ().IsSpawn = true;
				break;
			}
		} 
		else if (StartSpawn == false)
			return;
	}
}
