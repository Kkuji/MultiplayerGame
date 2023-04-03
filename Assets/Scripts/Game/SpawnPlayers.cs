using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpawnCoins))]
public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private Image _coinBar;
    [SerializeField] private Image _healthBar;
    [SerializeField] private GameObject _player;
    [SerializeField] private Button _shootButton;
    [SerializeField] private Button _leaveButton;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Vector2 minPostion, maxPosition;
    [SerializeField] private GameObject _winPanel;

    private SpawnCoins _spawnCoins;
    private GameObject currentPlayer;

    private void Start()
    {
        _spawnCoins = GetComponent<SpawnCoins>();

        Vector2 randomPosition = new Vector2(Random.Range(minPostion.x, maxPosition.x), Random.Range(minPostion.y, maxPosition.y));
        currentPlayer = PhotonNetwork.Instantiate(_player.name, randomPosition, Quaternion.identity);

        SetPlayerValues(currentPlayer);
    }

    private void SetPlayerValues(GameObject player)
    {
        player.GetComponent<PlayerShooter>().SetShootButton(_shootButton);
        player.GetComponent<PlayerMover>().joystick = _joystick;
        player.GetComponent<Player>().allCoins = _spawnCoins.GetAmountCoins();
        player.GetComponent<Player>().SetLeaveButton(_leaveButton);
        player.GetComponent<Player>().healthBar = _healthBar;
        player.GetComponent<Player>().winPanel = _winPanel;
        player.GetComponent<Player>().coinBar = _coinBar;
    }
}