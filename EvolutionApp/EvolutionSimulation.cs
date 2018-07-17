using System;
using System.Collections.Generic;

namespace EvolutionApp
{
	public class EvolutionSimulation
	{
		public Cell[,] World { get; set; }
		public int N { get; set; }
		public int M { get; set; }
		public List<Entity> ListEntities { get; set; } 
		private Random rand = new Random((int)DateTime.Now.Ticks);
		public EvolutionSimulation()
		{
		}

		public EvolutionSimulation CreateWorld(int n, int m)
		{
			N = n - 1;
			M = m - 1;
			World = InitializationCells(n, m);
			ListEntities = new List<Entity>();
			return this;
		}

		public void SetEntity(Entity entity)
		{			
			var isEmpty = false;
			while (!isEmpty)
			{
				var nIndex = rand.Next(N);
				var mIndex = rand.Next(M);
				if (World[nIndex, mIndex].StateCell == Owner.Empty)
				{
					isEmpty = true;
					World[nIndex, mIndex] = CreateCell(entity, nIndex, mIndex);
					entity.Coordinates = new Coordinates(nIndex, mIndex);
					ListEntities.Add(entity);
				}
			}			
		}

		public void NextGeneration()
		{
			foreach(var entity in ListEntities)
			{
				if (entity is Predator)
				{
					var freeCell = GetFreeCell(entity.Coordinates);
					MoveEntity(freeCell.CoordinatesCell, entity.Coordinates, entity);
				}
				else if (entity is Herbivorous)
				{
					var freeCell = GetFreeCell(entity.Coordinates);
					MoveEntity(freeCell.CoordinatesCell, entity.Coordinates, entity);
				}
				else
				{
					continue;
				}
			}
		}

		private void MoveEntity(Coordinates newCoordinate, Coordinates oldCoordinate, Entity entity)
		{
			World[newCoordinate.X, newCoordinate.Y].StateCell = entity.TypeEntity;
			World[oldCoordinate.X, oldCoordinate.Y].StateCell = Owner.Empty;
			entity.Coordinates = newCoordinate;
		}

		private Cell GetFreeCell(Coordinates coordinate)
		{
			var isEmptyCell = false;
			var newCoordinate = new Coordinates(coordinate.X, coordinate.Y);
			do
			{
				var deltaX = rand.Next(-1, 2);
				var deltaY = rand.Next(-1, 2);
				newCoordinate.X += deltaX;
				newCoordinate.Y += deltaY;
				isEmptyCell = IsEmptyCell(newCoordinate);
				if (!isEmptyCell)
					newCoordinate = new Coordinates(coordinate.X, coordinate.Y);
			} while (!isEmptyCell);
			return World[newCoordinate.X, newCoordinate.Y];
		}

		private bool IsEmptyCell(Coordinates coordinate)
		{
			if (coordinate.X <= 0 || coordinate.Y <= 0)
				return false;
			if (coordinate.X > N || coordinate.Y > M)
				return false;
			var isEmptyCell = World[coordinate.X, coordinate.Y].StateCell == Owner.Empty;
			return isEmptyCell;
		}

		private Cell CreateCell(Entity entity, int nIndex, int mIndex)
		{			
			var cell = new Cell(nIndex, mIndex);
			if (entity.TypeEntity == Owner.Predator)
			{
				cell.StateCell = Owner.Predator;
			}
			else if (entity.TypeEntity == Owner.Herbivorous)
			{
				cell.StateCell = Owner.Herbivorous;
			}
			else
			{
				cell.StateCell = Owner.Plant;
			}
			return cell;
		}

		private Cell[,] InitializationCells(int n, int m)
		{
			var a = new Cell[n, m];
			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < m; j++)
				{
					a[i, j] = new Cell()
					{
						CoordinatesCell = new Coordinates(i, j)
					};
				}
			}
			return a;
		}
	}
}