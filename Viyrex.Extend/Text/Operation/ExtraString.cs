
namespace System.Text.Operation
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Globalization;
    public sealed class ExtraString : IEnumerable<char>, IEnumerable, IComparable, ICloneable, IConvertible, IComparable<ExtraString>, IEquatable<ExtraString>
    {

        private string _token;

        public string Source => this._token;


        #region Constructors
        public ExtraString(string token)
            => this._token = token;

        public ExtraString(IEnumerable<char> token) : this(new string(token.ToArray()))
        {
        }

        public ExtraString(IEnumerable<byte> token) : this(new string(token.Select(x => (char)x).ToArray()))
        {
        }

        public unsafe ExtraString(char* token) : this(new string(token))
        {
        }
        #endregion

        #region Convert Operators
        public unsafe static implicit operator ExtraString(char* token)
            => new ExtraString(token);
        public static implicit operator ExtraString(char[] token)
            => new ExtraString(token);
        public static implicit operator ExtraString(byte[] token)
            => new ExtraString(token);
        public static implicit operator ExtraString(string token)
            => new ExtraString(token);

        public static implicit operator string(ExtraString token)
            => token._token;
        public static explicit operator char[] (ExtraString token)
            => token.ToArray();
        public unsafe static implicit operator char* (ExtraString token)
            => token;
        public static implicit operator byte[] (ExtraString token)
            => token;
        #endregion

        #region Object
        public override bool Equals(object obj)
            => this.Equals(obj);
        public override int GetHashCode()
            => this._token.GetHashCode();
        public override string ToString()
            => this._token.ToString();
        #endregion

        #region Operators        
        public static ExtraString operator +(ExtraString left, ExtraString right)
            => new ExtraString(left._token + right._token);
        public static ExtraString operator +(ExtraString left, object right)
            => new ExtraString(left._token + right);
        public static ExtraString operator +(object left, ExtraString right)
            => new ExtraString(left + right._token);


        public static ExtraString operator *(ExtraString token, uint count)
            => new ExtraString(string.Join(null, Enumerable.Repeat(token._token, (int)count)));
        public static ExtraString operator *(uint count, ExtraString token)
            => token * count;


        public static ExtraStringArray operator *(ExtraString left, ExtraStringArray right_array)
            => right_array.Select(x => left + x).ToArray();
        public static ExtraStringArray operator *(ExtraStringArray left_array, ExtraString right)
            => left_array.Select(x => x + right).ToArray();


        public static ExtraString operator -(ExtraString left, ExtraString right)
            => left._token.EndsWith(right._token)
            ? new ExtraString(left._token.TrimEnd(right._token.ToArray()))
            : left;
        #endregion

        #region IEnumerator<char>
        public IEnumerator<char> GetEnumerator()
            => this._token.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
            => this._token.GetEnumerator();
        #endregion

        #region IComparable
        int IComparable.CompareTo(object obj)
            => this._token.CompareTo(obj);
        #endregion

        #region ICloneable
        object ICloneable.Clone()
            => this._token.Clone();
        #endregion

        #region IComparable<ExtraString>
        int IComparable<ExtraString>.CompareTo(ExtraString other)
            => this._token.CompareTo(other.ToString());
        #endregion

        #region IEquatable<ExtraString>
        bool IEquatable<ExtraString>.Equals(ExtraString other)
            => this._token.Equals(other.ToString());
        #endregion

        #region IConvertible
        TypeCode IConvertible.GetTypeCode()
            => this._token.GetTypeCode();
        bool IConvertible.ToBoolean(IFormatProvider provider)
            => ((IConvertible)this._token).ToBoolean(provider);
        char IConvertible.ToChar(IFormatProvider provider)
            => ((IConvertible)this._token).ToChar(provider);
        sbyte IConvertible.ToSByte(IFormatProvider provider)
            => ((IConvertible)this._token).ToSByte(provider);
        byte IConvertible.ToByte(IFormatProvider provider)
            => ((IConvertible)this._token).ToByte(provider);
        short IConvertible.ToInt16(IFormatProvider provider)
            => ((IConvertible)this._token).ToInt16(provider);
        ushort IConvertible.ToUInt16(IFormatProvider provider)
            => ((IConvertible)this._token).ToUInt16(provider);
        int IConvertible.ToInt32(IFormatProvider provider)
            => ((IConvertible)this._token).ToInt32(provider);
        uint IConvertible.ToUInt32(IFormatProvider provider)
            => ((IConvertible)this._token).ToUInt32(provider);
        long IConvertible.ToInt64(IFormatProvider provider)
            => ((IConvertible)this._token).ToInt64(provider);
        ulong IConvertible.ToUInt64(IFormatProvider provider)
            => ((IConvertible)this._token).ToUInt64(provider);
        float IConvertible.ToSingle(IFormatProvider provider)
            => ((IConvertible)this._token).ToSingle(provider);
        double IConvertible.ToDouble(IFormatProvider provider)
            => ((IConvertible)this._token).ToDouble(provider);
        decimal IConvertible.ToDecimal(IFormatProvider provider)
            => ((IConvertible)this._token).ToDecimal(provider);
        DateTime IConvertible.ToDateTime(IFormatProvider provider)
            => ((IConvertible)this._token).ToDateTime(provider);
        string IConvertible.ToString(IFormatProvider provider)
            => this._token.ToString(provider);
        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
            => ((IConvertible)this._token).ToType(conversionType, provider);
        #endregion


        public char this[uint index]
        {
            get => this._token[(int)index];
            set => this._token = this._token.Remove((int)index, 1).Insert((int)index, value.ToString());
        }

        /*
        public ExtraString this[int start, int stop,int step]
        {
            get
            {
            }
            set
            {
            }
        }*/

        public ExtraString Range(uint startIndex, uint stopIndex, uint step = 1)
        {
            if (step == 0)
                return string.Empty;

            startIndex = checkLimit(startIndex);
            stopIndex = checkLimit(stopIndex);

            return new ExtraString(iter());

            IEnumerable<char> iter()
            {
                if (startIndex < stopIndex)
                    for (uint i = startIndex; i <= stopIndex; i += step)
                        yield return this._token[(int)i];
                else if (startIndex > stopIndex)
                    for (int i = (int)startIndex; i >= stopIndex; i -= (int)step)
                        yield return this._token[i];
                else
                    yield return this._token[(int)startIndex];
            }

            uint checkLimit(uint index)
            {
                if (index >= int.MaxValue)
                    index = (uint)int.MaxValue;
                return (uint)(this._token.Length - 1 < (int)index ? this._token.Length - 1 : (int)index);
            }
        }


        public ExtraStringArray Split(params byte[] separetor)
            => this.Split(separetor.Select(x => (char)x).ToArray());
        public ExtraStringArray Split(params char[] separetor)
            => this._token.Split(separetor);
        public ExtraStringArray Split(bool removeEmpty = false, params string[] separetor)
            => this._token.Split(separetor, (StringSplitOptions)(removeEmpty ? 1 : 0));


        public ExtraString Trim(params char[] trimChars)
            => trimChars.Length == 0 ? this._token.Trim() : this._token.Trim(trimChars);
        public ExtraString TrimStart(params char[] trimChars)
            => trimChars.Length == 0 ? this._token.TrimStart() : this._token.TrimStart(trimChars);
        public ExtraString TrimEnd(params char[] trimChars)
            => trimChars.Length == 0 ? this._token.TrimEnd() : this._token.TrimEnd(trimChars);


        public bool StartsWith(ExtraString token, bool ignoreCase = false, CultureInfo culture = default(CultureInfo))
            => this._token.StartsWith(token, ignoreCase, culture ?? CultureInfo.CurrentCulture);
        public bool EndstWith(ExtraString token, bool ignoreCase = false, CultureInfo culture = default(CultureInfo))
            => this._token.EndsWith(token, ignoreCase, culture ?? CultureInfo.CurrentCulture);


        public ExtraString ToUpper(CultureInfo culture = default(CultureInfo))
            => this._token.ToUpper(culture ?? CultureInfo.CurrentCulture);
        public ExtraString ToUpperInvariant()
            => this._token.ToUpperInvariant();


        public ExtraString ToLower(CultureInfo culture = default(CultureInfo))
            => this._token.ToLower(culture ?? CultureInfo.CurrentCulture);
        public ExtraString ToLowerInvariant()
            => this._token.ToLowerInvariant();


        public ExtraString Concat(object arg, params object[] args)
            => args.Aggregate(string.Concat(this._token, arg), (t, v) => string.Concat(t, v));

        public byte[] ToBytes()
            => this;
        public char[] ToChars()
            => (char[])this;
    }
}