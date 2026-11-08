package workoutcool.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable

object SessionTable : IdTable<String>("session") {
    override val id = text("id").entityId()
    val expiresAt = pgTimestamp("expiresAt", 3)
    val token = text("token").uniqueIndex("session_token_key")
    val createdAt = pgTimestamp("createdAt", 3)
    val updatedAt = pgTimestamp("updatedAt", 3)
    val ipAddress = text("ipAddress").nullable()
    val userAgent = text("userAgent").nullable()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"session_userId_fkey\"",
        )
    val impersonatedBy = text("impersonatedBy").nullable()
    override val primaryKey = PrimaryKey(id, name = "session_pkey")
}
