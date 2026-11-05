package openpanel.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object UserTotpTable : IdTable<String>("user_totp") {
    override val id = text("id").databaseDefault("gen_random_uuid()").entityId()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"user_totp_userId_fkey\"",
        ).uniqueIndex("\"user_totp_userId_key\"")
    val secret = text("secret")
    val recoveryCodes = array("recoveryCodes", TextColumnType()).nullable()
    val enabledAt = pgTimestamp("enabledAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "user_totp_pkey")
}
