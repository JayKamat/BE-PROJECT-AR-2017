using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WishAddRemove : MonoBehaviour {

	public void RunAddRemove(){
		StartCoroutine (AddRemove());
	}

	public IEnumerator AddRemove(){
		WWWForm form = new WWWForm ();
		string userid = "1";

		if (SceneManager.GetActiveScene ().name == "ProductScene") {
			string productid = transform.GetChild (11).GetComponent<Text> ().text;

				//update
				form.AddField ("pidPost", productid);
				form.AddField ("uidPost", userid);

				WWW www = new WWW ("https://augmentshop.000webhostapp.com/wishadd.php", form);
				yield return www;
				//Update finish

			transform.GetChild (10).gameObject.SetActive(false);

		} else if (SceneManager.GetActiveScene ().name == "WishlistScene") {
				string productid = transform.GetChild (3).GetComponent<Text> ().text;

				//update
				form.AddField ("pidPost", productid);
				form.AddField ("uidPost", userid);

				WWW www = new WWW ("https://augmentshop.000webhostapp.com/wishremove.php", form);
				yield return www;
				//Update finish

				Destroy (gameObject);
			}
	}
}
