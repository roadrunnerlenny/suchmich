using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MemoryGameLogicLib.Model;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace MemoryGameLogicLib.DataReader
{
	static class DeckReader 
	{
		const string DataFile = @"Decks/CardDeck.xml";
        const string AltDataFile = @"CardDeck.xml";

		static XDocument Doc
		{
			get
			{
                if (_doc == null)
                {
                    if (File.Exists(DataFile))
                        _doc = XDocument.Load(DataFile);
                    else if (File.Exists(AltDataFile))
                        _doc = XDocument.Load(AltDataFile);
                    else
                        throw new FileNotFoundException("Es wurde keine Datei mit der Kartenbeschreibung gefunden!");
                }
				return _doc;
			}
		}
		static XDocument _doc;


		internal static List<DeckDescription> ReadDeckNames()
		{	
			return (from deck in Doc.Descendants("Deck")
			        select new DeckDescription() {
                        Name = deck.Attribute("name").Value.ToString(),
                        FileName = "../"+ deck.Attribute("image").Value.ToString()
                    }).ToList();
		}

		internal static IList<CardSet> ReadCardsFromData(ushort numberOfSets, string deckName)
		{
			List<CardSet> result = GetAllCardSets(deckName);

			AddDirectoryToFileNames(result, DeckReader.ReadDirectory(deckName));

			return result.TakeRandom(numberOfSets);
		}

		private static List<CardSet> GetAllCardSets(string deckName)
		{
			return (from card in
										(from deck in Doc.Descendants("Deck")
										 where deck.Attribute("name").Value.ToString().ToLower().Equals(deckName.ToLower())
										 select deck).Descendants("CardSet")
									select CreateCardSetFromNode(card)).ToList();
		}

		static CardSet CreateCardSetFromNode(XElement card)
		{			
			return new CardSet(
				new Card()
				{
					CardSymbol = new Symbol(card.Element("Name").Value)
					{
						FileName = card.Element("FileName").Value
					}
				},
				new Card()
				{
					CardSymbol = new Symbol(card.Element("Name").Value)
					{
						FileName = card.Element("FileName").Value
					}
				}
				);			
		}

		private static void AddDirectoryToFileNames(List<CardSet> result, string dir)
		{
			foreach (CardSet cardSet in result)
			{
				cardSet.Card1.CardSymbol.FileName = dir + cardSet.Card1.CardSymbol.FileName;
				cardSet.Card2.CardSymbol.FileName = dir + cardSet.Card2.CardSymbol.FileName;
			}
		}

		internal static string ReadDirectory(string deckName)
		{
			string dir = (from deck in Doc.Descendants("Deck")
					where deck.Attribute("name").Value.ToString().ToLower().Equals(deckName.ToLower())
					select deck.Attribute("directory").Value.ToString()).FirstOrDefault();

			return AddSuffixesToDirectoryName(dir);
		}

		private static string AddSuffixesToDirectoryName(string dir)
		{
			dir = "../" + dir;

			if (!dir.EndsWith("/") || !dir.EndsWith(@"\"))
				dir += "/";

			return dir;
		}
	}
}
