/*
  -----------------------------------------------------------------------------------------
              MFRC522      Arduino       Arduino   Arduino    Arduino          Arduino
              Reader/PCD   Uno/101       Mega      Nano v3    Leonardo/Micro   Pro Micro
  Signal      Pin          Pin           Pin       Pin        Pin              Pin
  -----------------------------------------------------------------------------------------
  RST/Reset   RST          9             5         D9         RESET/ICSP-5     RST
  SPI SS      SDA(SS)      10            53        D10        10               10
  SPI MOSI    MOSI         11 / ICSP-4   51        D11        ICSP-4           16
  SPI MISO    MISO         12 / ICSP-1   50        D12        ICSP-1           14
  SPI SCK     SCK          13 / ICSP-3   52        D13        ICSP-3           15
*/

// Librerías necesarias
#include <SPI.h>
#include <MFRC522.h>

#define SS_PIN 53 // pin SDA hacia el pin 10
#define RST_PIN 5 // pin RST hacia el pin 9

int led1 = 10;
int led2 = 11;
int buzzer = 12;
MFRC522 rfid(SS_PIN, RST_PIN); // Creo la instancia de la clase MFRC522


// Inicializo vector que almacenará el NUID del PICC
byte nuidPICC[4];

void setup() {
  pinMode(buzzer, OUTPUT);
  pinMode(led1, OUTPUT);
  pinMode(led2, OUTPUT);
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
  detectado();





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
    Serial.print(buffer[i] , HEX);
  } Serial.println();

}


void detectado() {
  digitalWrite(led1, HIGH);
  digitalWrite(led2, HIGH);
  analogWrite(buzzer, 120); //emite sonido
  delay(100);
  analogWrite(buzzer, 160); //emite sonido
  delay(200);
  digitalWrite(buzzer, LOW); //deja de emitir
  digitalWrite(led1,LOW );
  digitalWrite(led2, LOW);

}


