#include "EletricSystem.h"

EletricSystemClass EletricSystem;

void EletricSystemClass::Dimerizar(ComodosEnum comodo)
{
	int Lampada = Comodos::GetLampadaID(comodo);
	int Sensor = Comodos::GetSensorID(comodo);
	
	int luminosidade = map(analogRead(Sensor), 0, 1023, 0, 200);
	if(LuminosidadeAtual[comodo] != luminosidade && luminosidade >= 25)
	{
		if(LuminosidadeAtual[comodo] > luminosidade)
		LuminosidadeAtual[comodo]--;
		else
		LuminosidadeAtual[comodo]++;
		
		delay(250);
		Serial.println(LuminosidadeAtual[comodo]);
		analogWrite(Lampada,LuminosidadeAtual[comodo]);
		LedOff[comodo] = false;
	}
	
	else if(luminosidade < 25 && !LedOff[comodo])
		DesligarLed(comodo, Lampada);	
}

void EletricSystemClass::DesligarLed(ComodosEnum comodo, int Lampada)
{
	 for(int i = LuminosidadeAtual[comodo]; i>=0;i--)
	 {
		 analogWrite(Lampada, i);
		 delay(120);
	 }
	 
	 analogWrite(Lampada, LOW);
	 LuminosidadeAtual[comodo] = 0;
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