/*
* MyClass.cpp
*
* Created: 5/11/2015 1:29:14 PM
* Author: Win 64 Btis
*/

//#include <SPI/SPI.h>
//#include <Ethernet.h>
#include "MainClass.h"
#include "ArgsType.h"
#include "WaterSystem.h"
#include "DefinesMaquete.h"
#include "EletricSystem.h"

//EthernetServer server(80);

void MainClass::setup()
{
	/*byte MAC[] = {0xDC, 0x44, 0xDE, 0xEF, 0xF1, 0xAD };
	IPAddress ip(198,168,1,88);
	IPAddress gateway(198,168,1,1);
	IPAddress mask(255,255,255,0);*/

	Serial.begin(4800);
	pinMode(Q1_Lampada,OUTPUT);
	pinMode(Q1_Sensor,INPUT);
	//Ethernet.begin(MAC,ip,gateway,mask);
}

void MainClass::loop()
{
	/*EthernetClient client =	server.available();
	if(client) { Serial.println("Novo client conectado"); }
	while(client.connected() && client.available())
	{
		if(client.parseInt() == VolumeR1)
			client.println("V1:51");
	}*/

	EletricSystem.Dimerizar(Quarto1);
}

void MainClass::serialEvent()
{
	short Args = (Serial.parseInt() & 0xFFFF);
	float D = 0;
	
	if((Args & VolumeR1) == VolumeR1)
	{
		
		D = WaterSystem.GetVolumeR1();
		Serial.println("V1:");
		Serial.println(D);
	}
	
	if((Args & VolumeR2) == VolumeR2)
	{
		D = WaterSystem.GetVolumeR2();
		Serial.print("V2:");
		Serial.println(D);
	}
	
	if((Args & VolumeTanque) == VolumeTanque)
	{
		D = WaterSystem.GetVolumeTanque();
		Serial.print("VT:");
		Serial.println(D);
	}
	
	if((Args & Fluxo) == Fluxo)
	{
		D = WaterSystem.GetFluxo();
		Serial.print("FL:");
		Serial.println(D);
	}
}

MainClass mainClass;

