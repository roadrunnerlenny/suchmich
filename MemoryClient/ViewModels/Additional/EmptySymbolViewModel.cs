using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryClient.ViewModels
{
	public class EmptySymbolViewModel : SymbolViewModel
	{
		public new string Name
		{
			get { return "LeerKarte"; }
		}

		public new bool HasName
		{
			get { return false; }
		}

		public new string FileName
		{
			get { return String.Empty; }
		}

		public new bool HasFileName
		{
			get { return false; }
		}

		public EmptySymbolViewModel()
		{ }

		public override string ToString()
		{
			return this.Name;
		}
	}
}
