package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ConnectedStatementEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ConnectedStatementEntity>(ConnectedStatementTable)

    var accountId by ConnectedStatementTable.accountId
    var month by ConnectedStatementTable.month
    var sentAt by ConnectedStatementTable.sentAt
}
