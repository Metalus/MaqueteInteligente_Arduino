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


 public:
	float GetVolumeR1();
	float GetVolumeR2();
	float GetVolumeTanque();
	float GetFluxo();
};

extern WaterSystemClass WaterSystem;

#endif

