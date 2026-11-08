package workoutcool.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object VerificationTable : IdTable<String>("verification") {
    override val id = text("id").entityId()
    val identifier = text("identifier")
    val value = text("value")
    val expiresAt = pgTimestamp("expiresAt", 3)
    val createdAt = pgTimestamp("createdAt", 3).nullable()
    val updatedAt = pgTimestamp("updatedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "verification_pkey")
}
