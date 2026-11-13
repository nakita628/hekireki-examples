package superproductivity.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class CaptureConnectCodeEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, CaptureConnectCodeEntity>(CaptureConnectCodeTable)

    var codeHash by CaptureConnectCodeTable.codeHash
    var tokenVersion by CaptureConnectCodeTable.tokenVersion
    var expiresAt by CaptureConnectCodeTable.expiresAt
    val user by UserEntity referencedOn CaptureConnectCodeTable.id
}
