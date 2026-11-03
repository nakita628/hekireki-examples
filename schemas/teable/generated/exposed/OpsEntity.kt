package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OpsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OpsEntity>(OpsTable)

    var collection by OpsTable.collection
    var docId by OpsTable.docId
    var docType by OpsTable.docType
    var version by OpsTable.version
    var operation by OpsTable.operation
    var createdTime by OpsTable.createdTime
    var createdBy by OpsTable.createdBy
}
