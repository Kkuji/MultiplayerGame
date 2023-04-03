using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Collections.Generic;

public class MenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField _createServer;
    [SerializeField] private TMP_InputField _joinServer;
    [SerializeField] private TMP_InputField _nickname;
    [SerializeField] private TextMeshProUGUI _errorText;
    [SerializeField] private byte _maximumPlayers;

    private List<RoomInfo> _roomList = new();

    private bool CheckCorrectName(string name)
    {
        int spaceAmount = 0;

        for (int i = 0; i < name.Length; i++)
        {
            if (name[i] == ' ')
            {
                spaceAmount++;
            }
        }

        if (spaceAmount == name.Length || name.Length == 0)
        {
            _errorText.SetText("Ќазвание комнаты не может содержать только пробелы или быть пустым");
        }

        return (!(spaceAmount == name.Length || name.Length == 0));
    }

    private bool CheckServerExist(string currentRoom)
    {
        bool isAvaliable = true;

        foreach (RoomInfo room in _roomList)
        {
            if (room.Name == currentRoom)
            {
                isAvaliable = false;
            }
        }

        return !isAvaliable;
    }

    private void SetNickname()
    {
        PhotonNetwork.NickName = _nickname.text;
        PlayerPrefs.SetString("name", _nickname.text);
    }

    private void CreateRoom()
    {
        RoomOptions roomOptions = new();
        roomOptions.MaxPlayers = _maximumPlayers;
        PhotonNetwork.CreateRoom(_createServer.text, roomOptions);
    }

    public void TryCreateRoom()
    {
        if (CheckCorrectName(_createServer.text) && !CheckServerExist(_createServer.text) && CheckCorrectName(_nickname.text))
        {
            SetNickname();

            CreateRoom();
        }
        else if (!CheckCorrectName(_nickname.text))
        {
            _errorText.SetText("ѕсевдоним не может содержать только пробелы или быть пустым");
        }
        else if (CheckServerExist(_createServer.text))
        {
            _errorText.SetText("Ќазвание комнаты зан€то");
        }
    }

    public void TryJoinRoom()
    {
        if (CheckServerExist(_joinServer.text))
        {
            SetNickname();

            PhotonNetwork.JoinRoom(_joinServer.text);
        }
        else if (!CheckCorrectName(_nickname.text))
        {
            _errorText.SetText("ѕсевдоним не может содержать только пробелы или быть пустым");
        }
        else if (!CheckServerExist(_joinServer.text))
        {
            _errorText.SetText(" омнаты с таким названием нет");
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        _roomList = roomList;
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}