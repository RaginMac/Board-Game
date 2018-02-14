using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Netmanager : NetworkBehaviour 
{
    public GameObject serverPrefab;
    public GameObject servantPrefab;
	// Use this for initialization
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
	void Start () 
	{
        DontDestroyOnLoad(this.gameObject);
		
		if (hasAuthority) 
		{
			GameObject.Find ("LobbyManager").GetComponent<LobbyManager> ().ownManager = this;
		}	
	}

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Checking name" + scene);
        if (scene.name == "LudoLevel")
        {
            if (isServer && hasAuthority)
            {
                GameObject obj = (GameObject) Instantiate(serverPrefab);
                NetworkServer.Spawn(obj);
            }
            GetComponent<GameNetworkServent>().SetColor();
        }
    }
	
	// Update is called once per frame
	void Update () 
	{
		
	}

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

	[Command]
	public void CmdSetData_1(string Name)
	{
		GameObject.Find ("LobbyManager").GetComponent<LobbyManager> ().playerOneField = Name;
	}
	[Command]
	public void CmdSetData_2(string Name)
	{
		GameObject.Find ("LobbyManager").GetComponent<LobbyManager> ().playerTwoField = Name;
	}
	[Command]
	public void CmdSetData_3(string Name)
	{
		GameObject.Find ("LobbyManager").GetComponent<LobbyManager> ().playerThreeField = Name;
	}
	[Command]
	public void CmdSetData_4(string Name)
	{
		GameObject.Find ("LobbyManager").GetComponent<LobbyManager> ().playerFourField = Name;
	}

}
