package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class BaseEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, BaseEntity>(BaseTable)

    var spaceId by BaseTable.spaceId
    var name by BaseTable.name
    var order by BaseTable.order
    var icon by BaseTable.icon
    var schemaPass by BaseTable.schemaPass
    var v2Enabled by BaseTable.v2Enabled
    var provisionState by BaseTable.provisionState
    var deletedTime by BaseTable.deletedTime
    var createdTime by BaseTable.createdTime
    var createdBy by BaseTable.createdBy
    var lastModifiedBy by BaseTable.lastModifiedBy
    var lastModifiedTime by BaseTable.lastModifiedTime
    var space by SpaceEntity referencedOn BaseTable.spaceId
    val tables by TableMetaEntity referrersOn TableMetaTable.baseId
    val dataDbMoveJobs by BaseDataDbMoveJobEntity referrersOn BaseDataDbMoveJobTable.baseId
    val userOrders by UserBaseOrderEntity referrersOn UserBaseOrderTable.baseId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(BaseTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
