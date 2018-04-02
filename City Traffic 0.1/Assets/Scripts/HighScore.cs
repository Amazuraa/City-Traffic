using UnityEngine.UI;
using UnityEngine;

public class HighScore : MonoBehaviour {

	public Text highscore;

	void Update () {
		highscore.text = PlayerPrefs.GetInt ("HighScore", 0).ToString ();
	}

}
