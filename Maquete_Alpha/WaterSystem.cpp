
#include "WaterSystem.h"
#include "DefinesMaquete.h"

int WaterSystemClass::GetVolumeR1()
{
	return Volumes[0];
}

int WaterSystemClass::GetVolumeR2()
{
	return Volumes[1];
}

int WaterSystemClass::GetVolumeTanque()
{
	return Volumes[2];
}

float WaterSystemClass::GetFluxo()
{
	return 0;
}

void WaterSystemClass::ControlarTanques()
{
	int sensorTanque1 = analogRead(Sensor_Tanque1);
	int sensorTanque2 = analogRead(Sensor_Tanque2);
	int sensorTanque3 = analogRead(Sensor_Tanque3);
	
	if(sensorTanque1>350)
	digitalWrite(Motor1,HIGH);
	else if(sensorTanque1<322)
	digitalWrite(Motor1,LOW);
	
	if(sensorTanque2 > 127)
	digitalWrite(Motor2,HIGH);
	else if (sensorTanque2 < 75)
	digitalWrite(Motor2,LOW);
	if (sensorTanque2 > 153)
	digitalWrite(Motor1,LOW);

	if(sensorTanque3 > 155 )
	{
		digitalWrite(Motor3,LOW);
		digitalWrite(Motor2,LOW);
		
	}
	else if( sensorTanque3 < 120)
		digitalWrite(Motor3, !digitalRead(Motor2));
	
	Volumes[0] = map(sensorTanque1,297,446,180,850);
	Volumes[1] = map(sensorTanque2,40,160,150,800);
	Volumes[2] = map(sensorTanque3,46,160,100,800);

	//Tanque 3
	if(sensorTanque3>150)
	digitalWrite(VermelhoT3,HIGH);
	else if(sensorTanque3<140)
	digitalWrite(VermelhoT3,LOW);
	
	if(sensorTanque3 > 100 && sensorTanque3<140)
	digitalWrite(AmareloT3,HIGH);
	else if(sensorTanque3 < 81 || sensorTanque3 > 150)
	digitalWrite(AmareloT3,LOW);
	
	if(sensorTanque3<80)
	digitalWrite(VerdeT3,HIGH);
	else if(sensorTanque3< 80 || sensorTanque3 > 100)
	digitalWrite(VerdeT3,LOW);

	//Tanque 2
	if(sensorTanque2>150)
	digitalWrite(VermelhoT2,HIGH);
	else if(sensorTanque2<140)
	digitalWrite(VermelhoT2,LOW);
	
	if(sensorTanque2 > 100 && sensorTanque2<140)
	digitalWrite(AmareloT2,HIGH);
	else if(sensorTanque2 < 81 || sensorTanque2 > 150)
	digitalWrite(AmareloT2,LOW);
	
	if(sensorTanque2<80)
	digitalWrite(VerdeT2,HIGH);
	else if(sensorTanque2< 80 || sensorTanque2 > 100)
	digitalWrite(VerdeT2,LOW);
	
	
	//Tanque 1
	if(sensorTanque1>450)
	digitalWrite(VermelhoT1,HIGH);
	else if(sensorTanque1<430)
	digitalWrite(VermelhoT1,LOW);
	
	if(sensorTanque1 > 350 && sensorTanque1<430)
	digitalWrite(AmareloT1,HIGH);
	else if(sensorTanque1 < 350 || sensorTanque1 > 450)
	digitalWrite(AmareloT1,LOW);
	
	if(sensorTanque1<350)
	digitalWrite(VerdeT1,HIGH);
	else if(sensorTanque1< 300 || sensorTanque1 > 350)
	digitalWrite(VerdeT1,LOW);
	
}

void WaterSystemClass::ControlarLuzTanque(int Tanque, int Nivel)
{
	switch(Tanque)
	{
		case Tanque1:
		digitalWrite(VermelhoT1,Nivel == 3);
		digitalWrite(AmareloT1, Nivel == 2);
		digitalWrite(VerdeT1, Nivel == 1);
		break;
		
		case Tanque2:
		digitalWrite(VermelhoT2,Nivel == 3);
		digitalWrite(AmareloT2, Nivel == 2);
		digitalWrite(VerdeT2, Nivel == 1);
		break;
		
		case Tanque3:
		digitalWrite(VermelhoT3,Nivel == 3);
		digitalWrite(AmareloT3, Nivel == 2);
		digitalWrite(VerdeT3, Nivel == 1);
		break;
	}
}


WaterSystemClass WaterSystem;

