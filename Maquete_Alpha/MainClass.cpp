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

char Buffer[8];
void MainClass::setup()
{
	memset(&Buffer[0],0,8);
	pinMode(Motor1,OUTPUT);
	pinMode(Motor2,OUTPUT);
	pinMode(Motor3,OUTPUT);
	
	pinMode(Alimentacao1, OUTPUT);
	pinMode(Alimentacao2,OUTPUT);
	pinMode(Alimentacao3,OUTPUT);
	pinMode(Alimentacao4,OUTPUT);

	pinMode(AlimentacaoLdr1,OUTPUT);
	pinMode(AlimentacaoLdr2,OUTPUT);
	pinMode(AlimentacaoLdr3,OUTPUT);
	pinMode(AlimentacaoLdr4,OUTPUT);
	pinMode(AlimentacaoLdr5,OUTPUT);
	
	pinMode(Q1_Lampada,OUTPUT);
	pinMode(Q1_Sensor,INPUT);
	
	pinMode(Q2_Lampada,OUTPUT);
	pinMode(Q2_Sensor,INPUT);
	
	pinMode(Sala_Lampada,OUTPUT);
	pinMode(Sala_Sensor,INPUT);
	
	pinMode(Cozinha_Lampada,OUTPUT);
	pinMode(Cozinha_Sensor,INPUT);
	
	pinMode(Banheiro_Lampada, OUTPUT);
	pinMode(Banheiro_Sensor,INPUT);
	
	pinMode(VermelhoT1,OUTPUT);
	pinMode(AmareloT1,OUTPUT);
	pinMode(VerdeT1,OUTPUT);
	
	pinMode(VermelhoT2,OUTPUT);
	pinMode(AmareloT2,OUTPUT);
	pinMode(VerdeT2,OUTPUT);
	
	pinMode(VermelhoT3,OUTPUT);
	pinMode(AmareloT3,OUTPUT);
	pinMode(VerdeT3,OUTPUT);
	
	pinMode(Banheiro_Lampada,OUTPUT);
	pinMode(Banheiro_Sensor,INPUT);
	
	
	digitalWrite(Alimentacao1,HIGH);
	digitalWrite(Alimentacao2,HIGH);
	digitalWrite(Alimentacao3,HIGH);
	digitalWrite(Alimentacao4,HIGH);
	
	digitalWrite(AlimentacaoLdr1,HIGH);
	digitalWrite(AlimentacaoLdr2,HIGH);
	digitalWrite(AlimentacaoLdr3,HIGH);
	digitalWrite(AlimentacaoLdr4,HIGH);
	digitalWrite(AlimentacaoLdr5,HIGH);
	
	Serial.begin(115200);
	//digitalWrite(Q1_Lampada,HIGH);
	//digitalWrite(Q2_Lampada,HIGH);
	//digitalWrite(Sala_Lampada,HIGH);
	//digitalWrite(Banheiro_Lampada,HIGH);
	//digitalWrite(Cozinha_Lampada,HIGH);

}

void MainClass::loop()
{
	for(int i = 0; i < 50;i++)
	{
		EletricSystem.Dimerizar(Quarto1);
		EletricSystem.Dimerizar(Quarto2);
		EletricSystem.Dimerizar(Cozinha);
		
		EletricSystem.Dimerizar(Sala);
		EletricSystem.Dimerizar(Banheiro);
		WaterSystem.ControlarTanques();
	}
	
	if(!Serial.available()) { return; }
	for(int i = 0; Serial.available(); i++) 
		Buffer[i] = Serial.read();
	
	int Args = atoi(Buffer);
	
	memset(&Buffer[0],0,8);
	if((Args & VolumeR1) == VolumeR1)
	{
		Serial.print("V1:");
		Serial.println(WaterSystem.GetVolumeR1());
	}
	if((Args & VolumeR2) == VolumeR2)
	{
		Serial.print("V2:");
		Serial.println(WaterSystem.GetVolumeR2());
	}

	if((Args & VolumeTanque) == VolumeTanque)
	{
		Serial.print("VT:");
		Serial.println(WaterSystem.GetVolumeTanque());
	}
	
	if((Args & ValorIluminacao) == ValorIluminacao)
	{
		Serial.println("VI:");
		Serial.println(EletricSystem.LuminosidadeAtual[Quarto1]);
		Serial.println(EletricSystem.LuminosidadeAtual[Quarto2]);
		Serial.println(EletricSystem.LuminosidadeAtual[Sala]);
		Serial.println(EletricSystem.LuminosidadeAtual[Cozinha]);
		Serial.println(EletricSystem.LuminosidadeAtual[Banheiro]);
	}
	
	Serial.print(";");
	EletricSystem.DesabilitarLeds[Quarto1]   = (Args & OffLuz_Q1)	    == OffLuz_Q1;
	EletricSystem.DesabilitarLeds[Quarto2]   = (Args & OffLuz_Q2)	    == OffLuz_Q2;
	EletricSystem.DesabilitarLeds[Sala]	     = (Args & OffLuz_Sala)     == OffLuz_Sala;
	EletricSystem.DesabilitarLeds[Cozinha]   = (Args & OffLuz_Cozinha)  == OffLuz_Cozinha;
	EletricSystem.DesabilitarLeds[Banheiro]  = (Args & OffLuz_Banheiro) == OffLuz_Banheiro;
	
}

MainClass mainClass;

