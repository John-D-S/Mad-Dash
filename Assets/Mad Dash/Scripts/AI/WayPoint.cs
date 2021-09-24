using UnityEngine;

public class WayPoint : MonoBehaviour
{
    /// <summary>
    /// the position of this Waypoint
    /// </summary>
    public Vector3 Position => transform.position;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
