// this script manages the wind settings UI panel
// player types a value into each input field and presses enter to apply it
// changes affect trees and WindZone instantly

using UnityEngine;
using TMPro;

public class WindSettingsUI : MonoBehaviour
{
    [Header("Panel")]
    public GameObject windPanel;

    [Header("Input Fields")]
    public TMP_InputField speedInput;
    public TMP_InputField strengthInput;
    public TMP_InputField directionInput;
    public TMP_InputField turbulenceInput;

    [Header("Wind Controller")]
    public DynamicWindController windController;

    void Start()
    {
        windPanel.SetActive(false);

        speedInput.text     = windController.windSpeed.ToString("F1");
        strengthInput.text  = windController.windStrength.ToString("F1");
        directionInput.text = windController.windDirection.ToString("F0");
        turbulenceInput.text= windController.turbulence.ToString("F1");

        speedInput.onEndEdit.AddListener(OnSpeedChanged);
        strengthInput.onEndEdit.AddListener(OnStrengthChanged);
        directionInput.onEndEdit.AddListener(OnDirectionChanged);
        turbulenceInput.onEndEdit.AddListener(OnTurbulenceChanged);
    }

    public void TogglePanel()
    {
        windPanel.SetActive(!windPanel.activeSelf);
    }

    void OnSpeedChanged(string value)
    {
        if (float.TryParse(value, out float result))
        {
            result = Mathf.Clamp(result, 0.1f, 5f);
            windController.SetWindSpeed(result);
            speedInput.text = result.ToString("F1");
            Debug.Log("Wind Speed set to: " + result);
        }
        else
        {
            speedInput.text = windController.windSpeed.ToString("F1");
        }
    }

    void OnStrengthChanged(string value)
    {
        if (float.TryParse(value, out float result))
        {
            result = Mathf.Clamp(result, 0f, 10f);
            windController.SetWindStrength(result);
            strengthInput.text = result.ToString("F1");
            Debug.Log("Wind Strength set to: " + result);
        }
        else
        {
            strengthInput.text = windController.windStrength.ToString("F1");
        }
    }

    void OnDirectionChanged(string value)
    {
        if (float.TryParse(value, out float result))
        {
            result = Mathf.Clamp(result, 0f, 360f);
            windController.SetWindDirection(result);
            directionInput.text = result.ToString("F0");
            Debug.Log("Wind Direction set to: " + result + "°");
        }
        else
        {
            directionInput.text = windController.windDirection.ToString("F0");
        }
    }

    void OnTurbulenceChanged(string value)
    {
        if (float.TryParse(value, out float result))
        {
            result = Mathf.Clamp(result, 0f, 2f);
            windController.SetTurbulence(result);
            turbulenceInput.text = result.ToString("F1");
            Debug.Log("Turbulence set to: " + result);
        }
        else
        {
            turbulenceInput.text = windController.turbulence.ToString("F1");
        }
    }
}