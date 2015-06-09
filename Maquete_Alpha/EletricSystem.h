// EletricSystem.h

#ifndef _ELETRICSYSTEM_h
#define _ELETRICSYSTEM_h
#if defined(ARDUINO) && ARDUINO >= 100
#include "arduino.h"
#else
#include "WProgram.h"
#endif

#include "DefinesMaquete.h"

enum ComodosEnum : byte
{
	Quarto1,
	Quarto2,
	Sala,
	Banheiro,
	Cozinha
};


class Comodos
{
	public:
	static int GetLampadaID(ComodosEnum comodo);
	static int GetSensorID(ComodosEnum comodo);
};

class EletricSystemClass
{
	private:
		bool LedOff[5] = {false,false,false,false,false};
	
	public:
	int LuminosidadeAtual[5] = {0,0,0,0,0};
	void Dimerizar(ComodosEnum comodo);
	void DesligarLed(ComodosEnum comodo, int Lampada);
};

extern EletricSystemClass EletricSystem;

#endif

