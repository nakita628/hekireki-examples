package ghostfolio.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AuthDeviceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AuthDeviceEntity>(AuthDeviceTable)

    var createdAt by AuthDeviceTable.createdAt
    var credentialId by AuthDeviceTable.credentialId
    var credentialPublicKey by AuthDeviceTable.credentialPublicKey
    var counter by AuthDeviceTable.counter
    var updatedAt by AuthDeviceTable.updatedAt
    var user by UserEntity referencedOn AuthDeviceTable.userId
    var userId by AuthDeviceTable.userId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AuthDeviceTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
