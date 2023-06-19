using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public float zoom;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
		public bool inputForMove = true;
		public bool interact;
		public bool plus;
		public bool minus;
		
		public event Action OnPauseClicked;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnPause(InputValue value)
		{
			OnPauseClicked?.Invoke();
			SetCursorState(cursorInputForLook);
		}
		
		public void OnMove(InputValue value)
		{
			if (inputForMove)
			{
				MoveInput(value.Get<Vector2>());
			}
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnZoom(InputValue value)
		{
			ZoomInput(value.Get<float>());
		}
		
		public void OnInteract(InputValue value)
		{
			InteractInput(value.isPressed);
		}
		
		public void OnPlus(InputValue value)
		{
			PlusInput(value.isPressed);
		}

		public void OnMinus(InputValue value)
		{
			MinusInput(value.isPressed);
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
#endif
		private void MinusInput(bool valueIsPressed)
		{
			minus = valueIsPressed;
		}

		private void PlusInput(bool valueIsPressed)
		{
			plus = valueIsPressed;
		}
		
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

		public void ZoomInput(float newZoom)
		{
			zoom = newZoom;
		}

		public void InteractInput(bool newInteractState)
		{
			interact = newInteractState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		
		private void OnApplicationFocus(bool hasFocus)
		{
			return;
			
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}