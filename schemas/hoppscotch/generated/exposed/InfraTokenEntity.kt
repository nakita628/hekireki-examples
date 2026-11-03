package hoppscotch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class InfraTokenEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, InfraTokenEntity>(InfraTokenTable)

    var creatorUid by InfraTokenTable.creatorUid
    var label by InfraTokenTable.label
    var token by InfraTokenTable.token
    var expiresOn by InfraTokenTable.expiresOn
    var createdOn by InfraTokenTable.createdOn
    var updatedOn by InfraTokenTable.updatedOn
}
