using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public GameObject RestartUI;


	public void EndGame(){
		if (RestartUI != null)
			RestartUI.SetActive(true);
	}

	public void RestartGame(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void GoToMenu(){
		SceneManager.LoadScene("Menu",LoadSceneMode.Single);
	}

	public void GoToGame(){
		SceneManager.LoadScene("Level1",LoadSceneMode.Single);
	}
}
