using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WishlistScript : MonoBehaviour {

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

	// Use this for initialization
	IEnumerator Start () {
		WWW UserPrefData = new WWW ("https://augmentshop.000webhostapp.com/userpref.php");
		yield return UserPrefData;
		string UserPrefDataString = UserPrefData.text;

		int usercount = UserPrefDataString.Split (';').Length - 1;

		//splitting users into array
		string[] UserPrefDataArray = new string[usercount];
		UserPrefDataArray = UserPrefDataString.Split (';');


		string UserWishData = UserPrefDataArray [0];
		for (int i=0;i < usercount;i++){
			UserPrefDataArray[i] = null;
		}

		//Get FavData String for Logged in User
		string WishData = UserWishData.Substring (UserWishData.IndexOf ("Wishlist:") + 9);
		WishData = WishData.Remove (WishData.IndexOf("|"));

		int check = WishData.Split ('-').Length - 1;
		string[] WishProdsArray = new string[check];

		WishProdsArray = WishData.Split ('-');

		int[] WishProds = new int [WishProdsArray.Length-1];
		int test;

		for (int i = 0; i < WishProdsArray.Length-1; i++) {
			if (int.TryParse (WishProdsArray [i], out test))
				WishProds [i] = int.Parse(WishProdsArray [i])-1;
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
		GameObject ChildObj;

		//instantiating product prefabs
		foreach (int i in WishProds) 
		{
			Products[i] = Instantiate (productobj,new Vector3(Mathf.Round(transform.position.x/800)*800,Mathf.Round(transform.position.y/500)*500,transform.position.z), Quaternion.identity)as GameObject;

			if (Itemset [i].getQuantity () > 0) {
				Products [i].transform.GetChild (0).GetComponent<Text> ().text = "Availibility: Yes";
			} else {
				Products [i].transform.GetChild (0).GetComponent<Text> ().text = "Availibility: No";
			}
			ChildObj = Products[i].transform.GetChild(2).gameObject;
			ChildObj.transform.GetChild(0).GetComponentInChildren<RawImage> ().texture = (Texture2D)Resources.Load (Itemset [i].getProductName ()+"Img1");
			ChildObj.transform.GetChild (1).GetComponent<Text> ().text = "Rs." + Itemset [i].getPrice().ToString();
			ChildObj.transform.GetChild (2).GetComponent<Text> ().text = Itemset [i].getProductName();

			Products [i].transform.GetChild (3).GetComponent<Text> ().text = Itemset [i].getProductID().ToString();

			//Products [i].transform.GetChild (3).GetComponent<Text> ().text = Itemset [i].getProductID().ToString();

			if(Itemset[i].getCategory()=="Men")
				Products[i].transform.SetParent (ParentMen.transform,false);
			else if (Itemset[i].getCategory()=="Women")
				Products[i].transform.SetParent (ParentWomen.transform,false);
			else
				Products[i].transform.SetParent (ParentKids.transform,false);
		}
	}

}
