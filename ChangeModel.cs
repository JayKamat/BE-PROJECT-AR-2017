using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class ChangeModel : MonoBehaviour {
	
	public Material[] materials;
	public Renderer rend;
	private int index = 0;
	void start(){
		rend = GetComponent<Renderer> ();
		rend.enabled = true;
	}

	public void NextColor(){
		Debug.Log ("NextColor");
		if (materials.Length == 0)
			return;
		else {
			if (index < materials.Length) {
				rend.sharedMaterial = materials [index];
				index++;
			}
			if (index >= materials.Length)
				index = 0;
			Debug.Log (index);

		}
		
	}

	public void PrevColor(){
		Debug.Log ("PrevColor");
		if (materials.Length == 0)
			return;
		else {
			if (index >= 0) {
				rend.sharedMaterial = materials [index];
				index--;
			}
			if (index < 0)
				index =  materials.Length-1;
			Debug.Log (index);

		}

	}
}
