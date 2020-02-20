using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public GameObject wheelFL;
    public GameObject wheelFR;
    public GameObject wheelRL;
    public GameObject wheelRR;

    private static float turnTime = 0.4f;
    private static float driftTurnTime = 0.8f;
    private static float changeSpeedTime = 1f;
    private static float wheelRotationSpeed = 600f;
    private Coroutine turnCoroutine;
    private bool turning = false;
    private Vector3 wheelFLRotation;
    private Vector3 wheelFRRotation;
    private Vector3 wheelRLRotation;
    private Vector3 wheelRRRotation;
    private new Rigidbody rigidbody;
    private float maxMovementSpeed = 3.5f;
    private float movementSpeed = 0f;
    private GameObject drifter;

    void Start()
    {
        this.wheelFLRotation = this.wheelFL.transform.localRotation.eulerAngles;
        this.wheelFRRotation = this.wheelFR.transform.localRotation.eulerAngles;
        this.wheelRLRotation = this.wheelRL.transform.localRotation.eulerAngles;
        this.wheelRRRotation = this.wheelRR.transform.localRotation.eulerAngles;

        this.rigidbody = this.GetComponent<Rigidbody>();
        this.rigidbody.isKinematic = false;

        this.drifter = new GameObject();
        this.drifter.transform.rotation = this.transform.rotation;

        StartCoroutine(ChangeSpeed(this.maxMovementSpeed));
    }

    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.transform.position = new Vector3(0, 0, 10);
            this.transform
        }















        transform.position += this.drifter.transform.forward * Time.deltaTime * movementSpeed;

        this.wheelFLRotation.z += Time.deltaTime * wheelRotationSpeed * movementSpeed;
        this.wheelFL.transform.localRotation = Quaternion.Euler(this.wheelFLRotation);
        this.wheelFRRotation.z -= Time.deltaTime * wheelRotationSpeed * movementSpeed;
        this.wheelFR.transform.localRotation = Quaternion.Euler(this.wheelFRRotation);
        this.wheelRLRotation.z += Time.deltaTime * wheelRotationSpeed * movementSpeed;
        this.wheelRL.transform.localRotation = Quaternion.Euler(this.wheelRLRotation);
        this.wheelRRRotation.z -= Time.deltaTime * wheelRotationSpeed * movementSpeed;
        this.wheelRR.transform.localRotation = Quaternion.Euler(this.wheelRRRotation);


        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.RightArrow)) && !this.turning)
        {
            this.turning = true;
            if (this.turnCoroutine != null)
            {
                StopCoroutine(this.turnCoroutine);
            }
            this.turnCoroutine = StartCoroutine(Turn(90));
        }
        else if (!(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.RightArrow)) && this.turning)
        {
            this.turning = false;
            if (this.turnCoroutine != null)
            {
                StopCoroutine(this.turnCoroutine);
            }
            this.turnCoroutine = StartCoroutine(Turn(0));
        }
    }

    private IEnumerator ChangeSpeed(float desiredSpeed)
    {
        float time = 0f;
        float startSpeed = this.movementSpeed;
        while (time < changeSpeedTime)
        {
            this.movementSpeed = Mathf.Lerp(startSpeed, desiredSpeed, time / changeSpeedTime);
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
        }
    }

    private IEnumerator Turn(float endAngle)
    {
        float time = 0f;
        var carRotation = this.transform.rotation.eulerAngles;
        var drifterRotation = this.drifter.transform.rotation.eulerAngles;

        float startAngle = carRotation.y;
        float drifterStartAngle = drifterRotation.y;
        if (startAngle > 180f)
        {
            startAngle = 0f;
        }
        while (time < driftTurnTime)
        {
            carRotation.y = Mathf.Lerp(startAngle, endAngle, time / turnTime);
            drifterRotation.y = Mathf.Lerp(drifterStartAngle, endAngle, time / driftTurnTime);

            this.transform.rotation = Quaternion.Euler(carRotation);
            this.drifter.transform.rotation = Quaternion.Euler(drifterRotation);
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
        }
        this.turnCoroutine = null;
    }
}
