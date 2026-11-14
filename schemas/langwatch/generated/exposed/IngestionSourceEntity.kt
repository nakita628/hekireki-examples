package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class IngestionSourceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, IngestionSourceEntity>(IngestionSourceTable)

    var organizationId by IngestionSourceTable.organizationId
    var teamId by IngestionSourceTable.teamId
    var sourceType by IngestionSourceTable.sourceType
    var name by IngestionSourceTable.name
    var description by IngestionSourceTable.description
    var ingestSecretHash by IngestionSourceTable.ingestSecretHash
    var parserConfig by IngestionSourceTable.parserConfig
    var pollerCursor by IngestionSourceTable.pollerCursor
    var errorCount by IngestionSourceTable.errorCount
    var pullSchedule by IngestionSourceTable.pullSchedule
    var status by IngestionSourceTable.status
    var traceProjectId by IngestionSourceTable.traceProjectId
    var providerAccountId by IngestionSourceTable.providerAccountId
    var lastEventAt by IngestionSourceTable.lastEventAt
    var lastSuccessAt by IngestionSourceTable.lastSuccessAt
    var lastReadThroughAt by IngestionSourceTable.lastReadThroughAt
    var lastRunCompleteness by IngestionSourceTable.lastRunCompleteness
    var unpricedUsageSince by IngestionSourceTable.unpricedUsageSince
    var unpricedUsageThrough by IngestionSourceTable.unpricedUsageThrough
    var archivedAt by IngestionSourceTable.archivedAt
    var createdAt by IngestionSourceTable.createdAt
    var updatedAt by IngestionSourceTable.updatedAt
    var createdById by IngestionSourceTable.createdById

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(IngestionSourceTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
