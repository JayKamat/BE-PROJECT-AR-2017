using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CartAddRemove : MonoBehaviour {


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

				WWW www = new WWW ("https://augmentshop.000webhostapp.com/cartadd.php", form);
				yield return www;
				//Update finish

			transform.GetChild (9).gameObject.SetActive (false);

		} else if (SceneManager.GetActiveScene ().name == "BuyCartScene") {
			string productid = transform.GetChild (6).GetComponent<Text> ().text;

			//update
			form.AddField ("pidPost", productid);
			form.AddField ("uidPost", userid);

			WWW www = new WWW ("https://augmentshop.000webhostapp.com/cartremove.php", form);
			yield return www;
			//Update finish

			Destroy (gameObject);
		}
	}
}
