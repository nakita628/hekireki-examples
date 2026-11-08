package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object UserTable : IdTable<String>("\"User\"") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val email = text("email")
    val password = text("password").nullable()
    val providerName = pgEnum("providerName", "\"Provider\"", Provider.entries, Provider::dbName)
    val name = text("name").nullable()
    val lastName = text("lastName").nullable()
    val isSuperAdmin = bool("isSuperAdmin").default(false)
    val bio = text("bio").nullable()
    val audience = integer("audience").default(0)
    val pictureId =
        optReference(
            "pictureId",
            MediaTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"User_pictureId_fkey\"",
        )
    val providerId = text("providerId").nullable()
    val timezone = integer("timezone")
    val timezoneName = text("timezoneName").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastReadNotifications =
        pgTimestamp("lastReadNotifications", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val inviteId = text("inviteId").nullable()
    val activated = bool("activated").default(true)
    val account = text("account").nullable()
    val connectedAccount = bool("connectedAccount").default(false)
    val lastOnline = pgTimestamp("lastOnline", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val ip = text("ip").nullable()
    val agent = text("agent").nullable()
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    val sendSuccessEmails = bool("sendSuccessEmails").default(true)
    val sendFailureEmails = bool("sendFailureEmails").default(true)
    val sendStreakEmails = bool("sendStreakEmails").default(true)
    override val primaryKey = PrimaryKey(id, name = "\"User_pkey\"")

    init {
        index("\"User_providerId_providerName_idx\"", false, providerId, providerName)
        index("\"User_lastReadNotifications_idx\"", false, lastReadNotifications)
        index("\"User_inviteId_idx\"", false, inviteId)
        index("\"User_account_idx\"", false, account)
        index("\"User_lastOnline_idx\"", false, lastOnline)
        index("\"User_pictureId_idx\"", false, pictureId)
        index("\"User_deletedAt_idx\"", false, deletedAt)
        uniqueIndex("\"User_email_providerName_key\"", email, providerName)
    }
}
