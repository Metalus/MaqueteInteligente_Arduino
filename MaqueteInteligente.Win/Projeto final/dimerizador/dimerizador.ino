//99898797  
int pinoled = 10; 
int pinopot = 5; 
int valorpot = 0;  
float luminosidade = 0; 
float luminosidadeAtual = 0;
   
void setup()  
{  
  Serial.begin(9600);     
  pinMode(pinoled, OUTPUT); 
  pinMode(pinopot, INPUT);  
}  
   
void loop()  

{  
 
  valorpot = analogRead(pinopot);  
  Serial.print("valorpot: ");
  Serial.println(valorpot);
  
  luminosidade = map(valorpot, 0, 1023, 0, 850) - 30; 
  Serial.print("luminosidade: ");
  Serial.println(luminosidade);
  /*
    if(luminosidade > 3){ 
        analogWrite(pinoled, luminosidade); 
    } else {
      analogWrite(pinoled,LOW);
           }
 */
     while(luminosidadeAtual != luminosidade && luminosidade >= 25)
      {
        if(luminosidadeAtual > luminosidade)
          luminosidadeAtual--;
        else
          luminosidadeAtual++;
          
         delay(100);
         
         analogWrite(pinoled,luminosidadeAtual);
      }        
      
      if(luminosidade < 25)
        DesligarLed();
 }

void DesligarLed()
{
  for(int i = luminosidadeAtual; i>=0;i--)
  {
    analogWrite(pinoled, i);
    delay(100);
  }
  
  analogWrite(pinoled, LOW);
}
