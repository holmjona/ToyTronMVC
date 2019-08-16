using Microsoft.VisualBasic;
using System;
namespace ToyTron
{
	public class MemoryWord
	{
		#region "Private Variables"
		private int _Value;
		//Private _Instruction As Integer
		//Private _MemoryLocationID As Integer
		#endregion

		#region "Public Properties"
		public int Instruction {
			//return only the instruction from the word
			get { return _Value / 100; }
		}
		public int MemoryLocationID {
			//return only the memorylocation from the word
			get { return _Value % 100; }
		}
		public string WordString {
			get {
				// word string always nees 4 chars so if less than one, leave space for "-" sign.
				if (_Value < 0) {
					return _Value.ToString("000");
				} else {
					return _Value.ToString("0000");
				}
			}
			set {
				//try {
                   if(! int.TryParse(value, out _Value)){
					//_Value = (int)value;
				// if gienv string can not be converted to an integer
				//} catch {
					throw new ArgumentException("Word not in correct format");
				}
			}
		}
		public int Value {
			get { return _Value; }
		}
		#endregion
		#region "Public Sub"
		public MemoryWord()
		{
			
		}
		public MemoryWord(string memString)
		{
			WordString = memString;
		}
		#endregion
	}
}