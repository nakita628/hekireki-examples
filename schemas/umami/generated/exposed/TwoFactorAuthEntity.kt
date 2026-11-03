package umami.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TwoFactorAuthEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TwoFactorAuthEntity>(TwoFactorAuthTable)

    var userId by TwoFactorAuthTable.userId
    var secret by TwoFactorAuthTable.secret
    var isEnabled by TwoFactorAuthTable.isEnabled
    var createdAt by TwoFactorAuthTable.createdAt
    var updatedAt by TwoFactorAuthTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TwoFactorAuthTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
