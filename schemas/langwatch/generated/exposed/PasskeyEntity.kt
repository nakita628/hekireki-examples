package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PasskeyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PasskeyEntity>(PasskeyTable)

    var name by PasskeyTable.name
    var publicKey by PasskeyTable.publicKey
    var userId by PasskeyTable.userId
    var credentialID by PasskeyTable.credentialID
    var counter by PasskeyTable.counter
    var deviceType by PasskeyTable.deviceType
    var backedUp by PasskeyTable.backedUp
    var transports by PasskeyTable.transports
    var aaguid by PasskeyTable.aaguid
    var createdAt by PasskeyTable.createdAt
    var updatedAt by PasskeyTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PasskeyTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
