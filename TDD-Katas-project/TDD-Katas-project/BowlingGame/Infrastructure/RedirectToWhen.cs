using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace TDD_Katas_project.BowlingGame.Infrastructure
{
    // ReSharper disable StaticFieldInGenericType
    public static class RedirectToWhen
    {
        private static readonly ThreadLocal<RedirectToMethod> Redirector =
            new ThreadLocal<RedirectToMethod>(() => new RedirectToMethod("When"));

        public static void TryInvokeEvent(object instance, object domainEvent)
        {
            Redirector.Value.TryInvokeMethod(instance, domainEvent);
        }
    }

    public static class RedirectToHandle
    {
        private static readonly ThreadLocal<RedirectToMethod> Redirector =
            new ThreadLocal<RedirectToMethod>(() => new RedirectToMethod("Handle"));

        public static void TryInvokeEvent(object instance, object domainEvent)
        {
            Redirector.Value.TryInvokeMethod(instance, domainEvent);
        }
    }

    public class RedirectToMethod
    {
        private static string _methodName;

        public RedirectToMethod(string methodName)
        {
            _methodName = methodName;
        }

        private static readonly MethodInfo InternalPreserveStackTraceMethod =
            typeof (Exception).GetMethod("InternalPreserveStackTrace", BindingFlags.Instance | BindingFlags.NonPublic);

        private interface IHandlersCache
        {
            bool TryGetHandler(Type argumentType, out MethodInfo handler);
        }

        private static class Cache
        {
            private static readonly object HandlerCachesSynch = new object();
            private static IDictionary<Type, IHandlersCache> _handlersCaches;

            public static IHandlersCache For(Type type)
            {
                lock (HandlerCachesSynch)
                {
                    if (_handlersCaches == null)
                        _handlersCaches = new Dictionary<Type, IHandlersCache>();

                    IHandlersCache chachedHandlers;
                    if (!_handlersCaches.TryGetValue(type, out chachedHandlers))
                    {
                        chachedHandlers = new HandlersCache(type);
                        _handlersCaches.Add(type, chachedHandlers);
                    }

                    return chachedHandlers;
                }
            }

            private class HandlersCache : IHandlersCache
            {
                private readonly IDictionary<Type, MethodInfo> _eventToHandlerMap;

                public HandlersCache(Type type)
                {
                    _eventToHandlerMap = type.GetMethods(BindingFlags.Instance | BindingFlags.Public |
                                                         BindingFlags.NonPublic)
                                             .Where(m => m.Name == _methodName)
                                             .Where(m => m.GetParameters().Length == 1)
                                             .ToDictionary(m => m.GetParameters().First().ParameterType, m => m);
                }

                public bool TryGetHandler(Type argumentType, out MethodInfo handler)
                {
                    return _eventToHandlerMap.TryGetValue(argumentType, out handler);
                }
            }
        }

        public void TryInvokeMethod(object instance, object argument)
        {
            MethodInfo info;
            var type = argument.GetType();

            if (!Cache.For(instance.GetType()).TryGetHandler(type, out info))
                return;

            try
            {
                info.Invoke(instance, new[] {argument});
            }
            catch (TargetInvocationException ex)
            {
                if (null != InternalPreserveStackTraceMethod)
                    InternalPreserveStackTraceMethod.Invoke(ex.InnerException, new object[0]);

                throw ex.InnerException;
            }
        }
    }
}