using A14Farming.UI.HUD;
using UnityInput = UnityEngine.Input;
using State = A14Farming.UI.HUD.GameUI.State;
using UnityEngine;
using Core.Input;

namespace A14Farming.Input
{
    [RequireComponent(typeof(GameUI))]
	public class TowerDefenseTouchInput : TouchInput
	{
		/// <summary>
		/// A percentage of the screen where panning occurs while dragging
		/// </summary>
		[Range(0, 0.5f)]
		public float panAreaScreenPercentage = 0.2f;

		/// <summary>
		/// The attached Game UI object
		/// </summary>
		GameUI m_GameUI;

		/// <summary>
		/// Keeps track of whether or not the ghost tower is selected
		/// </summary>
		bool m_IsGhostSelected;

		/// <summary>
		/// The pointer at the edge of the screen
		/// </summary>
		TouchInfo m_DragPointer;

		/// <summary>
		/// Register input events
		/// </summary>
		protected override void OnEnable()
		{
			base.OnEnable();

			m_GameUI = GetComponent<GameUI>();

			m_GameUI.stateChanged += OnStateChanged;
			m_GameUI.ghostBecameValid += OnGhostBecameValid;

			// Register tap event
			if (InputController.instanceExists)
			{
				InputController.instance.tapped += OnTap;
				InputController.instance.startedDrag += OnStartDrag;
			}

		}

		/// <summary>
		/// Deregister input events
		/// </summary>
		protected override void OnDisable()
		{
			base.OnDisable();

			if (InputController.instanceExists)
			{
				InputController.instance.tapped -= OnTap;
				InputController.instance.startedDrag -= OnStartDrag;
			}
			if (m_GameUI != null)
			{
				m_GameUI.stateChanged -= OnStateChanged;
				m_GameUI.ghostBecameValid -= OnGhostBecameValid;
			}
		}

		/// <summary>
		/// Hide UI 
		/// </summary>
		protected virtual void Awake()
		{

		}

		/// <summary>
		/// Decay flick
		/// </summary>
		protected override void Update()
		{
			base.Update();

			// Edge pan
			if (m_DragPointer != null)
			{
				EdgePan();
			}

			if (UnityInput.GetKeyDown(KeyCode.Escape))
			{
				switch (m_GameUI.state)
				{
					case State.Normal:
						if (m_GameUI.isTowerSelected)
						{
							m_GameUI.DeselectTower();
						}
						else
						{
							m_GameUI.Pause();
						}
						break;
					case State.Building:
						m_GameUI.CancelGhostPlacement();
						break;
				}
			}
		}

		/// <summary>
		/// Called on input press
		/// </summary>
		protected override void OnPress(PointerActionInfo pointer)
		{
			base.OnPress(pointer);
			var touchInfo = pointer as TouchInfo;
			// Press starts on a ghost? Then we can pick it up
			if (touchInfo != null)
			{
				if (m_GameUI.state == State.Building)
				{
					m_IsGhostSelected = m_GameUI.IsPointerOverGhost(pointer);
					if (m_IsGhostSelected)
					{
						m_DragPointer = touchInfo;
					}
				}
			}
		}

		/// <summary>
		/// Called on input release, for flicks
		/// </summary>
		protected override void OnRelease(PointerActionInfo pointer)
		{
			// Override normal behaviour. We only want to do flicks if there's no ghost selected
			// For this reason, we intentionally do not call base
			var touchInfo = pointer as TouchInfo;

			if (touchInfo != null)
			{
				// Show UI on release
				if (m_GameUI.isBuilding)
				{
					Vector2 screenPoint = cameraRig.cachedCamera.WorldToScreenPoint(m_GameUI.GetGhostPosition());
					if (m_GameUI.IsGhostAtValidPosition())
					{

					}
					else
					{

					}
					if (m_IsGhostSelected)
					{
						m_GameUI.ReturnToBuildMode();
					}
				}
				if (!m_IsGhostSelected && cameraRig != null)
				{
					// Do normal base behaviour here
					DoReleaseFlick(pointer);
				}

				m_IsGhostSelected = false;

				// Reset m_DragPointer if released
				if (m_DragPointer != null && m_DragPointer.touchId == touchInfo.touchId)
				{
					m_DragPointer = null;
				}
			}
		}

		/// <summary>
		/// Called on tap,
		/// calls confirmation of tower placement
		/// </summary>
		protected virtual void OnTap(PointerActionInfo pointerActionInfo)
		{
			var touchInfo = pointerActionInfo as TouchInfo;
			if (touchInfo != null)
			{
				if (m_GameUI.state == State.Normal && !touchInfo.startedOverUI)
				{
					m_GameUI.TrySelectTower(touchInfo);
				}
				else if (m_GameUI.state == State.Building && !touchInfo.startedOverUI)
				{
					m_GameUI.TryMoveGhost(touchInfo, false);
					if (m_GameUI.IsGhostAtValidPosition())
					{
					}
					else
					{

					}
				}
			}
		}

		/// <summary>
		/// Assigns the drag pointer and sets the UI into drag mode
		/// </summary>
		/// <param name="pointer"></param>
		protected virtual void OnStartDrag(PointerActionInfo pointer)
		{
			var touchInfo = pointer as TouchInfo;
			if (touchInfo != null)
			{
				if (m_IsGhostSelected)
				{
					//m_GameUI.ChangeToDragMode();
					m_DragPointer = touchInfo;
				}
			}
		}


		/// <summary>
		/// Called when we drag
		/// </summary>
		protected override void OnDrag(PointerActionInfo pointer)
		{
			// Override normal behaviour. We only want to pan if there's no ghost selected
			// For this reason, we intentionally do not call base
			var touchInfo = pointer as TouchInfo;
			if (touchInfo != null)
			{
				// Try to pick up the tower if it was dragged off
				if (m_IsGhostSelected)
				{
					m_GameUI.TryMoveGhost(pointer, false);
				}

				if (m_GameUI.state == State.BuildingWithDrag)
				{
					DragGhost(touchInfo);
				}
				else
				{
					// Do normal base behaviour only if no ghost selected
					if (cameraRig != null)
					{
						DoDragPan(pointer);
					}
				}
			}
		}

		/// <summary>
		/// Drags the ghost
		/// </summary>
		void DragGhost(TouchInfo touchInfo)
		{
			if (touchInfo.touchId == m_DragPointer.touchId)
			{
				m_GameUI.TryMoveGhost(touchInfo, false);
			}
		}

		/// <summary>
		/// pans at the edge of the screen
		/// </summary>
		void EdgePan()
		{
			float edgeWidth = panAreaScreenPercentage * Screen.width;
			PanWithScreenCoordinates(m_DragPointer.currentPosition, edgeWidth, panSpeed);
		}


		/// <summary>
		/// If the new state is <see cref="GameUI.State.Building"/> then move the ghost to the center of the screen
		/// </summary>
		/// <param name="previousState">
		/// The previous the GameUI was is in
		/// </param>
		/// <param name="currentState">
		/// The new state the GameUI is in
		/// </param>
		void OnStateChanged(State previousState, State currentState)
		{
			// Early return for two reasons
			// 1. We are not moving into Build Mode
			// 2. We are not actually touching
			if (UnityInput.touchCount == 0)
			{
				return;
			}
			if (currentState == State.Building && previousState != State.BuildingWithDrag)
			{
				m_GameUI.MoveGhostToCenter();
			}
			if (currentState == State.BuildingWithDrag)
			{
				m_IsGhostSelected = true;
			}
		}

		/// <summary>
		/// Displays the correct confirmation buttons when the tower has become valid
		/// </summary>
		void OnGhostBecameValid()
		{
			Vector2 screenPoint = cameraRig.cachedCamera.WorldToScreenPoint(m_GameUI.GetGhostPosition());
		}
	}
}

