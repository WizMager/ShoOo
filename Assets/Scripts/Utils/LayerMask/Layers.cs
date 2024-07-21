namespace Utils.LayerMask
{
    public class Layers
	{
		public const string Enemy = "Enemy";

		private static readonly Layer _enemyLayer = new (Enemy);

		public static int EnemyLayer => _enemyLayer.Id;

		private class Layer
		{
			private readonly string _name;

			private int? _id;

			public int Id
			{
				get
				{
					_id ??= UnityEngine.LayerMask.NameToLayer(_name);
					
					return _id.Value;
				}
			}

			public Layer(string name)
			{
				_name = name;
			}
		}
	}
}