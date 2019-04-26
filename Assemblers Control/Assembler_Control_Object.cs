using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using VRageMath;
using VRage.Game;
using VRage.Collections;
using Sandbox.ModAPI.Ingame;
using VRage.Game.Components;
using VRage.Game.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using Sandbox.Game.EntityComponents;
using SpaceEngineers.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.Definitions;
using Sandbox.Game.GameSystems;

namespace Assemblers_Control_Object
{

    // Script para o controle da produção da fábrica.
    // Script for factory production control.



    public class StructureProgram
    {
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
        public Dictionary<string, List<string>> FactoryAssembler()
        {
            Dictionary<string, List<string>> factoryAssembler = new Dictionary<string, List<string>>
            {
                { "BULLETPROOFGLASS", new List<string> { "Assembler Bullet Proof Glass 1" } },
                { "COMPUTER", new List<string> { "Assembler Computer 1", "Assembler Computer 2" } },
                { "CONSTRUCTION", new List<string> { "Assembler Construction 1", "Assembler Construction 2", "Assembler Construction 3" } },
                { "DETECTOR", new List<string> { "Assembler Detector 1" } },
                { "DISPLAY", new List<string> { "Assembler Display 1" } },
                { "EXPLOSIVE", new List<string> { "Assembler Explosive 1" } },
                { "GIRDER", new List<string> { "Assembler Girder 1" } },
                { "GRAVITYGENERATOR", new List<string> { "Assembler Gravity Generator 1" } },
                { "INTERIOR", new List<string> { "Assembler Interior 1", "Assembler Interior 2", "Assembler Interior 3" } },
                { "LARGETUBE", new List<string> { "Assembler Large Tube 1", "Assembler Large Tube 2" } },
                { "MEDICAL", new List<string> { "Assembler Medical 1" } },
                { "METALGRID", new List<string> { "Assembler Metal Grid 1", "Assembler Metal Grid 2" } },
                { "MISSILE200MM", new List<string> { "Assembler Missile200MM 1" } },
                { "MOTOR", new List<string> { "Assembler Motor 1", "Assembler Motor 2" } },
                { "NATO25", new List<string> { "Assembler Nato25 1" } },
                { "NATO5P56", new List<string> { "Assembler Nato5P56 1" } },
                { "POWERCELL", new List<string> { "Assembler Power Cell 1" } },
                { "RADIOCOMMUNICATION", new List<string> { "Assembler Radio Communication 1" } },
                { "REACTOR", new List<string> { "Assembler Reactor 1" } },
                { "SMALLTUBE", new List<string> { "Assembler Small Tube 1", "Assembler Small Tube 2" } },
                { "SOLARCELL", new List<string> { "Assembler Solar Cell 1" } },
                { "STEELPLATE", new List<string> { "Assembler Steel Plate 1", "Assembler Steel Plate 2", "Assembler Steel Plate 3" } },
                { "SUPERCONDUCTOR", new List<string> { "Assembler Super Conductor 1" } },
                { "THRUSTER", new List<string> { "Assembler Thruster 1" } }
            };

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
            Dictionary<string, int> componentsAmount = new Dictionary<string, int>
            {
                { "BULLETPROOFGLASS", 100 },
                { "COMPUTER", 500 },
                { "CONSTRUCTION", 1000 },
                { "DETECTOR", 10 },
                { "DISPLAY", 100 },
                { "EXPLOSIVE", 100 },
                { "GIRDER", 10 },
                { "GRAVITYGENERATOR", 10 },
                { "INTERIOR", 1000 },
                { "LARGETUBE", 500 },
                { "MEDICAL", 10 },
                { "METALGRID", 500 },
                { "MISSILE200MM", 100 },
                { "MOTOR", 500 },
                { "NATO25", 100 },
                { "NATO5P56", 100 },
                { "POWERCELL", 100 },
                { "RADIOCOMMUNICATION", 10 },
                { "REACTOR", 10 },
                { "SMALLTUBE", 500 },
                { "SOLARCELL", 100 },
                { "STEELPLATE", 1000 },
                { "SUPERCONDUCTOR", 10 },
                { "THRUSTER", 10 }
            };

            return componentsAmount;
        }

        // ----------------------------------------------------------------------------------------------------
        // Dicionario para guardar a quantidade de cada componente que está no container final.
        // Dictionary to store the quantity of each component that is in the final containe.
        public Dictionary<string, int> ComponentsAmountProduced()
        {
            Dictionary<string, int> componentsAmountProduced = new Dictionary<string, int>
            {
                { "BULLETPROOFGLASS", 0 },
                { "COMPUTER", 0 },
                { "CONSTRUCTION", 0 },
                { "DETECTOR", 0 },
                { "DISPLAY", 0 },
                { "EXPLOSIVE", 0 },
                { "GIRDER", 0 },
                { "GRAVITYGENERATOR", 0 },
                { "INTERIOR", 0 },
                { "LARGETUBE", 0 },
                { "MEDICAL", 0 },
                { "METALGRID", 0 },
                { "MISSILE200MM", 0 },
                { "MOTOR", 0 },
                { "NATO25", 0 },
                { "NATO5P56", 0 },
                { "POWERCELL", 0 },
                { "RADIOCOMMUNICATION", 0 },
                { "REACTOR", 0 },
                { "SMALLTUBE", 0 },
                { "SOLARCELL", 0 },
                { "STEELPLATE", 0 },
                { "SUPERCONDUCTOR", 0 },
                { "THRUSTER", 0 }
            };

            return componentsAmountProduced;
        }

        // ----------------------------------------------------------------------------------------------------
        // Dicionário de componentes e seus identificadores internos.
        // Dictionary of components and their internal identifiers.
        public Dictionary<string, string> ComponentsInternalIdentifiers()
        {
            Dictionary<string, string> componentsIDs = new Dictionary<string, string>
            {
                { "BULLETPROOFGLASS", "BulletproofGlass" },
                { "COMPUTER", "ComputerComponent" },
                { "CONSTRUCTION", "ConstructionComponent" },
                { "DETECTOR", "DetectorComponent" },
                { "DISPLAY", "Display" },
                { "EXPLOSIVE", "ExplosivesComponent" },
                { "GIRDER", "GirderComponent" },
                { "GRAVITYGENERATOR", "GravityGeneratorComponent" },
                { "INTERIOR", "InteriorPlate" },
                { "LARGETUBE", "LargeTube" },
                { "MEDICAL", "MedicalComponent" },
                { "METALGRID", "MetalGrid" },
                { "MISSILE200MM", "Missile200mm" },
                { "MOTOR", "MotorComponent" },
                { "NATO25", "NATO_25x184mmMagazine" },
                { "NATO5P56", "NATO_5p56x45mmMagazine" },
                { "POWERCELL", "PowerCell" },
                { "RADIOCOMMUNICATION", "RadioCommunicationComponent" },
                { "REACTOR", "ReactorComponent" },
                { "SMALLTUBE", "SmallTube" },
                { "SOLARCELL", "SolarCell" },
                { "STEELPLATE", "SteelPlate" },
                { "SUPERCONDUCTOR", "Superconductor" },
                { "THRUSTER", "ThrustComponent" }
            };

            return componentsIDs;
        }

        // ----------------------------------------------------------------------------------------------------
        // Dicionário para guardar as quantidades de cada componente a ser contruido.
        // Dictionary to save the quantities of each component to be built.
        public Dictionary<string, int> ComponentsAmountToProduce()
        {
            Dictionary<string, int> componentsAmountToProduce = new Dictionary<string, int>
            {
                { "BULLETPROOFGLASS", 0 },
                { "COMPUTER", 0 },
                { "CONSTRUCTION", 0 },
                { "DETECTOR", 0 },
                { "DISPLAY", 0 },
                { "EXPLOSIVE", 0 },
                { "GIRDER", 0 },
                { "GRAVITYGENERATOR", 0 },
                { "INTERIOR", 0 },
                { "LARGETUBE", 0 },
                { "MEDICAL", 0 },
                { "METALGRID", 0 },
                { "MISSILE200MM", 0 },
                { "MOTOR", 0 },
                { "NATO25", 0 },
                { "NATO5P56", 0 },
                { "POWERCELL", 0 },
                { "RADIOCOMMUNICATION", 0 },
                { "REACTOR", 0 },
                { "SMALLTUBE", 0 },
                { "SOLARCELL", 0 },
                { "STEELPLATE", 0 },
                { "SUPERCONDUCTOR", 0 },
                { "THRUSTER", 0 }
            };

            return componentsAmountToProduce;
        }
    }

    public class FinalContainer {
        private string containerName;
        private bool containerExist;
        private double containerCapacity;
        private double containerCapacityUsed;
        private bool containerIsFull;
        private bool containerFound;

        public void setContainerName(string value) {
            this.containerName = value;
        }
        public string getContainerName() {
            return this.containerName;
        }

        public void setContainerExist(bool value) {
            this.containerExist = value;
        }
        public bool getContainerExist() {
            return this.containerExist;
        }

        public void setContainerCapacity(double value) {
            this.containerCapacity = value;
        }
        public double getContainerCapacity() {
            return this.containerCapacity;
        }

        public void setContainerCapacityUsed(double value) {
            this.containerCapacityUsed = value;
        }
        public double getContainerCapacityUsed() {
            return this.containerCapacityUsed;
        }

        public void setContainerIsFull(bool value) {
            this.containerIsFull = value;
        }
        public bool getContainerIsFull() {
            return this.containerIsFull;
        }

        public void setContainerFound(bool value) {
            this.containerFound = value;
        }
        public bool getContainerFound() {
            return this.containerFound;
        }

    }

    public class ControlObject
    {
        private string assemblerKey;
        private List<string> assemblersNames;
        private string componentIDValue;
        private int componetAmountKeepStorage;
        private int componentsAmountProduced;
        private int componentsAmountToProduce;

        // Compos para validações
        private bool foundAllAssemblers;
        private int qtdyOfAssemblersFound;

        public void setAssemblerKey(string value)
        {
            this.assemblerKey = value;
        }
        public string getAssemblerKey()
        {
            return this.assemblerKey;
        }

        public void setAssemblersNames(List<string> value)
        {
            this.assemblersNames = value;
        }
        public List<string> getAssemblersNames()
        {
            return this.assemblersNames;
        }

        public void setComponentIDValue(string value)
        {
            this.componentIDValue = value;
        }
        public string getComponentIDValue()
        {
            return this.componentIDValue;
        }

        public void setComponetAmountKeepStorage(int value)
        {
            this.componetAmountKeepStorage = value;
        }
        public int getComponetAmountKeepStorage()
        {
            return this.componetAmountKeepStorage;
        }

        public void setComponentsAmountProduced(int value)
        {
            this.componentsAmountProduced = value;
        }
        public int getComponentsAmountProduced()
        {
            return this.componentsAmountProduced;
        }

        public void setComponentsAmountToProduce(int value)
        {
            this.componentsAmountToProduce = value;
        }
        public int getComponentsAmountToProduce()
        {
            return this.componentsAmountToProduce;
        }

        public void setFoundAllAssemblers(bool value)
        {
            this.foundAllAssemblers = value;
        }
        public bool getFoundAllAssemblers()
        {
            return this.foundAllAssemblers;
        }

        public void setQtdyOfAssemblersFound(int value)
        {
            this.qtdyOfAssemblersFound = value;
        }
        public int getQtdyOfAssemblersFound()
        {
            return this.qtdyOfAssemblersFound;
        }

        public ControlObject() { }

    }

    public class ProcessProgram
    {

        public ProcessProgram() { }

        public void CreateControlObject(ref List<ControlObject> controlObjectList,
                                        Dictionary<string, List<string>> factoryAssembler,
                                        Dictionary<string, string> componentsInternalIdentifiers,
                                        Dictionary<string, int> componentsAmount,
                                        Dictionary<string, int> componentsAmountProduce,
                                        Dictionary<string, int> componentsAmountToProduced,
                                        ref StringBuilder displayInfoLayout,
                                        ref StringBuilder displayDebugg,
                                        bool scriptLanguageEnglish,
                                        bool systemDebug) {

            string internalID = "";
            int amount = 0, amountProduce = 0, amountToProduced = 0;

            if (systemDebug == true && scriptLanguageEnglish == false) displayDebugg.AppendLine("CRIAÇÃO DO OBJETO CONTROLE");
            else if (systemDebug == true && scriptLanguageEnglish == true) displayDebugg.AppendLine("CREATION OF THE CONTROL OBJECT");

            foreach (KeyValuePair<string, List<string>> assembler in factoryAssembler) {
                ControlObject controlObject = new ControlObject();
                controlObject.setAssemblerKey(assembler.Key);
                controlObject.setAssemblersNames(assembler.Value);

                componentsInternalIdentifiers.TryGetValue(assembler.Key, out internalID);
                controlObject.setComponentIDValue(internalID);

                componentsAmount.TryGetValue(assembler.Key, out amount);
                controlObject.setComponetAmountKeepStorage(amount);

                componentsAmountProduce.TryGetValue(assembler.Key, out amountProduce);
                controlObject.setComponentsAmountProduced(amountProduce);

                componentsAmountToProduced.(assembler.Key, out amountToProduced);
                controlObject.setComponentsAmountToProduce(amountToProduced);

                if (systemDebug == true && scriptLanguageEnglish == false) {
                    displayDebugg.AppendLine("---------- Início ----------");
                    displayDebugg.AppendLine("Key do objeto: " + controlObject.getAssemblerKey());
                    displayDebugg.AppendLine("Nome(s) do(s) assembler(s): " + controlObject.getAssemblersNames());
                    displayDebugg.AppendLine("Nome do component interno: " + controlObject.getComponentIDValue());
                    displayDebugg.AppendLine("Quantidade há ser mantido no container: " + controlObject.getComponetAmountKeepStorage());
                    displayDebugg.AppendLine("Quantidade de component no container final: " + controlObject.getComponentsAmountProduced());
                    displayDebugg.AppendLine("Quantidade de component a ser produzido: " + controlObject.getComponentsAmountToProduce());
                    displayDebugg.AppendLine("---------- Final ----------");
                    displayDebugg.AppendLine("");
                } else if (systemDebug == true && scriptLanguageEnglish == true) {
                    displayDebugg.AppendLine("---------- Begin ----------");
                    displayDebugg.AppendLine("Object Key: " + controlObject.getAssemblerKey());
                    displayDebugg.AppendLine("Name(s) of assembler(s): " + controlObject.getAssemblersNames());
                    displayDebugg.AppendLine("Name of internal component: " + controlObject.getComponentIDValue());
                    displayDebugg.AppendLine("Quantity must be kept in container: " + controlObject.getComponetAmountKeepStorage());
                    displayDebugg.AppendLine("Quantity of component in final container: " + controlObject.getComponentsAmountProduced());
                    displayDebugg.AppendLine("Quantity of component to be produced: " + controlObject.getComponentsAmountToProduce());
                    displayDebugg.AppendLine("---------- End ----------");
                    displayDebugg.AppendLine("");
                }
                controlObjectList.Add(controlObject);
            }
        }

        public bool CreateFinalContainer(ref FinalContainer finalContainer,
                                         string nameOfFinalContainer,
                                         ref StringBuilder displayInfoLayout,
                                         ref StringBuilder displayDebugg,
                                         bool scriptLanguageEnglish,
                                         bool systemDebug)
        {
            VRage.MyFixedPoint currentVolume = 0;
            VRage.MyFixedPoint maxVolume = 0;

            IMyGridTerminalSystem GridTerminalSystem = null;
            IMyTerminalBlock myCargoContainers = GridTerminalSystem.GetBlockWithName(finalContainer.getContainerName());

            if (myCargoContainers != null) finalContainer.setContainerExist(true);
            else finalContainer.setContainerExist(false);

            if (!finalContainer.getContainerExist()) {
                if (scriptLanguageEnglish == false) displayInfoLayout.AppendLine("Container final não existe. Favor informar o nome corretamento do container final.");
                else displayInfoLayout.AppendLine("Final Container does not exist.Please inform the correct name of the final container.");
                return false; ;
            }

            IMyInventory containerInventory = myCargoContainers.GetInventory(0);
            containerInventory.IsFull;
            currentVolume += containerInventory.CurrentVolume;
            maxVolume += containerInventory.MaxVolume;

            double percentage = ((double)currentVolume / (double)maxVolume) * 100;

            finalContainer.setContainerCapacity((double)maxVolume);
            



            //finalContainer.setContainerName(nameOfFinalContainer);
            //MyGridProgram myGridProgram;
            ////GetBlockWithName(finalContainer.getContainerName());

            //IMyCargoContainer myCargoContainer = GridTerminalSystem.GetBlockWithName) as IMyCargoContainer;
            //MyGridTerminalSystem myGridTerminalSystem = new MyGridTerminalSystem();

            //MyGridTerminalSystem.GetBlocksOfType<IMyCargoContainer>(myCargoContainer);
            //GridTerminalSystem.GetBlocksOfType<IMyMechanicalConnectionBlock>(blocks);


            // VERIFICA SE O CONTAINER EXISTE
            // VERIFICA A CAPACIDADE TOTAL
            // VERIFICA A CAPACIDADE UTILIZADA
            // VERIFICA SE ESTÁ CHEIO
            // CASO NÂO EXISTA
            // RETORNOE FALSE, MOSTRAR ERRO NO LOG
        }

        public void CheckAssemblers(ref List<ControlObject> controlObjectList,
                                    string nameOfFinalContainer,
                                    ref StringBuilder displayInfoLayout,
                                    ref StringBuilder displayDebugg,
                                    bool scriptLanguageEnglish,
                                    bool systemDebug) {

            IMyAssembler foo = GridTerminalSystem.GetBlockWithName("My block");

            List<IMyAssembler> gridAssemblers = new List<IMyAssembler>();
            IMyGridTerminalSystem.GetBlocksOfType<IMyAssembler>(gridAssemblers);
            int assemblerFound = 0;

            if (systemDebug == true && scriptLanguageEnglish == false) displayDebugg.AppendLine("VERIFICAÇÃO DOS ASSEMBLERS E CONTAINER FINAL");
            else if (systemDebug == true && scriptLanguageEnglish == true) displayDebugg.AppendLine("VERIFICATION OF ASSEMBLERS AND FINAL CONTAINER");

            if (scriptLanguageEnglish == false) displayInfoLayout.AppendLine("Assembler Qtd Nome Informado / Qtd Encontrado");
            else displayInfoLayout.AppendLine("Assembler Qty Name Reported Qty Found");

            foreach (ControlObject controlObject in controlObjectList) {
                assemblerFound = 0;
                for (var i = 0; i < controlObject.getAssemblersNames().Count; i++) {
                    if (gridAssemblers.Name.Equals(controlObject.getAssemblersNames()[i])) {
                        assemblerFound = assemblerFound + 1;
                        if (systemDebug == true) displayDebugg.AppendLine("Assembler da Extrutura: " + assembler.key + " Assembler com o Nome: " + gridAssemblers.name);
                    }
                }
                displayInfoLayout.AppendLine(controlObject.getAssemblerKey() + " " controlObject.getAssemblersNames().Count + " / " + assemblerFound);

                controlObject.setQtdyOfAssemblersFound(assemblerFound);

                if (controlObject.getAssemblersNames().Count == controlObject.getQtdyOfAssemblersFound()) controlObject.setFoundAllAssemblers(true);
                else controlObject.setFoundAllAssemblers(false);
            }
        }

        public void CheckProductionNeed(ref Dictionary<string, int> componentsAmount,
                                        ref Dictionary<string, int> componentsAmountProduce,
                                        ref Dictionary<string, int> componentsAmountToProduced,
                                        string nameOfFinalContainer,
                                        ref StringBuilder displayInfoLayout,
                                        ref StringBuilder displayDebugg,
                                        bool scriptLanguageEnglish,
                                        bool systemDebug) {

            IMyTerminalBlock foo = GridTerminalSystem.GetBlockWithName("My block");

            IMyContainer finalContainer = new IMyContainer();
            IMyGridTerminalSystem.GetBlocksOfType<IMyAssembler>(finalContainer);

        }
    }

    public static void Main(string argument/*, UpdateType updateSource*/)
    {
        // Estrutura do layout para mostrar as informações na tela LCD.
        // Layout to display information on the LCD screen.
        StringBuilder displayInfoLayout = new StringBuilder();
        StringBuilder displayDebugg = new StringBuilder();

        var structureProgram = new StructureProgram();
        var processProgram = new ProcessProgram();

        IMyTextPanel infoLayoutLCD;

        List<ControlObject> controlObjectList = new List<ControlObject>();
        FinalContainer finalContainer = new FinalContainer();

        Dictionary<string, List<string>> factoryAssembler = structureProgram.FactoryAssembler();
        Dictionary<string, string> componentsInternalIdentifiers = structureProgram.ComponentsInternalIdentifiers();


        Dictionary<string, int> componentsAmount = structureProgram.ComponentsAmount();
        Dictionary<string, int> componentsAmountProduce = structureProgram.ComponentsAmountProduced();
        Dictionary<string, int> componentsAmountToProduced = structureProgram.ComponentsAmountToProduce();

        if (structureProgram.systemDebug == true & structureProgram.scriptLanguageEnglish == true) Console.Write("---------- DEBUG SYSTEM ----------");
        else if (structureProgram.systemDebug == true & structureProgram.scriptLanguageEnglish == false) Console.Write("---------- SISTEM EM DEBUG MODO ----------");

        if (structureProgram.scriptLanguageEnglish) displayInfoLayout.AppendLine("Assembler Control - Begin Process");
        else displayInfoLayout.AppendLine("Assembler Control - Início do Processo");
        displayInfoLayout.AppendLine("");
        displayInfoLayout.AppendLine("");

        //CONTAINER
        if (!processProgram.CreateFinalContainer(ref finalContainer,
                                                structureProgram.nameOfFinalContainer,
                                                ref displayInfoLayout,
                                                ref displayDebugg,
                                                structureProgram.scriptLanguageEnglish,
                                                structureProgram.systemDebug))
        {

        }


        if (!finalContainer.getContainerFound()) {
            if (structureProgram.systemDebug == true & structureProgram.scriptLanguageEnglish == true) Console.Write("Container final não encontrado. Adicione o nome do container final existe e reinicie o processo.");
            else if (structureProgram.systemDebug == true & structureProgram.scriptLanguageEnglish == false) Console.Write("Final Container not found. Add the name of the final container exists and restart the process.");
            return;
        }

        // CRIAÇÃO DO CONTROLOBJECT
        processProgram.CreateControlObject(ref controlObjectList,
                                           factoryAssembler,
                                           componentsInternalIdentifiers,
                                           componentsAmount,
                                           componentsAmountProduce,
                                           componentsAmountToProduced,
                                           ref displayInfoLayout,
                                           ref displayDebugg,
                                           structureProgram.scriptLanguageEnglish,
                                           structureProgram.systemDebug);


        // VERIFICAR SE OS ASSEMBLER EXISTEM
        if (structureProgram.scriptLanguageEnglish) displayInfoLayout.AppendLine("Assembler Control - Begin Process");
        else displayInfoLayout.AppendLine("Assembler Control - 1º Processo - Verificando se os ASSEMBLERS existem e se o CONTAINER FINAL existe");

        // VERIFICAR SE O CONTAINER FINAL EXISTE
        processProgram.CheckAssemblers(ref controlObjectList,
                                       structureProgram.nameOfFinalContainer,
                                       ref displayInfoLayout,
                                       ref displayDebugg,
                                       structureProgram.scriptLanguageEnglish,
                                       structureProgram.systemDebug);

        //VERIFICAR A NECESSIDADE DE PRODUÇÃO DE CADA COMPONENTE
        processProgram.CheckProductionNeed(ref componentsAmount,
                                           ref componentsAmountProduce,
                                           ref componentsAmountToProduced,
                                           structureProgram.nameOfFinalContainer,
                                           ref displayInfoLayout,
                                           ref displayDebugg,
                                           structureProgram.scriptLanguageEnglish,
                                           structureProgram.systemDebug);

        //ATUALIZAR OS ASSEMBLERS
        //ATUALIZAR LCD


    }
}