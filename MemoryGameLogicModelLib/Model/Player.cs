using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MemoryGameLogicLib.Model
{
	[DataContract]
	public class Player
	{
		[DataMember]
		public CardRack Rack { get; set; }

		[DataMember]
		public string Name { get; set; }

        [DataMember]
        public string AvatarFileName { get; set; }

		[DataMember]
		public int MissCount { get; set; }
				
		public Player(string name)
		{
			this.Name = name;
			Init();
		}

		public void Init()
		{
			this.Rack = new CardRack(this);
		}

		public override string ToString()
		{
			return this.Name;
		}
	}
}
