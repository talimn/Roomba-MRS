#include "BluetoothSerial.h"
#include "Arduino.h"
#include "Wire.h"
#include "DFRobot_VL53L0X.h"
#include <SoftwareSerial.h>
DFRobot_VL53L0X sensor;

int rx=16;
int tx=17;
int dd = 4; //device detect pin on roomba used to detect microcontroller
SoftwareSerial Roomba(rx,tx);

BluetoothSerial SerialBT;

void setup() 
{
  Serial.begin(115200);
  SerialBT.begin("3.0James_ESP32"); //Bluetooth device name
  Wire.begin();
  sensor.begin(0x50);
  sensor.setMode(sensor.eContinuous,sensor.eHigh);
  sensor.start();
  Roomba.begin(115200);


  
  pinMode(dd, OUTPUT); //turns roomba on

  digitalWrite(dd, HIGH);
  delay(100);
  digitalWrite(dd, LOW);
  delay(500);
  digitalWrite(dd, HIGH);
  delay(2000);
  
  fullMode(); //starts roomba with full control
}

void loop() {
  Serial.println(sensor.getDistance());
  int obstacle = 0;
  char bt;
  if (Serial.available()) {
    SerialBT.write(Serial.read());
  }
  if (SerialBT.available()) {
    bt = SerialBT.read();
    Serial.write(bt);
  }
  
    if (SerialBT.available()) {
      if (bt == 'j') { //turn left until new command
        SerialBT.write('s');
        SerialBT.write('u');
        SerialBT.write('c');
        SerialBT.write('c');
        SerialBT.write('e');
        SerialBT.write('s');
        SerialBT.println('s');
          turnLeftUndefined(200,90);
        
        
      } else if (bt == 'w') { //go forward
        SerialBT.write('s');
        SerialBT.write('u');
        SerialBT.write('c');
        SerialBT.write('c');
        SerialBT.write('e');
        SerialBT.write('s');
        SerialBT.println('s');

          while(true) { // loop to integrate object detection

            if (SerialBT.available()) {
            bt = SerialBT.read();
            Serial.write(bt);
            SerialBT.println(bt);
          }

          if (bt == 's') { //stop if 's' command given
            Stop();
            break;
          }
          
          //Serial.println(sensor.getDistance());
          if(sensor.getDistance() > 200) { //forward if dist is larger than 200 mm
            obstacle = 0;
            shortDrive();
          } else if (obstacle < 500) { //temporarily stop. int obstacle serves as timer to check if obstacle 
                                       //is static or dynamis
            obstacle += 1;
            shortStop();
          } else {
            driveWheelV(0,0); //alert if obstacle is static
            SerialBT.write('O');
            SerialBT.write('b');
            SerialBT.write('s');
            SerialBT.write('t');
            SerialBT.write('a');
            SerialBT.write('c');
            SerialBT.write('l');
            SerialBT.write('e');
            SerialBT.write(' ');
            SerialBT.write('i');
            SerialBT.write('n');
            SerialBT.write(' ');
            SerialBT.write('p');
            SerialBT.write('a');
            SerialBT.write('t');
            SerialBT.println('h');
            break;
          }
          
        }
        
      } else if (bt == 'l') { //turn right endlessly
        SerialBT.write('s');
        SerialBT.write('u');
        SerialBT.write('c');
        SerialBT.write('c');
        SerialBT.write('e');
        SerialBT.write('s');
        SerialBT.println('s');
            turnRightUndefined(200,90);
          
      } else if (bt == 's') { //stop
        SerialBT.write('s');
        SerialBT.write('u');
        SerialBT.write('c');
        SerialBT.write('c');
        SerialBT.write('e');
        SerialBT.write('s');
        SerialBT.println('s');
        Stop();
      } else if (bt == 'o') { // turn off
        SerialBT.write('s');
        SerialBT.write('u');
        SerialBT.write('c');
        SerialBT.write('c');
        SerialBT.write('e');
        SerialBT.write('s');
        SerialBT.println('s');
        off();
      } else if (bt == 'd') { //90 degree right turn
        SerialBT.write('s');
        SerialBT.write('u');
        SerialBT.write('c');
        SerialBT.write('c');
        SerialBT.write('e');
        SerialBT.write('s');
        SerialBT.println('s');
            turnRight(200,90);
          
      } else if (bt == 'a') { // 90 degree left turn
        SerialBT.write('s');
        SerialBT.write('u');
        SerialBT.write('c');
        SerialBT.write('c');
        SerialBT.write('e');
        SerialBT.write('s');
        SerialBT.println('s');
            turnLeft(200,90);
          
      } else if (bt == 'f') { // test using rectangle function
        SerialBT.write('s');
        SerialBT.write('u');
        SerialBT.write('c');
        SerialBT.write('c');
        SerialBT.write('e');
        SerialBT.write('s');
        SerialBT.println('s');
        rectangle();
          
      } else if (bt == 'b') { // test right turns
        SerialBT.write('s');
        SerialBT.write('u');
        SerialBT.write('c');
        SerialBT.write('c');
        SerialBT.write('e');
        SerialBT.write('s');
        SerialBT.println('s');
        anglesR();
          
      } else if (bt == 'v') { // test left turns
        SerialBT.write('s');
        SerialBT.write('u');
        SerialBT.write('c');
        SerialBT.write('c');
        SerialBT.write('e');
        SerialBT.write('s');
        SerialBT.println('s');
        anglesL();
          
      } 
    
    }
}
