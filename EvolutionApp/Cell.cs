namespace EvolutionApp
{
	public class Cell
	{
		public Coordinates CoordinatesCell { get; set; }
		public Owner StateCell { get; set; }

		public Cell()
		{
			CoordinatesCell	= new Coordinates();
			StateCell = Owner.Empty;
		}

		public Cell(int n, int m)
		{
			CoordinatesCell = new Coordinates(n,m);
		}
	}
}