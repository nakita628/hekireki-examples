package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ErasedIdentifierSuppressionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ErasedIdentifierSuppressionEntity>(ErasedIdentifierSuppressionTable)

    var organizationId by ErasedIdentifierSuppressionTable.organizationId
    var provider by ErasedIdentifierSuppressionTable.provider
    var identifierHash by ErasedIdentifierSuppressionTable.identifierHash
    var erasedAt by ErasedIdentifierSuppressionTable.erasedAt
}
