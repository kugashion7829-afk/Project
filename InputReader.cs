using System;
// using 領域を使用
// System システムという名前の領域

using UnityEngine;
// UnityEngine UnityEngineという名前の領域

using UnityEngine.InputSystem;
// InputSystem UnityEngine領域内のInputSystemという領域

namespace PerspectiveShift.Input
{
    /// Input Systemが収集した入力を、ゲーム内の各機能へ渡す窓口。
    /// キーやデバイスの詳細を、プレイヤーやカメラの処理から分離する。
    public sealed class InputReader : MonoBehaviour
    // public どこからでも
    // sealed 継承を禁止
    // class クラスを作成
    // MonoBehaviour UnityのCompornentとして使用
    {
        private GameInputActions _inputActions;
        // private このクラス内のみ
        // 

        public event Action JumpPressed;
        public event Action RotateLeftPressed;
        public event Action RotateRightPressed;
        public event Action RestartPressed;
        // event
        // Action

        /// 現在の移動入力。入力が無い場合はVector2.zeroを返す。
        public Vector2 Move
        {
            get
            {
                if (_inputActions == null)
                {
                    return Vector2.zero;
                }

                return
                _inputActions.Gameplay.Move.ReadValue<Vector2>
                ();
            }
        }

        private void Awake()
        {
            _inputActions = new GameInputActions();
        }

        private void OnEnable()
        {
            _inputActions.Gameplay.Jump.performed += OnJumpPerformed;
            _inputActions.Gameplay.RotateLeft.performed += OnRotateLeftPerformed;
            _inputActions.Gameplay.RotateRight.performed += OnRotateRightPerformed;
            _inputActions.Gameplay.Restart.performed += OnRestartPerformed;

            _inputActions.Gameplay.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Gameplay.Disable();

            _inputActions.Gameplay.Jump.performed -= OnJumpPerformed;
            _inputActions.Gameplay.RotateLeft.performed -= OnRotateLeftPerformed;
            _inputActions.Gameplay.RotateRight.performed -= OnRotateRightPerformed;
            _inputActions.Gameplay.Restart.performed -= OnRestartPerformed;
        }

        private void OnDestroy()
        {
            _inputActions.Dispose();
        }

        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            JumpPressed?.Invoke();
        }

        private void OnRotateLeftPerformed(InputAction.CallbackContext context)
        {
            RotateLeftPressed?.Invoke();
        }

        private void OnRotateRightPerformed(InputAction.CallbackContext context)
        {
            RotateRightPressed?.Invoke();
        }

        private void OnRestartPerformed(InputAction.CallbackContext context)
        {
            RestartPressed?.Invoke();
        }
    }
}