using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PinkPlanet : Planet {

	public override void DoAction() {
		SceneManager.LoadScene("AlarmGame");
	}
}
