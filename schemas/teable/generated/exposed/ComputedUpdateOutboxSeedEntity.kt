package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ComputedUpdateOutboxSeedEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ComputedUpdateOutboxSeedEntity>(ComputedUpdateOutboxSeedTable)

    var taskId by ComputedUpdateOutboxSeedTable.taskId
    var tableId by ComputedUpdateOutboxSeedTable.tableId
    var recordId by ComputedUpdateOutboxSeedTable.recordId
    var task by ComputedUpdateOutboxEntity referencedOn ComputedUpdateOutboxSeedTable.taskId
}
