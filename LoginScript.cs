using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour {

	public Text UsernameText;
	public Text PasswordText;

	public void onClickThis(){
		StartCoroutine (NextScene());
	}

	public IEnumerator NextScene()
	{
		WWWForm form = new WWWForm ();
		form.AddField ("usernamePost", UsernameText.text);
		form.AddField ("passwordPost", PasswordText.text);

		WWW www = new WWW ("https://augmentshop.000webhostapp.com/loginuser.php", form);
		yield return www;
		string Result = www.text;

		if (Result == "Username or Password is incorrect! Please Try Again!" || Result == "Error Occurred! Please Contact Support Immediately!") {
			transform.GetChild (1).GetComponent<Text> ().text = Result;
		} else {
			print (Result);
			PlayerPrefs.SetInt ("UserID", 1);
			SceneManager.LoadScene ("MenuScene");
		}

	}
}
