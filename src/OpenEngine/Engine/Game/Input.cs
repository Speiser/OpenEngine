using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;

namespace OpenEngine
{
    public static class Input
    {
        private static List<Key> _keys;
        private static List<Key> _keysLast;

        private static List<MouseButton> _btns;
        private static List<MouseButton> _btnsLast;

        public static void Init(GameWindow window)
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

        public static void Update()
        {
            _keysLast = new List<Key>(_keys);
            _btnsLast = new List<MouseButton>(_btns);
        }

        public static bool KeyPress(Key key) => _keys.Contains(key) && !_keysLast.Contains(key);
        public static bool KeyRelease(Key key) => !_keys.Contains(key) && _keysLast.Contains(key);
        public static bool KeyDown(Key key) => _keys.Contains(key);

        public static bool MouseButtonPress(MouseButton btn) => _btns.Contains(btn) && !_btnsLast.Contains(btn);
        public static bool MouseButtonRelease(MouseButton btn) => !_btns.Contains(btn) && _btnsLast.Contains(btn);
        public static bool MouseButtonDown(MouseButton btn) => _btns.Contains(btn);

        private static void MouseButtonUpCallback(object sender, MouseButtonEventArgs e)
        {
            _btns.Remove(e.Button);
        }
        private static void MouseButtonDownCallback(object sender, MouseButtonEventArgs e)
        {
            if (!_btns.Contains(e.Button))
                _btns.Add(e.Button);
        }
        private static void KeyUpCallback(object sender, KeyboardKeyEventArgs e)
        {
            _keys.Remove(e.Key);
        }
        private static void KeyDownCallback(object sender, KeyboardKeyEventArgs e)
        {
            if (!_keys.Contains(e.Key))
                _keys.Add(e.Key);
        }
    }
}
