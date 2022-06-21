using A14Farming.UI.HUD;
using Core.Input;
using UnityInput = UnityEngine.Input;
using State = A14Farming.UI.HUD.GameUI.State;
using UnityEngine;

namespace A14Farming.Input
{
    [RequireComponent(typeof(GameUI))]
    public class TowerDefenseKeyboardMouseInput : KeyboardMouseInput
    {
		/// <summary>
		/// Cached eference to gameUI
		/// </summary>
		GameUI m_GameUI;

		/// <summary>
		/// Register input events
		/// </summary>
		//      private void Start()
		//      {
		//	base.Init();

		//	m_GameUI = GetComponent<GameUI>();

		//	if (InputController.instanceExists)
		//	{
		//		InputController controller = InputController.instance;
		//		controller.tapped += OnTap;
		//		controller.mouseMoved += OnMouseMoved;
		//	}
		//}

		/// <summary>
		/// Register input events
		/// </summary>
		protected override void OnEnable()
		{
			base.OnEnable();

			m_GameUI = GetComponent<GameUI>();

			if (InputController.instanceExists)
			{
				InputController controller = InputController.instance;

				controller.tapped += OnTap;
				controller.mouseMoved += OnMouseMoved;
			}
		}

		/// <summary>
		/// Deregister input events
		/// </summary>
		protected override void OnDisable()
		{
			if (!InputController.instanceExists)
			{
				return;
			}

			InputController controller = InputController.instance;

			controller.tapped -= OnTap;
			controller.mouseMoved -= OnMouseMoved;
		}

		/// <summary>
		/// Handle camera panning behaviour
		/// </summary>
		protected override void Update()
		{
			base.Update();

			// Escape handling
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
					case State.BuildingWithDrag:
					case State.Building:
						m_GameUI.CancelGhostPlacement();
						break;
				}
			}

			// place towers with keyboard numbers
			
		}

		/// <summary>
		/// Ghost follows pointer
		/// </summary>
		void OnMouseMoved(PointerInfo pointer)
		{
			// We only respond to mouse info
			var mouseInfo = pointer as MouseCursorInfo;

			if ((mouseInfo != null) && (m_GameUI.isBuilding))
			{
				m_GameUI.TryMoveGhost(pointer, false);
			}
		}

		/// <summary>
		/// Select towers or position ghosts
		/// </summary>
		void OnTap(PointerActionInfo pointer)
		{
			// We only respond to mouse info
			var mouseInfo = pointer as MouseButtonInfo;

			if (mouseInfo != null && !mouseInfo.startedOverUI)
			{
				if (m_GameUI.isBuilding)
				{
					if (mouseInfo.mouseButtonId == 0) // LMB confirms
					{
						m_GameUI.TryPlaceTower(pointer);
					}
					else // RMB cancels
					{
						m_GameUI.CancelGhostPlacement();
					}
				}
				else
				{
					if (mouseInfo.mouseButtonId == 0)
					{
						// select towers
						m_GameUI.TrySelectTower(pointer);
					}
				}
			}
		}
	}
}

