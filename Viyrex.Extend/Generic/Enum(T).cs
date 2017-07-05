namespace System
{
    using System.Linq;
    /// <summary>
    /// 提供強型別的Enum轉換
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    public static class Enum<TEnum> where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        public static TEnum[] ResolveAll(TEnum @enum)
            => Enum<TEnum>.GetValues().Where(x => (@enum as Enum).HasFlag(x as Enum)).ToArray();

        public static TEnum[] Resolve(TEnum @enum)
            => @enum.ToString().Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).Select(x => Enum<TEnum>.Parse(x)).ToArray();

        public static string Format<TValue>(TValue value, string format) where TValue : struct, IComparable, IFormattable, IConvertible, IComparable<TValue>, IEquatable<TValue>
            => Enum.Format(typeof(TEnum), value, format);

        public static string GetName<TValue>(TValue value) where TValue : struct, IComparable, IFormattable, IConvertible, IComparable<TValue>, IEquatable<TValue>
            => Enum.GetName(typeof(TEnum), value);

        public static string[] GetNames()
            => Enum.GetNames(typeof(TEnum));

        public static Type GetUnderlyingType()
            => Enum.GetUnderlyingType(typeof(TEnum));

        public static TEnum[] GetValues()
            => Enum.GetValues(typeof(TEnum)).Cast<TEnum>().ToArray();

        public static bool IsDefined<TValue>(TValue value) where TValue : struct, IComparable, IFormattable, IConvertible, IComparable<TValue>, IEquatable<TValue>
            => Enum.IsDefined(typeof(TEnum), value);

        public static TEnum Parse(string value)
            => (TEnum)Enum.Parse(typeof(TEnum), value);

        public static TEnum Parse(string value, bool ignoreCase)
            => (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase);

        public static TEnum ToEnum(int value)
            => Enum<TEnum>.ToEnum(value);

        public static TEnum ToEnum(ulong value)
            => Enum<TEnum>.ToEnum(value);

        public static TEnum ToEnum(byte value)
            => Enum<TEnum>.ToEnum(value);

        public static TEnum ToEnum(uint value)
            => Enum<TEnum>.ToEnum(value);

        public static TEnum ToEnum(short value)
            => Enum<TEnum>.ToEnum(value);

        public static TEnum ToEnum(long value)
            => Enum<TEnum>.ToEnum(value);

        public static TEnum ToEnum(ushort value)
            => Enum<TEnum>.ToEnum(value);

        public static TEnum ToEnum(sbyte value)
            => Enum<TEnum>.ToEnum(value);

        public static TEnum ToEnum<TValue>(TValue value) where TValue : struct, IComparable, IFormattable, IConvertible, IComparable<TValue>, IEquatable<TValue>
            => (TEnum)Enum.ToObject(typeof(TEnum), value);

        public static bool TryParse(string value, out TEnum result)
            => Enum.TryParse<TEnum>(value, out result);

        public static bool TryParse(string value, bool ignoreCase, out TEnum result)
            => Enum.TryParse<TEnum>(value, ignoreCase, out result);
    }

    public static class EnumExpansion
    {
        public static string Format<TEnum>(this object value, string format) where TEnum : struct, IComparable, IFormattable, IConvertible
            => Enum.Format(typeof(TEnum), value, format);

        public static string GetName<TEnum>(this object value) where TEnum : struct, IComparable, IFormattable, IConvertible
            => Enum.GetName(typeof(TEnum), value);

        public static bool IsDefined<TEnum>(this object value) where TEnum : struct, IComparable, IFormattable, IConvertible
            => Enum.IsDefined(typeof(TEnum), value);

        public static TEnum ToEnum<TEnum>(this object value) where TEnum : struct, IComparable, IFormattable, IConvertible
            => (TEnum)Enum.ToObject(typeof(TEnum), value);

    }
}