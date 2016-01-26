namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSpecificationPropertiesMobileTabletLaptopDesktopPrinter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Item", "TermsAndConditions", c => c.String());
            AddColumn("dbo.Model", "Price", c => c.Int(nullable: false));
            AddColumn("dbo.Specification", "BoxContents", c => c.String());
            AddColumn("dbo.Specification", "Warranty", c => c.String());
            AddColumn("dbo.Specification", "Brand", c => c.String());
            AddColumn("dbo.Specification", "Form", c => c.String());
            AddColumn("dbo.Specification", "Sims", c => c.String());
            AddColumn("dbo.Specification", "SimSize", c => c.String());
            AddColumn("dbo.Specification", "Color", c => c.String());
            AddColumn("dbo.Specification", "OtherFeatures", c => c.String());
            AddColumn("dbo.Specification", "CallFeatures", c => c.String());
            AddColumn("dbo.Specification", "ScreenSize", c => c.String());
            AddColumn("dbo.Specification", "DisplayResolution", c => c.String());
            AddColumn("dbo.Specification", "ScreenProtection", c => c.String());
            AddColumn("dbo.Specification", "PixelDensity", c => c.String());
            AddColumn("dbo.Specification", "Multitouch", c => c.String());
            AddColumn("dbo.Specification", "OsVersion", c => c.String());
            AddColumn("dbo.Specification", "PreInstalledApps", c => c.String());
            AddColumn("dbo.Specification", "MultiLanguageSupport", c => c.String());
            AddColumn("dbo.Specification", "RearCamera", c => c.String());
            AddColumn("dbo.Specification", "AutoFocus", c => c.String());
            AddColumn("dbo.Specification", "Flash", c => c.String());
            AddColumn("dbo.Specification", "FrontCamera", c => c.String());
            AddColumn("dbo.Specification", "Gsm", c => c.String());
            AddColumn("dbo.Specification", "CDMA", c => c.String());
            AddColumn("dbo.Specification", "_3G", c => c.String());
            AddColumn("dbo.Specification", "_4G", c => c.String());
            AddColumn("dbo.Specification", "ProcessorSpeed", c => c.String());
            AddColumn("dbo.Specification", "ProcessorCores", c => c.String());
            AddColumn("dbo.Specification", "ProcessorBrand", c => c.String());
            AddColumn("dbo.Specification", "Ram", c => c.String());
            AddColumn("dbo.Specification", "InternalMemory", c => c.String());
            AddColumn("dbo.Specification", "UserMemory", c => c.String());
            AddColumn("dbo.Specification", "ExpandableMemory", c => c.String());
            AddColumn("dbo.Specification", "MemoryCardSlot", c => c.String());
            AddColumn("dbo.Specification", "Accelerometer", c => c.String());
            AddColumn("dbo.Specification", "LightSensor", c => c.String());
            AddColumn("dbo.Specification", "Compass", c => c.String());
            AddColumn("dbo.Specification", "GSensor", c => c.String());
            AddColumn("dbo.Specification", "Bluetooth", c => c.String());
            AddColumn("dbo.Specification", "AudioJack", c => c.String());
            AddColumn("dbo.Specification", "SarValue", c => c.String());
            AddColumn("dbo.Specification", "FMRadio", c => c.String());
            AddColumn("dbo.Specification", "BatteryCapacity", c => c.String());
            AddColumn("dbo.Specification", "BatteryType", c => c.String());
            AddColumn("dbo.Specification", "ReplaceableBattery", c => c.String());
            AddColumn("dbo.Specification", "TalkTime", c => c.String());
            AddColumn("dbo.Specification", "StandbyTime", c => c.String());
            AddColumn("dbo.Specification", "Dimensions", c => c.String());
            AddColumn("dbo.Specification", "Weight", c => c.String());
            AddColumn("dbo.Specification", "VoiceCall", c => c.String());
            AddColumn("dbo.Specification", "DisplayType", c => c.String());
            AddColumn("dbo.Specification", "WifiOnly", c => c.String());
            AddColumn("dbo.Specification", "_3GThroughDongle", c => c.String());
            AddColumn("dbo.Specification", "WifiAnd3GData", c => c.String());
            AddColumn("dbo.Specification", "_3GCalling", c => c.String());
            AddColumn("dbo.Specification", "_2GCalling", c => c.String());
            AddColumn("dbo.Specification", "VideoPlayback", c => c.String());
            AddColumn("dbo.Specification", "Speakers", c => c.String());
            AddColumn("dbo.Specification", "Type", c => c.String());
            AddColumn("dbo.Specification", "ModelNumber", c => c.String());
            AddColumn("dbo.Specification", "Variant", c => c.String());
            AddColumn("dbo.Specification", "Cache", c => c.String());
            AddColumn("dbo.Specification", "ClockSpeed", c => c.String());
            AddColumn("dbo.Specification", "EmmcCapacity", c => c.String());
            AddColumn("dbo.Specification", "Architecture", c => c.String());
            AddColumn("dbo.Specification", "GraphicProcessor", c => c.String());
            AddColumn("dbo.Specification", "IntegratedCamera", c => c.String());
            AddColumn("dbo.Specification", "PointerDevice", c => c.String());
            AddColumn("dbo.Specification", "Keyboard", c => c.String());
            AddColumn("dbo.Specification", "InternalMic", c => c.String());
            AddColumn("dbo.Specification", "WirelessLan", c => c.String());
            AddColumn("dbo.Specification", "BatteryBackup", c => c.String());
            AddColumn("dbo.Specification", "PowerSupply", c => c.String());
            AddColumn("dbo.Specification", "StandardBattery", c => c.String());
            AddColumn("dbo.Specification", "UsbPorts", c => c.String());
            AddColumn("dbo.Specification", "MicIn", c => c.String());
            AddColumn("dbo.Specification", "Note", c => c.String());
            AddColumn("dbo.Specification", "ScreenType", c => c.String());
            AddColumn("dbo.Specification", "SoundEffect", c => c.String());
            AddColumn("dbo.Specification", "MultiCardSlot", c => c.String());
            AddColumn("dbo.Specification", "ProcessorType", c => c.String());
            AddColumn("dbo.Specification", "Chipset", c => c.String());
            AddColumn("dbo.Specification", "MemorySpeed", c => c.String());
            AddColumn("dbo.Specification", "MemorySlots", c => c.String());
            AddColumn("dbo.Specification", "HardDrive", c => c.String());
            AddColumn("dbo.Specification", "Voltage", c => c.String());
            AddColumn("dbo.Specification", "Wattage", c => c.String());
            AddColumn("dbo.Specification", "OpticalDrive", c => c.String());
            AddColumn("dbo.Specification", "PowerSource", c => c.String());
            AddColumn("dbo.Specification", "HardwarePlatform", c => c.String());
            AddColumn("dbo.Specification", "IntegratedGraphicProcessor", c => c.String());
            AddColumn("dbo.Specification", "Ethernet", c => c.String());
            AddColumn("dbo.Specification", "OtherPorts", c => c.String());
            AddColumn("dbo.Specification", "Function", c => c.String());
            AddColumn("dbo.Specification", "PrintSpeedBlack", c => c.String());
            AddColumn("dbo.Specification", "DutyCycle", c => c.String());
            AddColumn("dbo.Specification", "RecommendedPageVolumeMonthly", c => c.String());
            AddColumn("dbo.Specification", "PrintTechnology", c => c.String());
            AddColumn("dbo.Specification", "PrintQualityBlack", c => c.String());
            AddColumn("dbo.Specification", "ConnectivityStandard", c => c.String());
            AddColumn("dbo.Specification", "SystemRequirements", c => c.String());
            AddColumn("dbo.Specification", "CompatibleOperatingSystem", c => c.String());
            AddColumn("dbo.Specification", "Standard", c => c.String());
            AddColumn("dbo.Specification", "MaximumMemory", c => c.String());
            AddColumn("dbo.Specification", "PaperHandlingInput", c => c.String());
            AddColumn("dbo.Specification", "PaperHandlingOutput", c => c.String());
            AddColumn("dbo.Specification", "DuplexPrinting", c => c.String());
            AddColumn("dbo.Specification", "MediaSizeSupported", c => c.String());
            AddColumn("dbo.Specification", "MediaSizeCustom", c => c.String());
            AddColumn("dbo.Specification", "MediaType", c => c.String());
            AddColumn("dbo.Specification", "ScannerType", c => c.String());
            AddColumn("dbo.Specification", "ScanFileFormat", c => c.String());
            AddColumn("dbo.Specification", "ScanResolutionFormat", c => c.String());
            AddColumn("dbo.Specification", "ScanResolutionOptical", c => c.String());
            AddColumn("dbo.Specification", "ScanSizeFlatbed", c => c.String());
            AddColumn("dbo.Specification", "ScanInputModes", c => c.String());
            AddColumn("dbo.Specification", "CopierSpeed", c => c.String());
            AddColumn("dbo.Specification", "ReduceEnlargeSettings", c => c.String());
            AddColumn("dbo.Specification", "CopiesMaximum", c => c.String());
            AddColumn("dbo.Specification", "Power", c => c.String());
            AddColumn("dbo.Specification", "PowerConsumption", c => c.String());
            AddColumn("dbo.Specification", "EnergyEfficiency", c => c.String());
            AddColumn("dbo.Specification", "OperatingTemperatureRange", c => c.String());
            AddColumn("dbo.Specification", "OperatingHumidityRange", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Specification", "OperatingHumidityRange");
            DropColumn("dbo.Specification", "OperatingTemperatureRange");
            DropColumn("dbo.Specification", "EnergyEfficiency");
            DropColumn("dbo.Specification", "PowerConsumption");
            DropColumn("dbo.Specification", "Power");
            DropColumn("dbo.Specification", "CopiesMaximum");
            DropColumn("dbo.Specification", "ReduceEnlargeSettings");
            DropColumn("dbo.Specification", "CopierSpeed");
            DropColumn("dbo.Specification", "ScanInputModes");
            DropColumn("dbo.Specification", "ScanSizeFlatbed");
            DropColumn("dbo.Specification", "ScanResolutionOptical");
            DropColumn("dbo.Specification", "ScanResolutionFormat");
            DropColumn("dbo.Specification", "ScanFileFormat");
            DropColumn("dbo.Specification", "ScannerType");
            DropColumn("dbo.Specification", "MediaType");
            DropColumn("dbo.Specification", "MediaSizeCustom");
            DropColumn("dbo.Specification", "MediaSizeSupported");
            DropColumn("dbo.Specification", "DuplexPrinting");
            DropColumn("dbo.Specification", "PaperHandlingOutput");
            DropColumn("dbo.Specification", "PaperHandlingInput");
            DropColumn("dbo.Specification", "MaximumMemory");
            DropColumn("dbo.Specification", "Standard");
            DropColumn("dbo.Specification", "CompatibleOperatingSystem");
            DropColumn("dbo.Specification", "SystemRequirements");
            DropColumn("dbo.Specification", "ConnectivityStandard");
            DropColumn("dbo.Specification", "PrintQualityBlack");
            DropColumn("dbo.Specification", "PrintTechnology");
            DropColumn("dbo.Specification", "RecommendedPageVolumeMonthly");
            DropColumn("dbo.Specification", "DutyCycle");
            DropColumn("dbo.Specification", "PrintSpeedBlack");
            DropColumn("dbo.Specification", "Function");
            DropColumn("dbo.Specification", "OtherPorts");
            DropColumn("dbo.Specification", "Ethernet");
            DropColumn("dbo.Specification", "IntegratedGraphicProcessor");
            DropColumn("dbo.Specification", "HardwarePlatform");
            DropColumn("dbo.Specification", "PowerSource");
            DropColumn("dbo.Specification", "OpticalDrive");
            DropColumn("dbo.Specification", "Wattage");
            DropColumn("dbo.Specification", "Voltage");
            DropColumn("dbo.Specification", "HardDrive");
            DropColumn("dbo.Specification", "MemorySlots");
            DropColumn("dbo.Specification", "MemorySpeed");
            DropColumn("dbo.Specification", "Chipset");
            DropColumn("dbo.Specification", "ProcessorType");
            DropColumn("dbo.Specification", "MultiCardSlot");
            DropColumn("dbo.Specification", "SoundEffect");
            DropColumn("dbo.Specification", "ScreenType");
            DropColumn("dbo.Specification", "Note");
            DropColumn("dbo.Specification", "MicIn");
            DropColumn("dbo.Specification", "UsbPorts");
            DropColumn("dbo.Specification", "StandardBattery");
            DropColumn("dbo.Specification", "PowerSupply");
            DropColumn("dbo.Specification", "BatteryBackup");
            DropColumn("dbo.Specification", "WirelessLan");
            DropColumn("dbo.Specification", "InternalMic");
            DropColumn("dbo.Specification", "Keyboard");
            DropColumn("dbo.Specification", "PointerDevice");
            DropColumn("dbo.Specification", "IntegratedCamera");
            DropColumn("dbo.Specification", "GraphicProcessor");
            DropColumn("dbo.Specification", "Architecture");
            DropColumn("dbo.Specification", "EmmcCapacity");
            DropColumn("dbo.Specification", "ClockSpeed");
            DropColumn("dbo.Specification", "Cache");
            DropColumn("dbo.Specification", "Variant");
            DropColumn("dbo.Specification", "ModelNumber");
            DropColumn("dbo.Specification", "Type");
            DropColumn("dbo.Specification", "Speakers");
            DropColumn("dbo.Specification", "VideoPlayback");
            DropColumn("dbo.Specification", "_2GCalling");
            DropColumn("dbo.Specification", "_3GCalling");
            DropColumn("dbo.Specification", "WifiAnd3GData");
            DropColumn("dbo.Specification", "_3GThroughDongle");
            DropColumn("dbo.Specification", "WifiOnly");
            DropColumn("dbo.Specification", "DisplayType");
            DropColumn("dbo.Specification", "VoiceCall");
            DropColumn("dbo.Specification", "Weight");
            DropColumn("dbo.Specification", "Dimensions");
            DropColumn("dbo.Specification", "StandbyTime");
            DropColumn("dbo.Specification", "TalkTime");
            DropColumn("dbo.Specification", "ReplaceableBattery");
            DropColumn("dbo.Specification", "BatteryType");
            DropColumn("dbo.Specification", "BatteryCapacity");
            DropColumn("dbo.Specification", "FMRadio");
            DropColumn("dbo.Specification", "SarValue");
            DropColumn("dbo.Specification", "AudioJack");
            DropColumn("dbo.Specification", "Bluetooth");
            DropColumn("dbo.Specification", "GSensor");
            DropColumn("dbo.Specification", "Compass");
            DropColumn("dbo.Specification", "LightSensor");
            DropColumn("dbo.Specification", "Accelerometer");
            DropColumn("dbo.Specification", "MemoryCardSlot");
            DropColumn("dbo.Specification", "ExpandableMemory");
            DropColumn("dbo.Specification", "UserMemory");
            DropColumn("dbo.Specification", "InternalMemory");
            DropColumn("dbo.Specification", "Ram");
            DropColumn("dbo.Specification", "ProcessorBrand");
            DropColumn("dbo.Specification", "ProcessorCores");
            DropColumn("dbo.Specification", "ProcessorSpeed");
            DropColumn("dbo.Specification", "_4G");
            DropColumn("dbo.Specification", "_3G");
            DropColumn("dbo.Specification", "CDMA");
            DropColumn("dbo.Specification", "Gsm");
            DropColumn("dbo.Specification", "FrontCamera");
            DropColumn("dbo.Specification", "Flash");
            DropColumn("dbo.Specification", "AutoFocus");
            DropColumn("dbo.Specification", "RearCamera");
            DropColumn("dbo.Specification", "MultiLanguageSupport");
            DropColumn("dbo.Specification", "PreInstalledApps");
            DropColumn("dbo.Specification", "OsVersion");
            DropColumn("dbo.Specification", "Multitouch");
            DropColumn("dbo.Specification", "PixelDensity");
            DropColumn("dbo.Specification", "ScreenProtection");
            DropColumn("dbo.Specification", "DisplayResolution");
            DropColumn("dbo.Specification", "ScreenSize");
            DropColumn("dbo.Specification", "CallFeatures");
            DropColumn("dbo.Specification", "OtherFeatures");
            DropColumn("dbo.Specification", "Color");
            DropColumn("dbo.Specification", "SimSize");
            DropColumn("dbo.Specification", "Sims");
            DropColumn("dbo.Specification", "Form");
            DropColumn("dbo.Specification", "Brand");
            DropColumn("dbo.Specification", "Warranty");
            DropColumn("dbo.Specification", "BoxContents");
            DropColumn("dbo.Model", "Price");
            DropColumn("dbo.Item", "TermsAndConditions");
        }
    }
}
