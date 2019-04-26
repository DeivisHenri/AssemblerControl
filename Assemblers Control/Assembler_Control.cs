using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblers_Control
{

    // Script para o controle da produção da fábrica.
    // Script for factory production control.

    public class StructureProgram {
        public StructureProgram() { }

        // ----------------------------------------------------------------------------------------------------
        // [PT-BR]
        // Definir se a informação será mostrada em português ou em inglês. Se a variável for definida como "false",
        // o script irá mostrar as informações em português, caso a variável for definida como "true", então o sript
        // irá mostrar informações em inglês.

        // [EN-US]
        // Define whether the information will be displayed in Portuguese or English. If the variable is set to "false",
        // the script will display the information in Portuguese, if variable is set to "true", then the sript
        // will display information in English.
        // Ex:
        //  public static bool scriptLanguageEnglish = false; => Dislay in Portuguese
        //  public static bool scriptLanguageEnglish = true;  => Dislay in English
        public bool scriptLanguageEnglish = false;

        // ----------------------------------------------------------------------------------------------------
        // [PT-BR]
        // Definir o nome do "container final" que será realizado a leitura dos componentes que já foram produzidos.

        // [EN-US]
        // Set the name of "final container" which will be readout the components that were produced
        public string nameOfFinalContainer = "Final Container";

        // ----------------------------------------------------------------------------------------------------
        // [PT-BR]
        // Para propósito de debug, essa variável receberá o valor "TRUE", caso você queira ver o debug do sistema,
        // caso você deseja que o programa execute sem ver o debug, atribuir o valor de "FALSE" para a variável.

        // [EN-US]
        // For debug purposes, this variable will receive the value "TRUE", in case you want to see the debug of the system,
        //  in case you want the program to execute without seeing the debug, assign the value of "FALSE" to the variable.
        public bool systemDebug = true;

        // ----------------------------------------------------------------------------------------------------
        // [PT-BR]
        // Nome das montadoras que serão utilizadas para produzir os itens.
        // Nesse caso, o processo de produção será dividido em duas montadoras,
        // quanto mais montadoras, mais rápido será a produção.

        // [EN-US]
        // Name of ASSEMBLERS that are going to produce the items.
        // In this case, the production process will be divided into two units,
        // the more ASSEMBLER names, the faster the production will be.
        // Ex:
        //  Dictionary<string, List<string>> factoryAssembler =  new Dictionary<string, List<string>>();
        //  factoryAssembler.Add("assemblerSteelPlate", ["Assembler Steel Plate 1", "Assembler Steel Plate 2"]);
        public Dictionary<string, List<string>> FactoryAssembler() {
            Dictionary<string, List<string>> factoryAssembler = new Dictionary<string, List<string>>();
            factoryAssembler.Add("assemblerBULLETPROOFGLASS", new List<string> { "Assembler Bullet Proof Glass 1" });
            factoryAssembler.Add("assemblerCOMPUTER", new List<string> { "Assembler Computer 1", "Assembler Computer 2" });
            factoryAssembler.Add("assemblerCONSTRUCTION", new List<string> { "Assembler Construction 1", "Assembler Construction 2", "Assembler Construction 3" });
            factoryAssembler.Add("assemblerDETECTOR", new List<string> { "Assembler Detector 1" });
            factoryAssembler.Add("assemblerDISPLAY", new List<string> { "Assembler Display 1" });
            factoryAssembler.Add("assemblerEXPLOSIVE", new List<string> { "Assembler Explosive 1" });
            factoryAssembler.Add("assemblerGIRDER", new List<string> { "Assembler Girder 1" });
            factoryAssembler.Add("assemblerGRAVITYGENERATOR", new List<string> { "Assembler Gravity Generator 1" });
            factoryAssembler.Add("assemblerINTERIOR", new List<string> { "Assembler Interior 1", "Assembler Interior 2", "Assembler Interior 3" });
            factoryAssembler.Add("assemblerLARGETUBE", new List<string> { "Assembler Large Tube 1", "Assembler Large Tube 2" });
            factoryAssembler.Add("assemblerMEDICAL", new List<string> { "Assembler Medical 1" });
            factoryAssembler.Add("assemblerMETALGRID", new List<string> { "Assembler Metal Grid 1", "Assembler Metal Grid 2" });
            factoryAssembler.Add("assemblerMISSILE200MM", new List<string> { "Assembler Missile200MM 1" });
            factoryAssembler.Add("assemblerMOTOR", new List<string> { "Assembler Motor 1", "Assembler Motor 2" });
            factoryAssembler.Add("assemblerNATO25", new List<string> { "Assembler Nato25 1" });
            factoryAssembler.Add("assemblerNATO5P56", new List<string> { "Assembler Nato5P56 1" });
            factoryAssembler.Add("assemblerPOWERCELL", new List<string> { "Assembler Power Cell 1" });
            factoryAssembler.Add("assemblerRADIOCOMMUNICATION", new List<string> { "Assembler Radio Communication 1" });
            factoryAssembler.Add("assemblerREACTOR", new List<string> { "Assembler Reactor 1" });
            factoryAssembler.Add("assemblerSMALLTUBE", new List<string> { "Assembler Small Tube 1", "Assembler Small Tube 2" });
            factoryAssembler.Add("assemblerSOLARCELL", new List<string> { "Assembler Solar Cell 1" });
            factoryAssembler.Add("assemblerSTEELPLATE", new List<string> { "Assembler Steel Plate 1", "Assembler Steel Plate 2", "Assembler Steel Plate 3" });
            factoryAssembler.Add("assemblerSUPERCONDUCTOR", new List<string> { "Assembler Super Conductor 1" });
            factoryAssembler.Add("assemblerTHRUSTER", new List<string> { "Assembler Thruster 1" });

            return factoryAssembler;
        }

        // ----------------------------------------------------------------------------------------------------
        // [PT-BR]
        // Quantidade de componentes para serem mantidos no container. NÃO MUDE A CHAVE, APENAS A QUANTIDADE PARA PRODUÇÃO
        // Por padrão serão:
        //   1000 para componentes primários (ConstructionComponent - InteriorPlate - SteelPlate);
        //    500 para componentes segundários (ComputerComponent - LargeTube - MetalGrid - MotorComponent - SmallTube);
        //    100 para componentes terciários (BulletproofGlass - Display - ExplosivesComponent - Missile200mm -
        //                                     PowerCell - SolarCell);
        //     10 para componentes quaternário (DetectorComponent - GirderComponent - GravityGeneratorComponent - MedicalComponent -
        //                               NATO_25x184mmMagazine - NATO_5p56x45mmMagazine - RadioCommunicationComponent -
        //                               ReactorComponent - Superconductor - ThrustComponent).

        // [EN-US]
        // Quantity of components to be kept in the container. DO NOT CHANGE THE KEY, ONLY THE QUANTITY FOR PRODUCTION
        // By default they will be:
        //   1000 for primary components (ConstructionComponent - InteriorPlate - SteelPlate);
        //    500 for secondary components (ComputerComponent - LargeTube - MetalGrid - MotorComponent - SmallTube);
        //    100 for tertiary components (BulletproofGlass - Display - ExplosivesComponent - Missile200mm -
        //                                 NATO_25x184mmMagazine - NATO_5p56x45mmMagazine - PowerCell - SolarCell);
        //     10 for fourth components (DetectorComponent - GirderComponent - GravityGeneratorComponent - MedicalComponent -
        //                               RadioCommunicationComponent - ReactorComponent - Superconductor - ThrustComponent).

        // Ex:
        //  Dictionary<string, int> componentsAmount =  new Dictionary<string, int>();
        //  componentsAmount.Add("STEELPLATE", 1000);
        public Dictionary<string, int> ComponentsAmount()
        {
            Dictionary<string, int> componentsAmount = new Dictionary<string, int>();
            componentsAmount.Add("BULLETPROOFGLASS", 100);
            componentsAmount.Add("COMPUTER", 500);
            componentsAmount.Add("CONSTRUCTION", 1000);
            componentsAmount.Add("DETECTOR", 10);
            componentsAmount.Add("DISPLAY", 100);
            componentsAmount.Add("EXPLOSIVE", 100);
            componentsAmount.Add("GIRDER", 10);
            componentsAmount.Add("GRAVITYGENERATOR", 10);
            componentsAmount.Add("INTERIOR", 1000);
            componentsAmount.Add("LARGETUBE", 500);
            componentsAmount.Add("MEDICAL", 10);
            componentsAmount.Add("METALGRID", 500);
            componentsAmount.Add("MISSILE200MM", 100);
            componentsAmount.Add("MOTOR", 500);
            componentsAmount.Add("NATO25", 100);
            componentsAmount.Add("NATO5P56", 100);
            componentsAmount.Add("POWERCELL", 100);
            componentsAmount.Add("RADIOCOMMUNICATION", 10);
            componentsAmount.Add("REACTOR", 10);
            componentsAmount.Add("SMALLTUBE", 500);
            componentsAmount.Add("SOLARCELL", 100);
            componentsAmount.Add("STEELPLATE", 1000);
            componentsAmount.Add("SUPERCONDUCTOR", 10);
            componentsAmount.Add("THRUSTER", 10);

            return componentsAmount;
        }

        // ----------------------------------------------------------------------------------------------------
        // Dicionário de componentes e seus identificadores internos.
        // Dictionary of components and their internal identifiers.
        public Dictionary<string, string> ComponentsInternalIdentifiers()
        {
            Dictionary<string, string> componentsIDs = new Dictionary<string, string>();
            componentsIDs.Add("BULLETPROOFGLASS", "BulletproofGlass");
            componentsIDs.Add("COMPUTER", "ComputerComponent");
            componentsIDs.Add("CONSTRUCTION", "ConstructionComponent");
            componentsIDs.Add("DETECTOR", "DetectorComponent");
            componentsIDs.Add("DISPLAY", "Display");
            componentsIDs.Add("EXPLOSIVE", "ExplosivesComponent");
            componentsIDs.Add("GIRDER", "GirderComponent");
            componentsIDs.Add("GRAVITYGENERATOR", "GravityGeneratorComponent");
            componentsIDs.Add("INTERIOR", "InteriorPlate");
            componentsIDs.Add("LARGETUBE", "LargeTube");
            componentsIDs.Add("MEDICAL", "MedicalComponent");
            componentsIDs.Add("METALGRID", "MetalGrid");
            componentsIDs.Add("MISSILE200MM", "Missile200mm");
            componentsIDs.Add("MOTOR", "MotorComponent");
            componentsIDs.Add("NATO25", "NATO_25x184mmMagazine");
            componentsIDs.Add("NATO5P56", "NATO_5p56x45mmMagazine");
            componentsIDs.Add("POWERCELL", "PowerCell");
            componentsIDs.Add("RADIOCOMMUNICATION", "RadioCommunicationComponent");
            componentsIDs.Add("REACTOR", "ReactorComponent");
            componentsIDs.Add("SMALLTUBE", "SmallTube");
            componentsIDs.Add("SOLARCELL", "SolarCell");
            componentsIDs.Add("STEELPLATE", "SteelPlate");
            componentsIDs.Add("SUPERCONDUCTOR", "Superconductor");
            componentsIDs.Add("THRUSTER", "ThrustComponent");

            return componentsIDs;
        }

        // ----------------------------------------------------------------------------------------------------
        // Dicionario para guardar a quantidade de cada componente que está no container final.
        // Dictionary to store the quantity of each component that is in the final containe.
        public Dictionary<string, int> ComponentsAmountProduced()
        {
            Dictionary<string, int> componentsAmountProduced = new Dictionary<string, int>();
            componentsAmountProduced.Add("BULLETPROOFGLASS", 0);
            componentsAmountProduced.Add("COMPUTER", 0);
            componentsAmountProduced.Add("CONSTRUCTION", 0);
            componentsAmountProduced.Add("DETECTOR", 0);
            componentsAmountProduced.Add("DISPLAY", 0);
            componentsAmountProduced.Add("EXPLOSIVE", 0);
            componentsAmountProduced.Add("GIRDER", 0);
            componentsAmountProduced.Add("GRAVITYGENERATOR", 0);
            componentsAmountProduced.Add("INTERIOR", 0);
            componentsAmountProduced.Add("LARGETUBE", 0);
            componentsAmountProduced.Add("MEDICAL", 0);
            componentsAmountProduced.Add("METALGRID", 0);
            componentsAmountProduced.Add("MISSILE200MM", 0);
            componentsAmountProduced.Add("MOTOR", 0);
            componentsAmountProduced.Add("NATO25", 0);
            componentsAmountProduced.Add("NATO5P56", 0);
            componentsAmountProduced.Add("POWERCELL", 0);
            componentsAmountProduced.Add("RADIOCOMMUNICATION", 0);
            componentsAmountProduced.Add("REACTOR", 0);
            componentsAmountProduced.Add("SMALLTUBE", 0);
            componentsAmountProduced.Add("SOLARCELL", 0);
            componentsAmountProduced.Add("STEELPLATE", 0);
            componentsAmountProduced.Add("SUPERCONDUCTOR", 0);
            componentsAmountProduced.Add("THRUSTER", 0);

            return componentsAmountProduced;
        }

        // ----------------------------------------------------------------------------------------------------
        // Dicionário para guardar as quantidades de cada componente a ser contruido.
        // Dictionary to save the quantities of each component to be built.
        public Dictionary<string, int> ComponentsAmountToProduce() {
            Dictionary<string, int> componentsAmountToProduce = new Dictionary<string, int>();
            componentsAmountToProduce.Add("BULLETPROOFGLASS", 0);
            componentsAmountToProduce.Add("COMPUTER", 0);
            componentsAmountToProduce.Add("CONSTRUCTION", 0);
            componentsAmountToProduce.Add("DETECTOR", 0);
            componentsAmountToProduce.Add("DISPLAY", 0);
            componentsAmountToProduce.Add("EXPLOSIVE", 0);
            componentsAmountToProduce.Add("GIRDER", 0);
            componentsAmountToProduce.Add("GRAVITYGENERATOR", 0);
            componentsAmountToProduce.Add("INTERIOR", 0);
            componentsAmountToProduce.Add("LARGETUBE", 0);
            componentsAmountToProduce.Add("MEDICAL", 0);
            componentsAmountToProduce.Add("METALGRID", 0);
            componentsAmountToProduce.Add("MISSILE200MM", 0);
            componentsAmountToProduce.Add("MOTOR", 0);
            componentsAmountToProduce.Add("NATO25", 0);
            componentsAmountToProduce.Add("NATO5P56", 0);
            componentsAmountToProduce.Add("POWERCELL", 0);
            componentsAmountToProduce.Add("RADIOCOMMUNICATION", 0);
            componentsAmountToProduce.Add("REACTOR", 0);
            componentsAmountToProduce.Add("SMALLTUBE", 0);
            componentsAmountToProduce.Add("SOLARCELL", 0);
            componentsAmountToProduce.Add("STEELPLATE", 0);
            componentsAmountToProduce.Add("SUPERCONDUCTOR", 0);
            componentsAmountToProduce.Add("THRUSTER", 0);

            return componentsAmountToProduce;
        }        
    }

    public class ProcessProgram {

        public ProcessProgram() { }

        public void CheckAssemblersANDContainer(ref Dictionary<string, List<string>> factoryAssembler,
                                                string nameOfFinalContainer,
                                                ref StringBuilder displayInfoLayout,
												ref StringBuilder displayDebugg,
												bool scriptLanguageEnglish,
                                                bool systemDebug) {
													
			List<IMyAssembler> gridAssemblers = new List<IMyAssembler>();
            IMyGridTerminalSystem.GetBlocksOfType<IMyAssembler>(gridAssemblers);
			int assemblerFound = 0;

            if (systemDebug == true && scriptLanguageEnglish == false) displayDebugg.AppendLine("VERIFICAÇÃO DOS ASSEMBLERS E CONTAINER FINAL");
			else (systemDebug == true && scriptLanguageEnglish == true) displayDebugg.AppendLine("VERIFICATION OF ASSEMBLERS AND FINAL CONTAINER");
			
			if (scriptLanguageEnglish == false) displayInfoLayout.AppendLine("Assembler Qtd Nome Informado Qtd Encontrado")
			else displayInfoLayout.AppendLine("Assembler Qty Name Reported Qty Found")

            foreach (KeyValuePair<string, List<string>> assembler in factoryAssembler) {
				assemblerFound = 0;
                for (var i = 0; i < assembler.Value.Count; i++) {
                    if (gridAssemblers.Name.Equals(assembler.Value[i])){
						assemblerFound = assemblerFound + 1;
						if (systemDebug == true) displayDebugg.AppendLine("Assembler da Extrutura: " + assembler.key + " Assembler com o Nome: " + gridAssemblers.name);
					}
                }
				displayInfoLayout.AppendLine(assembler.key + " " assembler.Value.Count + " " + assemblerFound)
            }
        }
		
		public void CheckProductionNeed(ref Dictionary<string, int> componentsAmount,
									    ref Dictionary<string, int> componentsAmountProduce,
									    ref Dictionary<string, int> componentsAmountToProduced,
									    string nameOfFinalContainer,
									    ref StringBuilder displayInfoLayout,
									    ref StringBuilder displayDebugg,
									    bool scriptLanguageEnglish,
									    bool systemDebug){
			
			
				
		}
    }

    public static void Main(string argument/*, UpdateType updateSource*/) {
        // Estrutura do layout para mostrar as informações na tela LCD.
        // Layout to display information on the LCD screen.
        StringBuilder displayInfoLayout = new StringBuilder();
		StringBuilder displayDebugg = new StringBuilder();
        var structureProgram = new StructureProgram();
        var processProgram = new ProcessProgram();

        Dictionary<string, List<string>> factoryAssembler = structureProgram.FactoryAssembler();
        Dictionary<string, int> componentsAmount = structureProgram.ComponentsAmount();
        Dictionary<string, int> componentsAmountProduce = structureProgram.ComponentsAmountProduced();
        Dictionary<string, int> componentsAmountToProduced = structureProgram.ComponentsAmountToProduce();

        if(structureProgram.systemDebug == true & structureProgram.scriptLanguageEnglish == true) Console.Write("---------- DEBUG SYSTEM ----------");
        else if (structureProgram.systemDebug == true & structureProgram.scriptLanguageEnglish == false) Console.Write("---------- SISTEM EM DEBUG MODO ----------");

        if (structureProgram.scriptLanguageEnglish) displayInfoLayout.AppendLine("Assembler Control - Begin Process");
        else displayInfoLayout.AppendLine("Assembler Control - Início do Processo");
        displayInfoLayout.AppendLine("");
        displayInfoLayout.AppendLine("");
        // VERIFICAR SE OS ASSEMBLER EXISTEM
        if (structureProgram.scriptLanguageEnglish) displayInfoLayout.AppendLine("Assembler Control - Begin Process");
        else displayInfoLayout.AppendLine("Assembler Control - 1º Processo - Verificando se os ASSEMBLERS existem e se o CONTAINER FINAL existe");

		// VERIFICAR SE O CONTAINER FINAL EXISTE
        processProgram.CheckAssemblersANDContainer(ref factoryAssembler,
												   structureProgram.nameOfFinalContainer,
												   ref displayInfoLayout,
												   ref displayDebugg,
												   scriptLanguageEnglish,
												   structureProgram.systemDebug);

        //VERIFICAR A NECESSIDADE DE PRODUÇÃO DE CADA COMPONENTE
		processProgram.CheckProductionNeed(ref componentsAmount,
										   ref componentsAmountProduce,
										   ref componentsAmountToProduced,
										   structureProgram.nameOfFinalContainer,
										   ref displayInfoLayout,
										   ref displayDebugg,
										   scriptLanguageEnglish,
										   structureProgram.systemDebug);
										   
        //ATUALIZAR OS ASSEMBLERS
        //ATUALIZAR LCD


    }
}