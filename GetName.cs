﻿using System;
using System.Linq.Expressions;

namespace GraDeMarCoWPF
{
    public static class GetName
    {
        public static string Of<MemberType>(Expression<Func<MemberType>> expression)
        {
            return (expression.Body as MemberExpression).Member.Name;
        }
    }
}
