using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BontoBuy.Web.Models
{
    [Table("Specification")]
    public class SpecificationViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Specification Id")]
        public int SpecificationId { get; set; }
        [Display(Name = "Tag Id")]
        public int TagId { get; set; }
        [Display(Name = "Price")]
        public int Price { get; set; }

        #region Mobile

        public string BoxContents { get; set; }
        public string Warranty { get; set; }
        public string Brand { get; set; }
        public string Form { get; set; }
        public string Sims { get; set; }
        public string SimSize { get; set; }
        public string Color { get; set; }
        public string OtherFeatures { get; set; }
        public string CallFeatures { get; set; }
        public string ScreenSize { get; set; }
        public string DisplayResolution { get; set; }
        public string ScreenProtection { get; set; }
        public string PixelDensity { get; set; }
        public string Multitouch { get; set; }
        public string OsVersion { get; set; }
        public string PreInstalledApps { get; set; }
        public string MultiLanguageSupport { get; set; }
        public string RearCamera { get; set; }
        public string AutoFocus { get; set; }
        public string Flash { get; set; }
        public string FrontCamera { get; set; }
        public string Gsm { get; set; }
        public string CDMA { get; set; }
        [Display(Name = "3G")]
        public string _3G { get; set; }
        [Display(Name = "4G")]
        public string _4G { get; set; }
        public string ProcessorSpeed { get; set; }
        public string ProcessorCores { get; set; }
        public string ProcessorBrand { get; set; }
        public string Ram { get; set; }
        public string InternalMemory { get; set; }
        public string UserMemory { get; set; }
        public string ExpandableMemory { get; set; }
        public string MemoryCardSlot { get; set; }
        public string Accelerometer { get; set; }
        public string LightSensor { get; set; }
        public string Compass { get; set; }
        [Display(Name = "G-Sensor")]
        public string GSensor { get; set; }
        public string Bluetooth { get; set; }
        public string AudioJack { get; set; }
        [Display(Name = "SAR Value")]
        public string SarValue { get; set; }
        [Display(Name = "FM Radio")]
        public string FMRadio { get; set; }
        public string BatteryCapacity { get; set; }
        public string BatteryType { get; set; }
        public string ReplaceableBattery { get; set; }
        public string TalkTime { get; set; }
        public string StandbyTime { get; set; }
        public string Dimensions { get; set; }
        public string Weight { get; set; }

        #endregion Mobile

        #region Tablet

        public string VoiceCall { get; set; }
        public string DisplayType { get; set; }
        public string WifiOnly { get; set; }
        public string _3GThroughDongle { get; set; }
        public string WifiAnd3GData { get; set; }
        public string _3GCalling { get; set; }
        public string _2GCalling { get; set; }
        public string VideoPlayback { get; set; }
        public string Speakers { get; set; }

        #endregion Tablet

        #region Laptop

        public string Type { get; set; }
        public string ModelNumber { get; set; }
        public string Variant { get; set; }
        public string Cache { get; set; }
        public string ClockSpeed { get; set; }
        public string EmmcCapacity { get; set; }
        public string Architecture { get; set; }
        public string GraphicProcessor { get; set; }
        public string IntegratedCamera { get; set; }
        public string PointerDevice { get; set; }
        public string Keyboard { get; set; }
        public string InternalMic { get; set; }
        public string WirelessLan { get; set; }
        public string BatteryBackup { get; set; }
        public string PowerSupply { get; set; }
        public string StandardBattery { get; set; }
        public string UsbPorts { get; set; }
        public string MicIn { get; set; }
        public string Note { get; set; }
        public string ScreenType { get; set; }
        public string SoundEffect { get; set; }
        public string MultiCardSlot { get; set; }

        #endregion Laptop

        #region Desktop

        public string ProcessorType { get; set; }
        public string Chipset { get; set; }
        public string MemorySpeed { get; set; }
        public string MemorySlots { get; set; }
        public string HardDrive { get; set; }
        public string Voltage { get; set; }
        public string Wattage { get; set; }
        public string OpticalDrive { get; set; }
        public string PowerSource { get; set; }
        public string HardwarePlatform { get; set; }
        public string IntegratedGraphicProcessor { get; set; }
        public string Ethernet { get; set; }
        public string OtherPorts { get; set; }

        #endregion Desktop

        #region Printers

        public string Function { get; set; }
        public string PrintSpeedBlack { get; set; }
        public string DutyCycle { get; set; }
        public string RecommendedPageVolumeMonthly { get; set; }
        public string PrintTechnology { get; set; }
        public string PrintQualityBlack { get; set; }
        public string ConnectivityStandard { get; set; }
        public string SystemRequirements { get; set; }
        public string CompatibleOperatingSystem { get; set; }
        public string Standard { get; set; }
        public string MaximumMemory { get; set; }
        public string PaperHandlingInput { get; set; }
        public string PaperHandlingOutput { get; set; }
        public string DuplexPrinting { get; set; }
        public string MediaSizeSupported { get; set; }
        public string MediaSizeCustom { get; set; }
        public string MediaType { get; set; }
        public string ScannerType { get; set; }
        public string ScanFileFormat { get; set; }
        public string ScanResolutionFormat { get; set; }
        public string ScanResolutionOptical { get; set; }
        public string ScanSizeFlatbed { get; set; }
        public string ScanInputModes { get; set; }
        public string CopierSpeed { get; set; }
        public string ReduceEnlargeSettings { get; set; }
        public string CopiesMaximum { get; set; }
        public string Power { get; set; }
        public string PowerConsumption { get; set; }
        public string EnergyEfficiency { get; set; }
        public string OperatingTemperatureRange { get; set; }
        public string OperatingHumidityRange { get; set; }

        #endregion Printers

        public IEnumerable<ModelSpecViewModel> ModelSpecNav { get; set; }
        [ForeignKey("TagId")]
        public TagViewModel SpecificationTagNav { get; set; }
    }
}