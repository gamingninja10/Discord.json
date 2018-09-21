using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EzBot.Json
{
	public class ReflectionHelper
	{
		public static Object GetPropValue(object obj, String name)
		{
			foreach (String part in name.Split('.'))
			{
				if (obj == null) { return null; }

				Type type = obj.GetType();
				PropertyInfo info = type.GetProperty(part);
				if (info == null) { return null; }

				obj = info.GetValue(obj, null);
			}
			return obj;
		}

		public static T GetPropValue<T>(object obj, String name)
		{
			Object retval = GetPropValue(obj, name);
			if (retval == null) { return default(T); }

			// throws InvalidCastException if types are incompatible
			return (T)retval;
		}

		public static IEnumerable<MethodInfo> GetMethodsWithAttribute(Type classType, Type attributeType)
		{
			return classType.GetMethods().Where(methodInfo => methodInfo.GetCustomAttributes(attributeType, true).Length > 0);
		}

		public static IEnumerable<MemberInfo> GetMembersWithAttribute(Type classType, Type attributeType)
		{
			return classType.GetMembers().Where(memberInfo => memberInfo.GetCustomAttributes(attributeType, true).Length > 0);
		}

		public static async Task InvokeMethod<TType>(object sender, string method, object[] parameters)
		{
			Type thisType = typeof(TType);
			//MethodInfo theMethod = thisType.GetMethod(method);
			await (Task)thisType.GetTypeInfo().GetDeclaredMethod(method).Invoke(sender, parameters);
		}
	}
}
