package com.siko25.siko.character.player.playerId

import org.springframework.data.repository.CrudRepository
import org.springframework.stereotype.Repository

@Repository interface PlayerIdRepositoryRedis : CrudRepository<PlayerId, String> {}
