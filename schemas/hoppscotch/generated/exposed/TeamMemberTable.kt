package hoppscotch.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.util.UUID

object TeamMemberTable : IdTable<String>("\"TeamMember\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val role = pgEnum("role", "\"TeamAccessRole\"", TeamAccessRole.entries, TeamAccessRole::dbName)
    val userUid = text("userUid")
    val teamID =
        reference(
            "teamID",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"TeamMember_teamID_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"TeamMember_pkey\"")

    init {
        uniqueIndex("\"TeamMember_teamID_userUid_key\"", teamID, userUid)
    }
}
