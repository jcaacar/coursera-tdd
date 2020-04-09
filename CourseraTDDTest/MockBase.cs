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

        public void VerifyCalledMethod(string method, object[] args, object returned)
        {
            VerifyMethodsCalled(new string[] { method },
                                            new MethodData[] {
                                                new MethodData {
                                                    Args = args?.ToList(),
                                                    Return = returned
                                                } });
        }

        public void VerifyMethodsCalled(string[] methods, params MethodData[] returnedValues)
        {
            for (var i = 0; i < methods.Length; i++)
            {
                var method = methods[i];
                var found = methodsCalled.ContainsKey(method);

                if (!found) Assert.Fail($"Method: {method} not called");

                var argsExpected = returnedValues[i].Args;
                var argsReceived = methodsCalled[method][i].Args;

                var returnExpected = returnedValues[i].Return;
                var returned = methodsCalled[method][i].Return;

                Assert.That(argsReceived, Is.EqualTo(argsExpected), $"Method {method} position[{i}] received args:\n {argsReceived?.ToString(" - ")} but expected:\n {argsExpected?.ToString(" - ")}");
                Assert.AreEqual(returnExpected, returned, $"Method {method} position[{i}] returned: {returned} but expected: {returnExpected}");
            }
        }

        public void VerifyCalledCountMethod(string method, object[] args, object returned, int count)
        {
            if(methodsCalled.TryGetValue(method, out var items))
            {
                ValidateCalledCount(method, count, items);

                ValidateArgs(method, items, args);

                ValidateReturned(method, items, returned);
            }
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
            var argsOk = items.All(i => i.Args.All(a => args.Contains(a)));

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
            sb.AppendLine("Return: ").AppendLine(Return?.ToString());

            return sb.ToString();
        }
    }
}
