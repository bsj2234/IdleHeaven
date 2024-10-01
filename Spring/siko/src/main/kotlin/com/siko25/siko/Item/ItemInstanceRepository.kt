package com.siko25.siko.item

import org.springframework.data.repository.CrudRepository
import org.springframework.stereotype.Repository

@Repository interface ItemInstanceRepository : CrudRepository<ItemInstance, String> {}
