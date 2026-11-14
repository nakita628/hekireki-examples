package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class AiToolEntryTeamEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, AiToolEntryTeamEntity>(AiToolEntryTeamTable)

    var entryId by AiToolEntryTeamTable.entryId
    var teamId by AiToolEntryTeamTable.teamId
}
