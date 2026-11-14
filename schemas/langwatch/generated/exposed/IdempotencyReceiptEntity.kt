package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class IdempotencyReceiptEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, IdempotencyReceiptEntity>(IdempotencyReceiptTable)

    var scopeId by IdempotencyReceiptTable.scopeId
    var key by IdempotencyReceiptTable.key
    var claimId by IdempotencyReceiptTable.claimId
    var requestFingerprint by IdempotencyReceiptTable.requestFingerprint
    var responseStatus by IdempotencyReceiptTable.responseStatus
    var responseBody by IdempotencyReceiptTable.responseBody
    var createdAt by IdempotencyReceiptTable.createdAt
    var heartbeatAt by IdempotencyReceiptTable.heartbeatAt
    var expiresAt by IdempotencyReceiptTable.expiresAt
}
