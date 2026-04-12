using Elemental.Framework.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Elemental.Framework.UI
{
    public class UIManager : Singleton<UIManager>
    {
        public bool IsMouseOnUI { get; private set; }
        double openUITime;
        public IUIPanel CurrentOpenUI { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            CurrentOpenUI = null;
        }

        void Update()
        {
            IsMouseOnUI = EventSystem.current.IsPointerOverGameObject();
        }

        public void OpenUI(IUIPanel onUI)
        {
            if (CurrentOpenUI == onUI) return;
            CloseUI();
            openUITime = Time.timeAsDouble;
            onUI.OnUI();
            CurrentOpenUI = onUI;
        }

        public void CloseUI()
        {
            if (CurrentOpenUI == null) return;
            CurrentOpenUI.OffUI();
            CurrentOpenUI = null;
        }

        public void OnCloseUIESC(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                if (CurrentOpenUI == null) return;
                CloseUI();
            }
        }

        public void OnCloseUILeftClick(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                if ((Time.timeAsDouble - openUITime) < Time.deltaTime) return;
                if (IsMouseOnUI) return;
                if (CurrentOpenUI == null) return;
                CloseUI();
            }
        }
    }
}
