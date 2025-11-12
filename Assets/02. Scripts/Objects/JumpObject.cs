using UnityEngine;

public class JumpObject : DetectableObject
{
    [SerializeField] [Range(0,5)] int _weight = 3;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform
           .GetComponent<Player>() is Player player)
        {
            player.ChangeAirborneState(null);
            player.SetJumpWeight(_weight);
            player.TryJump();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform
            .GetComponent<Player>() is Player player)
        {
            player.SetJumpWeight(default);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.parent
            .GetComponent<Player>() is Player player)
        {
            player.ChangeAirborneState(null);
            player.SetJumpWeight(_weight);
            player.TryJump();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent
            .GetComponent<Player>() is Player player)
        {
            player.SetJumpWeight(default);
        }
    }
}
