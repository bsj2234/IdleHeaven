package com.siko25.siko.item.itemdrop

import org.springframework.stereotype.Service

@Service
class ItemDropRateFamilySetService(
        private val itemDropRateFamilySetRepository: ItemDropRateFamilySetRepository
) {
        fun initDropRateFamilySet(hardInit: Boolean) {
                if (itemDropRateFamilySetRepository.count() == 0L || hardInit) {
                        val itemDropRateFamilySet =
                                ItemDropRateFamilySet(
                                        id = "0",
                                        dropRateFamilyRate =
                                                arrayOf(
                                                        ItemDropRateFamilyRate(
                                                                familyId = "0",
                                                                dropRate = 1.0
                                                        ),
                                                        ItemDropRateFamilyRate(
                                                                familyId = "1",
                                                                dropRate = 1.0
                                                        ),
                                                        ItemDropRateFamilyRate(
                                                                familyId = "2",
                                                                dropRate = 1.0
                                                        ),
                                                ),
                                )
                        itemDropRateFamilySetRepository.save(itemDropRateFamilySet)
                }
        }
}
