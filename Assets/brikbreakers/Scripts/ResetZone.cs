using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ResetZone : MonoBehaviour
{

    public static ResetZone instance;
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.instance.OnBallMiss();
    }

}
