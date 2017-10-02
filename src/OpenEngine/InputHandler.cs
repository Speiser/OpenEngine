using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;

namespace OpenEngine
{
    public class InputHandler
    {
        private List<Key> _keys;
        private List<Key> _keysLast;

        private List<MouseButton> _btns;
        private List<MouseButton> _btnsLast;

        public InputHandler(GameWindow window)
        {
            _keys = new List<Key>();
            _keysLast = new List<Key>();

            _btns = new List<MouseButton>();
            _btnsLast = new List<MouseButton>();

            window.KeyDown += KeyDownCallback;
            window.KeyUp += KeyUpCallback;

            window.MouseDown += MouseButtonDownCallback;
            window.MouseUp += MouseButtonUpCallback;
        }

        public void Update()
        {
            _keysLast = new List<Key>(_keys);
            _btnsLast = new List<MouseButton>(_btns);
        }

        public bool KeyPress(Key key) => _keys.Contains(key) && !_keysLast.Contains(key);
        public bool KeyRelease(Key key) => !_keys.Contains(key) && _keysLast.Contains(key);
        public bool KeyDown(Key key) => _keys.Contains(key);

        public bool MouseButtonPress(MouseButton btn) => _btns.Contains(btn) && !_btnsLast.Contains(btn);
        public bool MouseButtonRelease(MouseButton btn) => !_btns.Contains(btn) && _btnsLast.Contains(btn);
        public bool MouseButtonDown(MouseButton btn) => _btns.Contains(btn);

        private void MouseButtonUpCallback(object sender, MouseButtonEventArgs e)
        {
            _btns.Remove(e.Button);
        }
        private void MouseButtonDownCallback(object sender, MouseButtonEventArgs e)
        {
            if (!_btns.Contains(e.Button))
                _btns.Add(e.Button);
        }
        private void KeyUpCallback(object sender, KeyboardKeyEventArgs e)
        {
            _keys.Remove(e.Key);
        }
        private void KeyDownCallback(object sender, KeyboardKeyEventArgs e)
        {
            if (!_keys.Contains(e.Key))
                _keys.Add(e.Key);
        }
    }
}
