using UnityEngine;

namespace PerspectiveShift.Input
{
    /// <summary>
    /// InputReaderが取得した入力をConsoleへ表示する、開発中の動作確認用コンポーネント。
    /// </summary>
    public sealed class InputDebugLogger : MonoBehaviour
    {
        [SerializeField]
        private InputReader inputReader;

        private Vector2 _previousMove;

        /// <summary>
        /// 同じGameObjectにInputReaderがあれば、Inspectorの参照へ自動設定する。
        /// </summary>
        private void Reset()
        {
            inputReader = GetComponent<InputReader>();
        }

        /// <summary>
        /// Inspectorの参照が未設定の場合、同じGameObjectからInputReaderを取得する。
        /// </summary>
        private void Awake()
        {
            if (inputReader == null)
            {
                inputReader = GetComponent<InputReader>();
            }
        }

        /// <summary>
        /// InputReaderのボタンイベントを購読する。
        /// </summary>
        private void OnEnable()
        {
            if (inputReader == null)
            {
                Debug.LogError(
                    "InputDebugLoggerにInputReaderが設定されていません。",
                    this
                );
                enabled = false;
                return;
            }

            inputReader.JumpPressed += OnJumpPressed;
            inputReader.RotateLeftPressed += OnRotateLeftPressed;
            inputReader.RotateRightPressed += OnRotateRightPressed;
            inputReader.RestartPressed += OnRestartPressed;
        }

        /// <summary>
        /// InputReaderのボタンイベント購読を解除する。
        /// </summary>
        private void OnDisable()
        {
            if (inputReader == null)
            {
                return;
            }

            inputReader.JumpPressed -= OnJumpPressed;
            inputReader.RotateLeftPressed -= OnRotateLeftPressed;
            inputReader.RotateRightPressed -= OnRotateRightPressed;
            inputReader.RestartPressed -= OnRestartPressed;
        }

        /// <summary>
        /// 移動入力が変化した場合だけ、現在値をConsoleへ表示する。
        /// </summary>
        private void Update()
        {
            Vector2 currentMove = inputReader.Move;

            if (currentMove == _previousMove)
            {
                return;
            }

            _previousMove = currentMove;
            Debug.Log($"Move: {currentMove}", this);
        }

        /// <summary>
        /// ジャンプ入力をConsoleへ表示する。
        /// </summary>
        private void OnJumpPressed()
        {
            Debug.Log("Jump pressed", this);
        }

        /// <summary>
        /// 左回転入力をConsoleへ表示する。
        /// </summary>
        private void OnRotateLeftPressed()
        {
            Debug.Log("Rotate left pressed", this);
        }

        /// <summary>
        /// 右回転入力をConsoleへ表示する。
        /// </summary>
        private void OnRotateRightPressed()
        {
            Debug.Log("Rotate right pressed", this);
        }

        /// <summary>
        /// リスタート入力をConsoleへ表示する。
        /// </summary>
        private void OnRestartPressed()
        {
            Debug.Log("Restart pressed", this);
        }
    }
}
