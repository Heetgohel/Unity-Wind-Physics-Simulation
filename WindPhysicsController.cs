// WindPhysicsController applies continuous wind force to
// lightweight Rigidbody objects in the scene.
// force direction and strength update automatically when
// DynamicWindController values change via the UI.

using UnityEngine;

public class WindPhysicsController : MonoBehaviour
{
    [Header("Reference")]
    public DynamicWindController windController;

    [Header("Physics Objects")]
    public Rigidbody[] windObjects;

    [Header("Settings")]
    public float forceMultiplier = 0.3f;

    void FixedUpdate()
    {
        if (windController == null) return;
        if (windObjects == null) return;

        float dirRad = windController.windDirection * Mathf.Deg2Rad;

        Vector3 windForce = new Vector3(
            Mathf.Cos(dirRad) * windController.windStrength * forceMultiplier,0f,
            Mathf.Sin(dirRad) * windController.windStrength * forceMultiplier
        );

        foreach (Rigidbody rb in windObjects)
        {
            if (rb == null) continue;
            rb.AddForce(windForce, ForceMode.Force);
        }
    }
}
