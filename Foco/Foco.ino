int foco = 13;

char orden;

void setup() {
  Serial.begin(9600);
  pinMode(foco, OUTPUT);
  digitalWrite(foco, LOW);

}

void loop() {

  if (Serial.available() > 0)
  {
    orden = Serial.read();
    Serial.println(orden);

    if (orden == '1')
    {
      digitalWrite(foco, HIGH);
    }
    if (orden == 'a')
    {
      digitalWrite(foco, LOW);
    }
  }



}
