using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace LoginDemo.Views
{
	public abstract class BaseCommandBindingProxy
	{
		public static implicit operator CommandBinding(BaseCommandBindingProxy proxy)
		{
			return proxy.CommandBinding;
		}

		public abstract CommandBinding CommandBinding { get; }
	}
}
