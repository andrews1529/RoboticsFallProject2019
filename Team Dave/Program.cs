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
            CTRE.Phoenix.MotorControl.CAN.TalonSRX myTalon3 = new CTRE.Phoenix.MotorControl.CAN.TalonSRX(25);

            
            float speed;
            float turn;
            float avg;
            bool rampDown = false;
            bool rampUp = false;
            int c = 0;

            var startTime = DateTime.UtcNow;

            while(DateTime.UtcNow - startTime < TimeSpan.FromTicks(50000000))
            {
                myTalon3.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, 1);
                Debug.Print("Test: " + c++);
                /* allow motor control */
                CTRE.Phoenix.Watchdog.Feed();
            }

            startTime = DateTime.UtcNow;

            while (DateTime.UtcNow - startTime < TimeSpan.FromTicks(50000000))
            {
                myTalon3.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, -1);
                Debug.Print("Test: " + c++);
                CTRE.Phoenix.Watchdog.Feed();
            }


            /* wait a bit */
            System.Threading.Thread.Sleep(50);
        


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
                    rampDown = myGamepad.GetButton(1);
                    rampUp = myGamepad.GetButton(2);
                    /* print the axis value */
                    //Turning is overriding the speed 
                    Debug.Print("axis:" + myGamepad.GetAxis(0) + ", " + myGamepad.GetAxis(1) + ", " + myGamepad.GetAxis(2) + ", " + myGamepad.GetAxis(5));
                    Debug.Print("speed:" + speed);
                    Debug.Print("turn:" + turn);    
                    Debug.Print("Button 1(DOWN): " + rampDown);
                    Debug.Print("Button 2(UP): " + rampUp);

                    /* pass axis value to talon */
                   // myTalon.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, speed);
                   // myTalon2.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, speed);

                    //myTalon.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, turn);
                    //myTalon2.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, turn * -1);

                    
                    myTalon.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, avg * -1);
                    myTalon2.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, avg);
                    if (rampDown)
                    {
                        myTalon3.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, -1.0);
                    }
                    else if (rampUp)
					{
						myTalon3.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, 1.0);
					}
					else
					{
						myTalon3.Set(CTRE.Phoenix.MotorControl.ControlMode.PercentOutput, 0);
					}
					
                    //once button is pressed move motor specific amount of times so that the ramp rests at a 90 degree angle

                    /* allow motor control */
                    CTRE.Phoenix.Watchdog.Feed();
                }

                /* wait a bit */
                System.Threading.Thread.Sleep(50);
            }
        }
    }
}
