using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterScript : MonoBehaviour {

	public Text user;
	public Text pass;

	public void onClickThis(){
		StartCoroutine (RegisterUser ());
	}

	public IEnumerator RegisterUser(){
		WWWForm form = new WWWForm ();
		form.AddField ("usernamePost", user.text);
		form.AddField ("passwordPost", pass.text);

		WWW www = new WWW ("https://augmentshop.000webhostapp.com/registeruser.php", form);
		yield return www;
		string Result = www.text;
		transform.GetChild(1).GetComponent<Text>().text = Result;
	}
}
