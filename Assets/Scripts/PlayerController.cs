using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction movements;
    [SerializeField] InputAction firing;

    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down based upon player input")]
    [SerializeField] float controlSpeed = 15f;
    [SerializeField] float xRange = 10f;
    [SerializeField] float posyRange = 9f;
    [SerializeField] float negyRange = -5f;

    [Header("Laser gun array")]
    [Tooltip("Add all player lasers here")]
    [SerializeField] GameObject[] lasers;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchMultiplier = -2f;
    [SerializeField] float positionYawMultiplier = 3;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchMultiplier = -10;
    [SerializeField] float controlRollMultiplier = -5;

    [Header("Lerp factors")]
    [SerializeField] float rotationLerpIncrement = 3f;          // Lerp variable
    [SerializeField] float rollRotation = -5f;                   // Lerp variable
    [SerializeField] float pitchRotation = -3f;                  // Lerp variable

    float xLerpThrow, yLerpThrow;                               //smooth-rotation
    float tForRollLerp = 0.5f;                                  // Lerp variable
    float tForPitchLerp = 0.5f;                                 // Lerp variable

    float Xthrow, Ythrow;

    SoundManager _soundManager;

    private void Awake()
    {
        _soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }

    void OnEnable()
    {
        movements.Enable();
        firing.Enable();
    }

    void OnDisable()
    {
        movements.Disable();
        firing.Disable();
    }

    void Update()
    {
        translate();
        rotaion();
        fire();
    }

    void rotaion()
    {
        xLerpThrow = Mathf.Lerp(-rollRotation, rollRotation, tForRollLerp);
        yLerpThrow = Mathf.Lerp(-pitchRotation, pitchRotation, tForPitchLerp);

        float pitchDueToPosition = transform.localPosition.y * positionPitchMultiplier;
        float pitchDueToControl = yLerpThrow * controlPitchMultiplier;

        float pitch = pitchDueToPosition + pitchDueToControl;
        float yaw = transform.localPosition.x * positionYawMultiplier;
        float roll = xLerpThrow * controlRollMultiplier;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);

        // Lerp smooth movement code begins here
        if (Xthrow > 0)
            tForRollLerp = Mathf.Max(0, tForRollLerp - rotationLerpIncrement * Time.deltaTime);
        else if (Xthrow < 0)
            tForRollLerp = Mathf.Min(1, tForRollLerp + rotationLerpIncrement * Time.deltaTime);
        else
            if (tForRollLerp > 0.5)
            tForRollLerp = Mathf.Max(0.5f, tForRollLerp - rotationLerpIncrement * Time.deltaTime);
        else if (tForRollLerp < 0.5)
            tForRollLerp = Mathf.Min(0.5f, tForRollLerp + rotationLerpIncrement * Time.deltaTime);

        if (Ythrow > 0)
            tForPitchLerp = Mathf.Max(0, tForPitchLerp - rotationLerpIncrement * Time.deltaTime);
        else if (Ythrow < 0)
            tForPitchLerp = Mathf.Min(1, tForPitchLerp + rotationLerpIncrement * Time.deltaTime);
        else
            if (tForPitchLerp > 0.5)
            tForPitchLerp = Mathf.Max(0.5f, tForPitchLerp - rotationLerpIncrement * Time.deltaTime);
        else if (tForPitchLerp < 0.5)
            tForPitchLerp = Mathf.Min(0.5f, tForPitchLerp + rotationLerpIncrement * Time.deltaTime);
    }

    void translate()
    {
        //saving movements as value(inputs)
        Xthrow = movements.ReadValue<Vector2>().x;
        Ythrow = movements.ReadValue<Vector2>().y;

        float Xoffset = Xthrow * controlSpeed * Time.deltaTime;
        float rawXpos = transform.localPosition.x + Xoffset;
        float clampedX = Mathf.Clamp(rawXpos, -xRange, xRange);

        float Yoffset = Ythrow * controlSpeed * Time.deltaTime;
        float rawYpos = transform.localPosition.y + Yoffset;
        float clampedY = Mathf.Clamp(rawYpos, negyRange, posyRange);


        transform.localPosition = new Vector3(clampedX, transform.localPosition.y, transform.localPosition.z);
        transform.localPosition = new Vector3(transform.localPosition.x, clampedY, transform.localPosition.z);
    }

    void fire()
    {
        if(firing.ReadValue<float>() > 0.1f)
        {
            _soundManager.PlaySFX1();
            activateLasers(true);
        }
        else
        {
            activateLasers(false);
        }
    }

    void activateLasers(bool isActive)
    {
        foreach(GameObject laser in lasers)
        {
            var laseremission = laser.GetComponent<ParticleSystem>().emission;
            laseremission.enabled = isActive;
        }
    }
}
