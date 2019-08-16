using Microsoft.VisualBasic;
using System;
namespace ToyTron
{
	public class MemoryLocation
	{
		#region "Private Variables"
		private MemoryLocation _Prev;
		private MemoryLocation _Next;
		private MemoryWord _Value;
		private int _ID;
			#endregion
		private bool _HasError;
		#region "Public Properties"
		public MemoryLocation PreviousLocation {
			get { return _Prev; }
			set { _Prev = value; }
		}
		public MemoryLocation NextLocation {
			get { return _Next; }
			set { _Next = value; }
		}
		public int ID {
			get { return _ID; }
			set { _ID = value; }
		}
		public bool HasError {
			// if the memory word had an error
			get { return _HasError; }
			set { _HasError = value; }
		}
		#endregion
		#region "Public Functions"
		public MemoryWord Word()
		{
                return _Value;
		}
		#endregion
		#region "Public Sub"
		public MemoryLocation()
		{
			
		}
		public MemoryLocation(string newWordString)
		{
			// create a new word from the given word string.
			_Value = new MemoryWord(newWordString);
		}
		public void Word(MemoryWord newWord)
		{
			// sets the word
			_Value = newWord;
		}
		public void Word(string newWordString)
		{
			//set word from string
			try {
				MemoryWord newWord = new MemoryWord(newWordString);
				_Value = newWord;
			} catch (ArgumentException ex) {
				throw ex;
			} catch (Exception exAll) {
				throw new Exception("Unknown Exception");
			}

		}
		#endregion
	}
}