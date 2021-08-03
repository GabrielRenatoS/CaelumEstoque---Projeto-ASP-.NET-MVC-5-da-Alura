using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaelumEstoque.DAO;
using CaelumEstoque.Filtros;
using CaelumEstoque.Models;

namespace CaelumEstoque.Controllers
{
    [AutorizacaoFilter]
    public class ProdutoController : Controller
    {
        [Route("produtos", Name = "ListaProdutos")]        
        public ActionResult Index()
        {
            ProdutosDAO dao = new ProdutosDAO();
            IList<Produto> produtos = dao.Lista();

            //acessado pela View usando ViewBag.Produtos
            //ViewBag.Produtos = produtos; 
            //return View();

            //acessado pela View usando Model
            return View(produtos);
        }

        public ActionResult Form()
        {
            CategoriasDAO dao = new CategoriasDAO();
            IList<CategoriaDoProduto> categorias = dao.Lista();
            ViewBag.Categorias = categorias;
            ViewBag.Produto = new Produto();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adiciona(Produto produto)
        {
            int idDaInformatcia = 1;
            if (produto.CategoriaId.Equals(idDaInformatcia) && produto.Preco < 100)
            {
                ModelState.AddModelError("produto.Invalido", "Informática com preço abaixo de 100 reais.");
            }
            if (ModelState.IsValid)
            {
                ProdutosDAO dao = new ProdutosDAO();
                dao.Adiciona(produto);
                //return RedirectToAction("index", "Produto");
                return RedirectToAction("index");
            }
            else
            {
                ViewBag.Produto = produto;
                CategoriasDAO categoriasDAO = new CategoriasDAO();
                ViewBag.Categorias = categoriasDAO.Lista();
                return View("Form");
            }
        }

        [Route("produtos/{id}", Name = "VisualizaProdutos")]
        public ActionResult Visualiza(int id)
        {
            ProdutosDAO produtosDAO = new ProdutosDAO();
            Produto produto = produtosDAO.BuscaPorId(id);
            ViewBag.Produto = produto;
            return View();
        }

        public ActionResult DecrementaQtd(int id)
        {
            ProdutosDAO produtosDAO = new ProdutosDAO();
            Produto produto = produtosDAO.BuscaPorId(id);
            produto.Quantidade--;
            produtosDAO.Atualiza(produto);
            return Json(produto);
        }
    }
}