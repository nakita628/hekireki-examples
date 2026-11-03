package umami.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TwoFactorRateLimitEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TwoFactorRateLimitEntity>(TwoFactorRateLimitTable)

    var userId by TwoFactorRateLimitTable.userId
    var attempts by TwoFactorRateLimitTable.attempts
    var lockedUntil by TwoFactorRateLimitTable.lockedUntil
    var updatedAt by TwoFactorRateLimitTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TwoFactorRateLimitTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
