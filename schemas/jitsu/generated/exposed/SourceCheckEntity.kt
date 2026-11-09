package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SourceCheckEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SourceCheckEntity>(SourceCheckTable)

    var `package` by SourceCheckTable.`package`
    var version by SourceCheckTable.version
    var status by SourceCheckTable.status
    var description by SourceCheckTable.description
    var timestamp by SourceCheckTable.timestamp
}
