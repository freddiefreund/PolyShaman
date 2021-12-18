using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        ShootBullet();
    }

    void ShootBullet()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
