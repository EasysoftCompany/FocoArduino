

// Librerías necesarias
#include <SPI.h>
#include <MFRC522.h>

#define SS_PIN 53 // pin SDA hacia el pin 10
#define RST_PIN 5 // pin RST hacia el pin 9

int led = 10;
int rosa = 12;
int buzzer = 11;
char orden = '1';
MFRC522 rfid(SS_PIN, RST_PIN); // Creo la instancia de la clase MFRC522


// Inicializo vector que almacenará el NUID del PICC
byte nuidPICC[4];

void setup() {
  pinMode(buzzer, OUTPUT);
  pinMode(led, OUTPUT);
  pinMode(rosa, OUTPUT);
  Serial.begin(9600);
  SPI.begin(); // Inicia el bus de SPI
  rfid.PCD_Init(); // Inicia el lector


}

void loop() {

  // Buscando nuevas tarjetas
  if ( ! rfid.PICC_IsNewCardPresent())
    return;

  // Lee tarjeta
  if ( ! rfid.PICC_ReadCardSerial())
    return;


  for (byte i = 0; i < 4; i++) {
    nuidPICC[i] = rfid.uid.uidByte[i];
  }
  printHex(rfid.uid.uidByte, rfid.uid.size);


  orden = Serial.read();
  if(orden == '1'){
  aprobado();
  
  }else{

  noaprobado();
    
  }
  

  // Halt PICC
  rfid.PICC_HaltA();

  // Detiene el cifrado en el PCD
  rfid.PCD_StopCrypto1();
}


/**
   Función que ayuda a representar valores hex en el monitor serial
*/
void printHex(byte *buffer, byte bufferSize) {
  for (byte i = 0; i < bufferSize; i++) {
    Serial.print(buffer[i] < 0x10 ? " 0" : " ");
    Serial.print(buffer[i], HEX);   
  }Serial.println();
}

/**
   Función que ayuda a representar valores en dec en el monitor serial
*/
void printDec(byte *buffer, byte bufferSize) {
  for (byte i = 0; i < bufferSize; i++) {
    
    Serial.println(buffer[i], DEC);
  }
}
void aprobado() {
  digitalWrite(led, HIGH);
  analogWrite(buzzer, 120); //emite sonido
  delay(100);
  analogWrite(buzzer, 160); //emite sonido
  delay(200);
  digitalWrite(buzzer, LOW); //deja de emitir
  digitalWrite(led, LOW);
  delay(200);

}

void noaprobado() {
  digitalWrite(rosa, HIGH);
  analogWrite(buzzer, 160); //emite sonido
  delay(100);
  analogWrite(buzzer, 120); //emite sonido
  delay(200);
  digitalWrite(buzzer, LOW); //deja de emitir
  digitalWrite(rosa, LOW);
  delay(200);

}

