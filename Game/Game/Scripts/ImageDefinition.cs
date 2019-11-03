using System;

namespace Game.Scripts
{
	[Serializable]
	public class ImageDefinition
	{
		public readonly ImagePool.FrontTypes frontImageType = default;
		public readonly int frontImageId = 0;
		public readonly ImagePool.BackTypes backImageType = default;

		public ImageDefinition(ImagePool.FrontTypes frontImageName, int frontImageId, ImagePool.BackTypes backImageName)
		{
			this.frontImageType = frontImageName;
			this.frontImageId = frontImageId;
			this.backImageType = backImageName;
		}
	}
}
