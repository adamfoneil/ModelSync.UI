using System;

namespace ModelSync.CLI
{
    /// <summary>
    /// base class for parsing a string[] into arguments for downstream use
    /// </summary>
    public abstract class ArgParser
    {
        protected readonly string[] _args;

        public ArgParser(string[] args)
        {
            _args = args;
        }

        protected T Get<T>(int index, Func<string, T> convert, T defaultValue = default)
        {
            try
            {
                return convert.Invoke(_args[index]);
            }
            catch 
            {
                return defaultValue;
            }
        }

        protected string Get(int index, string defaultValue = null)
        {
            try
            {
                return _args[index];
            }
            catch 
            {
                return defaultValue;
            }
        }
    }
}
