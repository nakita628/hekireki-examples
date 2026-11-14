package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ConnectedCreditGrantEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ConnectedCreditGrantEntity>(ConnectedCreditGrantTable)

    var accountId by ConnectedCreditGrantTable.accountId
    var stripeCreditGrantId by ConnectedCreditGrantTable.stripeCreditGrantId
    var amountUsdCents by ConnectedCreditGrantTable.amountUsdCents
    var kind by ConnectedCreditGrantTable.kind
    var termEndsAt by ConnectedCreditGrantTable.termEndsAt
    var expiresAt by ConnectedCreditGrantTable.expiresAt
    var createdAt by ConnectedCreditGrantTable.createdAt
}
