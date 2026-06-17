// DynamicWindController runtime wind management system
// controls three wind systems simultaneously
// unity windzone: affects new tree assets and environment
// terrain grass: directly controls waving grass speed and strength  
// leaf particle systems: drifts falling leaves in wind direction
// all values update instantly at runtime via UI input fields.

using UnityEngine;

public class DynamicWindController : MonoBehaviour
{
    [Header("Wind Settings")]
    public float windSpeed     = 1f;
    public float windStrength  = 2f;
    public float windDirection = 0f;
    public float turbulence    = 0.3f;

    [Header("References")]
    public WindZone windZone;
    public ParticleSystem[] leafParticleSystems;

    void Start()
    {
        UpdateWindZone();
        UpdateLeafParticles();
    }

    void Update()
    {
        if (windZone != null)
            windZone.transform.rotation = Quaternion.Euler(0f, windDirection, 0f);
    }

    void UpdateWindZone()
    {
        if (windZone != null)
        {
            windZone.transform.rotation = Quaternion.Euler(0f, windDirection, 0f);

            windZone.windMain           = windStrength * 0.5f;
            windZone.windTurbulence     = turbulence;
            windZone.windPulseMagnitude = windStrength * 0.2f;
            windZone.windPulseFrequency = windSpeed    * 0.1f;
        }

        if (Terrain.activeTerrain != null)
        {
            TerrainData td         = Terrain.activeTerrain.terrainData;
            td.wavingGrassSpeed    = windSpeed;
            td.wavingGrassStrength = windStrength * 0.3f;
            td.wavingGrassAmount   = turbulence   * 0.5f;
        }
    }

    void UpdateLeafParticles()
    {
        if (leafParticleSystems == null) return;

        float dirRad = windDirection * Mathf.Deg2Rad;

        foreach (ParticleSystem ps in leafParticleSystems)
        {
            if (ps == null) continue;

            var velocity   = ps.velocityOverLifetime;
            velocity.enabled = true;
            velocity.space   = ParticleSystemSimulationSpace.World;

            velocity.x = new ParticleSystem.MinMaxCurve(
                Mathf.Cos(dirRad) * windStrength * 0.5f);
            velocity.z = new ParticleSystem.MinMaxCurve(
                Mathf.Sin(dirRad) * windStrength * 0.5f);
        }
    }

    public void SetWindSpeed(float value)
    {
        windSpeed = value;
        UpdateWindZone();
        UpdateLeafParticles();
    }

    public void SetWindStrength(float value)
    {
        windStrength = value;
        UpdateWindZone();
        UpdateLeafParticles();
    }

    public void SetWindDirection(float value)
    {
        windDirection = value;
        UpdateWindZone();
        UpdateLeafParticles();
    }

    public void SetTurbulence(float value)
    {
        turbulence = value;
        UpdateWindZone();
        UpdateLeafParticles();
    }
}