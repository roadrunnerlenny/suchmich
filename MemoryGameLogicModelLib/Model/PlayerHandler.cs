using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MemoryGameLogicLib.Exceptions;
using System.Runtime.Serialization;

namespace MemoryGameLogicLib.Model
{
	[DataContract]
	public class PlayerHandler
	{
		[DataMember]
		public LinkedList<Player> AllPlayers { get; set; }

		public bool HasAllPlayers 
		{
			get
			{
				return AllPlayers != null & AllPlayers.Count > 0;
			}
		}

		[DataMember]
		public Player CurrentPlayer { get; set; }

		public bool HasCurrentPlayer
		{
			get
			{
				return CurrentPlayer != null;
			}
		}

		public PlayerHandler()
		{
			AllPlayers = new LinkedList<Player>();
			
		}

		public void DiscardCards(Card card1, Card card2)
		{
			if (HasCurrentPlayer)
				CurrentPlayer.Rack.DiscardCards(card1, card2);
			else
				throw new NoPlayerActiveException();
		}

		public void InitPlayers(List<Player> players)
		{
			AllPlayers = new LinkedList<Player>(players);
			CurrentPlayer = AllPlayers.First.Value;
		}

		public void NextTurn()
		{
			CurrentPlayer = GetNextPlayer();
		}
		
		Player GetNextPlayer()
		{
			var next = AllPlayers.Find(CurrentPlayer).Next;
			if (next != null)
				return next.Value;
			else 
				return AllPlayers.First.Value;
		}
	}
}
