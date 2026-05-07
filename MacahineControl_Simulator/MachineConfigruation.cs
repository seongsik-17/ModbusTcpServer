using System;

namespace MacahineControl_Simulator
{
    public class PidController
    {
        public double P { get; set; } = 1.0;
        public double I { get; set; } = 0.1;
        public double D { get; set; } = 0.01;

        private double _integral = 0;
        private double _lastError = 0;

        public double Calculate(double setpoint, double current, double dt)
        {
            double error = setpoint - current;
            _integral += error * dt;
            double derivative = (error - _lastError) / dt;
            _lastError = error;

            double output = (P * error) + (I * _integral) + (D * derivative);
            return Math.Clamp(output, 0, 100); // 0-100% Heater Output
        }

        public void Reset()
        {
            _integral = 0;
            _lastError = 0;
        }
    }

    public class HeatingMachine
    {
        public double CurrentTemperature { get; private set; } = 25.0; // Ambient start
        public double TargetTemperature { get; set; } = 25.0;
        public double AmbientTemperature { get; set; } = 25.0;
        public double HeaterOutput { get; private set; } = 0; // 0-100%
        public bool IsRunning { get; set; } = false;

        public PidController Pid { get; } = new PidController();

        private const double HeatEfficiency = 0.05; // How fast it heats
        private const double CoolingRate = 0.01;   // How fast it loses heat to ambient

        public void Update(double dt)
        {
            if (IsRunning)
            {
                HeaterOutput = Pid.Calculate(TargetTemperature, CurrentTemperature, dt);
            }
            else
            {
                HeaterOutput = 0;
                Pid.Reset();
            }

            // Simple Thermal Model: T = T + (HeaterPower * Efficiency) - (LossToAmbient)
            double heating = HeaterOutput * HeatEfficiency * dt;
            double cooling = (CurrentTemperature - AmbientTemperature) * CoolingRate * dt;

            CurrentTemperature += heating - cooling;
        }
    }

    public enum MachineState
    {
        Stopped = 0,
        Heating = 1,
        Stabilizing = 2,
        Idle = 3
    }
}
