package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object AiToolEntryTeamTable : IdTable<String>("\"AiToolEntryTeam\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val entryId = text("entryId")
    val teamId = text("teamId")
    override val primaryKey = PrimaryKey(id, name = "\"AiToolEntryTeam_pkey\"")

    init {
        index("\"AiToolEntryTeam_teamId_idx\"", false, teamId)
        uniqueIndex("\"AiToolEntryTeam_entryId_teamId_key\"", entryId, teamId)
    }
}
