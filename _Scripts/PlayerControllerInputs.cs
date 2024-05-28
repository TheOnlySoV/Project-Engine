using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class PlayerControllerInputs : MonoBehaviour
{
    public ViewState viewState;
    public PlayerState playerInputState = PlayerState.Playing;

    PlayerInput playerInputLink;

    public void SetPlayerState(int index)
    {
        playerInputState = (PlayerState)index;
        InputCheck();
    }
    public void SetPlayerState(PlayerState index)
    {
        playerInputState = index;
        InputCheck();
    }

    public bool freezeOnPause = false;

    [Header("Character Input Values")]
    #region
    public Vector2 move;
	public Vector2 look;
	public bool jump;
    public bool crouch;
    public bool sprint;
    public bool dodge;
    public bool interact;
    public bool viewSwitch;
    #endregion

    [Header("Movement Settings")]
	public bool analogMovement;

	[Header("Mouse Cursor Settings")]
	public bool cursorLocked = true;
	public bool cursorInputForLook = true;

    public bool InputCheck()
    {
        bool returnBoolean = true;

        switch (playerInputState)
        {
            case PlayerState.Playing:
                Cursor.lockState = CursorLockMode.Locked;
                returnBoolean = true;
                break;

            case PlayerState.Paused:
                if (playerInputLink.currentControlScheme == "Gamepad")
                    Cursor.lockState = CursorLockMode.Locked;
                else
                    Cursor.lockState = CursorLockMode.None;
                returnBoolean = false;
                break;

            case PlayerState.InConversation:
                if (playerInputLink.currentControlScheme == "Gamepad")
                    Cursor.lockState = CursorLockMode.Locked;
                else
                    Cursor.lockState = CursorLockMode.None;
                returnBoolean = false;
                break;

            case PlayerState.Other:
                if (playerInputLink.currentControlScheme == "Gamepad")
                    Cursor.lockState = CursorLockMode.Locked;
                else
                    Cursor.lockState = CursorLockMode.None;
                returnBoolean = false;
                break;
        }

        return returnBoolean;
    }

    bool PauseInputCheck()
    {
        bool returnBoolean = true;

        switch (playerInputState)
        {
            case PlayerState.Playing:
                Cursor.lockState = CursorLockMode.Locked;
                returnBoolean = true;
                break;

            case PlayerState.Paused:
                if (playerInputLink.currentControlScheme == "Gamepad")
                    Cursor.lockState = CursorLockMode.Locked;
                else
                    Cursor.lockState = CursorLockMode.None;
                returnBoolean = true;
                break;

            case PlayerState.InConversation:
                if (playerInputLink.currentControlScheme == "Gamepad")
                    Cursor.lockState = CursorLockMode.Locked;
                else
                    Cursor.lockState = CursorLockMode.None;
                returnBoolean = false;
                break;

            case PlayerState.Other:
                if (playerInputLink.currentControlScheme == "Gamepad")
                    Cursor.lockState = CursorLockMode.Locked;
                else
                    Cursor.lockState = CursorLockMode.None;
                returnBoolean = false;
                break;
        }

        return returnBoolean;
    }

    #region Inputs
#if ENABLE_INPUT_SYSTEM

    private void Awake()
    {
        playerInputLink = GetComponent<PlayerInput>();
    }
    public void OnMove(InputValue value)
    {
        if (!InputCheck())
            return;
        MoveInput(value.Get<Vector2>());
	}

	public void OnLook(InputValue value)
    {
        if (!InputCheck())
            return;
        if (cursorInputForLook)
		{
			LookInput(value.Get<Vector2>());
		}
    }

    public void OnJump(InputValue value)
    {
        if (!InputCheck())
            return;
        JumpInput(value.isPressed);
    }

    public void OnDodge(InputValue value)
    {
        if (!InputCheck())
            return;
        DodgeInput(value.isPressed);
    }

    public void OnInteract(InputValue value)
    {
        if (!InputCheck())
            return;
        InteractInput(value.isPressed);
    }

    public void OnViewSwitch(InputValue value)
    {
        if (!InputCheck())
            return;
        ViewSwitchInput(value.isPressed);
    }

    public void OnSprint(InputValue value)
    {
        if (!InputCheck())
            return;
        SprintInput(value.isPressed);
    }

    public void OnCrouch(InputValue value)
    {
        if (!InputCheck())
            return;
        CrouchInput(value.isPressed);
    }
#endif
    #endregion

    #region Responses
    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    public void DodgeInput(bool newDodgeState)
    {
        dodge = newDodgeState;
    }

    public void InteractInput(bool newInteractState)
    {
        interact = newInteractState;
    }

    public void ViewSwitchInput(bool newViewSwitchState)
    {
        viewSwitch = newViewSwitchState;
    }

    public void SprintInput(bool newSprintState)
    {
        sprint = newSprintState;
    }

    public void CrouchInput(bool newCrouchState)
    {
        crouch = newCrouchState;
    }
    #endregion

    private void OnApplicationFocus(bool hasFocus)
	{
		SetCursorState(cursorLocked);
	}

	private void SetCursorState(bool newState)
	{
		Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}

public enum ViewState { FirstPersonView, ThirdPersonView }