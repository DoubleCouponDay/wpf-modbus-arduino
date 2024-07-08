# wpf-modbus-arduino
Example application of a WPF HMI connected to an Arduino Uno R3.

## Setting up 

Installing the following libraries using the Arduino IDE:

- ModbusSerial

Manually install the following libraries into your `~/Documents/Arduino/Libraries` folder:

- [modbus-arduino](https://github.com/epsilonrt/modbus-arduino/releases/tag/v1.3.0)

## Calculations (based on Arduino Servo Library)

    min pulse width = 544
    max pulse width = 2400
    maximum PWM frequency = 500hz

    servo min = min pulse width - (min pulse width * 4)
    => -1632 //not sure why the arduino servo library is calculating this as negative

    servo max = max pulse width - (max pulse width * 4)
    => -7200

    servo angle ∈ [0 deg, 180 deg]
        eg: 90 deg

    servo min < pulse width < servo max

    clock cycles per microsecond = 16 * 10^6 / 1 * 10^6
    => 16
    
    //on time
    on pulse width = (servo max - servo min) / (180 - 0) * servo angle + servo min
    => -4416

    on pulse width = on pulse width - 2
    => -4418

    on frequency = clock cycles per microsecond * on pulse width / 8
    => -8836

    on time = abs(1 / on frequency) //I used abs because the negative period didn't make sense to me
    => 113.17338 * 10^-6

## References

- [Arduino PLC Physical Addressing](https://autonomylogic.com/docs/2-4-physical-addressing/)

- [Arduino PLC Modbus Addressing](https://autonomylogic.com/docs/2-5-modbus-addressing/)

- [Open PLC Tutorials](https://www.youtube.com/@openplc/videos)

- [frequency equation](https://en.wikipedia.org/wiki/Frequency)

- [Arduino Servo Library](https://github.com/arduino-libraries/Servo)

- AtMega328P baudrate: `115200`
