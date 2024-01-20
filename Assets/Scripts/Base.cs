using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Enemy e))
        {
            Destroy(e.gameObject);
            GameManager.Instance.DecreaseLive();
        }

    }
}
