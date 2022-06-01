using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Holoon.NewtonsoftUtils.Trimming
{
    public class StringPropertiesToNotTrimHandlerCollection
    {
        internal List<(Type ObjectType, string PropertyName)> _InternalListOfPropertiesToIgnore = new();
        public void Add<TObject>(Expression<Func<TObject, object>> propertyToIgnore) =>
            _InternalListOfPropertiesToIgnore.Add((typeof(TObject), (propertyToIgnore.Body as MemberExpression)?.Member?.Name));
    }
}
