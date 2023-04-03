using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private int _amount;

    private List<GameObject> _pool = new();

    protected void Initialize(GameObject prefab)
    {
        for (int i = 0; i < _amount; i++)
        {
            GameObject spawned = PhotonNetwork.Instantiate(prefab.name, transform.position, Quaternion.identity);
            spawned.SetActive(false);

            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(gameObject => gameObject.activeSelf == false);

        return result != null;
    }
}