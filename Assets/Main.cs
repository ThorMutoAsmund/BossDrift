using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public GameObject car;

    private float fadeTime = 0.8f;

    void Start()
    {
        StartCoroutine(StartCar());
    }

    private IEnumerator StartCar()
    {
        yield return new WaitForSeconds(1.0f);
        this.car.GetComponent<Car>().enabled = true;
    }

    void Update()
    {
        if (this.car.transform.position.y < -10)
        {
            Initiate.Fade("Game01", Color.black, this.fadeTime);
        }
    }
}
