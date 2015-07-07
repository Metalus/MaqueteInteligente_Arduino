#include "EletricSystem.h"

EletricSystemClass EletricSystem;

void EletricSystemClass::Dimerizar(ComodosEnum comodo)
{
	int luminosidade;
	int Lampada = Comodos::GetLampadaID(comodo);
	int Sensor = Comodos::GetSensorID(comodo);
	if(DesabilitarLeds[comodo])
	{
		LuminosidadeAtual[comodo] = 0;
		LedOff[comodo] = true;
		digitalWrite(Lampada, LOW);
		return;
	}
	
	switch(comodo)
	{
		case Quarto1:
		luminosidade = map(analogRead(Sensor), 0, 950, 0, 255);
		break;
		
		case Quarto2:
		luminosidade = map(analogRead(Sensor),0, 290,0,255);
		break;
		
		case Sala:
		luminosidade = map(analogRead(Sensor),0, 450,0,255);
		break;
		
		case Banheiro:
		luminosidade = map(analogRead(Sensor),0, 450,0,255);
		break;
		
		case Cozinha:
		luminosidade = map(analogRead(Sensor), 0, 1023,0,255);
		break;
		
	}
	luminosidade = luminosidade > 255 ? 255 : luminosidade;
	if(LuminosidadeAtual[comodo] != luminosidade && luminosidade >= 32)
	{
		if(LuminosidadeAtual[comodo] > luminosidade)
		LuminosidadeAtual[comodo]--;
		else
		LuminosidadeAtual[comodo]++;
		
		analogWrite(Lampada, LuminosidadeAtual[comodo]);
		LedOff[comodo] = false;
	}
	
	else if(LuminosidadeAtual[comodo] != luminosidade && luminosidade < 30 && !LedOff[comodo])
		DesligarLed(comodo, Lampada);
}

void EletricSystemClass::DesligarLed(ComodosEnum comodo, int Lampada)
{
	for(int i = LuminosidadeAtual[comodo]; i>=0;i--)
	{
		analogWrite(Lampada, i);
	}
	
	digitalWrite(Lampada, LOW);
	LuminosidadeAtual[comodo] = 1;
	LedOff[comodo] = true;
}

int Comodos::GetLampadaID(ComodosEnum comodo)
{
	switch (comodo)
	{
		case Quarto1:
		return Q1_Lampada;
		
		case Quarto2:
		return Q2_Lampada;
		
		case Banheiro:
		return Banheiro_Lampada;
		
		case Sala:
		return Sala_Lampada;
		
		case Cozinha:
		return Cozinha_Lampada;
		
		default:
		return -1;
	}
}

int Comodos::GetSensorID(ComodosEnum comodo)
{
	switch (comodo)
	{
		case Quarto1:
		return Q1_Sensor;
		
		case Quarto2:
		return Q2_Sensor;
		
		case Banheiro:
		return Banheiro_Sensor;
		
		case Sala:
		return Sala_Sensor;
		
		case Cozinha:
		return Cozinha_Sensor;
		
		default:
		return -1;
	}
}