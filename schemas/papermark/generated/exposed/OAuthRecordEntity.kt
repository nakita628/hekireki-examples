package papermark.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OAuthRecordEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OAuthRecordEntity>(OAuthRecordTable)

    var kind by OAuthRecordTable.kind
    var payload by OAuthRecordTable.payload
    var grantId by OAuthRecordTable.grantId
    var userCode by OAuthRecordTable.userCode
    var uid by OAuthRecordTable.uid
    var expiresAt by OAuthRecordTable.expiresAt
    var consumedAt by OAuthRecordTable.consumedAt
    var createdAt by OAuthRecordTable.createdAt
}
