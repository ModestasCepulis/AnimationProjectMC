using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;

    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_InputField playerNameInputField;
    [SerializeField] TMP_Text ErrorText;
    [SerializeField] TMP_Text RoomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject roomListPrefab;
    [SerializeField] GameObject playerListPrefab;
    [SerializeField] GameObject startGameButton;
    [SerializeField] GameObject TitleButtons;
    [SerializeField] TMP_Text InputedPlayerName;
    [SerializeField] string playerName;

    PhotonView PV;
    public string playerNameAsString;

    private void Awake()
    {
        Instance = this;
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        MenuManager.Instance.OpenMenu("loading");
        Debug.Log("Connecting to Master");
        //PhotonNetwork.SendRate = 20;
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Update()
    {
        if (string.IsNullOrEmpty(playerNameInputField.text))
        {
            TitleButtons.SetActive(false);
        }
        else
        {
            TitleButtons.SetActive(true);
        }

/*        if(PV.IsMine)
        {*/
            //playerNameAsString = InputedPlayerName.text.ToString();
       // }

    }

    public string getPlayerName()
    {
        return playerNameAsString;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");

        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;




    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("title");
        Debug.Log("Joined lobby");

    }

    public void CreateRoom()
    {
        PhotonNetwork.NickName = playerNameInputField.text;
        //DontDestroyOnLoad(gameObject);
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.Instance.OpenMenu("loading");

    }

    public override void OnJoinedRoom()
    {

        MenuManager.Instance.OpenMenu("room");
        RoomNameText.text = PhotonNetwork.CurrentRoom.Name;
        PhotonNetwork.NickName = playerNameInputField.text;


        Player[] players = PhotonNetwork.PlayerList;

        foreach(Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Count(); i++)
        {
            Instantiate(playerListPrefab, playerListContent).GetComponent<playerListItem>().SetUp(players[i]);
        }

        //PhotonNetwork.NickName = playerNameInputField.text;
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        ErrorText.text = "Room creation failed: " + message;
        MenuManager.Instance.OpenMenu("error");

    }
    
    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("loading");


    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("title");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
        for(int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListPrefab, roomListContent).GetComponent<roomListItem>().SetUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListPrefab, playerListContent).GetComponent<playerListItem>().SetUp(newPlayer);
    }
}
