using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Discord.json.Helpers
{
	public class ReflectionHelper
	{
		public static object GetPropValue(object obj, string name)
		{
			foreach (string part in name.Split('.'))
			{
				if (obj == null) { return null; }

				Type type = obj.GetType();
				PropertyInfo info = type.GetProperty(part);
				if (info == null) { return null; }

				obj = info.GetValue(obj, null);
			}
			return obj;
		}

		public static T GetPropValue<T>(object obj, string name)
		{
			object retval = GetPropValue(obj, name);
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
			await (Task)thisType.GetTypeInfo().GetDeclaredMethod(method).Invoke(sender, parameters);
		}
	}
}
