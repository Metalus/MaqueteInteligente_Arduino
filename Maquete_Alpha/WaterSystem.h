// WaterSystem.h

#ifndef _WATERSYSTEM_h
#define _WATERSYSTEM_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif

class WaterSystemClass
{
 protected:
	int Volumes[3] = {180,150,100};

 public:
	void ControlarTanques();
	int GetVolumeR1();
	int GetVolumeR2();
	int GetVolumeTanque();
	void ControlarLuzTanque(int Tanque, int Nivel);
	float GetFluxo();
};

extern WaterSystemClass WaterSystem;

#endif

