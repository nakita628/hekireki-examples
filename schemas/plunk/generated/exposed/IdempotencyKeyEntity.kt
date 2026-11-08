package plunk.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class IdempotencyKeyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, IdempotencyKeyEntity>(IdempotencyKeyTable)

    var key by IdempotencyKeyTable.key
    var project by ProjectEntity referencedOn IdempotencyKeyTable.projectId
    var projectId by IdempotencyKeyTable.projectId
    var method by IdempotencyKeyTable.method
    var path by IdempotencyKeyTable.path
    var statusCode by IdempotencyKeyTable.statusCode
    var createdAt by IdempotencyKeyTable.createdAt
    var expiresAt by IdempotencyKeyTable.expiresAt
}
