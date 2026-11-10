package opensaas.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LogsEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, LogsEntity>(LogsTable)

    var createdAt by LogsTable.createdAt
    var message by LogsTable.message
    var level by LogsTable.level
}
