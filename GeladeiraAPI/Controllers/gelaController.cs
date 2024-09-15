using Domain;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System;
using System.Collections.Generic;
//Atividade Feita pelas Alunas:
// Adrielly Ribeiro
// Aline Dos Santos


namespace GeladeiraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GelaController : ControllerBase
    {
        private readonly IServices<ItemDomain> _itemService;

        public GelaController(IServices<ItemDomain> itemService)
        {
            _itemService = itemService;
        }

        // GET: TODO OS ITENS - VALIDADO
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var items = _itemService.GetAllItems();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar itens: {ex.Message}");
            }
        }

        // GET: api/gela/5
        [HttpGet("{id}")]
        public IActionResult GetItem(int id)
        {
            try
            {
                var item = _itemService.GetItem(id);
                if (item != null)
                {
                    return Ok(item);
                }
                return NotFound($"Item com ID {id} não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar item: {ex.Message}");
            }
        }

        // POST: api/gela
        [HttpPost]
        public IActionResult AdicionarItem([FromBody] ItemDomain item)
        {
            if (item == null)
            {
                return BadRequest("O item não pode ser nulo.");
            }

            try
            {
            
                var existingItem = _itemService.GetAllItems().Find(i => i.Nome == item.Nome);
                if (existingItem != null)
                {
                    return Conflict($"Item com nome '{item.Nome}' já existe.");
                }

                _itemService.AddItem(item);

        
                return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao adicionar item: {ex.Message}");
            }
        }

        // PUT: api/gela/5
        [HttpPut("{id}")]
        public IActionResult UpdateItem(int id, [FromBody] ItemDomain itemDomain)
        {
            if (itemDomain == null || itemDomain.Id != id)
            {
                return BadRequest("Dados do item são inválidos.");
            }

            try
            {
                var existingItem = _itemService.GetItem(id);
                if (existingItem == null)
                {
                    return NotFound($"Item com ID {id} não encontrado.");
                }

                _itemService.UpdateItem(itemDomain);
                return Ok("Item atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar item: {ex.Message}");
            }
        }

        // DELETE: api/gela/5
        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            try
            {
                var item = _itemService.GetItem(id);
                if (item == null)
                {
                    return NotFound($"Item com ID {id} não encontrado.");
                }

                _itemService.DeleteItem(id);
                return Ok("Item deletado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao deletar item: {ex.Message}");
            }
        }

    }
}
