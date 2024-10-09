package com.siko25.siko.item

import com.siko25.siko.item.*
import com.siko25.siko.item.effect.*

data class EquipmentComponent(
        override val name: String,
        override val dataName: String,
        override val ownerName: String,
        val effects: List<Effect>
) : ItemComponent(name, dataName, ownerName)
