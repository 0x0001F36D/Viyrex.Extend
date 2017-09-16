
namespace System.Text.Operation
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Globalization;
    using System.Runtime.Serialization;

    public sealed class ExtraString : IEnumerable<char>, IEnumerable, IComparable, ICloneable, IConvertible, IComparable<ExtraString>, IEquatable<ExtraString> ,ISerializable
    {

        private string _token;

        public string Source => this._token;

        public uint Length => (uint)this._token.Length;

        #region Constructors
        public ExtraString(string token)
            => this._token = token ?? string.Empty;

        public ExtraString(IEnumerable<string> token):this(string.Join(null,token))
        {
        }

        public ExtraString(IEnumerable<char> token) : this(new string(token.ToArray()))
        {
        }

        unsafe public ExtraString(char* token) : this(new string(token))
        {
        }
        #endregion

        #region Convert Operators
        unsafe public static implicit operator ExtraString(char* token)
            => new ExtraString(token);
        public static implicit operator ExtraString(char[] token)
            => new ExtraString(token);
        public static explicit operator ExtraString(char token)
            => new ExtraString(token.ToString());
        public static implicit operator ExtraString(string token)
            => new ExtraString(token);

        public static implicit operator string(ExtraString token)
            => token._token;
        public static explicit operator char[] (ExtraString token)
            => token.ToArray();
        unsafe public static implicit operator char* (ExtraString token)
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

        #region ISerializable
        private ExtraString(SerializationInfo info, StreamingContext context): this(info.GetString(nameof(_token)))
        {
        }
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
            => info.AddValue(nameof(_token), this._token);
        #endregion


        public char this[uint index]
        {
            get => this._token.Length > index ? this._token[(int)index] : char.MinValue;
        
            set
            {
                if (this._token.Length > index)
                    this._token = this._token.Remove((int)index, 1).Insert((int)index, value.ToString());
                else
                    this._token += value;
            }
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

            
        public bool Contains(ExtraString token)
            => this._token.Contains(token);
        public bool Contains(char token, out IEnumerable<uint> indexes)
            => this.Contains((ExtraString)token, out indexes);
        public bool Contains(ExtraString token, out IEnumerable<uint> indexes)
        {
            indexes = new List<uint>();
            int index = 0;
            while ((index = this._token.IndexOf(token, index)) != -1)
                (indexes as List<uint>).Add((uint)index++);
            return (indexes as List<uint>).Count > 0;
        }
        

        public class Index
        {
            internal Index(uint startIndex, ExtraString token)
            {
                this.StartIndex = startIndex;
                this.Token = token;
                this.Length = token.Length;
            }


            public uint StartIndex { get; }
            public ExtraString Token { get; }
            public uint Length { get; }

            public string PrettyPrint()
                => $"[{this.Token}, {this.Length}] = {this.Token}";

            public override bool Equals(object obj) => obj is Index p ? p.GetHashCode() == this.GetHashCode() : false;
            public override int GetHashCode() => this.StartIndex.GetHashCode() + this.Token.GetHashCode();
        }
        public IEnumerable<Index> Mapping(ExtraString token, params ExtraString[] tokens)
        {
            return mapping();
            IEnumerable<Index> mapping()
            {
                if (token == null)
                    yield break;

                var list = new HashSet<ExtraString>(tokens?.Where(x=> x!= null) ?? new ExtraString[0]) { token };

                foreach (var t in list)
                    if (this.Contains(t, out var collection))
                        foreach (var index in collection)
                            yield return new Index(index, t);
            }
        }
        

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
        public bool EndsWith(ExtraString token, bool ignoreCase = false, CultureInfo culture = default(CultureInfo))
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
            => args?.Aggregate(string.Concat(this._token, arg), (t, v) => string.Concat(t, v)) ?? string.Concat(this._token, arg);
        
        public char[] ToCharArray()
            => (char[])this;
    }
}