//
//
//

#include "WaterSystem.h"
#include "DefinesMaquete.h"

float WaterSystemClass::GetVolumeR1()
{
	return 50.0;
}

float WaterSystemClass::GetVolumeR2()
{
	return 15.5;
}

float WaterSystemClass::GetVolumeTanque()
{
	return 75.9;
}

float WaterSystemClass::GetFluxo()
{
	return 0;
}

void WaterSystemClass::ControlarTanques()
{
	int Sensor1 = digitalRead(Sensor_Tanque1);
	int Sensor2 = digitalRead(Sensor_Tanque2);
	int Sensor3 = digitalRead(Sensor_Tanque3);

	digitalWrite(Motor1,  Sensor1);
	digitalWrite(Motor2,  Sensor2 || (!Sensor3 && Sensor2));
	digitalWrite(Motor3, !Sensor2 || (!Sensor3 && Sensor2));
}


/*void WaterSystemClass::ControlarTanques()
{
if(digitalRead(Sensor_Tanque1)) // Se tanque 1 == MAX
digitalWrite(Motor1, HIGH);
else // Se tanque 1 == MIN
digitalWrite(Motor1, LOW);

if(digitalRead(Sensor_Tanque2)) // Se tanque 2 == MAX
{
digitalWrite(Motor2,HIGH);
digitalWrite(Motor3,LOW);
}
else // Se tanque 2 == MIN
{
digitalWrite(Motor2,LOW);
digitalWrite(Motor3,HIGH);
}


if(digitalRead(Sensor_Tanque3)) // Se tanque 3 == MAX
{
digitalWrite(Motor2,LOW);
digitalWrite(Motor3,LOW);
}
else
{
if(digitalRead(Sensor_Tanque2))
digitalWrite(Motor2,HIGH);
else
digitalWrite(Motor3,HIGH);
}
}*/



WaterSystemClass WaterSystem;

