using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PerspectiveShift.Input
{
    /// <summary>
    /// Input Systemが収集した入力を、ゲーム内の各機能へ渡す窓口。
    /// キーやデバイスの詳細を、プレイヤーやカメラの処理から分離する。
    /// </summary>
    public sealed class InputReader : MonoBehaviour
    {
        /// <summary>
        /// Input Actionsアセットから自動生成された入力操作クラスのインスタンス。
        /// </summary>
        private GameInputActions _inputActions;

        /// <summary>
        /// ジャンプ入力が成立したことを通知する。
        /// </summary>
        public event Action JumpPressed;

        /// <summary>
        /// 左方向へのカメラ回転入力が成立したことを通知する。
        /// </summary>
        public event Action RotateLeftPressed;

        /// <summary>
        /// 右方向へのカメラ回転入力が成立したことを通知する。
        /// </summary>
        public event Action RotateRightPressed;

        /// <summary>
        /// ステージのリスタート入力が成立したことを通知する。
        /// </summary>
        public event Action RestartPressed;

        /// <summary>
        /// 現在の移動入力。入力が無い場合はVector2.zeroを返す。
        /// </summary>
        public Vector2 Move
        {
            get
            {
                if (_inputActions == null)
                {
                    return Vector2.zero;
                }

                return _inputActions.Gameplay.Move.ReadValue<Vector2>();
            }
        }

        /// <summary>
        /// Input Actionsの実行用インスタンスを生成する。
        /// </summary>
        private void Awake()
        {
            _inputActions = new GameInputActions();
        }

        /// <summary>
        /// ボタン入力のコールバックを登録し、Gameplay Action Mapを有効にする。
        /// </summary>
        private void OnEnable()
        {
            _inputActions.Gameplay.Jump.performed += OnJumpPerformed;
            _inputActions.Gameplay.RotateLeft.performed += OnRotateLeftPerformed;
            _inputActions.Gameplay.RotateRight.performed += OnRotateRightPerformed;
            _inputActions.Gameplay.Restart.performed += OnRestartPerformed;

            _inputActions.Gameplay.Enable();
        }

        /// <summary>
        /// Gameplay Action Mapを無効にし、登録したコールバックを解除する。
        /// </summary>
        private void OnDisable()
        {
            _inputActions.Gameplay.Disable();

            _inputActions.Gameplay.Jump.performed -= OnJumpPerformed;
            _inputActions.Gameplay.RotateLeft.performed -= OnRotateLeftPerformed;
            _inputActions.Gameplay.RotateRight.performed -= OnRotateRightPerformed;
            _inputActions.Gameplay.Restart.performed -= OnRestartPerformed;
        }

        /// <summary>
        /// Input Actionsが使用していたリソースを解放する。
        /// </summary>
        private void OnDestroy()
        {
            _inputActions.Dispose();
        }

        /// <summary>
        /// Jump Actionのperformedを受け取り、ジャンプイベントを発行する。
        /// </summary>
        /// <param name="context">Jump Actionが成立したときの入力情報。</param>
        private void OnJumpPerformed(InputAction.CallbackContext context)
        {
            JumpPressed?.Invoke();
        }

        /// <summary>
        /// RotateLeft Actionのperformedを受け取り、左回転イベントを発行する。
        /// </summary>
        /// <param name="context">RotateLeft Actionが成立したときの入力情報。</param>
        private void OnRotateLeftPerformed(InputAction.CallbackContext context)
        {
            RotateLeftPressed?.Invoke();
        }

        /// <summary>
        /// RotateRight Actionのperformedを受け取り、右回転イベントを発行する。
        /// </summary>
        /// <param name="context">RotateRight Actionが成立したときの入力情報。</param>
        private void OnRotateRightPerformed(InputAction.CallbackContext context)
        {
            RotateRightPressed?.Invoke();
        }

        /// <summary>
        /// Restart Actionのperformedを受け取り、リスタートイベントを発行する。
        /// </summary>
        /// <param name="context">Restart Actionが成立したときの入力情報。</param>
        private void OnRestartPerformed(InputAction.CallbackContext context)
        {
            RestartPressed?.Invoke();
        }
    }
}
