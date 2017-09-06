using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MemoryGameLogicLib.Model;
using WpfFundamentals.ViewModelBase;

namespace MemoryClient.ViewModels
{
	public class SymbolViewModel : ViewModel<Symbol>, IEquatable<SymbolViewModel>, IEquatable<Symbol>
	{
		public string Name
		{
			get { return Model.Name; }
		}

		public bool HasName
		{
			get
			{
				return Model.HasName;
			}
		}

		public string FileName
		{
            get { return Model.FileName; }
		}

		public bool HasFileName
		{
			get { return Model.HasFileName; }
		}

		public SymbolViewModel() : base(new Symbol(String.Empty))
		{ }

		public SymbolViewModel(Symbol model) : base(model)
		{ }

		public SymbolViewModel(SymbolViewModel other) : base(other)
		{ }

		public override string ToString()
		{
			return Model.ToString();
		}

		public bool Equals(SymbolViewModel other)
		{
			if (other != null)
				return other.Model.Equals(this.Model);
			else
				return false;
		}

		#region IEquatable<Symbol> Member

		public bool Equals(Symbol other)
		{
			if (other != null)
				return other.Equals(this.Model);
			else
				return false;
		}

		#endregion
	}
}
