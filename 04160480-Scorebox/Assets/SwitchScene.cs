using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown (KeyCode.M)){
			switchScene ("Main");
		} else if (Input.GetKeyDown (KeyCode.O)) {
			switchScene ("Game1");
		} else if (Input.GetKeyDown (KeyCode.T)) {
			switchScene ("Game2");
		}
	}
		public void switchScene(string sname){
			SceneManager.LoadScene(sname);
		}
		public void ExitGame(){
			Application.Quit ();
		}

}
