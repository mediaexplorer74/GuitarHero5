// Decompiled with JetBrains decompiler
// Type: com.glu.shared.CAccelerometer
// Assembly: shared, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5E0A1C2A-EDE2-425A-9575-6DFDE88D9D48
// Assembly location: C:\Users\Admin\Desktop\RE\Guitar_Hero_5_v1.2\shared.dll

// Remove or comment out the following line, as the 'Microsoft.Devices.Sensors' namespace does not exist in your current project or referenced assemblies.
// using Microsoft.Devices.Sensors;

#nullable disable
using System;
using Windows.Devices.Sensors;

namespace com.glu.shared
{
  public sealed class CAccelerometer
  {
    private CVector3d m_vec = new CVector3d();
    private DateTime m_lastSampleTimeMS;
    private uint m_accelerometerFreqHz = 50;
        // Replace this line:
        // private Accelerometer m_sensor = new Accelerometer();
        // With the following:

        private Accelerometer m_sensor = Accelerometer.GetDefault();

        public CAccelerometer()
        {
            this.m_vec.Set(0, 0, -65536);
            this.m_lastSampleTimeMS = DateTime.Now;
            this.m_accelerometerFreqHz = 0U;
            if (this.m_sensor == null)
                return;
            //this.m_sensor.ReadingChanged += new EventHandler<AccelerometerReadingEventArgs>(this.AccelerometerReadingChanged);
            //this.m_sensor.Start();
            this.m_sensor.ReadingChanged += (sender, args) =>
            {
                // args is of type AccelerometerReadingChangedEventArgs
                var reading = args.Reading;
                // Convert the Windows.Devices.Sensors.AccelerometerReading to your AccelerometerReadingEventArgs
                var customArgs = new AccelerometerReadingEventArgs(reading.AccelerationX, reading.AccelerationY, reading.AccelerationZ);
                this.AccelerometerReadingChanged(sender, customArgs);
            };
        }

    public int GetX() => this.m_vec.m_i;

    public int GetY() => this.m_vec.m_j;

    public int GetZ() => this.m_vec.m_k;

    public void AccelerometerReadingChanged(object sender, AccelerometerReadingEventArgs e)
    {
      DateTime now = DateTime.Now;
      if ((uint) (now - this.m_lastSampleTimeMS).TotalMilliseconds * this.m_accelerometerFreqHz < 1000U)
        return;
      this.m_lastSampleTimeMS = now;
      this.m_vec.m_i = CMathFixed.FloatToFixed((float) e.X);
      this.m_vec.m_j = CMathFixed.FloatToFixed((float) e.Y);
      this.m_vec.m_k = CMathFixed.FloatToFixed((float) e.Z);
      ulong num1 = (ulong) (((long) this.m_vec.m_k & 2097151L) << 42 | ((long) this.m_vec.m_j & 2097151L) << 21 | (long) this.m_vec.m_i & 2097151L);
      ulong num2 = num1 >> 32 & (ulong) uint.MaxValue;
      CApplet.GetInstance().m_eventQueue.Queue(2903985391U, (uint) num1, (uint) num2);
    }

    public void SetAccelerometerFrequency(uint freqHz) => this.m_accelerometerFreqHz = freqHz;
  }
    // Add this class definition to resolve CS0246 if you do not have a reference to the real AccelerometerReadingEventArgs.
    // This is a stub and should be replaced with the actual implementation if available.
    public class AccelerometerReadingEventArgs : EventArgs
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public AccelerometerReadingEventArgs(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
