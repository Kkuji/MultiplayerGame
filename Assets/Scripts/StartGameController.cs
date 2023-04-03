using UnityEngine;
using Photon.Pun;
using TMPro;

[RequireComponent(typeof(SpawnCoins))]
public class StartGameController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _waitingText;

    private bool _gameStarted = false;
    private SpawnCoins _spawnCoins;

    private void Start()
    {
        _spawnCoins = GetComponent<SpawnCoins>();
    }

    private void Update()
    {
        if (PhotonNetwork.PlayerList.Length > 1 && !_gameStarted)
        {
            _waitingText.gameObject.SetActive(false);
            _gameStarted = true;
            _spawnCoins.Spawn();
        }

        if (PhotonNetwork.PlayerList.Length < 2 && _gameStarted)
        {
            _waitingText.gameObject.SetActive(true);
            _gameStarted = false;
            _spawnCoins.Clear();
        }
    }
}