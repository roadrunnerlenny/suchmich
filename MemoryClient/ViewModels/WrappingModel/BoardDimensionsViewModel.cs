using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfFundamentals.ViewModelBase;
using MemoryGameLogicLib.Model;
using System.Collections.ObjectModel;
using WpfFundamentals.Helper;

namespace MemoryClient.ViewModels
{
    public class BoardDimensionsViewModel : ViewModel<BoardDimensions>
    {
        public ushort? Width
        {
            get { return Model.Width; }
            set
            {
                if (Width != value)
                {
                    Model.Width = value;
                    OnPropertyChanged(() => this.Width);
                    OnPropertyChanged(() => this.HasValidDimensions);
                }
            }
        }

        public ushort? Height
        {
            get { return Model.Height; }
            set
            {
                if (Height != value)
                {
                    Model.Height = value;
                    OnPropertyChanged(() => this.Height);
                    OnPropertyChanged(() => this.HasValidDimensions);
                }
            }
        }

        public bool HasValidDimensions
        {
            get
            {
                return Model.HasValidDimensions;
            }
        }

        public string DisplayValue
        {
            get
            {
                return this.Width + " x " + this.Height;
            }
        }

        public string DisplayImage
        {
            get { return _displayImage; }
            set
            {
                if (DisplayImage != value)
                {
                    _displayImage = value;
                    OnPropertyChanged(() => this.DisplayImage);
                }
            }
        }
        string _displayImage;

        public BoardDimensionsViewModel()
            : base(new BoardDimensions(0, 0))
        { }

        public BoardDimensionsViewModel(BoardDimensions model)
            : base(model)
        { }

        public BoardDimensionsViewModel(BoardDimensionsViewModel other)
            : base(other)
        { }

        public void VerifyDimensions()
        {
            Model.VerifyDimensions();
        }
    }
}
