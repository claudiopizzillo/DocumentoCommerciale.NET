using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace DocumentoCommerciale
{
    public class Send
    {
        public List<Esiti.Esito> SendDC(List<DC.RootObject> dc, string usr, string pwd, string pin, string piva, string tipoincarico)
        {
                List<Esiti.Esito> esiti = new List<Esiti.Esito> { };
                Esiti.Esito emptyesito = new Esiti.Esito { esito = false, idtrx = null, progressivo = null, errori = null };

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.Expect100Continue = false;

                CookieContainer cookieJar = new CookieContainer();
                RestClient client = new RestClient(@"https://ivaservizi.agenziaentrate.gov.it/portale/web/guest")
                {
                    CookieContainer = cookieJar
                };
                RestRequest req = new RestRequest(Method.GET);
                IRestResponse res = client.Execute(req);

                int StatusCode = (int)res.StatusCode;
                if (StatusCode != 200)
                {
                    Esiti.Errore errore = new Esiti.Errore { codice = StatusCode.ToString(), descrizione = "Fase 1" };
                    emptyesito.errori = new List<Esiti.Errore>();
                    emptyesito.errori.Add(errore);
                    foreach (DC.RootObject dctemp in dc) {
                        esiti.Add(emptyesito);
                    }
                    return esiti;
                }

                client = new RestClient(@"https://ivaservizi.agenziaentrate.gov.it/portale/home?p_p_id=58&p_p_lifecycle=1&p_p_state=normal&p_p_mode=view&p_p_col_id=column-1&p_p_col_pos=3&p_p_col_count=4&_58_struts_action=%2Flogin%2Flogin")
                {
                    CookieContainer = cookieJar
                };
                req = new RestRequest(Method.POST);
                req.AddParameter("_58_login", usr);
                req.AddParameter("_58_pin", pin);
                req.AddParameter("_58_password", pwd);

                res = client.Execute(req);

                StatusCode = (int)res.StatusCode;
                if (StatusCode != 200)
                {
                    Esiti.Errore errore = new Esiti.Errore { codice = StatusCode.ToString(), descrizione = "Fase 2" };
                    emptyesito.errori = new List<Esiti.Errore>();
                    emptyesito.errori.Add(errore);
                    foreach (DC.RootObject dctemp in dc)
                    {
                        esiti.Add(emptyesito);
                    }
                return esiti;
                }

                string p_auth = getBetween(res.Content, "Liferay.authToken = '", "';");

                client = new RestClient(@"https://ivaservizi.agenziaentrate.gov.it/dp/api?v=" + unixTime())
                {
                    CookieContainer = cookieJar
                };
                req = new RestRequest(Method.GET);
                res = client.Execute(req);

                StatusCode = (int)res.StatusCode;
                if (StatusCode != 200)
                {
                    Esiti.Errore errore = new Esiti.Errore { codice = StatusCode.ToString(), descrizione = "Fase 3" };
                    emptyesito.errori = new List<Esiti.Errore>();
                    emptyesito.errori.Add(errore);
                    foreach (DC.RootObject dctemp in dc)
                    {
                        esiti.Add(emptyesito);
                    }
                return esiti;
                }

                client = new RestClient(@"https://ivaservizi.agenziaentrate.gov.it/portale/scelta-utenza-lavoro?p_auth=" + p_auth + "&p_p_id=SceltaUtenzaLavoro_WAR_SceltaUtenzaLavoroportlet&p_p_lifecycle=1&p_p_state=normal&p_p_mode=view&p_p_col_id=column-1&p_p_col_count=1&_SceltaUtenzaLavoro_WAR_SceltaUtenzaLavoroportlet_javax.portlet.action=incarichiAction")
                                                                                                                                 
                {
                    CookieContainer = cookieJar
                };
                req = new RestRequest(Method.POST);
                req.AddParameter("sceltaincarico", piva);
                req.AddParameter("tipoincaricante", tipoincarico);
                res = client.Execute(req);

                StatusCode = (int)res.StatusCode;
                if (StatusCode != 200)
                {
                    Esiti.Errore errore = new Esiti.Errore { codice = StatusCode.ToString(), descrizione = "Fase 4" };
                    emptyesito.errori = new List<Esiti.Errore>();
                    emptyesito.errori.Add(errore);
                    foreach (DC.RootObject dctemp in dc)
                    {
                        esiti.Add(emptyesito);
                    }
                return esiti;
                }

                client = new RestClient(@"https://ivaservizi.agenziaentrate.gov.it/ser/api/fatture/v1/ul/me/adesione/stato/")
                {
                    CookieContainer = cookieJar
                };
                req = new RestRequest(Method.GET);
                res = client.Execute(req);

                StatusCode = (int)res.StatusCode;
                if (StatusCode != 200)
                {
                    Esiti.Errore errore = new Esiti.Errore { codice = StatusCode.ToString(), descrizione = "Fase 5" };
                    emptyesito.errori = new List<Esiti.Errore>();
                    emptyesito.errori.Add(errore);
                    foreach (DC.RootObject dctemp in dc)
                    {
                        esiti.Add(emptyesito);
                    }
                return esiti;
                }

                foreach (DC.RootObject item in dc) {
                    client = new RestClient(@"https://ivaservizi.agenziaentrate.gov.it/ser/api/documenti/v1/doc/documenti/?v=" + unixTime())
                    {
                        CookieContainer = cookieJar
                    };

                    var settings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented };
                    var json = JsonConvert.SerializeObject(item, settings);
                    //Console.WriteLine(json);
                    req = new RestRequest(Method.POST);
                    req.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.119 Safari/537.36");
                    req.AddHeader("x-xss-protection", "1; mode = block");
                    req.AddHeader("strict-transport-security", "max-age=16070400; includeSubDomains");
                    req.AddHeader("x-content-type-options", "nosniff");
                    req.AddHeader("x-frame-options", "deny");
                    req.AddHeader("content-type", "application/json");
                    req.AddParameter("application/octet-stream", json, ParameterType.RequestBody);
                    //req.AddJsonBody(item);
                    res = client.Execute(req);

                    StatusCode = (int)res.StatusCode;
                    if (StatusCode != 200)
                    {
                        Esiti.Errore errore = new Esiti.Errore { codice = StatusCode.ToString(), descrizione = "Fase 6 POST JSON" };
                        emptyesito.errori = new List<Esiti.Errore>();
                        emptyesito.errori.Add(errore);
                        esiti.Add(emptyesito);
                    } else
                    {
                        esiti.Add(JsonConvert.DeserializeObject<Esiti.Esito>(res.Content));
                    }
                }
                return esiti;
        }

        static string unixTime()
        {
            DateTime foo = DateTime.UtcNow;
            long unixTime = ((DateTimeOffset)foo).ToUnixTimeMilliseconds();
            return unixTime.ToString();
        }

        static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

    }
}
