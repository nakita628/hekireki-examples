package teable.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class UserBaseOrderEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserBaseOrderEntity>(UserBaseOrderTable)

    var userId by UserBaseOrderTable.userId
    var baseId by UserBaseOrderTable.baseId
    var order by UserBaseOrderTable.order
    var createdTime by UserBaseOrderTable.createdTime
    var lastModifiedTime by UserBaseOrderTable.lastModifiedTime
    var user by UserEntity referencedOn UserBaseOrderTable.userId
    var base by BaseEntity referencedOn UserBaseOrderTable.baseId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserBaseOrderTable.lastModifiedTime)) {
                lastModifiedTime = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
