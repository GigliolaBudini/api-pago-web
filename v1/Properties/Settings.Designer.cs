﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace API.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.8.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://medqlcinmxq.sap.meds.cl:50000/XISOAPAdapt")]
        public string API_RegistraPagoWebReference_SI_os_TransaccionSAPCreateService {
            get {
                return ((string)(this["API_RegistraPagoWebReference_SI_os_TransaccionSAPCreateService"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://medqlcinmxq.sap.meds.cl:50000/XISOAPAdapter/MessageServlet?senderParty=&se" +
            "nderService=BC_WEBPAY&receiverParty=&receiverService=&interface=SI_os_PagoEpisod" +
            "ioWebQuery&interfaceNamespace=urn%3Ameds.cl%3Awebpay%3Apagocliente")]
        public string API_PagoEpisodioWebReference_SI_os_PagoEpisodioWebQueryService {
            get {
                return ((string)(this["API_PagoEpisodioWebReference_SI_os_PagoEpisodioWebQueryService"]));
            }
        }
    }
}