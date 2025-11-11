using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseInput : MonoBehaviour
{
    public abstract void OnMove(InputAction.CallbackContext context);
    public abstract void OnAttack(InputAction.CallbackContext context);
}