package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class DataDbConnectionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, DataDbConnectionEntity>(DataDbConnectionTable)

    var provider by DataDbConnectionTable.provider
    var encryptedUrl by DataDbConnectionTable.encryptedUrl
    var urlFingerprint by DataDbConnectionTable.urlFingerprint
    var displayHost by DataDbConnectionTable.displayHost
    var displayDatabase by DataDbConnectionTable.displayDatabase
    var internalSchema by DataDbConnectionTable.internalSchema
    var status by DataDbConnectionTable.status
    var schemaVersion by DataDbConnectionTable.schemaVersion
    var capabilities by DataDbConnectionTable.capabilities
    var lastValidatedAt by DataDbConnectionTable.lastValidatedAt
    var lastError by DataDbConnectionTable.lastError
    var healthState by DataDbConnectionTable.healthState
    var healthReason by DataDbConnectionTable.healthReason
    var healthChangedAt by DataDbConnectionTable.healthChangedAt
    var lastHealthCheckAt by DataDbConnectionTable.lastHealthCheckAt
    var consecutiveHealthFailures by DataDbConnectionTable.consecutiveHealthFailures
    var createdBy by DataDbConnectionTable.createdBy
    var createdTime by DataDbConnectionTable.createdTime
    var lastModifiedTime by DataDbConnectionTable.lastModifiedTime
    val spaceBindings by SpaceDataDbBindingEntity optionalReferrersOn SpaceDataDbBindingTable.dataDbConnectionId
    val sourceMigrationJobs by SpaceDataDbMigrationJobEntity optionalReferrersOn SpaceDataDbMigrationJobTable.sourceConnectionId
    val targetMigrationJobs by SpaceDataDbMigrationJobEntity optionalReferrersOn SpaceDataDbMigrationJobTable.targetConnectionId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(DataDbConnectionTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
