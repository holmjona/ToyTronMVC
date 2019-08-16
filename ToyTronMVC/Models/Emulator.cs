using Microsoft.VisualBasic;
using System.Collections.Generic;
using System;
namespace ToyTron
{
    public class Emulator
    {
        #region "Private Variables"
        private Memory _Program;
        //MemoryLocation 
        private Pointer _Pointer = new Pointer();
        private Accumulator _Accumulator;
        private bool _ProgramRunning;
        private bool needToAdvancePointer = false;
        #endregion
        private int _Output;
        #region "Public Properties"
        public int Output
        {
            //return the Output value stored.
            get { return _Output; }
        }
        public string CurrentProgramAsText
        {
            //return the Memory as a Text.
            get
            {
                string retString = "";
                ToyTron.MemoryLocation mLoc;
                mLoc = _Program.FirstLocation;
                if (mLoc != null)
                {
                    do
                    {
                        retString += mLoc.ID.ToString("00") + " : " + mLoc.Word().WordString + "\r\n";
                        mLoc = mLoc.NextLocation;
                    } while (mLoc != null);
                }
                return retString;
            }
        }
        public List<MemoryLocation> CurrentProgramAsList
        {
            //return the Memory as a Text.
            get
            {
                List<MemoryLocation> retList = new List<MemoryLocation>();
                ToyTron.MemoryLocation mLoc;
                mLoc = _Program.FirstLocation;
                if (mLoc != null)
                {
                    do
                    {
                        retList.Add(mLoc);
                        mLoc = mLoc.NextLocation;
                    } while (mLoc != null);
                }
                return retList;
            }
        }
        public Memory CurrentProgram
        {
            //return the Memory as a Text.
            get { return _Program; }
        }
        public string GetCurrentInstruction
        {
            //return the Current Memory Word Value.
            get { return _Pointer.Word().WordString; }
        }
        public MemoryLocation Pointer
        {
            //return pointer
            get { return _Pointer; }
        }
        public int RequestedInput
        {
            //places the given value into the current memory location
            set
            {
                MemoryLocation tempLoc;
                tempLoc = _Program.Locations(_Pointer.Word().MemoryLocationID);
                tempLoc.Word(value.ToString("0000"));
                //advancePointer()
            }
        }
        public bool Run
        {
            //Return if Program is running
            get { return _ProgramRunning; }
        }
        public ProgramState CurrentState
        {
            get
            {
                //Return the State of the program at its current Pointer
                if (_Pointer.Word() == null)
                    return new ProgramState(ProgramState.State.NotStarted);
                switch (_Pointer.Word().Instruction)
                {
                    case  // ERROR: Case labels with binary operators are unsupported : Equality
Instruction.Read:
                        return new ProgramState(ProgramState.State.GetInput);
                    case  // ERROR: Case labels with binary operators are unsupported : Equality
Instruction.Write:
                        return new ProgramState(ProgramState.State.WriteOutput);
                    case  // ERROR: Case labels with binary operators are unsupported : Equality
Instruction.Halt:
                        return new ProgramState(ProgramState.State.Finished);
                    default:
                        return new ProgramState(ProgramState.State.Running);
                }
                //If _Pointer.Word Is Nothing Then Return New ProgramState(ProgramState.NotStarted)
                //Select Case _Pointer.Word.Instruction
                //    Case Is = Instruction.Read
                //        Return New ProgramState(ProgramState.GetInput)
                //    Case Is = Instruction.Write
                //        Return New ProgramState(ProgramState.WriteOutput)
                //    Case Is = Instruction.Halt
                //        Return New ProgramState(ProgramState.Finished)
                //    Case Else
                //        Return New ProgramState(ProgramState.Running)
                //End Select
            }
        }
        public Accumulator Accumulator
        {
            get { return _Accumulator; }
        }
        #endregion
        #region "Public Functions"
        public int Execute()
        {
            try
            {
                if (!Run)
                {
                    //if not running then restart program and set program running to true
                    setProgramToStartPosition();
                    _ProgramRunning = true;
                }
                else
                {
                    throw new Exception("Progam Not Running");
                }
                //return 1 means successful
                return 1;
            }
            catch (Exception ex)
            {
                //return -1 means error
                return -1;
            }
        }
        public ProgramState DoNext()
        {
            // does next step until a state other than running occurs
            do
            {
                if (needToAdvancePointer)
                {
                    _Pointer.Advance();
                }
                else
                {
                    needToAdvancePointer = true;
                }
                ProgramState currentProgramState;
                currentProgramState = handleLocation(ref _Pointer);
                //handle current instruction set.
                if (currentProgramState != ProgramState.State.Running)
                    return currentProgramState;
                //Overloaded Operator here
            } while (_ProgramRunning);
            return new ProgramState(ProgramState.State.Finished);
        }
        public ProgramState StepNext()
        {
            //If CurrentState <> ProgramState.Finished Or CurrentState <> ProgramState.GetInput Then _Pointer.Advance()
            //needToAdvancePointer = True
            if (needToAdvancePointer && CurrentState != ProgramState.State.Finished)
            {
                _Pointer.Advance();
            }
            else
            {
                needToAdvancePointer = true;
            }
            return handleLocation(ref _Pointer);
            //handle current instruction set.
        }

        #endregion
        #region "Private Functions"
        private ProgramState handleLocation(ref Pointer myPoint)
        {
            MemoryLocation storageLocation;
            storageLocation = _Program.Locations(myPoint.Word().MemoryLocationID);
            switch (myPoint.Word().Instruction)
            {
                case  Instruction.Read:
                    //Read a word from the keyboard (input box) into a specific location in memory.
                    return new ProgramState(ProgramState.State.GetInput);
                case  Instruction.Write:
                        //Write a word from a specific memory location to the screen (msgbox).
                        _Output = storageLocation.Word().Value;
                        return new ProgramState(ProgramState.State.WriteOutput);
                case  Instruction.Load:
                    //Load a word from a specific location in memory into the accumulator in the CPU.
                    _Accumulator.Value = storageLocation.Word().Value;
                    break;
                case  Instruction.Store:
                    //Store a word from the accumulator into a specific location in memory.
                    storageLocation.Word().WordString = _Accumulator.Value.ToString();
                    break;
                case  Instruction.Add:
                    //Add a word from a specific location in memory to the word in the accumulator, 
                    //leaving the results in the accumulator.
                    _Accumulator.Add(storageLocation.Word().Value);
                    break;
                case  Instruction.Substract:
                    //Subtract a word from a specific location in memory from the word in the accumulator, 
                    //leaving the results in the accumulator.
                    _Accumulator.Subtract(storageLocation.Word().Value);
                    break;
                case  Instruction.Divide:
                    //Divide a word from a specific location in memory into the word in the accumulator, 
                    //leaving the results in the accumulator.
                    _Accumulator.Divide(storageLocation.Word().Value);
                    break;
                case  Instruction.Multiply:
                    //Multiple a word from a specific location in memory by the word in the accumulator, 
                    //leaving the results in the accumulator.
                    _Accumulator.Multiply(storageLocation.Word().Value);
                    break;
                case  Instruction.Branch:
                    //Branch (goto) a specific location in memory.
                    goToBranch(storageLocation);
                    break;
                case  Instruction.BranchNegative:
                    //Branch to a specific location in memory IFF (If and only if) the value in the 
                    //accumulator is negative
                    if (_Accumulator.Value < 0)
                        goToBranch(storageLocation);
                    break;
                case  Instruction.BranchZero:
                    //Branch to a specific location in memory IFF the value in the accumulator is Zero.
                    if (_Accumulator.Value == 0)
                        goToBranch(storageLocation);
                    break;
                case Instruction.Halt:
                    //Halt - The program has completed its task.
                    _ProgramRunning = false;
                    return new ProgramState(ProgramState.State.Finished);
                default:
                    // 00  No instruction given
                    return new ProgramState(ProgramState.State.Running);
            }
            return new ProgramState(ProgramState.State.Running);
        }
        #endregion
        #region "Public Sub"
        public void AddMemoryToEmulator(Memory mem)
        {
            //sets program pointer to memory object
            _Program = mem;
        }
        public void Restart()
        {
            //wrapper for user to restart the program
            setProgramToStartPosition();
        }
        #endregion
        #region "Private Subs"
        private void setRunToFalseIfEnded()
        {
            //sets program running variable to false if the program is at the end.
            if (_Pointer == null)
            {
                _ProgramRunning = false;
            }
        }
        private void setProgramToStartPosition()
        {
            //restarts the program by setting all pointers to the start locations
            _Accumulator = new Accumulator();
            _Pointer.Bind(_Program.FirstLocation);
            needToAdvancePointer = false;
        }
        //Private Sub advancePointer()
        //    _Pointer.Advance()
        //End Sub
        private void goToBranch(MemoryLocation storLoc)
        {
            //moves pointer to the new memory location
            _Pointer.Bind(storLoc);
            needToAdvancePointer = false;
        }
        #endregion
    }
}