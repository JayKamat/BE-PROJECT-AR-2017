using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ARoption : MonoBehaviour {
	public int choice;

	static private int staticchoice;
	static private string SceneName;

	public Toggle[] myToggles;

	void Start(){

		choice = staticchoice;

		switch (choice) {
		case 0:
			{
				myToggles [0].isOn = true;
				SceneName = "VuforiaImplementation";
				break;
			}
		case 1:
			{
				myToggles [1].isOn = true;
				SceneName = "KudanMarkerImplementation";
				break;
			}
		case 2:
			{
				myToggles [2].isOn = true;
				SceneName = "KudanMarkerlessImplementation";
				break;
			}
		default:
			{
				myToggles [0].isOn = true;
				SceneName = "VuforiaImplementation";
				break;
			}
		}
	}

	public void changevalue(int option){
		choice = option;
		staticchoice = choice;

		switch (choice) {
		case 0:
			{
				SceneName = "VuforiaImplementation";
				break;
			}
		case 1:
			{
				SceneName = "KudanMarkerImplementation";
				break;
			}
		case 2:
			{
				SceneName = "KudanMarkerlessImplementation";
				break;
			}
		default:
			{
				SceneName = "VuforiaImplementation";
				break;
			}
		}
	}

	public void GoAR()
	{
		SceneManager.LoadScene (SceneName);
	}
}
