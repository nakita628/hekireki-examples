package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LogEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LogEntity>(LogTable)

    var createdAt by LogTable.createdAt
    var resultId by LogTable.resultId
    var status by LogTable.status
    var context by LogTable.context
    var description by LogTable.description
    var details by LogTable.details
    var result by ResultEntity referencedOn LogTable.resultId
}
