package ghostfolio.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class AnalyticsEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AnalyticsEntity>(AnalyticsTable)

    var activityCount by AnalyticsTable.activityCount
    var country by AnalyticsTable.country
    var dataProviderGhostfolioDailyRequests by AnalyticsTable.dataProviderGhostfolioDailyRequests
    var lastRequestAt by AnalyticsTable.lastRequestAt
    var lastRequestClient by AnalyticsTable.lastRequestClient
    var updatedAt by AnalyticsTable.updatedAt
    val user by UserEntity referencedOn AnalyticsTable.id

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(AnalyticsTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
