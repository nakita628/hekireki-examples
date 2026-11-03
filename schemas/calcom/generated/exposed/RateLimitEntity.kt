package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class RateLimitEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, RateLimitEntity>(RateLimitTable)

    var name by RateLimitTable.name
    var apiKeyId by RateLimitTable.apiKeyId
    var ttl by RateLimitTable.ttl
    var limit by RateLimitTable.limit
    var blockDuration by RateLimitTable.blockDuration
    var createdAt by RateLimitTable.createdAt
    var updatedAt by RateLimitTable.updatedAt
    var apiKey by ApiKeyEntity referencedOn RateLimitTable.apiKeyId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(RateLimitTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
