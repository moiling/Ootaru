using UnityEngine;

namespace Nonograms.Scripts {
    public class MouseController : MonoBehaviour {
        public int MouseType = -1;

        public bool LeftDown;
        public bool RightDown;
        public bool MiddleDown;

        #region Singleton

        private static MouseController _instance;

        public static MouseController GetInstance() {
            return _instance;
        }

        public void Awake() {
            _instance = this;
        }

        #endregion

        private void Update() {
            LeftDown = Input.GetMouseButtonDown(0);
            RightDown = Input.GetMouseButtonDown(1);
            MiddleDown = Input.GetMouseButtonDown(2);

            // 如果当前判定按下的键松开了，就置为-1
            if (MouseType == -1 || Input.GetMouseButtonUp(MouseType)) {
                MouseType = -1;
            }

            // 如果已经有一个键按下去了，就不做变化
            MouseType = MouseType != -1 ? MouseType : RightDown ? 1 : LeftDown ? 0 : MiddleDown ? 2 : -1;
        }
    }
}