#include "EletricSystem.h"

EletricSystemClass EletricSystem;

void EletricSystemClass::Dimerizar(ComodosEnum comodo)
{
	int Lampada = Comodos::GetLampadaID(comodo);
	int Sensor = Comodos::GetSensorID(comodo);
	if(LuminosidadeAtual[comodo] < 0)
	{
		digitalWrite(Lampada, LOW);
		return;
	}
	int luminosidade = map(analogRead(Sensor), 0, 1023, 0, 200);
	if(LuminosidadeAtual[comodo] != luminosidade && luminosidade >= 31)
	{
		if(LuminosidadeAtual[comodo] > luminosidade)
		LuminosidadeAtual[comodo]--;
		else
		LuminosidadeAtual[comodo]++;
		
		delay(30);
		Serial.println(LuminosidadeAtual[comodo]);
		analogWrite(Lampada,LuminosidadeAtual[comodo] * 2);
		LedOff[comodo] = false;
	}
	
	else if(LuminosidadeAtual[comodo] != luminosidade && luminosidade < 31 && !LedOff[comodo])
	DesligarLed(comodo, Lampada);
}

void EletricSystemClass::DesligarLed(ComodosEnum comodo, int Lampada)
{
	for(int i = LuminosidadeAtual[comodo]; i>=0;i--)
	{
		analogWrite(Lampada, i * 2);
		delay(10);
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