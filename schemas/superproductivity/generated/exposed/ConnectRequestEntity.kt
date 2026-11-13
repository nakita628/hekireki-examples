package superproductivity.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ConnectRequestEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ConnectRequestEntity>(ConnectRequestTable)

    var pollSecretHash by ConnectRequestTable.pollSecretHash
    var matchCode by ConnectRequestTable.matchCode
    var deviceName by ConnectRequestTable.deviceName
    var platform by ConnectRequestTable.platform
    var status by ConnectRequestTable.status
    var userId by ConnectRequestTable.userId
    var tokenVersion by ConnectRequestTable.tokenVersion
    var expiresAt by ConnectRequestTable.expiresAt
    var user by UserEntity optionalReferencedOn ConnectRequestTable.userId
}
