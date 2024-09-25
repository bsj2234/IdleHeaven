package com.siko25.siko.document

import org.springframework.data.mongodb.core.mapping.Document

@Document data class Effect(val effectType: String, val effectValues: FloatArray)
