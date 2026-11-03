package formbricks.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TeamEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TeamEntity>(TeamTable)

    var createdAt by TeamTable.createdAt
    var updatedAt by TeamTable.updatedAt
    var name by TeamTable.name
    var organizationId by TeamTable.organizationId
    var organization by OrganizationEntity referencedOn TeamTable.organizationId
    val teamUsers by TeamUserEntity referrersOn TeamUserTable.teamId
    val workspaceTeams by WorkspaceTeamEntity referrersOn WorkspaceTeamTable.teamId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TeamTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
