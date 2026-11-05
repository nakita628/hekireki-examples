package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

class GscConnectionEntity(
    id: EntityID<UUID>,
) : Entity<UUID>(id) {
    companion object : EntityClass<UUID, GscConnectionEntity>(GscConnectionTable)

    var projectId by GscConnectionTable.projectId
    var project by ProjectEntity referencedOn GscConnectionTable.projectId
    var siteUrl by GscConnectionTable.siteUrl
    var accessToken by GscConnectionTable.accessToken
    var refreshToken by GscConnectionTable.refreshToken
    var accessTokenExpiresAt by GscConnectionTable.accessTokenExpiresAt
    var lastSyncedAt by GscConnectionTable.lastSyncedAt
    var lastSyncStatus by GscConnectionTable.lastSyncStatus
    var lastSyncError by GscConnectionTable.lastSyncError
    var backfillStatus by GscConnectionTable.backfillStatus
    var createdAt by GscConnectionTable.createdAt
    var updatedAt by GscConnectionTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(GscConnectionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
