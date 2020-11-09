using System.Collections.Generic;

namespace DocumentoCommerciale
{
    public class Esiti
    {
        public class Esito
        {
            public bool esito { get; set; }
            public string idtrx { get; set; }
            public string progressivo { get; set; }
            public List<Errore> errori { get; set; }
        }

        public class Errore
        {
            public string codice { get; set; }
            public string descrizione { get; set; }
        }

    }
}
