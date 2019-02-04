using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickUpRotator : MonoBehaviour
{


    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);

    }


}
