package openpanel.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserTable : IdTable<String>("users") {
    override val id = text("id").databaseDefault("gen_random_uuid()").entityId()
    val email = text("email").uniqueIndex("users_email_key")
    val firstName = text("firstName").nullable()
    val lastName = text("lastName").nullable()
    val name = text("name").nullable()
    val emailVerified = bool("emailVerified").default(false)
    val image = text("image").nullable()
    val twoFactorEnabled = bool("twoFactorEnabled").default(false)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val deletedAt = pgTimestamp("deletedAt", 3).nullable()
    override val primaryKey = PrimaryKey(id, name = "users_pkey")
}
