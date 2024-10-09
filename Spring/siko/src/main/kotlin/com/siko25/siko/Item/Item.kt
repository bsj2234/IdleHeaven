package com.siko25.siko.item

import com.siko25.siko.item.effect.EffectService
import com.siko25.siko.item.rarity.Rarity
import kotlin.reflect.KClass
import org.springframework.data.annotation.Id
import org.springframework.data.redis.core.RedisHash
import org.springframework.data.redis.core.RedisTemplate
import org.springframework.stereotype.Component
import org.springframework.stereotype.Service

@Component
class IdGenerator(private val redisTemplate: RedisTemplate<String, String>) {
        fun <T : Any> generatedId(classInfo: KClass<T>): Long {
                val entityType =
                        classInfo.simpleName?.lowercase()
                                ?: throw IllegalArgumentException(
                                        "Entity type name cannot be null or empty"
                                )
                val idKey = "$entityType:id"
                return redisTemplate.opsForValue().increment(idKey)!!
        }
}

@RedisHash("items")
data class Item(
        @Id val id: Long,
        val name: String,
        val dataName: String,
        val components: ComponentContainer<ItemComponent>
)

abstract class IBaseComponent()

abstract class ItemComponent(
        open val name: String,
        open val dataName: String,
        open val ownerName: String
) : IBaseComponent()

sealed class ItemDataComponent(open val name: String, open val ownerName: String) :
        IBaseComponent()

data class EquipmentDataComponent(
        override val name: String,
        override val ownerName: String,
        val randomEffectSetName: String,
        val fixedEffectSetName: String,
        val effectService: EffectService
) : ItemDataComponent(name, ownerName)

data class ComponentContainer<T : IBaseComponent>(
        private val components: MutableList<T> = mutableListOf()
) {

        fun findComponent(predicate: (T) -> Boolean): T? {
                return components.find(predicate)
        }

        fun findComponentByType(type: Class<out T>): T? {
                return components.find { type.isInstance(it) }
        }

        fun findComponentByName(name: String): T? {
                return components.find { it is ItemComponent && it.name == name }
        }

        fun addComponent(component: T) {
                components.add(component)
        }

        fun getAllComponents(): List<T> = components
}

interface ItemComponentFactory {
        fun createComponent(
                owner: Item,
                dataOwner: ItemData,
                dataComponent: ItemDataComponent
        ): ItemComponent
}

@Service
class EquipmentComponentFactory(
        private val effectService: EffectService,
) : ItemComponentFactory {
        override fun createComponent(
                owner: Item,
                dataOwner: ItemData,
                dataComponent: ItemDataComponent
        ): ItemComponent {
                if (dataComponent !is EquipmentDataComponent) {
                        throw IllegalArgumentException("Expected EquipmentDataComponent")
                }
                val rarity = dataOwner.rarity

                val count =
                        when (rarity) {
                                Rarity.Common -> 1
                                Rarity.Uncommon -> 2
                                Rarity.Rare -> 3
                                Rarity.Epic -> 4
                                Rarity.Legendary -> 5
                        }
                val effects =
                        effectService.getEffectsBySetName(dataComponent.randomEffectSetName, count)
                return EquipmentComponent(
                        dataComponent.name,
                        dataComponent.name,
                        owner.name,
                        effects
                )
        }
}

@Service
class ItemFactory(
        private val idGenerator: IdGenerator,
        private val equipmentComponentFactory: EquipmentComponentFactory
) {
        fun createItem(itemData: ItemData): Item {
                val item =
                        Item(
                                idGenerator.generatedId(Item::class),
                                itemData.name,
                                itemData.name,
                                ComponentContainer<ItemComponent>()
                        )
                val dataComponents = itemData.components.getAllComponents()
                for (dataComponent in dataComponents) {
                        val instanceComponent =
                                equipmentComponentFactory.createComponent(
                                        item,
                                        itemData,
                                        dataComponent
                                )
                        item.components.addComponent(instanceComponent)
                }
                return item
        }
}
