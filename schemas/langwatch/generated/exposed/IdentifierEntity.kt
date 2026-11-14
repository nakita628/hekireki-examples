package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class IdentifierEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, IdentifierEntity>(IdentifierTable)

    var userId by IdentifierTable.userId
    var provider by IdentifierTable.provider
    var value by IdentifierTable.value
    var domain by IdentifierTable.domain
    var identifierHash by IdentifierTable.identifierHash
    var accountId by IdentifierTable.accountId
    var providerId by IdentifierTable.providerId
    var issuer by IdentifierTable.issuer
    var providerAccountId by IdentifierTable.providerAccountId
    var state by IdentifierTable.state
    var connectionId by IdentifierTable.connectionId
    var verifiedAt by IdentifierTable.verifiedAt
    var attachedAt by IdentifierTable.attachedAt
    var detachedAt by IdentifierTable.detachedAt
    var lastUsedAt by IdentifierTable.lastUsedAt
    var createdAt by IdentifierTable.createdAt
    var updatedAt by IdentifierTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(IdentifierTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
