################################################################################
# Automatically-generated file. Do not edit!
################################################################################

SHELL := cmd.exe
RM := rm -rf

USER_OBJS :=

LIBS := 
PROJ := 

O_SRCS := 
C_SRCS := 
S_SRCS := 
S_UPPER_SRCS := 
OBJ_SRCS := 
ASM_SRCS := 
PREPROCESSING_SRCS := 
OBJS := 
OBJS_AS_ARGS := 
C_DEPS := 
C_DEPS_AS_ARGS := 
EXECUTABLES := 
OUTPUT_FILE_PATH :=
OUTPUT_FILE_PATH_AS_ARGS :=
AVR_APP_PATH :=$$$AVR_APP_PATH$$$
QUOTE := "
ADDITIONAL_DEPENDENCIES:=
OUTPUT_FILE_DEP:=
LIB_DEP:=
LINKER_SCRIPT_DEP:=

# Every subdirectory with source files must be described here
SUBDIRS :=  \
../Visual\ Micro/


# Add inputs and outputs from these tool invocations to the build variables 
C_SRCS +=  \
../EletricSystem.cpp \
../MainClass.cpp \
../WaterSystem.cpp


PREPROCESSING_SRCS += 


ASM_SRCS += 


OBJS +=  \
EletricSystem.o \
MainClass.o \
WaterSystem.o

OBJS_AS_ARGS +=  \
EletricSystem.o \
MainClass.o \
WaterSystem.o

C_DEPS +=  \
EletricSystem.d \
MainClass.d \
WaterSystem.d

C_DEPS_AS_ARGS +=  \
EletricSystem.d \
MainClass.d \
WaterSystem.d

OUTPUT_FILE_PATH +=Maquete_Beta.elf

OUTPUT_FILE_PATH_AS_ARGS +=Maquete_Beta.elf

ADDITIONAL_DEPENDENCIES:=

OUTPUT_FILE_DEP:= ./makedep.mk

LIB_DEP+= 

LINKER_SCRIPT_DEP+= 


# AVR32/GNU C Compiler







./%.o: .././%.cpp
	@echo Building file: $<
	@echo Invoking: AVR8/GNU C Compiler : 4.8.1
	$(QUOTE)C:\Program Files (x86)\Atmel\Atmel Toolchain\AVR8 GCC\Native\3.4.1061\avr8-gnu-toolchain\bin\avr-g++.exe$(QUOTE) -funsigned-char -funsigned-bitfields -D__AVR_ATmega2560__ -D_VMDEBUG=1 -DARDUINO=161 -DARDUINO_MAIN -DF_CPU=16000000L -D__AVR__  -I"{runtime.tools.avr-gcc.path}/lib/gcc/avr/4.8.1/include" -I"{runtime.tools.avr-gcc.path}//avr/include/avr" -I"{runtime.tools.avr-gcc.path}/avr/include" -I"C:/Program Files (x86)/Visual Micro/Visual Micro for Arduino/Micro Platforms/default/debuggers" -I"D:/Program Files (x86)/Arduino/hardware/arduino/avr/libraries" -I"D:/Program Files (x86)/Arduino/libraries" -I"../../Maquete_Alpha" -I"D:/Program Files (x86)/Arduino/hardware/arduino/avr/variants/mega" -I"D:/Program Files (x86)/Arduino/hardware/arduino/avr/cores/arduino"  -O1 -ffunction-sections -fdata-sections -fpack-struct -fshort-enums -g2 -Wall -mmcu=atmega2560 -c -MD -MP -MF "$(@:%.o=%.d)" -MT"$(@:%.o=%.d)" -MT"$(@:%.o=%.o)"   -o "$@" "$<" 
	@echo Finished building: $<
	



# AVR32/GNU Preprocessing Assembler



# AVR32/GNU Assembler




ifneq ($(MAKECMDGOALS),clean)
ifneq ($(strip $(C_DEPS)),)
-include $(C_DEPS)
endif
endif

# Add inputs and outputs from these tool invocations to the build variables 

# All Target
all: $(OUTPUT_FILE_PATH) $(ADDITIONAL_DEPENDENCIES)

$(OUTPUT_FILE_PATH): $(OBJS) $(USER_OBJS) $(OUTPUT_FILE_DEP) $(LIB_DEP) $(LINKER_SCRIPT_DEP)
	@echo Building target: $@
	@echo Invoking: AVR8/GNU Linker : 4.8.1
	$(QUOTE)C:\Program Files (x86)\Atmel\Atmel Toolchain\AVR8 GCC\Native\3.4.1061\avr8-gnu-toolchain\bin\avr-g++.exe$(QUOTE) -o$(OUTPUT_FILE_PATH_AS_ARGS) $(OBJS_AS_ARGS) $(USER_OBJS) $(LIBS) -Wl,-Map="Maquete_Beta.map" -Wl,--start-group  -Wl,--end-group -Wl,--gc-sections -mmcu=atmega2560  
	@echo Finished building target: $@
	"C:\Program Files (x86)\Atmel\Atmel Toolchain\AVR8 GCC\Native\3.4.1061\avr8-gnu-toolchain\bin\avr-objcopy.exe" -O ihex -R .eeprom -R .fuse -R .lock -R .signature -R .user_signatures  "Maquete_Beta.elf" "Maquete_Beta.hex"
	"C:\Program Files (x86)\Atmel\Atmel Toolchain\AVR8 GCC\Native\3.4.1061\avr8-gnu-toolchain\bin\avr-objcopy.exe" -j .eeprom  --set-section-flags=.eeprom=alloc,load --change-section-lma .eeprom=0  --no-change-warnings -O ihex "Maquete_Beta.elf" "Maquete_Beta.eep" || exit 0
	"C:\Program Files (x86)\Atmel\Atmel Toolchain\AVR8 GCC\Native\3.4.1061\avr8-gnu-toolchain\bin\avr-objdump.exe" -h -S "Maquete_Beta.elf" > "Maquete_Beta.lss"
	"C:\Program Files (x86)\Atmel\Atmel Toolchain\AVR8 GCC\Native\3.4.1061\avr8-gnu-toolchain\bin\avr-objcopy.exe" -O srec -R .eeprom -R .fuse -R .lock -R .signature -R .user_signatures "Maquete_Beta.elf" "Maquete_Beta.srec"
	"C:\Program Files (x86)\Atmel\Atmel Toolchain\AVR8 GCC\Native\3.4.1061\avr8-gnu-toolchain\bin\avr-size.exe" "Maquete_Beta.elf"
	
	





# Other Targets
clean:
	-$(RM) $(OBJS_AS_ARGS) $(EXECUTABLES)  
	-$(RM) $(C_DEPS_AS_ARGS)   
	rm -rf "Maquete_Beta.elf" "Maquete_Beta.a" "Maquete_Beta.hex" "Maquete_Beta.lss" "Maquete_Beta.eep" "Maquete_Beta.map" "Maquete_Beta.srec" "Maquete_Beta.usersignatures"
	