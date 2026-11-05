package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass
import java.util.UUID

class EmailUnsubscribeEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, EmailUnsubscribeEntity>(EmailUnsubscribeTable)

    var email by EmailUnsubscribeTable.email
    var category by EmailUnsubscribeTable.category
    var createdAt by EmailUnsubscribeTable.createdAt
}
