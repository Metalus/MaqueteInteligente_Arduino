/*
 * MyClass.h
 *
 * Created: 5/11/2015 1:29:14 PM
 * Author: Win 64 Btis
 */ 

#ifndef _MAINCLASS_h
#define _MAINCLASS_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "Arduino.h"
#else
	#include "WProgram.h"
#endif

class MainClass
{
 private:


 public:
	void setup();
	void loop();
};

extern MainClass mainClass;

#endif

