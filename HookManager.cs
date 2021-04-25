using MonoMod.RuntimeDetour;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BetterUI
{
    public static class HookManager
    {
        private static List<(MethodInfo methodFrom, MethodInfo methodTo)> hookSignatures = new List<(MethodInfo, MethodInfo)>();
        private static List<Hook> hooks = new List<Hook>();
        static HookManager()
        {
            BetterUIPlugin.onEnable += onEnable;
            BetterUIPlugin.onDisable += onDisable;
        }
        private static void onEnable(BetterUIPlugin plugin)
        {
            foreach (var hook in hookSignatures)
            {
                hooks.Add(new Hook(hook.methodFrom, hook.methodTo));
            }
        }

        private static void onDisable(BetterUIPlugin plugin)
        {
            foreach (var hook in hooks)
            {
                hook.Dispose();
            }
        }
        public static void Add<T1>(string MethodName, Delegate hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1>(string MethodName, Action<Action<T1>, T1> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1>(string MethodName, Action<Action> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2>(string MethodName, Action<Action<T1, T2>, T1, T2> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2>(string MethodName, Action<Action<T2>, T2> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3>(string MethodName, Action<Action<T1, T2, T3>, T1, T2, T3> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3>(string MethodName, Action<Action<T2, T3>, T2, T3> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3, T4>(string MethodName, Action<Action<T1, T2, T3, T4>, T1, T2, T3, T4> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3, T4>(string MethodName, Action<Action<T2, T3, T4>, T2, T3, T4> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3, T4, T5>(string MethodName, Action<Action<T1, T2, T3, T4, T5>, T1, T2, T3, T4> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3, T4, T5>(string MethodName, Action<Action<T2, T3, T4, T5>, T2, T3, T4> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3, T4, T5, T6>(string MethodName, Action<Action<T1, T2, T3, T4, T5, T6>, T2, T1, T3, T4, T5, T6> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3, T4, T5, T6>(string MethodName, Action<Action<T2, T3, T4, T5, T6>, T2, T3, T4, T5, T6> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3, T4, T5, T6, T7>(string MethodName, Action<Action<T1, T2, T3, T4, T5, T6, T7>, T1, T2, T3, T4, T5, T6, T7> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3, T4, T5, T6, T7>(string MethodName, Action<Action<T2, T3, T4, T5, T6, T7>, T2, T3, T4, T5, T6, T7> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3, T4, T5, T6, T7, T8>(string MethodName, Action<Action<T1, T2, T3, T4, T5, T6, T7, T8>, T1, T2, T3, T4, T5, T6, T7, T8> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3, T4, T5, T6, T7, T8>(string MethodName, Action<Action<T2, T3, T4, T5, T6, T7, T8>, T2, T3, T4, T5, T6, T7, T8> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string MethodName, Action<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>, T1, T2, T3, T4, T5, T6, T7, T8, T9> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string MethodName, Action<Action<T2, T3, T4, T5, T6, T7, T8, T9>, T2, T3, T4, T5, T6, T7, T8, T9> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string MethodName, Action<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string MethodName, Action<Action<T2, T3, T4, T5, T6, T7, T8, T9, T10>, T2, T3, T4, T5, T6, T7, T8, T9, T10> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string MethodName, Action<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string MethodName, Action<Action<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string MethodName, Action<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string MethodName, Action<Action<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string MethodName, Action<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(string MethodName, Action<Action<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string MethodName, Action<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(string MethodName, Action<Action<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string MethodName, Action<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(string MethodName, Action<Action<T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> hookMethod, BindingFlags bindings = BindingFlags.Default) { Add<T1>(MethodName, hookMethod.Method, bindings); }
        public static void Add<T>(string MethodName, MethodInfo hookMethod, BindingFlags bindings = BindingFlags.Default)
        {
            MethodInfo methodFrom = null;
            if (bindings != BindingFlags.Default)
            {
                methodFrom = typeof(T).GetMethod(MethodName, bindings);
            }
            else
            {
                methodFrom = typeof(T).GetMethod(MethodName, BindingFlags.Public | BindingFlags.Instance)
                    ?? typeof(T).GetMethod(MethodName, BindingFlags.NonPublic | BindingFlags.Instance)
                    ?? typeof(T).GetMethod(MethodName, BindingFlags.Public | BindingFlags.Static)
                    ?? typeof(T).GetMethod(MethodName, BindingFlags.NonPublic | BindingFlags.Static); ;
            }
            if (methodFrom == null)
            {
                UnityEngine.Debug.LogWarning($"Could not hook method {MethodName} of {typeof(T)}, method not found.");
                return;
            }
            hookSignatures.Add((methodFrom, hookMethod));
        }
    }
}