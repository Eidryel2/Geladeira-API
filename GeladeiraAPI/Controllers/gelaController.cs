using Domain;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using static Domain.ItemDomain;
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

            // Validate floor
            if (item.Andar < 1 || item.Andar > 3)
            {
                return BadRequest($"Andar inválido. Deve ser entre 1 e 3.");
            }

            // Validate container
            if (item.Container < 1 || item.Container > 2)
            {
                return BadRequest($"Container inválido. Deve ser entre 1 e 2.");
            }

            // Validate position
            if (item.Posicao < 1 || item.Posicao > 4)
            {
                return BadRequest($"Posição inválida. Deve ser entre 1 e 4.");
            }

            try
            {
                // Validate position
                if (item.Posicao < 1 || item.Posicao > 4)
                {
                    return BadRequest($"Posição inválida. Deve ser entre 1 e 4 para cada container.");
                }

                // Check if the position is already occupied in the container
                var existingItem = _itemService.GetAllItems()
                    .FirstOrDefault(i => i.Container == item.Container
                        && i.Posicao == item.Posicao);

                if (existingItem != null)
                {
                    return Conflict($"Já existe um item na posição {item.Posicao} do container {item.Container}.");
                }

                // Check if an item with the same name already exists
                existingItem = _itemService.GetAllItems().Find(i => i.Nome == item.Nome);
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
        public IActionResult UpdateItem(int id, [FromBody] ItemUpdateDomain itemUpdateDomain)
        {
            if (itemUpdateDomain == null || itemUpdateDomain.Id != id)
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

                // Only update the Nome property
                existingItem.Nome = itemUpdateDomain.Nome;

                _itemService.UpdateItem(existingItem);
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
