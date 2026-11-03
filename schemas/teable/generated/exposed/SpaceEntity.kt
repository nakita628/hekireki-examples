package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SpaceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SpaceEntity>(SpaceTable)

    var name by SpaceTable.name
    var avatar by SpaceTable.avatar
    var credit by SpaceTable.credit
    var deletedTime by SpaceTable.deletedTime
    var createdTime by SpaceTable.createdTime
    var createdBy by SpaceTable.createdBy
    var lastModifiedBy by SpaceTable.lastModifiedBy
    var lastModifiedTime by SpaceTable.lastModifiedTime
    var isTemplate by SpaceTable.isTemplate
    val baseGroup by BaseEntity referrersOn BaseTable.spaceId
    val dataDbBinding by SpaceDataDbBindingEntity optionalBackReferencedOn SpaceDataDbBindingTable.spaceId
    val dataDbMigrationJobs by SpaceDataDbMigrationJobEntity referrersOn SpaceDataDbMigrationJobTable.spaceId
    val baseDataDbMoveJobsAsSource by BaseDataDbMoveJobEntity referrersOn BaseDataDbMoveJobTable.sourceSpaceId
    val baseDataDbMoveJobsAsTarget by BaseDataDbMoveJobEntity referrersOn BaseDataDbMoveJobTable.targetSpaceId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SpaceTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
