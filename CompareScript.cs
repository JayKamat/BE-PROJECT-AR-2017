using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompareScript : MonoBehaviour {

	public class Items{

		private int ProductID;
		private string ProductName;
		private string Category;
		private decimal Price;
		private string Belt;
		private string Waterproof;
		private int Quantity;
		private string Info;

		public Items(){
			ProductID=-1;
			ProductName=null;
			Category=null;
			Price=-1;
			Belt=null;
			Waterproof=null;
			Quantity=-1;
			Info=null;

		}

		public void create(string s){
			string temp;
			temp= s.Substring(s.IndexOf("ID:")+3);
			temp = temp.Remove (temp.IndexOf ("|"));
			ProductID =int.Parse(temp);

			temp= s.Substring(s.IndexOf("Name:")+5);
			temp = temp.Remove (temp.IndexOf ("|"));
			ProductName = temp;

			temp= s.Substring(s.IndexOf("Category:")+9);
			temp = temp.Remove (temp.IndexOf ("|"));
			Category = temp;

			temp=s.Substring(s.IndexOf("Price:")+6);
			temp = temp.Remove (temp.IndexOf ("|"));
			Price = decimal.Parse(temp);

			temp=s.Substring(s.IndexOf("Belt:")+5);
			temp = temp.Remove (temp.IndexOf ("|"));
			Belt = temp;

			temp=s.Substring(s.IndexOf("Waterproof:")+11);
			temp = temp.Remove (temp.IndexOf ("|"));
			Waterproof = temp;

			temp=s.Substring(s.IndexOf("Quantity:")+9);
			temp = temp.Remove (temp.IndexOf ("|"));
			Quantity =int.Parse(temp);

			Info = s.Substring(s.IndexOf("Info:")+5);
		}

		public int getProductID()
		{
			return ProductID;
		}

		public string getProductName()
		{
			return ProductName;
		}

		public string getCategory()
		{
			return Category;
		}

		public decimal getPrice()
		{
			return Price;
		}

		public string getBelt()
		{
			return Belt;
		}

		public string getWaterproof()
		{
			return Waterproof;
		}

		public int getQuantity()
		{
			return Quantity;
		}

		public string getInfo()
		{
			return Info;
		}

	};

	public GameObject productobj;
	public GameObject ParentContent;

	// Use this for initialization
	IEnumerator Start () {
		WWW UserPrefData = new WWW ("https://augmentshop.000webhostapp.com/userpref.php");
		yield return UserPrefData;
		string UserPrefDataString = UserPrefData.text;

		int usercount = 0;

		//splitting users into array
		string[] UserPrefDataArray = new string[usercount];
		UserPrefDataArray = UserPrefDataString.Split (';');


		string UserCompData = UserPrefDataArray [0];
		for (int i=0;i < usercount;i++){
			UserPrefDataArray[i] = null;
		}

		//Get CompData String for Logged in User
		string CompData = UserCompData.Substring (UserCompData.IndexOf ("Complist:") + 9);
		CompData = CompData.Remove (CompData.IndexOf("|"));

		int check = CompData.Split ('-').Length - 1;
		string[] CompProdsArray = new string[check];

		CompProdsArray = CompData.Split ('-');

		int[] CompProds = new int [CompProdsArray.Length-1];
		int test;

		for (int i = 0; i < CompProdsArray.Length-1; i++) {
			if (int.TryParse (CompProdsArray [i], out test))
				CompProds [i] = int.Parse(CompProdsArray [i])-1;
		}






		WWW itemsData = new WWW ("https://augmentshop.000webhostapp.com/Itemset.php");

		yield return itemsData;
		string itemDataString = itemsData.text;

		int productcount = itemDataString.Split (';').Length-1;	

		//splitting info into array
		string[] itemDataArray1 = new string[productcount];
		itemDataArray1 = itemDataString.Split (';');

		//splitting info into Items Objects()
		Items[] Itemset = new Items[productcount];
		string s1;

		for (int i = 0; i < productcount; i++) {
			Itemset [i] = new Items ();
			s1 = itemDataArray1 [i];
			Itemset [i].create (s1);
		}

		//dynamic list of products
		GameObject[] Products = new GameObject[productcount];

		//instantiating product prefabs
		foreach (int i in CompProds) 
		{
			Products[i] = Instantiate (productobj,new Vector3(Mathf.Round(transform.position.x/300)*300,Mathf.Round(transform.position.y/1015)*1015,transform.position.z), Quaternion.identity)as GameObject;

			Products[i].transform.GetChild (0).GetComponentInChildren<RawImage> ().texture = (Texture2D)Resources.Load (Itemset [i].getProductName ()+"Img1");
			Products[i].transform.GetChild (1).GetComponentInChildren<Text> ().text = Itemset [i].getProductName();
			Products[i].transform.GetChild (2).GetComponentInChildren<Text> ().text = Itemset [i].getCategory();
			Products[i].transform.GetChild (3).GetComponentInChildren<Text> ().text = "Rs." + Itemset [i].getPrice().ToString();
			Products[i].transform.GetChild (4).GetComponentInChildren<Text> ().text = Itemset [i].getBelt();
			Products[i].transform.GetChild (5).GetComponentInChildren<Text> ().text = Itemset [i].getWaterproof();
			Products[i].transform.GetChild (6).GetComponent<Text> ().text = Itemset [i].getProductID().ToString();

			Products[i].transform.SetParent (ParentContent.transform,false);
		}
	}

}
