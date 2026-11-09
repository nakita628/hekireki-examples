package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class KvStoreEntryEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, KvStoreEntryEntity>(KvStoreEntryTable)

    var value by KvStoreEntryTable.value
    var expiresAt by KvStoreEntryTable.expiresAt
}
