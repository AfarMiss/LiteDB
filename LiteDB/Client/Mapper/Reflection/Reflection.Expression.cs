using System;
using System.Reflection;

namespace LiteDB
{
    /// <summary>
    /// Using Expressions is the easy and fast way to create classes, structs, get/set fields/properties. But it not works in NET35
    /// </summary>
    internal partial class Reflection
    {
        public static CreateObject CreateClass(Type type)
        {
            return value => Activator.CreateInstance(type);
        }

        public static CreateObject CreateStruct(Type type)
        {
            return value => Activator.CreateInstance(type);
        }

        public static GenericGetter CreateGenericGetter(Type type, MemberInfo memberInfo)
        {
            if (memberInfo is FieldInfo fieldInfo)
            {
                return fieldInfo.GetValue;
            }

            if (memberInfo is PropertyInfo propertyInfo && propertyInfo.CanRead)
            {
                return target => propertyInfo.GetValue(target);
            }

            return null;
        }

        public static GenericSetter CreateGenericSetter(Type type, MemberInfo memberInfo)
        {
            if (memberInfo is FieldInfo fieldInfo)
            {
                return fieldInfo.SetValue;
            }

            if (memberInfo is PropertyInfo propertyInfo && propertyInfo.CanWrite)
            {
                return (target, value) => propertyInfo.SetValue(target, value);
            }
            return null;
        }
    }
}