using Editor;
using Sandbox;
using Sandbox.Internal;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
namespace Ian.Commentary
{

	/// <summary>
	/// A Commentary Node. Used for developer insight into maps, gameplay mechanics, etc.
	/// </summary>
	[HammerEntity, Library( "point_commentary_node" ), EditorModel( "models/sbox_props/watermelon/watermelon.vmdl" ), Display( Name = "Commentary Node" ), Category( "Developer" ), Icon( "radio_button_checked" )]
	public partial class CommentaryNode : Prop, IUse
	{
		public bool Enable { get; set; } = false;

		[Property, ResourceType( "vmdl" )]
		public string Model { get; set; }

		[Property, ResourceType( "sound" )]
		public string CommentarySound { get; set; }

		private Sound Commentary;

		public override void Spawn()
		{
			base.Spawn();
			CheckSnd();
			SetModel( Model );
			SetupPhysicsFromModel(PhysicsMotionType.Keyframed, true);
		}

		public bool IsUsable(Entity user)
		{
			return true;
		}

		public bool OnUse(Entity user)
		{
			if (user is Player player)
			{
				CheckSnd();
				Log.Info( $"Used Commentary node! now playing commentary file: {CommentarySound} " );
			}


			PlaySound("ui.button.press");

			return false;
		}

		public void CheckSnd()
		{
			if (Enable == true)
			{
				Enable = false;
				Commentary = base.PlaySound( CommentarySound );
			}
			else
			{
				Enable = true;
				Commentary.Stop();
			}
		}

		protected override void OnDestroy()
		{
			Commentary.Stop();
			base.OnDestroy();
		}

		public void Tick()
		{

		}
	}
}
