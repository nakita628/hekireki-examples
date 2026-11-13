package superproductivity.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class CaptureGrantEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, CaptureGrantEntity>(CaptureGrantTable)

    var userId by CaptureGrantTable.userId
    var kind by CaptureGrantTable.kind
    var clientName by CaptureGrantTable.clientName
    var secretHash by CaptureGrantTable.secretHash
    var createdAt by CaptureGrantTable.createdAt
    var expiresAt by CaptureGrantTable.expiresAt
    var lastUsedAt by CaptureGrantTable.lastUsedAt
    var revokedAt by CaptureGrantTable.revokedAt
    var clientId by CaptureGrantTable.clientId
    var resource by CaptureGrantTable.resource
    var refreshHash by CaptureGrantTable.refreshHash
    var prevRefreshHash by CaptureGrantTable.prevRefreshHash
    var rotatedAt by CaptureGrantTable.rotatedAt
    var refreshExpiresAt by CaptureGrantTable.refreshExpiresAt
    var user by UserEntity referencedOn CaptureGrantTable.userId
}
