using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd_Tarefa.Controllers
{
    public class UsuariosController:Controller
    {
        public string uriBase = " http://DB-DS-LuizVieira.somee.com/Usuarios/";

     [HttpGet]
        public ActionResult Index()
        {
            return View("CadastrarMeta");
        }
        
        [HttpPost]
        public async Task<ActionResult> RegistrarAsync(UsuarioViewModel u)
        {
            try
            {
                HttpClient HttpClient = new HttpClient();
                string uriComplementar = "Registrar";

                var content = new StringContent(JsonConvert.SerializeObject(u));
                content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await HttpClient.PostAsync(uriBase + uriComplementar, content);

                string serialized = await response.Content.ReadAsStringAsync();

                if(response.StatusCode == System.Net.HttpStatusCode.ok)

                {
                    TempData["Mensagem"] =
                    string.Format("Meta {0} Registrada com sucesso! Faça o login para acessar.", u.UserName);
                    return View("AutenticarMeta");
                }
                else
                {
                    throw new System.Exception(serialized);
                }
            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult IndexLogin()
        {
            return View("AutenticarUsuario");
        }
        [GetPost]
        public async Task<ActionResult> AutenticarAsync(UsuarioViewModel u)
        {
            try
            {
                HttpClient HttpClient = new HttpClient();
                string uriComplementar = "Autenticar";

                var content = new StringContent(JsonConvert.SerializeObject(u));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await response.Content.ReadAsStringAsync();

                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    UsuarioViewModel uLogado = JsonConvert.DeserializeObject<UsuarioViewModel>(serialized);
                    HttpContext.Session.SetString("SessionToKenUsuario", uLogado.Token);
                    TempData["Mensagem"] = string.Format("Bem-Vindo {0}!!!", uLogado.Username);
                    return RedirectToAction("Index", "Metas");
                }
                else
                {
                    throw new System.Exception(serialized);
                }

            }
            catch (System.Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return IndexLogin();
            }
        }
    }
    
}