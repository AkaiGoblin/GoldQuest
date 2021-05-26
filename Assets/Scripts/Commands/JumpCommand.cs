using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Commands
{
	public class JumpCommand : Command
	{
		public JumpCommand(Ninja ninjaPlayer) : base(ninjaPlayer) { }
		protected override bool MoveInDirection()
		{
			_ninjaPlayer.Jump();
			return true;
		}
	}
}
