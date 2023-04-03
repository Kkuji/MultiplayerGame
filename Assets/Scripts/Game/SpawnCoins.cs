using Photon.Pun;
using UnityEngine;
using System.Linq;

public class SpawnCoins : MonoBehaviour
{
    [SerializeField] private float _amount;
    [SerializeField] private GameObject _coin;
    [SerializeField] private Vector2 minPostion, maxPosition;

    public void Spawn()
    {
        Clear();
        if (PhotonNetwork.PlayerList.Length > 1 && PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < _amount; i++)
            {
                Vector2 randomPosition = new Vector2(Random.Range(minPostion.x, maxPosition.x), Random.Range(minPostion.y, maxPosition.y));
                PhotonNetwork.Instantiate(_coin.name, randomPosition, Quaternion.identity);
            }
        }
    }

    public void Clear()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Coin[] coins = FindObjectsOfType<MonoBehaviour>().OfType<Coin>().ToArray();

            if (coins.Length != 0)
            {
                for (int i = 0; i < coins.Length; i++)
                {
                    if (coins[i] != null)
                    {
                        Destroy(coins[i]);
                    }
                }
            }
        }
    }

    public float GetAmountCoins()
    {
        return _amount;
    }
}