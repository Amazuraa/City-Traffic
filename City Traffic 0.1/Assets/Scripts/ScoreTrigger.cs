using UnityEngine;

public class ScoreTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		if (col.tag == "Car") {
			GameObject score;
			score = GameObject.FindGameObjectWithTag ("Score");

			if (score != null)
				score.GetComponent<Score> ().ScoreNumber += 1;
			else
				return;
		}
	}
}
