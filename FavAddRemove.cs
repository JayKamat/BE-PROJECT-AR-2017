using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FavAddRemove : MonoBehaviour {

	public void RunAddRemove(){
		StartCoroutine (AddRemove());
	}

	public IEnumerator AddRemove(){
		WWWForm form = new WWWForm ();
		string productid = transform.GetChild (3).GetComponent<Text> ().text;
		string userid = "1";

		if (transform.GetChild (4).GetComponent<Image> ().color == Color.red) {
			if (SceneManager.GetActiveScene ().name == "MenuScene") {
				transform.GetChild (4).GetComponent<Image> ().color = Color.white;

				//update
				form.AddField ("pidPost", productid);
				form.AddField ("uidPost", userid);

				WWW www = new WWW ("https://augmentshop.000webhostapp.com/favlist.php", form);
				yield return www;
				//Update finish
			} else if (SceneManager.GetActiveScene ().name == "FavouritesScene") {

				//update
				form.AddField ("pidPost", productid);
				form.AddField ("uidPost", userid);

				WWW www = new WWW ("https://augmentshop.000webhostapp.com/favlist.php", form);
				yield return www;
				//Update finish

				Destroy (gameObject);
			}
		} else {
			transform.GetChild (4).GetComponent<Image> ().color = Color.red;

			//update
			form.AddField ("pidPost", productid);
			form.AddField ("uidPost", userid);

			WWW www = new WWW ("https://augmentshop.000webhostapp.com/favlistadd.php", form);
			yield return www;
			//Update finish
		}

	}

}
