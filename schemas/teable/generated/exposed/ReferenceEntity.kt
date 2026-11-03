package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ReferenceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ReferenceEntity>(ReferenceTable)

    var fromFieldId by ReferenceTable.fromFieldId
    var toFieldId by ReferenceTable.toFieldId
    var createdTime by ReferenceTable.createdTime
}
