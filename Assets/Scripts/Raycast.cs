using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Raycast : MonoBehaviour
{
    public Text _uiText;
    private InputAction _clickInput;
    private InputAction _positionInput;
    private Vector2 _mousePosition;

    private float _initialSeconds = 5f;
    private float _remainingSeconds;
    private bool _timerFlag;
    private string _escenaPendiente;


    void Awake()
    {
        _clickInput = InputSystem.actions["Attack"];
        _positionInput = InputSystem.actions["MousePosition"];
    }

    void Update()
    {
        if(_timerFlag)
        {
            _remainingSeconds -= Time.deltaTime;
            _uiText.text = Mathf.CeilToInt(_remainingSeconds).ToString();

            if(_remainingSeconds <= 0f)
            {
                _timerFlag = false;
                SceneManager.LoadScene(_escenaPendiente);
            }
        }

        if(_clickInput.WasPressedThisFrame())
        {
            ShotRayCast();
        }
    }

    void ShotRayCast()
    {
        _mousePosition = _positionInput.ReadValue<Vector2>();
        Ray ray = Camera.main.ScreenPointToRay(_mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(hit.collider.name == "Cube1")
            {
                Debug.Log("Impacto contra Cube1");
                _escenaPendiente = "Scene1";
                ReiniciarTimer();
            }
        }

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(hit.collider.name == "Sphere")
            {
                Debug.Log("Impacto contra Sphere");
                _escenaPendiente = "Scene2";
                ReiniciarTimer();
            }
        }

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(hit.collider.name == "Cube2")
            {
                Debug.Log("Impacto contra Cube2");
                _escenaPendiente = "Scene3";
                ReiniciarTimer();
            }
        } 
    }

    void ReiniciarTimer()
    {
        _remainingSeconds = _initialSeconds;
        _timerFlag = true;
    }
}
