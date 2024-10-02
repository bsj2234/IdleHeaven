package com.siko25.siko.character.player.inventory

import org.springframework.data.repository.CrudRepository
import org.springframework.stereotype.Repository

@Repository interface InventoryRepositoryRedis : CrudRepository<Inventory, String> {}
