using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MemoryGameLogicLib.Exceptions;
using System.Runtime.Serialization;

namespace MemoryGameLogicLib.Model
{
	[DataContract]
	public class BoardDimensions
	{
		[DataMember]
		public ushort? Width { get; set; }

		[DataMember]
		public ushort? Height { get; set; }
		
		public bool HasValidDimensions
		{
			get
			{
				try
				{
					VerifyDimensions();
					return true;
				}
				catch
				{
					return false;
				}
			}
		}

		public BoardDimensions(ushort width, ushort height)
		{
			this.Width = width;
			this.Height = height;
			VerifyDimensions();
		}

		public void VerifyDimensions()
		{
			if (!Width.HasValue) throw new InvalidBoardDimensionsException("Define a value for width");
			if (!Height.HasValue) throw new InvalidBoardDimensionsException("Define a value for height");
			if ((Width * Height % 2) != 0) throw new InvalidBoardDimensionsException("The number of cards must be even");
		}
	}
}

