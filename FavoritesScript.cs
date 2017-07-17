using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FavoritesScript : MonoBehaviour {

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
	public GameObject ParentMen,ParentWomen,ParentKids;

	IEnumerator Start () {
		
		WWW UserPrefData = new WWW ("https://augmentshop.000webhostapp.com/userpref.php");
		yield return UserPrefData;
		string UserPrefDataString = UserPrefData.text;

		int usercount = 0;

		//splitting users into array
		string[] UserFavDataArray = new string[usercount];
		UserFavDataArray = UserPrefDataString.Split (';');

		string UserFavData = UserFavDataArray [PlayerPrefs.GetInt("UserID")-1];
		for (int i=0;i < usercount;i++){
			UserFavDataArray[i] = null;
		}

		//Get FavData String for Logged in User
		string FavData = UserFavData.Substring (UserFavData.IndexOf ("Favlist:") + 8);
		FavData = FavData.Remove (FavData.IndexOf("|"));

		int check = FavData.Split ('-').Length - 1;
		string[] FavProdsArray = new string[check];

		FavProdsArray = FavData.Split ('-');

		int[] FavProds = new int [FavProdsArray.Length-1];
		int test;

		for (int i = 0; i < FavProdsArray.Length-1; i++) {
			if (int.TryParse (FavProdsArray [i], out test))
				FavProds [i] = int.Parse(FavProdsArray [i])-1;
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
		foreach (int i in FavProds) 
		{
			Products [i]=Instantiate (productobj,new Vector3(Mathf.Round(transform.position.x/350)*350,Mathf.Round(transform.position.y/500)*500,transform.position.z), Quaternion.identity)as GameObject;
			Products [i].transform.GetChild (1).GetComponent<Text> ().text = Itemset [i].getPrice().ToString();
			Products [i].transform.GetChild (2).GetComponent<Text> ().text = Itemset [i].getProductName();
			Products [i].transform.GetChild (3).GetComponent<Text> ().text = Itemset [i].getProductID().ToString();
			Products [i].transform.GetChild (0).GetComponentInChildren<RawImage> ().texture = (Texture2D)Resources.Load (Itemset [i].getProductName ()+"Img1");
			Products [i].transform.GetChild (4).GetComponent<Image> ().color = Color.red;

			if(Itemset[i].getCategory()=="Men")
				Products[i].transform.SetParent (ParentMen.transform,false);
			else if (Itemset[i].getCategory()=="Women")
				Products[i].transform.SetParent (ParentWomen.transform,false);
			else
				Products[i].transform.SetParent (ParentKids.transform,false);
		}
	}



}
