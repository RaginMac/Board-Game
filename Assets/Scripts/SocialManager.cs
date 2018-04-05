using System.Collections;
using System.Collections.Generic;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.UI;

public class SocialManager : MonoBehaviour {

    public static bool IsConnectedtoGoogle = false;
    //public static SocialManager Instance = null;
    public Text debugtext;

	public GameObject[] allBoards;
	public GameObject loadSaveScreen;

	public InternetChecker _icScript;

	int syncCount = 0;

	Ping ping;

    private void Awake()
    {
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder ()
//	  //--------enables saving game progress--------------
	   .EnableSavedGames().Build();
//		
     	PlayGamesPlatform.InitializeInstance(config);

        //------------------ recommended for debugging:-------------------------------

        //PlayGamesPlatform.DebugLogEnabled = true;
      
      ////------------------Activate the Google Play Games platform--------------------------
        PlayGamesPlatform.Activate();
      
        //ConnectToGoogleService();
		//LoadAllData ();

		//StartCoroutine(CheckInternet());
		//CheckInternet();
		InvokeRepeating("CheckInternet", 0f, 2f);
    }


	void CheckInternet()
	{
//		ping = new Ping("8.8.8.8");
//		yield return new WaitForSeconds (0.5f);
//		if (ping.time == -1) {
//			Debug.Log ("Ping time is\t"+ping.time);
//			StartCoroutine (CheckInternet ());
//		} 
//		else 
//		{
//			Debug.Log ("Ping time is\t"+ping.time);
//			ConnectToGoogleService ();
//		}
		Debug.Log("Checking for internet from social manager");

		if (InternetChecker.internetConnectBool)
		{
			//StartCoroutine (CheckInternet ());

			ConnectToGoogleService();

		}

	}

    public void ConnectToGoogleService()
    {
		if (!IsConnectedtoGoogle) 
		{
			Social.localUser.Authenticate 
			(
				(bool success) => {
					IsConnectedtoGoogle = success;
					Debug.Log("Is Connected to google\t"+ IsConnectedtoGoogle);

					if(IsConnectedtoGoogle){
						SyncBoardData();
					}

			});
		} 
		CancelInvoke ("CheckInternet");
    }

	public void SyncBoardData()
	{
		if (!PlayerPrefs.HasKey ("JustInstalled")) 
		{
			Invoke ("LoadAllData", 1f);
		}
	}

    public void LoadAllData()
	{
		loadSaveScreen.SetActive(true);
		//Invoke ("LoadComplete", 6f);
		for (int i = 0; i < allBoards.Length; i++)
		{
			allBoards [i].GetComponent<GPG_CloudSaveSystem> ().StartLoadingData (this);	
		}
       


		print("Is playerpref created ? " + PlayerPrefs.HasKey("JustInstalled"));
    }

	public void GetLoadData()
	{
		print ("Receive load data");
		syncCount++;
		if (syncCount == allBoards.Length) 
		{
			LoadComplete ();
		}
	}

	void LoadComplete()
	{
		//this.gameObject.SetActive (false);

		loadSaveScreen.SetActive(false);
	}

 }
