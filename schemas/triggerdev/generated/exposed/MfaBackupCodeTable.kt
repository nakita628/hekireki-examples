package triggerdev.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object MfaBackupCodeTable : IdTable<String>("\"MfaBackupCode\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val code = text("code")
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"MfaBackupCode_userId_fkey\"",
        )
    val usedAt = pgTimestamp("usedAt", 3).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"MfaBackupCode_pkey\"")

    init {
        uniqueIndex("\"MfaBackupCode_userId_code_key\"", userId, code)
    }
}
