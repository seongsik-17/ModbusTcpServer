using System;
using System.Collections.Generic;
using System.Text;

namespace MacahineControl_Simulator
{
	internal class MachineConfigruation
	{
		int _targetTemperature;
		float _pidP;
		float _pidI;
		float _pidD;

		bool heatingOn;
		bool heatingOff;
		public MachineConfigruation() { }
	}

	public class MachineStatus 	{
		int _currentTemperature;
		bool _isHeatingOn;
		public MachineStatus() { }
	}


	public class MachineStateMainForm 
	{
		MachineConfigruation config = new MachineConfigruation();
		MachineStatus status = new MachineStatus();
		void test()
		{
			
			
		}

	}
}
