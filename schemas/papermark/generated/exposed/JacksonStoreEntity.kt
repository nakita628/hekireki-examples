package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class JacksonStoreEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, JacksonStoreEntity>(JacksonStoreTable)

    var value by JacksonStoreTable.value
    var iv by JacksonStoreTable.iv
    var tag by JacksonStoreTable.tag
    var namespace by JacksonStoreTable.namespace
    var createdAt by JacksonStoreTable.createdAt
    var modifiedAt by JacksonStoreTable.modifiedAt
}
