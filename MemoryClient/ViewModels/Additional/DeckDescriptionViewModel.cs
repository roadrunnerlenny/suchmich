using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfFundamentals.ViewModelBase;
using MemoryGameLogicLib.DataReader;

namespace MemoryClient.ViewModels
{
    public class DeckDescriptionViewModel : ViewModel<DeckDescription>, IEquatable<DeckDescriptionViewModel>, IEquatable<DeckDescription>
    {   
        public string Name
        {
            get { return Model.Name; }
            set
            {
                if (Name != value)
                {
                    Model.Name = value;
                    OnPropertyChanged(() => this.Name);
                }
            }
        }

        public string FileName
        {
            get { return Model.FileName; }
            set
            {
                if (FileName != value)
                {
                    Model.FileName = value;
                    OnPropertyChanged(() => this.FileName);
                }
            }
        }

        public DeckDescriptionViewModel() : base(new DeckDescription())
        { }

        public DeckDescriptionViewModel(DeckDescription model) : base(model)
        { }

        public DeckDescriptionViewModel(DeckDescriptionViewModel other) : base(other)
        { }

        public bool Equals(DeckDescriptionViewModel other)
        {
            return Model.Equals(other.Model);
        }

        public bool Equals(DeckDescription other)
        {
            return Model.Equals(other);
        }
    }
}
