package com.siko25.siko.item.effect

import org.springframework.data.mongodb.repository.MongoRepository

interface EffectDataRepository : MongoRepository<EffectData, String> {}
