using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchScript : MonoBehaviour {

	public Text SearchQuery;

	public void Search () {

		int number = transform.childCount;
		GameObject[] Products=new GameObject[number];

		for (int i = 0; i < number; i++) {
			Products[i] = transform.GetChild(i).gameObject;
		}

		for (int i = 0; i < Products.Length; i++) {
			if ((Products [i].transform.GetChild (2).GetComponent<Text> ().text).Contains (SearchQuery.text)) 
			{
				Products [i].SetActive (true);
			} 
			else { 
				Products [i].SetActive (false);
			}
		}
	}

}
