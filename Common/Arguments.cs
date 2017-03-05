using System.Collections.Generic;

namespace Common
{
    public class Arguments
    {
        private readonly List<string> _singleArgs;
        private readonly Dictionary<string, string> _valuesArgs;

        public Arguments(params string[] args)
        {
            _singleArgs = new List<string>();
            _valuesArgs = new Dictionary<string, string>();
            foreach (var arg in args)
            {
                if (IsKeyedArg(arg))
                {
                    var arr = arg.Split('=');
                    _valuesArgs[arr[0].ToUpper().Substring(1)] = arg.Substring(arr.Length + 1);
                }
                else
                {
                    _singleArgs.Add(arg);
                }
            }
        }

        public int Length => _singleArgs.Count + _valuesArgs.Count;

        public string this[string key] => _valuesArgs[key.ToUpper()];
        public string this[int index] => _singleArgs[index];
        public bool HasKey(string key) => _valuesArgs.ContainsKey(key.ToUpper());

        private bool IsKeyedArg(string arg) => arg.StartsWith("/") || arg.StartsWith("-");

    }
}