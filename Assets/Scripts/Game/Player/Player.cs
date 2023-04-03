using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PhotonView))]
public class Player : MonoBehaviour
{
    private float _minClamp = 0;
    private float _maxClamp = 1f;
    private PhotonView _view;

    [HideInInspector] public float coins = 0;
    [HideInInspector] public float allCoins;
    [HideInInspector] public float currentHealth;
    [HideInInspector] public Image healthBar;
    [HideInInspector] public Image coinBar;
    [HideInInspector] public GameObject winPanel;
    [HideInInspector] public Button leaveButton;

    public float maxHealth;

    private void Start()
    {
        _view = GetComponent<PhotonView>();
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            Destroy(coin.gameObject);
            coins++;

            if (_view.IsMine)
            {
                coinBar.fillAmount = Mathf.Clamp(coins / allCoins, _minClamp, _maxClamp);
            }
        }
        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            Destroy(collision.gameObject);
        }
    }

    [PunRPC]
    public void Win()
    {
        if (_view.IsMine)
        {
            winPanel.SetActive(true);
            winPanel.GetComponentInChildren<TextMeshProUGUI>().SetText
                ("Победил - " + GetComponent<PhotonView>().Owner.NickName + "\nКоличество собранных монет - " + GetComponent<Player>().coins);
        }
    }

    public void GetDamage(float damage)
    {
        if (_view.IsMine)
        {
            currentHealth -= damage;
            healthBar.fillAmount = Mathf.Clamp(currentHealth / maxHealth, _minClamp, _maxClamp);
            CheckIsDead();
        }
    }

    public void CheckIsDead()
    {
        if (currentHealth <= 0)
        {
            if (PhotonNetwork.PlayerList.Length == 2)
            {
                Player winPlayer = FindObjectsOfType<Player>().First(Player => Player.currentHealth > 0);

                PhotonView photonView = PhotonView.Get(winPlayer);
                photonView.RPC("Win", RpcTarget.All);
            }

            Leave();
        }
    }

    public void SetLeaveButton(Button button)
    {
        leaveButton = button;
        leaveButton.onClick.AddListener(delegate () { Leave(); });
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Lobby");
    }
}
