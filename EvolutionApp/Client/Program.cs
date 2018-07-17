using System;

namespace EvolutionApp
{
	class Program
	{
		static void Main(string[] args)
		{
			const int row = 5, column = 5;
			var life = new EvolutionSimulation().CreateWorld(row, column);
			var predator1 = new Predator();
			var predator2 = new Predator();
			var herb1 = new Herbivorous();
			var plant1 = new Plant();
			var plant2 = new Plant();
			var plant3 = new Plant();

			life.SetEntity(predator1);
			life.SetEntity(herb1);
			life.SetEntity(predator2);
			life.SetEntity(plant1);
			life.SetEntity(plant2);
			life.SetEntity(plant3);


			string key = string.Empty;
			do
			{
				for (int i = 0; i < life.World.GetLength(0); i++)
				{
					for (int j = 0; j < life.World.GetLength(1); j++)
					{
						Console.WriteLine($"[{i}]-[{j}] : {life.World[i, j].StateCell}");
					}
				}
				key = Console.ReadLine();
				life.NextGeneration();
			} while (key == "n");
		}
	}
}
