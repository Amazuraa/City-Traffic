using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour {

	public int ScoreNumber = 0;
	public Text textNumber;

	void Update () {
		textNumber.text = ScoreNumber.ToString();

		if (ScoreNumber > PlayerPrefs.GetInt("HighScore", 0))
			PlayerPrefs.SetInt ("HighScore", ScoreNumber);
	}
}
