using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcOAuthUsuarios.Filters;
using MvcOAuthUsuarios.Models;
using MvcOAuthUsuarios.Services;


namespace MvcOAuthUsuarios.Controllers
{
    public class UsuariosController : Controller
    {
        private ServiceApiUsuario service;
        private ServiceStorageBlobs serviceblob;

        public UsuariosController(ServiceApiUsuario service, ServiceStorageBlobs serviceblob)
        {
            this.service = service;
            this.serviceblob = serviceblob;
        }



        [AuthorizeUsers]
        public IActionResult Index()
        {
            return View();
        }

        [AuthorizeUsers]
        public async Task<IActionResult> Perfil()
        {
            string token =
                HttpContext.Session.GetString("TOKEN");
            Usuario usuario = await
                this.service.GetPerfilUsuarioAsync(token);
            BlobModel blobPerfil = await this.serviceblob.FindBlobPerfil("imagenesrestaurante", usuario.Imagen , usuario.Nombre);
            ViewData["IMAGEN_PERFIL"]= blobPerfil;
            return View(usuario);
        }

       

        


    }
}
