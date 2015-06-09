/*
* MyClass.cpp
*
* Created: 5/11/2015 1:29:14 PM
* Author: Win 64 Btis
*/

#include "MainClass.h"
#include "ArgsType.h"
#include "WaterSystem.h"
#include "DefinesMaquete.h"
#include "EletricSystem.h"

//EthernetServer server(80);
//byte Ethernet::buffer[500];

void MainClass::setup()
{
	/*byte MAC [] = {0xDC, 0x44, 0xDE, 0xEF, 0xF1, 0xAD };
	byte ip  [] = {192,168,1,88};
	byte gate[] = {192,168,1,1};
	

	if(!ether.begin(sizeof Ethernet::buffer,MAC))
	Serial.println("Módulo Wifi erro");
	
	ether.staticSetup(ip,gate);
	
	if(!ether.dhcpSetup())
	Serial.println("DHCP Failed.");*/
	
	Serial.begin(115200);
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
	EletricSystem.Dimerizar(Quarto2);
	EletricSystem.Dimerizar(Cozinha);
	EletricSystem.Dimerizar(Banheiro);
	EletricSystem.Dimerizar(Sala);
	
	WaterSystem.ControlarTanques();
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
	
	if((Args & ValorIluminacao) == ValorIluminacao)
	{
		Serial.print("VA:");
		Serial.println(EletricSystem.LuminosidadeAtual[Quarto1]);
		Serial.println(EletricSystem.LuminosidadeAtual[Quarto2]);
		Serial.println(EletricSystem.LuminosidadeAtual[Sala]);
		Serial.println(EletricSystem.LuminosidadeAtual[Cozinha]);
		Serial.println(EletricSystem.LuminosidadeAtual[Banheiro]);
	}
	
	EletricSystem.LuminosidadeAtual[Quarto1]  = (Args & OffLuz_Q1)		 == OffLuz_Q1		? -1 : EletricSystem.LuminosidadeAtual[Quarto1];
	EletricSystem.LuminosidadeAtual[Quarto2]  = (Args & OffLuz_Q2)		 == OffLuz_Q2		? -1 : EletricSystem.LuminosidadeAtual[Quarto2];
	EletricSystem.LuminosidadeAtual[Sala]	  = (Args & OffLuz_Sala)     == OffLuz_Sala		? -1 : EletricSystem.LuminosidadeAtual[Sala];
	EletricSystem.LuminosidadeAtual[Cozinha]  = (Args & OffLuz_Cozinha)  == OffLuz_Cozinha	? -1 : EletricSystem.LuminosidadeAtual[Cozinha];
	EletricSystem.LuminosidadeAtual[Banheiro] = (Args & OffLuz_Banheiro) == OffLuz_Banheiro ? -1 : EletricSystem.LuminosidadeAtual[Banheiro];
}

MainClass mainClass;

