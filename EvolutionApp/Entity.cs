namespace EvolutionApp
{
	public abstract class Entity
	{
		public Coordinates Coordinates { get; set; }
		public Owner TypeEntity { get; set; }

		public Entity()
		{
			Coordinates = new Coordinates();
		}		
	}
}