using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpHook;
using SharpHook.Native;

namespace multiboxApp
{
    public static class SharpHook
    {
        public static TaskPoolGlobalHook Hook = new();
        public static EventSimulator Simulator = new();

        //private static SharpHook _instance;
        //public static SharpHook Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //        {
        //            _instance = new SharpHook();
        //        }
        //        return _instance;
        //    }
        //    private set => _instance = value;
        //}

        //private SharpHook()
        //{
        //    //hook.HookEnabled += OnHookEnabled;
        //    //hook.HookDisabled += OnHookDisabled;

        //    //hook.KeyTyped += OnKeyTyped;
        //    //hook.KeyPressed += OnKeyPressed;
        //    //hook.KeyReleased += OnKeyReleased;

        //    //hook.MouseClicked += OnMouseClicked;
        //    //hook.MousePressed += OnMousePressed;
        //    //hook.MouseReleased += OnMouseReleased;
        //    //hook.MouseMoved += OnMouseMoved;
        //    //hook.MouseDragged += OnMouseDragged;

        //    //hook.MouseWheel += OnMouseWheel;
        //}

       
    }
}


//using Gma.System.MouseKeyHook;

//namespace multiboxApp
//{
//    public class GlobalMouseKeyHook
//    {


//        public IKeyboardMouseEvents? m_GlobalHook;

//        private static GlobalMouseKeyHook _instance;
//        public static GlobalMouseKeyHook Instance
//        {
//            get
//            {
//                if (_instance == null)
//                {
//                    _instance = new GlobalMouseKeyHook();
//                }
//                return _instance;
//            }
//            private set => _instance = value;
//        }

//        public List<Action<KeyPressEventArgs>> KeyActions = new();
//        public List<Action<MouseEventArgs>> MouseActions = new();

//        public Dictionary<Combination, Action> CombinationMap = new();

//        //var map = new Dictionary<Combination, Action>
//        //    {
//        //        //Specify which key combinations to detect and action - what to do if detected.
//        //        //You can create a key combinations directly from string or ...
//        //        {Combination.FromString("A+B+C"), () => Console.WriteLine(":-)")},
//        //        //... or alternatively you can use builder methods
//        //        {Combination.TriggeredBy(Keys.F).With(Keys.E).With(Keys.D), () => Console.WriteLine(":-D")},
//        //        {Combination.FromString("Alt+A"), () => Console.WriteLine(":-P")},
//        //        {Combination.FromString("Control+Shift+Z"), () => Console.WriteLine(":-/")},
//        //        {Combination.FromString("Escape"), quit}
//        //    };


//        private GlobalMouseKeyHook()
//        {
//            Subscribe();
//        }

//        ~GlobalMouseKeyHook()
//        {
//            Unsubscribe();
//        }



//        public void Subscribe()
//        {
//            // Note: for the application hook, use the Hook.AppEvents() instead
//            m_GlobalHook = Hook.GlobalEvents();

//            Hook.GlobalEvents().OnCombination(CombinationMap);
//            m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
//            m_GlobalHook.KeyPress += GlobalHookKeyPress;
//        }


//        private void GlobalHookKeyPress(object? sender, KeyPressEventArgs e)
//        {
//            Console.WriteLine("KeyPress: \t{0}", e.KeyChar);

//            foreach (var action in KeyActions)
//            {
//                action.Invoke(e);
//            }
//        }

//        private void GlobalHookMouseDownExt(object? sender, MouseEventExtArgs e)
//        {
//            Console.WriteLine("MouseDown: \t{0}; \t System Timestamp: \t{1}", e.Button, e.Timestamp);

//            foreach (var action in MouseActions)
//            {
//                action.Invoke(e);
//            }
//            // uncommenting the following line will suppress the middle mouse button click
//            // if (e.Buttons == MouseButtons.Middle) { e.Handled = true; }
//        }

//        public void Unsubscribe()
//        {
//            m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
//            m_GlobalHook.KeyPress -= GlobalHookKeyPress;

//            //It is recommened to dispose it
//            m_GlobalHook.Dispose();
//        }
//    }
//}