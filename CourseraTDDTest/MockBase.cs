using CourseraTDD.Extension;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CourseraTDDTEST
{
    public abstract class MockBase
    {
        private readonly Dictionary<string, IList<MethodData>> methodsCalled = new Dictionary<string, IList<MethodData>>();

        public void AddMethodCalled([CallerMemberName] string method = null)
        {
            AddMethodCalled(method);
        }

        public void AddMethodCalled([CallerMemberName] string method = null, object[] args = null, object returnValue = null)
        {
            var found = methodsCalled.ContainsKey(method);
            if (found)
            {
                methodsCalled[method].Add(new MethodData { Args = args?.ToList(), Return = returnValue });
            }
            else
            {
                methodsCalled.Add(method, new List<MethodData> { new MethodData { Args = args?.ToList(), Return = returnValue } });
            }
        }

        public void VerifyCalledMethod(string method, object[] args = null, object returned = null, int count = 1)
        {
            if (methodsCalled.TryGetValue(method, out var items))
            {
                ValidateCalledCount(method, count, items);

                ValidateArgs(method, items, args);

                ValidateReturned(method, items, returned);
            }
        }

        public int GetMethodCalledCount([CallerMemberName] string method = null)
        {
            if (methodsCalled.TryGetValue(method, out var items))
            {
                return items.Count;
            }
            return 0;
        }

        public void ClearCalledMethods()
        {
            methodsCalled.Clear();
        }

        private void ValidateCalledCount(string method, int count, IList<MethodData> items)
        {
            if (items.Count != count)
            {
                Assert.Fail($"Method {method} called 0 times, but expected {count} times");
            }
        }

        private void ValidateArgs(string method, IEnumerable<MethodData> items, object[] args)
        {
            var argsOk = items.All(i => (i.Args == null && args == null) || i.Args.All(a => args.Contains(a)));

            if (!argsOk)
            {
                Assert.Fail($"Method {method} received args:\n {items.ToString(" - ")} but expected:\n {args?.ToString(" - ")}");
            }
        }

        private void ValidateReturned(string method, IEnumerable<MethodData> items, object returned)
        {
            var returnedOk = items.All(i =>
            {
                if (i.Return == null && returned == null)
                {
                    return true;
                }

                return (i.Return?.Equals(returned)).HasValue;
            });

            if (!returnedOk)
            {
                Assert.Fail($"Method {method} returned:\n {items.ToString(" - ")} but expected:\n {returned}");
            }
        }
    }

    public struct MethodData
    {
        public List<object> Args;
        public object Return;

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Args: ").AppendLine(Args?.ToString(" - "));
            sb.AppendLine("\nReturn: ").AppendLine(Return?.ToString());

            return sb.ToString();
        }
    }
}
