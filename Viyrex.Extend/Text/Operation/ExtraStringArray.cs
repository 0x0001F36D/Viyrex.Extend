﻿
namespace System.Text.Operation
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    public sealed class ExtraStringArray : IEnumerable<ExtraString>
    {
        private List<ExtraString> _tokens;

        private ExtraStringArray(IEnumerable<string> tokens) : this(tokens.Select(x => (ExtraString)x))
        {
        }

        private ExtraStringArray(IEnumerable<char> tokens) : this(tokens.Select(x => x.ToString()))
        {
        }
        private ExtraStringArray(IEnumerable<byte> tokens) : this(tokens.Select(x => (char)x))
        {
        }

        private ExtraStringArray(IEnumerable<ExtraString> tokens)
            => this._tokens = tokens.ToList();

        public ExtraString this[uint index]
        {
            get => this._tokens[(int)index];
            set => this._tokens[(int)index] = value;
        }

        public char this[uint x, uint y, bool xySwap = false]
        {
            get => this[xySwap ? y : x][xySwap ? x : y];
            set => this[xySwap ? y : x][xySwap ? x : y] = value;
        }

        public int Length => this._tokens.Count;

        #region Convert Operators


        public static implicit operator ExtraStringArray(ExtraString[] tokens)
            => new ExtraStringArray(tokens);

        public static implicit operator ExtraStringArray(string[] tokens)
            => new ExtraStringArray(tokens);

        public static implicit operator ExtraStringArray(char[] tokens)
            => new ExtraStringArray(tokens);

        public static implicit operator ExtraStringArray(byte[] tokens)
            => new ExtraStringArray(tokens);

        public static implicit operator ExtraStringArray(string tokens)
            => new ExtraStringArray(tokens);

        public static implicit operator string[] (ExtraStringArray tokens)
            => tokens.Select(x => x.Source).ToArray();


        #endregion

        #region Operators


        public static ExtraStringArray operator *(ExtraStringArray left_array, ExtraStringArray right_array)
            => left_array.SelectMany(x => x * right_array).ToArray();


        public static ExtraStringArray operator *(ExtraStringArray array, uint count)
            => array.Select(x => x * count).ToArray();
        public static ExtraStringArray operator *(uint count, ExtraStringArray array)
            => array * count;


        public static ExtraStringArray operator +(ExtraStringArray array, ExtraString token)
            => array.Select(x => x + token).ToArray();
        public static ExtraStringArray operator +(ExtraString token, ExtraStringArray array)
            => array.Select(x => token + x).ToArray();


        public static ExtraStringArray operator +(ExtraStringArray left_array, ExtraStringArray right_array)
            => left_array.Zip(right_array, (l, r) => l + r).ToArray();
        
        #endregion


        public IEnumerator<ExtraString> GetEnumerator()
            => ((IEnumerable<ExtraString>)this._tokens).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
            => ((IEnumerable<ExtraString>)this._tokens).GetEnumerator();



        public ExtraString Arrange(object separator = null)
            => string.Join(separator?.ToString(), this._tokens);
        



    }
}