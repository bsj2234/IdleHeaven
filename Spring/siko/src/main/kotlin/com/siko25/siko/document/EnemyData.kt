package com.siko25.siko.document

import org.springframework.data.annotation.Id
import org.springframework.data.mongodb.core.mapping.Document

@Document data class EnemyData(@Id val id: String, val name: String, val dropId: String)
