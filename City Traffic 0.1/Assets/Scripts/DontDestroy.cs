using UnityEngine;

public class DontDestroy : MonoBehaviour {

	public GameObject[] objects;
	private static GameObject[] instance;

	void Awake(){
		foreach (GameObject g in objects) 
		{
			DontDestroyOnLoad (g);
		}

		if (instance == null)
			instance = objects;
		else 
		{
			foreach (GameObject go in objects) 
			{
				Destroy (go);
			}
			return;
		}

	}

}
