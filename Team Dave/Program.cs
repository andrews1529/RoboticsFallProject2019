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
            CTRE.Phoenix.MotorControl.CAN.TalonSRX myTalon2 = new CTRE.Phoenix.MotorControl.CAN.TalonSRX(2);

            float speed;
            float turn;
            float avg;

            /* loop forever */
            while (true)
            {

                /* added inside the while loop */
                if (myGamepad.GetConnectionStatus() == CTRE.Phoenix.UsbDeviceConnection.Connected)
                {
                    //If you want to change it to right bumper use get.axis(2) and (5)
                    speed = myGamepad.GetAxis(1);
                    turn = myGamepad.GetAxis(0);
                    avg = speed / 2 + turn / 2;
                    /* print the axis value */
                    //Turning is overriding the speed 
                    Debug.Print("axis:" + myGamepad.GetAxis(0) + ", " + myGamepad.GetAxis(1) + ", " + myGamepad.GetAxis(2) + ", " + myGamepad.GetAxis(5));
                    Debug.Print("speed:" + speed);
                    Debug.Print("turn:" + turn);

                    /* pass axis value to talon */
                   // myTalon.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, speed);
                   // myTalon2.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, speed);

                    //myTalon.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, turn);
                    //myTalon2.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, turn * -1);

                    
                    myTalon.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, avg * -1);
                    myTalon2.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, avg);


                    /* allow motor control */
                    CTRE.Phoenix.Watchdog.Feed();
                }

                /* wait a bit */
                System.Threading.Thread.Sleep(50);
            }
        }
    }
}
