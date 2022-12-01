


void fullMode(void)
{  
  Roomba.write(128);  //Start
  Roomba.write(132);  //Full mode
  delay(1000);
}

void disconnectCommanding(void) //disconnect roomba from ESP32
{
  Roomba.write(173);
}

void off(void)
{
  Roomba.write(133);
}

void driveWheelV(int rightWheel, int leftWheel)
{
  Roomba.write(145);
  Roomba.write(rightWheel >> 8); //1st byte
  Roomba.write(rightWheel);      //2nd byte
  Roomba.write(leftWheel >> 8); //3rd byte
  Roomba.write(leftWheel);      //4th byte
}

void drive(int velocity, int turnRadius)
{
  
  Roomba.write(137);
  Roomba.write(velocity >> 8);   //1st byte
  Roomba.write(velocity);        //2nd byte
  Roomba.write(turnRadius >> 8); //3rd byte
  Roomba.write(turnRadius);      //4th byte
}

void turnRightUndefined(int velocity, int degrees) //turn right endlessly
{
  drive(velocity, -1);
  //delay((735000/velocity)/(360/degrees));
  //drive(0,0);
}

void turnLeftUndefined(int velocity, int degrees)  //turn right endlessly
{
  drive(velocity, 1); 
  //delay((735000/velocity)/(360/degrees));
  //drive(0,0);
}

void turnRight(int velocity, int degrees) //turn right a specified number of degrees
{
  drive(velocity, -1);
  delay((775000/velocity)/(360/degrees)); // delay by (roomba radius / velocity) * (360 / degrees desired)
  drive(0,0);
}

void turnLeft(int velocity, int degrees) //turn left a specified number of degrees
{
  drive(velocity, 1); 
  delay((775000/velocity)/(360/degrees)); // delay by (roomba radius / velocity) * (360 / degrees desired)
  drive(0,0);
}

void Stop(void)
{
  drive(0,0);
}


void rectangle(void) // function to do rectangle forward and backwards
{
  driveWheelV(200, 200);
  delay(4000);
  turnRight(200,90);
  driveWheelV(200, 200);
  delay(4000);
  turnRight(200,90);
  driveWheelV(200, 200);
  delay(4000);
  turnRight(200,90);
  driveWheelV(200, 200);
  delay(4000);
  driveWheelV(-200, -200);
  delay(4000);
  turnLeft(200,90);
  driveWheelV(-200, -200);
  delay(4000);
  turnLeft(200,90);
  driveWheelV(-200, -200);
  delay(4000);
  turnLeft(200,90);
  driveWheelV(-200, -200);
  delay(4000);
  Stop();
}

void anglesR(void) // function to test right turns
{
  turnRight(200, 360);
  driveWheelV(0,0);
  delay(4000);
  turnRight(200, 180);
  driveWheelV(0,0);
  delay(4000);
  turnRight(200, 90);
  driveWheelV(0,0);
  delay(4000);
  turnRight(200, 60);
  driveWheelV(0,0);
  delay(4000);
  turnRight(200, 30);
  driveWheelV(0,0);
}

void anglesL(void)  //function to test left turns
{
  turnLeft(200, 360);
  driveWheelV(0,0);
  delay(4000);
  turnLeft(200, 180);
  driveWheelV(0,0);
  delay(4000);
  turnLeft(200, 90);
  driveWheelV(0,0);
  delay(4000);
  turnLeft(200, 60);
  driveWheelV(0,0);
  delay(4000);
  turnLeft(200, 30);
  driveWheelV(0,0);
}


void shortDrive() //drive for 1 millisecond
{
  driveWheelV(200, 200);
  delay(1);
}

void shortStop() // stop for 1 millisecond
{
  driveWheelV(0, 0);
  delay(1);
}
