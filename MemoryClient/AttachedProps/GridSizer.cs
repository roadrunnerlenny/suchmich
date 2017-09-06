using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MemoryClient.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MemoryClient.AttachedProps
{	
	public class GridSizer
	{
		public static BoardDimensionsViewModel GetBoardDimensionsForRows(DependencyObject obj)
		{
			return (BoardDimensionsViewModel)obj.GetValue(BoardDimensionsForRowsProperty);
		}

		public static void SetBoardDimensionsForRows(DependencyObject obj, BoardDimensionsViewModel value)
		{
			obj.SetValue(BoardDimensionsForRowsProperty, value);
		}

		// Using a DependencyProperty as the backing store for BoardDimensionsForRows.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty BoardDimensionsForRowsProperty =
			DependencyProperty.RegisterAttached("BoardDimensionsForRows", typeof(BoardDimensionsViewModel), typeof(GridSizer), 
			new UIPropertyMetadata(BoardDimensionsForRows_Callback));

		public static BoardDimensionsViewModel GetBoardDimensionsForColumns(DependencyObject obj)
		{
			return (BoardDimensionsViewModel)obj.GetValue(BoardDimensionsForColumnsProperty);
		}

		public static void SetBoardDimensionsForColumns(DependencyObject obj, BoardDimensionsViewModel value)
		{
			obj.SetValue(BoardDimensionsForColumnsProperty, value);
		}

		// Using a DependencyProperty as the backing store for BoardDimensionsForColumns.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty BoardDimensionsForColumnsProperty =
			DependencyProperty.RegisterAttached("BoardDimensionsForColumns", typeof(BoardDimensionsViewModel), typeof(GridSizer),
			new UIPropertyMetadata(BoardDimensionsForColumns_Callback));


		public static void BoardDimensionsForRows_Callback(DependencyObject d, DependencyPropertyChangedEventArgs e) {
			Grid grid = d as Grid;
			BoardDimensionsViewModel dimensions = d.GetValue(BoardDimensionsForRowsProperty) as BoardDimensionsViewModel;			
			if (dimensions == null || grid == null) return;
			SetRowDefinitionsForGrid(grid, dimensions);
		}

		static void SetRowDefinitionsForGrid(Grid grid, BoardDimensionsViewModel dimensions)
		{
			grid.RowDefinitions.Clear();
			for (ushort row = 0; row < dimensions.Height; row++)
			{
				grid.RowDefinitions.Add(new RowDefinition()
				{
					Height = new GridLength(1, GridUnitType.Star)//GridLength.Auto
				});				
			}			
		}

		public static void BoardDimensionsForColumns_Callback(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Grid grid = d as Grid;
			BoardDimensionsViewModel dimensions = d.GetValue(BoardDimensionsForColumnsProperty) as BoardDimensionsViewModel;
			if (dimensions == null || grid == null) return;
			SetColumnDefinitionsForGrid(grid, dimensions);
		}

		static void SetColumnDefinitionsForGrid(Grid grid, BoardDimensionsViewModel dimensions)
		{
			grid.ColumnDefinitions.Clear();
			for (ushort column = 0; column < dimensions.Width; column++)
			{				
				grid.ColumnDefinitions.Add(new ColumnDefinition()
				{
					Width = new GridLength(1, GridUnitType.Star)//GridLength.Auto
				});
			}
		}
	}
}
