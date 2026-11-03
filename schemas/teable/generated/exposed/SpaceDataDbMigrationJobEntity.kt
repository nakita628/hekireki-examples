package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SpaceDataDbMigrationJobEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SpaceDataDbMigrationJobEntity>(SpaceDataDbMigrationJobTable)

    var spaceId by SpaceDataDbMigrationJobTable.spaceId
    var sourceConnectionId by SpaceDataDbMigrationJobTable.sourceConnectionId
    var targetConnectionId by SpaceDataDbMigrationJobTable.targetConnectionId
    var targetMode by SpaceDataDbMigrationJobTable.targetMode
    var switchOnCompletion by SpaceDataDbMigrationJobTable.switchOnCompletion
    var state by SpaceDataDbMigrationJobTable.state
    var targetUrlFingerprint by SpaceDataDbMigrationJobTable.targetUrlFingerprint
    var targetInternalSchema by SpaceDataDbMigrationJobTable.targetInternalSchema
    var inventory by SpaceDataDbMigrationJobTable.inventory
    var copyStats by SpaceDataDbMigrationJobTable.copyStats
    var validationStats by SpaceDataDbMigrationJobTable.validationStats
    var lastError by SpaceDataDbMigrationJobTable.lastError
    var startedAt by SpaceDataDbMigrationJobTable.startedAt
    var completedAt by SpaceDataDbMigrationJobTable.completedAt
    var createdBy by SpaceDataDbMigrationJobTable.createdBy
    var createdTime by SpaceDataDbMigrationJobTable.createdTime
    var lastModifiedTime by SpaceDataDbMigrationJobTable.lastModifiedTime
    var space by SpaceEntity referencedOn SpaceDataDbMigrationJobTable.spaceId
    var sourceConnection by DataDbConnectionEntity optionalReferencedOn SpaceDataDbMigrationJobTable.sourceConnectionId
    var targetConnection by DataDbConnectionEntity optionalReferencedOn SpaceDataDbMigrationJobTable.targetConnectionId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SpaceDataDbMigrationJobTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
