package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ScimTokenEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ScimTokenEntity>(ScimTokenTable)

    var organizationId by ScimTokenTable.organizationId
    var connectionId by ScimTokenTable.connectionId
    var hashedToken by ScimTokenTable.hashedToken
    var hashScheme by ScimTokenTable.hashScheme
    var description by ScimTokenTable.description
    var createdAt by ScimTokenTable.createdAt
    var lastUsedAt by ScimTokenTable.lastUsedAt
}
