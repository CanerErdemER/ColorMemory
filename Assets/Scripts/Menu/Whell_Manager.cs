using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whell_Manager : MonoBehaviour
{
    [SerializeField]
    int speed;
    

    
    void Update()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }
}
