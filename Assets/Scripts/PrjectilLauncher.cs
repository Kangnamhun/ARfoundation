using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrjectilLauncher : MonoBehaviour
{
    [SerializeField]
    Rigidbody m_ProjectilePrefab;
    [SerializeField]
    float m_InitalSpeed = 25;

    private void Update()
    {
        if(m_ProjectilePrefab == null)
        {
            return;
        }
        if(Input.touchCount == 0)
        {
            return;
        }

        var touch = Input.touches[0];
        if(touch.phase == TouchPhase.Began)
        {
            var ray = GetComponent<Camera>().ScreenPointToRay(touch.position);
            var prjectile = Instantiate(m_ProjectilePrefab, ray.origin, Quaternion.identity);
            var rigibody = m_ProjectilePrefab.GetComponent<Rigidbody>();
            rigibody.velocity = ray.direction * m_InitalSpeed;
        }
    }
}
