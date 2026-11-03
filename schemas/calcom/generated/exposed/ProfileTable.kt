package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ProfileTable : IdTable<Int>("\"Profile\"") {
    override val id = integer("id").autoIncrement().entityId()
    val uid = text("uid")
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Profile_userId_fkey\"",
        )
    val organizationId =
        reference(
            "organizationId",
            TeamTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"Profile_organizationId_fkey\"",
        )
    val username = text("username")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"Profile_pkey\"")

    init {
        index("\"Profile_uid_idx\"", false, uid)
        index("\"Profile_userId_idx\"", false, userId)
        index("\"Profile_organizationId_idx\"", false, organizationId)
        uniqueIndex("\"Profile_userId_organizationId_key\"", userId, organizationId)
        uniqueIndex("\"Profile_username_organizationId_key\"", username, organizationId)
    }
}
