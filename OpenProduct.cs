using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class OpenProduct : MonoBehaviour {

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

	public GameObject marker;

	public void Start(){
		if (SceneManager.GetActiveScene ().name == "ProductScene") {
			StartCoroutine (SetScene ());
		}
		if (SceneManager.GetActiveScene ().name == "VuforiaImplementation" || SceneManager.GetActiveScene ().name == "KudanMarkerImplementation" || SceneManager.GetActiveScene ().name == "KudanMarkerlessImplementation") {
			StartCoroutine (SetARmodel ());
		}
	}

	public void NextScene(string SceneName)
	{ 
		int CurrentID = int.Parse(transform.GetChild (3).GetComponent<Text>().text);
		PlayerPrefs.SetInt ("ProductID", CurrentID);
		SceneManager.LoadScene (SceneName);
	}

	public IEnumerator SetScene(){
		
		int CurrentID = PlayerPrefs.GetInt ("ProductID") - 1;

		WWW itemsData = new WWW ("https://augmentshop.000webhostapp.com/Itemset.php");

		yield return itemsData;
		string itemDataString = itemsData.text;

		int productcount = itemDataString.Split (';').Length-1;	

		//splitting info into array
		string[] itemDataArray = new string[productcount];
		itemDataArray = itemDataString.Split (';');

		//splitting info into Items Objects
		Items item= new Items();
		string s=itemDataArray [CurrentID];
		item.create (s);

		transform.GetChild (0).GetComponent<Text>().text = item.getProductName();
		transform.GetChild (1).GetComponent<Text>().text = "Rs." + item.getPrice().ToString();
		transform.GetChild (2).GetComponent<Text>().text = item.getInfo();
		transform.GetChild (3).GetComponent<Text>().text = item.getBelt();
		transform.GetChild (4).GetComponent<Text>().text = item.getWaterproof();
		transform.GetChild (11).GetComponent<Text>().text = item.getProductID().ToString();
		RawImage[] ProductImages = new RawImage[2];
		ProductImages = transform.GetChild(7).GetComponentsInChildren<RawImage>();
		int input=0;
		foreach (RawImage i in ProductImages) {
			i.texture = (Texture2D)Resources.Load (item.getProductName ()+"Img"+input);
			input++;
		}
		transform.GetChild(7).GetComponentInChildren<RawImage>().texture = (Texture2D)Resources.Load (item.getProductName ()+"Img2");

		//Fav Color
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

		//Changing Color
		foreach (int i in FavProds) {
			if(i==CurrentID)
				transform.GetChild (8).GetComponent<Image> ().color = Color.red;
		}



		//Get WishData String for Logged in User
		string WishData = UserFavData.Substring (UserFavData.IndexOf ("Wishlist:") + 9);
		WishData = WishData.Remove (WishData.IndexOf("|"));

		int check2 = WishData.Split ('-').Length - 1;
		string[] WishProdsArray = new string[check2];

		WishProdsArray = WishData.Split ('-');

		int[] WishProds = new int [WishProdsArray.Length-1];
		int test2;

		for (int i = 0; i < WishProdsArray.Length-1; i++) {
			if (int.TryParse (WishProdsArray [i], out test2))
				WishProds [i] = int.Parse(WishProdsArray [i])-1;
		}

		int test3 = 1;
		foreach (int i in WishProds) {
			if (i == CurrentID)
				test3 = 0;		
		}
		if(test3==1)
			transform.GetChild (10).gameObject.SetActive (true);



		//Get CompData String for Logged in User
		string CompData = UserFavData.Substring (UserFavData.IndexOf ("Complist:") + 9);
		CompData = CompData.Remove (CompData.IndexOf("|"));

		int check4 = CompData.Split ('-').Length - 1;
		string[] CompProdsArray = new string[check4];

		CompProdsArray = CompData.Split ('-');

		int[] CompProds = new int [CompProdsArray.Length-1];
		int test4;

		for (int i = 0; i < CompProdsArray.Length-1; i++) {
			if (int.TryParse (CompProdsArray [i], out test4))
				CompProds [i] = int.Parse(CompProdsArray [i])-1;
		}
		test3 = 1;
		foreach (int i in CompProds) {
			if (i == CurrentID)
				test3 = 0;		
		}
		if(test3==1)
			transform.GetChild (12).gameObject.SetActive (true);




		//Get BuyData String for Logged in User
		string BuyData = UserFavData.Substring (UserFavData.IndexOf ("Buylist:") + 8);

		int check7 = BuyData.Split ('-').Length - 1;
		string[] BuyProdsArray = new string[check7];

		BuyProdsArray = BuyData.Split ('-');

		int[] BuyProds = new int [BuyProdsArray.Length-1];
		int test7;

		for (int i = 0; i < BuyProdsArray.Length-1; i++) {
			if (int.TryParse (BuyProdsArray [i], out test7))
				BuyProds [i] = int.Parse(BuyProdsArray [i])-1;
		}
		test3 = 1;
		foreach (int i in BuyProds) {
			if (i == CurrentID)
				test3 = 0;		
		}
		if(test3==1)
			transform.GetChild (9).gameObject.SetActive (true);
		
	}

	public IEnumerator SetARmodel(){

		int CurrentID = PlayerPrefs.GetInt ("ProductID")-1;

		WWW itemsData = new WWW ("https://augmentshop.000webhostapp.com/Itemset.php");

		yield return itemsData;
		string itemDataString = itemsData.text;

		int productcount = itemDataString.Split (';').Length-1;	

		//splitting info into array
		string[] itemDataArray = new string[productcount];
		itemDataArray = itemDataString.Split (';');

		//splitting info into Items Objects
		Items item= new Items();
		string s=itemDataArray [CurrentID];
		item.create (s);

		GameObject armodel = (GameObject)Instantiate(Resources.Load(item.getProductName()));
		armodel.transform.SetParent (marker.transform, false);

		print("in");

		if (SceneManager.GetActiveScene ().name == "KudanMarkerImplementation" || SceneManager.GetActiveScene ().name == "KudanMarkerlessImplementation") {
			print ("loop");
			armodel.transform.localScale += new Vector3 (30, 30, 30);
			armodel.transform.Rotate(new Vector3 (0,0,0));
		}
	}
	

}
