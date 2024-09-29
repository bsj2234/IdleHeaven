package com.siko25.siko.item.effect

import org.springframework.data.mongodb.core.mapping.Document

@Document data class EffectData(val id: String, val description: String)
