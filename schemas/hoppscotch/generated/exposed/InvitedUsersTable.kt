package hoppscotch.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object InvitedUsersTable : IdTable<String>("\"InvitedUsers\"") {
    val adminUid =
        reference(
            "adminUid",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"InvitedUsers_adminUid_fkey\"",
        )
    val adminEmail = text("adminEmail")
    override val id = text("inviteeEmail").entityId()
    val invitedOn = pgTimestamptz("invitedOn", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }

    init {
        uniqueIndex("\"InvitedUsers_inviteeEmail_key\"", id)
    }
}
