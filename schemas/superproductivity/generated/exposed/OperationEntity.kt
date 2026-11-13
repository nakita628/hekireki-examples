package superproductivity.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class OperationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OperationEntity>(OperationTable)

    var userId by OperationTable.userId
    var clientId by OperationTable.clientId
    var serverSeq by OperationTable.serverSeq
    var actionType by OperationTable.actionType
    var opType by OperationTable.opType
    var entityType by OperationTable.entityType
    var entityId by OperationTable.entityId
    var entityIds by OperationTable.entityIds
    var payload by OperationTable.payload
    var payloadBytes by OperationTable.payloadBytes
    var vectorClock by OperationTable.vectorClock
    var schemaVersion by OperationTable.schemaVersion
    var clientTimestamp by OperationTable.clientTimestamp
    var receivedAt by OperationTable.receivedAt
    var isPayloadEncrypted by OperationTable.isPayloadEncrypted
    var syncImportReason by OperationTable.syncImportReason
    var repairBaseServerSeq by OperationTable.repairBaseServerSeq
    var user by UserEntity referencedOn OperationTable.userId
}
