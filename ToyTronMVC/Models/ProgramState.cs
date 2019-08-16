using Microsoft.VisualBasic;
namespace ToyTron
{
	public class ProgramState
	{
		//Class is for easy way of knowing what integer values returned from Emulator Object mean what.
		#region "Private Variables"
			#endregion
		private State _Value;
		#region "Overloaded Operators"
		public static bool operator ==(ProgramState instObject, State intValue)
		{
			return instObject.Value == intValue;
		}
		public static bool operator !=(ProgramState instObject, State intValue)
		{
			return instObject.Value != intValue;
		}
		#endregion
		public enum State : int
		{
			NotStarted = -3,
			GetInput = -2,
			WriteOutput = -1,
			Running = 0,
			Finished = 1
		}
		#region "Public Subs"
		public ProgramState()
		{
			
		}
		public ProgramState(State stateValue)
		{
			_Value = stateValue;
		}
		//Public Sub New(ByVal stateValue As Integer)
		//    Me.New()
		//    _Value = stateValue
		//End Sub
		#endregion
		#region "Public Properties"
		protected State Value {
			get { return _Value; }
		}
		#endregion
		#region "Shared Properties"
		//Shared ReadOnly Property NotStarted() As Integer
		//    Get
		//        Return -3
		//    End Get
		//End Property
		//Shared ReadOnly Property GetInput() As Integer
		//    Get
		//        Return -2
		//    End Get
		//End Property
		//Shared ReadOnly Property WriteOutput() As Integer
		//    Get
		//        Return -1
		//    End Get
		//End Property
		//Shared ReadOnly Property Running() As Integer
		//    Get
		//        Return 0
		//    End Get
		//End Property
		//Shared ReadOnly Property Finished() As Integer
		//    Get
		//        Return 1
		//    End Get
		//End Property

		#endregion
	}
}