package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class TaskReferenceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TaskReferenceEntity>(TaskReferenceTable)

    var fromFieldId by TaskReferenceTable.fromFieldId
    var toFieldId by TaskReferenceTable.toFieldId
    var createdTime by TaskReferenceTable.createdTime
}
