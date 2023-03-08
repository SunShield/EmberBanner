using System.Collections.Generic;
using UnityEngine;

namespace EmberBanner.Core.Service.Debug
{
    public static class EBDebugger
    {
        private static EBDebugParameters parameters;
        private static bool IsInitialized => parameters != null;
        
        private static bool IsDebugEnabled
        {
            get
            {
                InitializeIfNeeded();
                return parameters.debugEnabled && UnityEngine.Debug.unityLogger.logEnabled;
            }
        }

        private static readonly Dictionary<EBDebugContext, string> contextStrings = new();

        public static void Log(string message) => Log(EBDebugContext.NoContext, message);

        public static void Log(EBDebugContext context, string message)
        {
            if (!IsDebugEnabled) return;
            
            InitializeIfNeeded();
            
            var contextString = GetContextString(context);
            var formattedMessage = $"{contextString} :{message}";
            UnityEngine.Debug.Log(formattedMessage);
        }
        
        public static void Log(EBDebugContext context1, EBDebugContext context2, string message)
        {
            if (!IsDebugEnabled) return;
            
            InitializeIfNeeded();
            
            var contextString1 = GetContextString(context1);
            var contextString2 = GetContextString(context2);
            var formattedMessage = $"{contextString1}{contextString2} :{message}";
            UnityEngine.Debug.Log(formattedMessage);
        }
        
        public static void Log(EBDebugContext context1, EBDebugContext context2, EBDebugContext context3, string message)
        {
            if (!IsDebugEnabled) return;
            
            InitializeIfNeeded();
            
            var contextString1 = GetContextString(context1);
            var contextString2 = GetContextString(context2);
            var contextString3 = GetContextString(context3);
            var formattedMessage = $"{contextString1}{contextString2}{contextString3} :{message}";
            UnityEngine.Debug.Log(formattedMessage);
        }
        
        public static void LogError(EBDebugContext context1, EBDebugContext context2, string message)
        {
            if (!IsDebugEnabled) return;
            
            InitializeIfNeeded();
            
            var contextString1 = GetContextString(context1);
            var contextString2 = GetContextString(context2);
            var formattedMessage = $"{contextString1}{contextString2} :{message}";
            UnityEngine.Debug.LogError(formattedMessage);
        }

        private static void InitializeIfNeeded()
        {
            if (IsInitialized) return;

            parameters = EBDebugParamsHolder.I.Parameters;
        }

        private static string GetContextString(EBDebugContext context)
        {
            if (!contextStrings.ContainsKey(context)) ConstructContextString(context);

            return contextStrings[context];
        }

        private static void ConstructContextString(EBDebugContext context)
        {
            var colorHex = ColorUtility.ToHtmlStringRGB(parameters.contextColors[context]);
            var str = $"[<color=#{colorHex}><b>{context.ToString().ToUpper()}</b></color>]";
            contextStrings.Add(context, str);
        }
    }
}