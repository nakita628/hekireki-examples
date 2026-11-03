package hoppscotch.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object TeamInvitationTable : IdTable<String>("\"TeamInvitation\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val teamID =
        reference(
            "teamID",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TeamInvitation_teamID_fkey\"",
        )
    val creatorUid = text("creatorUid")
    val inviteeEmail = text("inviteeEmail")
    val inviteeRole = pgEnum("inviteeRole", "\"TeamAccessRole\"", TeamAccessRole.entries, TeamAccessRole::dbName)
    override val primaryKey = PrimaryKey(id, name = "\"TeamInvitation_pkey\"")

    init {
        index("\"TeamInvitation_teamID_idx\"", false, teamID)
        uniqueIndex("\"TeamInvitation_teamID_inviteeEmail_key\"", teamID, inviteeEmail)
    }
}
