using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MI.Modules.Serial
{
    public enum Comodos
    {
        Quarto1 = 0,
        Quarto2 = 1,
        Sala = 2,
        Cozinha = 3,
        Banheiro = 4,
    }
    public enum ArgsType : int 
    {
        //Sistema hídrico
        VolumeR1		= 0x1,
        VolumeR2		= 0x2,
        VolumeTanque	= 0x4,
        Fluxo			= 0x8,

	    //Desligar as luzes de determinado cômodo
	    OffLuz_Sala		= 0x10,
	    OffLuz_Q1		= 0x20,
	    OffLuz_Q2		= 0x40,
	    OffLuz_Banheiro = 0x80,
	    OffLuz_Cozinha	= 0x100,

	    //Desligar AutoDimmer
	    OffDimmer_Sala		= 0x200,
	    OffDimmer_Q1		= 0x400,
	    OffDimmer_Q2		= 0x800,
	    OffDimmer_Banheiro	= 0x1000,
	    OffDimmer_Cozinha	= 0x2000,

        //Pegar o consumo de iluminação da casa
        ValorIluminacao = 0x4000,

        //Desligar client
        ShutdownClient = 0x8000,
    }
}
