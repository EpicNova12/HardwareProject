// C++ code
//
int PotIN = 0;

int currentInput = 1;

void setup() {
  // put your setup code here, to run once:
  pinMode(A0, INPUT);
  pinMode(A1, INPUT);
  Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
  PotIN = analogRead(A1);
  //Check current Mode
  switch(Serial.read())
  {
    case 'A':
      currentInput = 1;
      break;
    case 'B':
      currentInput = 2;
      break;
  }
  // Check if potentiometer has changed
  if(currentInput==1)
  {
    if (analogRead(A0) <= 50) {
    //Serial.println(analogRead(A0));
    Serial.println("Turned");
    delay(10);
    }
  }
  if(currentInput==2)
  {
    if(analogRead(A1) != PotIN)
    {
       Serial.println(analogRead(A1));
    }
    
  }
 
  // Check if potentiometer 2 has changed
}
