package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class PendingPremiumGrantEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PendingPremiumGrantEntity>(PendingPremiumGrantTable)

    var createdAt by PendingPremiumGrantTable.createdAt
    var updatedAt by PendingPremiumGrantTable.updatedAt
    var email by PendingPremiumGrantTable.email
    var tier by PendingPremiumGrantTable.tier
    var count by PendingPremiumGrantTable.count
    var emailAccountsAccess by PendingPremiumGrantTable.emailAccountsAccess

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(PendingPremiumGrantTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
