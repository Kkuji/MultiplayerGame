using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _distanceMultiplier;

    [HideInInspector] public Button shootButton;

    public void SetShootButton(Button button)
    {
        shootButton = button;
        shootButton.onClick.AddListener(delegate () { Shoot(); });
    }

    public void Shoot()
    {
        if (PhotonNetwork.PlayerList.Length > 1)
        {
            PhotonNetwork.Instantiate(_bullet.name, transform.position + transform.up * _distanceMultiplier, transform.rotation);
        }
    }
}