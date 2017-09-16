
namespace System.Text.Operation
{
    using System.Collections.Generic;
    using System.Linq;

    public static class ExtraStringHelper
    {
        public static ExtraStringArray ToExtraArray(this IEnumerable<char> token)
            => token.ToArray();
        public static ExtraStringArray ToExtraArray(this IEnumerable<byte> token)
            => token.ToArray();
        public static ExtraStringArray ToExtraArray(this string token)
            => token;
        public static ExtraStringArray ToExtraArrayy(this ExtraString token)
            => token.Source;
        public static ExtraStringArray ToExtraArray(this string[] array)
            => array;

        public static ExtraString ToExtra(this string token)
            => token;


    }
}