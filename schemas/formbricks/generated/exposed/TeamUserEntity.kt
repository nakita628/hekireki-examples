package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import java.time.Instant
import java.time.temporal.ChronoUnit

class TeamUserEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<TeamUserEntity>(TeamUserTable)

    var createdAt by TeamUserTable.createdAt
    var updatedAt by TeamUserTable.updatedAt
    val teamId by TeamUserTable.teamId
    val team by TeamEntity referencedOn TeamUserTable.teamId
    val userId by TeamUserTable.userId
    val user by UserEntity referencedOn TeamUserTable.userId
    var role by TeamUserTable.role

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TeamUserTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
