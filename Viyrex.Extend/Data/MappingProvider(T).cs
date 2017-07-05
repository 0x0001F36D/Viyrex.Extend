namespace System.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    public class MappingProvider<T>
    {
        internal MappingProvider(DataTable table)
        {
            this.Table = table;
            this.RelationShip = new Dictionary<string, MemberInfo>();
        }

        private readonly IDictionary<string, MemberInfo> RelationShip;

        private readonly DataTable Table;

        /// <summary>
        /// 讓欄位與物件欄位產生關連
        /// </summary>
        /// <typeparam name="TMappingField"></typeparam>
        /// <param name="columnName">欄位名稱</param>
        /// <param name="selector">物件欄位選擇器</param>
        /// <returns></returns>
        public MappingProvider<T> MapTo(string columnName, Expression<Func<T, object>> selector)
        {
            if (this.Table.Columns.Cast<DataColumn>().All(x => x.ColumnName != columnName)) throw new ArgumentException(nameof(columnName));
            var member = (selector.Body as MemberExpression)?.Member ?? throw new NotSupportedException("Not supported this lambda expression");
            this.RelationShip.Add(columnName, member);
            return this;
        }

        /// <summary>
        /// 執行關聯作業
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> Execute(Func<T> instance)
            => this.Table
            .AsEnumerable()
            .Select(row => this.RelationShip.Aggregate(instance(), (obj, rs) => this.AttachValue(obj, rs.Value, row.Field<object>(rs.Key))));

        private T AttachValue(T target, MemberInfo member, object value)
        {
            switch (member)
            {
                case PropertyInfo p:
                    p.SetValue(target, value);
                    break;

                case FieldInfo f:
                    f.SetValue(target, value);
                    break;
            }
            return target;
        }

    }
    public static class MappingExtension
    {
        public static MappingProvider<T> Mapping<T>(this DataTable table)
            => new MappingProvider<T>(table);

    }
}
