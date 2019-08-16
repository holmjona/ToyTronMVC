using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Accumulator
/// </summary>
public class Accumulator
{
	#region "Private Variables"
			#endregion
		private int _Value;
		#region "Public Properties"
		public int Value {
			//Get and set the value of the accumulator
			get { return _Value; }
			set { _Value = value; }
		}
		#endregion
		#region "Public Functions"

		#endregion
		#region "Public Sub"
		public void Add(int numberToAdd)
		{
			//adds given value to the preexisting value
			_Value += numberToAdd;
		}
		public void Subtract(int numberToSubtract)
		{
			//subtracts given value from the preexisting value
			_Value -= numberToSubtract;
		}
		public void Divide(int numberToBeDivisor)
		{
			//Divides the the preexisting value by the number given
			Value = (int)_Value / numberToBeDivisor;
		}
		public void Multiply(int numberToMultiplyBy)
		{
			//Multiplys the preexisting value by the number given
			_Value *= numberToMultiplyBy;
		}
		#endregion


}