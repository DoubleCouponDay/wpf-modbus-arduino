#include <Servo.h>
#include <ModbusSerial.h>

const int servoPin = 10;
const int baudRate = 115200;
const int defaultAngle = 90;

Servo servoInstance;
ModbusSerial modbusInstance(Serial, 1, -1); //RS485 not used so TX pin set to disabled

int angleAddress; 
int aliveAddress;

void setup() {
  servoInstance.attach(servoPin);
  Setup_ModbusServer();
}

void Setup_ModbusServer() {
  Serial.begin(baudRate, MB_PARITY_EVEN);
  modbusInstance.config(baudRate);
  modbusInstance.setAdditionalServerData("SERVO");
  modbusInstance.addHreg(angleAddress, defaultAngle);
  modbusInstance.addCoil(aliveAddress, true);
}

void loop() {
  //apply the servo angle
  modbusInstance.task();
  word angle = modbusInstance.Hreg(angleAddress);

  if(angle > 180) {
    modbusInstance.setHreg(angleAddress, 180);
    angle = 180;
  }

  else if(angle < 0) {
    modbusInstance.setHreg(angleAddress, 0);
    angle = 0;
  }
  servoInstance.write(angle);
  delay(15);

  //apply the alive signal
  bool isAlive = modbusInstance.Coil(aliveAddress);
  isAlive = !isAlive;
  modbusInstance.setCoil(aliveAddress, isAlive);
}
