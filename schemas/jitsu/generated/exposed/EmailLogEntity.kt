package jitsu.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class EmailLogEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, EmailLogEntity>(EmailLogTable)

    var time by EmailLogTable.time
    var email by EmailLogTable.email
    var error by EmailLogTable.error
    var previewUrl by EmailLogTable.previewUrl
    var messageId by EmailLogTable.messageId
    var status by EmailLogTable.status
}
