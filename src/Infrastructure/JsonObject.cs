using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Collections;
using System.Reflection;
using System.Web.Script.Serialization;

namespace Stripe
{
	public class JsonObject : DynamicObject, IDictionary<string, object>
	{
		private IDictionary<string, object> _model;
		private readonly bool _throwErrorOnMissingMethod;

		internal JsonObject()
		{
			_throwErrorOnMissingMethod = false;
		}

		internal void SetModel(IDictionary<string, object> model)
		{
			_model = new Dictionary<string, object>(model, new JsonPropertyNameEqualityComparer());
		}

		private string GetSingleIndexOrNull(object[] indexes)
		{
			if (indexes.Length == 1)
				return (string)indexes[0];

			return null;
		}

		public bool HasProperty(string name)
		{
			if (String.IsNullOrEmpty(name))
				return false;

			return _model.ContainsKey(name);
		}

		public object GetProperty(string name)
		{
			object result;
			if (TryGetProperty(name, out result))
				return result;

			return null;
		}

		public object this[string name]
		{
			get { return GetProperty(name); }
		}

		private bool TryGetValue(string name, out object result)
		{
			result = null;

			if (String.IsNullOrEmpty(name))
				return true;

			return _model.TryGetValue(name, out result);
		}

		private object WrapObjectIfNessisary(object result)
		{
			object output = result;

			// handle special types in model object
			if (result is IDictionary<string, object>)
			{
				output = new JsonObject();
				((JsonObject)output).SetModel((IDictionary<string, object>)result);
			}
			else if (result is ICollection || result is Array)
			{
				var itemList = new List<object>();
				foreach (var item2 in (IEnumerable)result)
					itemList.Add(WrapObjectIfNessisary(item2));

				output = itemList;
			}

			return output;
		}

		public override IEnumerable<string> GetDynamicMemberNames()
		{
			return _model.Keys;
		}

		public override bool TryConvert(ConvertBinder binder, out object result)
		{
			result = null;

			if (!binder.Type.IsAssignableFrom(_model.GetType()))
				throw new InvalidOperationException(String.Format(@"Unable to convert to ""{0}"".", binder.Type));

			result = _model;
			return true;
		}

		protected bool TryGetProperty(string name, out object result)
		{
			if (!TryGetValue(name, out result) && _throwErrorOnMissingMethod)
				throw new MissingMemberException(String.Format(@"Member ""{0}"" was not found in the body of the JSON posted.", name));

			result = WrapObjectIfNessisary(result);

			return true;
		}

		protected bool TryGetProperty(string name, out object result, Type resultType)
		{
			TryGetProperty(name, out result);

			if (result == null)
				return true;

			if (Type.GetTypeCode(result.GetType()) != TypeCode.Object)
				result = Convert.ChangeType(result, resultType);

			return true;
		}

		public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
		{
			var key = GetSingleIndexOrNull(indexes);

			return TryGetProperty(key, out result, binder.ReturnType);
		}

		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			return TryGetProperty(binder.Name, out result, binder.ReturnType);
		}

		public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
		{
			result = _model.GetType().InvokeMember(binder.Name, BindingFlags.InvokeMethod, null, _model, args);
			result = WrapObjectIfNessisary(result);

			return true;
		}

		public override string ToString()
		{
			var serializer = new JavaScriptSerializer();
			return serializer.Serialize(_model);
		}

		#region IDictionary<string,object> Members

		void IDictionary<string, object>.Add(string key, object value)
		{
			throw new NotImplementedException();
		}

		bool IDictionary<string, object>.ContainsKey(string key)
		{
			return _model.ContainsKey(key);
		}

		ICollection<string> IDictionary<string, object>.Keys
		{
			get { return _model.Keys; }
		}

		bool IDictionary<string, object>.Remove(string key)
		{
			throw new NotImplementedException();
		}

		bool IDictionary<string, object>.TryGetValue(string key, out object value)
		{
			return _model.TryGetValue(key, out value);
		}

		ICollection<object> IDictionary<string, object>.Values
		{
			get { return _model.Values; }
		}

		object IDictionary<string, object>.this[string key]
		{
			get { return _model[key]; }
			set
			{
				if (!_model.ContainsKey(key))
				{
					throw new NotImplementedException();
				}
				_model[key] = value;
			}
		}

		#endregion

		#region ICollection<KeyValuePair<string,object>> Members

		void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
		{
			throw new NotImplementedException();
		}

		void ICollection<KeyValuePair<string, object>>.Clear()
		{
			throw new NotImplementedException();
		}

		bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
		{
			return _model.Contains(item);
		}

		void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
		{
			_model.CopyTo(array, arrayIndex);
		}

		int ICollection<KeyValuePair<string, object>>.Count
		{
			get { return _model.Count; }
		}

		bool ICollection<KeyValuePair<string, object>>.IsReadOnly
		{
			get { return true; }
		}

		bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IEnumerable<KeyValuePair<string,object>> Members

		IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
		{
			return _model.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _model.GetEnumerator();
		}
		#endregion
	}
}