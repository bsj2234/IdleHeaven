package com.siko25.siko.item.effect

import org.springframework.data.mongodb.repository.MongoRepository
import org.springframework.stereotype.Repository

@Repository interface EffectDataRepository : MongoRepository<EffectData, String> {}
