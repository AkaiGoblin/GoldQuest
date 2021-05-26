using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Commands
{
	public class MoveLeftCommand : Command
	{
		public MoveLeftCommand(Ninja ninjaPlayer): base(ninjaPlayer) { }
		protected override bool MoveInDirection()
		{
			_ninjaPlayer.MoveLeft();
			return true;
		}
	}
}
