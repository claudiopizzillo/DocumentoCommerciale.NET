using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace DocumentoCommerciale
{
    public class DC
    {
        public class DatiTrasmissione
        {
            public string formato { get; set; }
        }

        public class IdentificativiFiscali
        {
            public string codicePaese { get; set; }
            public string partitaIva { get; set; }
            public string codiceFiscale { get; set; }
        }

        public class AltriDatiIdentificativi
        {
            public string denominazione { get; set; }
            public string indirizzo { get; set; }
            public string numeroCivico { get; set; }
            public string cap { get; set; }
            public string comune { get; set; }
            public string provincia { get; set; }
            public string nazione { get; set; }
            public bool modificati { get; set; }
            public string defAliquotaIVA { get; set; }
            public bool nuovoUtente { get; set; }
        }
        public class CedentePrestatore
        {
            public IdentificativiFiscali identificativiFiscali { get; set; }
            public AltriDatiIdentificativi altriDatiIdentificativi { get; set; }
        }

        public struct NaturaIVA
        {
            public const string IVA4 = "4";
            public const string IVA5 = "5";
            public const string IVA10 = "10";
            public const string IVA22= "22";
            public const string IVAN1 = "N1";
            public const string IVAN2 = "N2";
            public const string IVAN3 = "N3";
            public const string IVAN4 = "N4";
            public const string IVAN5 = "N5";
            public const string IVAN6 = "N6";
            public const string IVAN7 = "N7";
        }


        public class ElementiContabili
        {
            public string idElementoContabile { get; set; }
            [JsonConverter(typeof(StringDecimalConverter))]
            public decimal resiPregressi { get; set; }
            [JsonConverter(typeof(StringDecimalConverter))]
            public decimal reso { get; set; }
            [JsonConverter(typeof(StringDecimalConverter))]
            public decimal quantita { get; set; }
            public string descrizioneProdotto { get; set; }

            [JsonConverter(typeof(StringDecimalConverter))]
            public decimal prezzoLordo { get; set; }

            [JsonConverter(typeof(StringDecimalConverter))]
            public decimal prezzoUnitario { get; set; }

            [JsonConverter(typeof(StringDecimalConverter))]
            public decimal scontoUnitario { get; set; }

            [JsonConverter(typeof(StringDecimalConverter))]
            public decimal scontoLordo { get; set; }

            public string aliquotaIVA { get; set; }

            [JsonConverter(typeof(StringDecimalConverter))]
            public decimal importoIVA { get; set; }

            [JsonConverter(typeof(StringDecimalConverter))]
            public decimal imponibile { get; set; }
            [JsonConverter(typeof(StringDecimalConverter))]
            public decimal imponibileNetto { get; set; }

            [JsonConverter(typeof(StringDecimalConverter))]
            public decimal totale { get; set; }
            public string omaggio { get; set; }
        }

        public class Vendita
        {
            public string tipo { get; set; }
            [JsonConverter(typeof(StringDecimalConverter))]
            public decimal importo { get; set; }
        }

        public class ResoAnnullo
        {
            public string tipologia { get; set; }
            public string dataOra { get; set; }
            public string progressivo { get; set; }
        }

        public class DocumentoCommerciale
        {
            public bool flagDocCommPerRegalo { get; set; }
            public string progressivoCollegato { get; set; }
            public string dataOra { get; set; }
            [JsonConverter(typeof(StringFloatConverter))]
            public float importoTotaleIva { get; set; }
            [JsonConverter(typeof(StringDecimalConverter))]
            public decimal scontoTotale { get; set; }
            [JsonConverter(typeof(StringDecimalConverter))]
            public decimal scontoTotaleLordo { get; set; }
            [JsonConverter(typeof(StringDecimalConverter))]
            public decimal totaleImponibile { get; set; }
            [JsonConverter(typeof(StringDecimalConverter))]
            public decimal ammontareComplessivo { get; set; }
            [JsonConverter(typeof(StringDecimalConverter))]
            public decimal totaleNonRiscosso { get; set; }
            public List<ElementiContabili> elementiContabili { get; set; }
            public List<Vendita> vendita { get; set; }

            [JsonConverter(typeof(StringDecimalConverter))]
            public decimal scontoAbbuono { get; set; }

            [JsonConverter(typeof(StringDecimalConverter))]
            public decimal importoDetraibileDeducibile { get; set; }
            public ResoAnnullo resoAnnullo { get; set; }
            public string numeroProgressivo { get; set; }
        }

        public class RootObject
        {
            public DatiTrasmissione datiTrasmissione { get; set; }
            public CedentePrestatore cedentePrestatore { get; set; }
            public DocumentoCommerciale documentoCommerciale { get; set; }
            public bool flagIdentificativiModificati { get; set; }
            public string idtrx { get; set; }
        }

        public class StringDecimalConverter : JsonConverter
        {
                public override bool CanRead => false;

                public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(decimal) || objectType == typeof(decimal?);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                writer.WriteValue(string.Format(CultureInfo.InvariantCulture, "{0:N2}", value).Replace(",",""));
            }
        }

        public class StringFloatConverter : JsonConverter
        {
            public override bool CanRead => false;

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(float) || objectType == typeof(float?);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                writer.WriteValue(string.Format(CultureInfo.InvariantCulture, "{0:N2}", value).Replace(",", ""));
            }
        }


    }
}
