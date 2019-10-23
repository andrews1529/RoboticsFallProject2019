using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace Team_Dave
{
    public class Program
    {
        public static void Main()
        {
            /* create a gamepad object */
            CTRE.Phoenix.Controller.GameController myGamepad = new CTRE.Phoenix.Controller.GameController(new CTRE.Phoenix.UsbHostDevice(0));

            /* create a talon, the Talon Device ID in HERO LifeBoat is zero */
            CTRE.Phoenix.MotorControl.CAN.TalonSRX myTalon = new CTRE.Phoenix.MotorControl.CAN.TalonSRX(1);

            float speed = myGamepad.GetAxis(1);

            /* loop forever */
            while (true)
            {
                speed = myGamepad.GetAxis(1);

                /* added inside the while loop */
                if (myGamepad.GetConnectionStatus() == CTRE.Phoenix.UsbDeviceConnection.Connected)
                {
                    /* print the axis value */
                    Debug.Print("axis:" + myGamepad.GetAxis(0) + ", " + myGamepad.GetAxis(1) + ", " + myGamepad.GetAxis(2) + ", " + myGamepad.GetAxis(5));

                    /* pass axis value to talon */
                    myTalon.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, speed);

                    /* allow motor control */
                    CTRE.Phoenix.Watchdog.Feed();
                }

                /* wait a bit */
                System.Threading.Thread.Sleep(50);
            }
        }
    }
}
