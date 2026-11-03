package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object TeamEmailVerificationTable : IdTable<Int>("\"TeamEmailVerification\"") {
    override val id =
        integer("teamId")
            .references(
                TeamTable.id,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "\"TeamEmailVerification_teamId_fkey\"",
            ).entityId()
    val name = text("name")
    val email = text("email")
    val token = text("token").uniqueIndex("\"TeamEmailVerification_token_key\"")
    val completed = bool("completed").default(false)
    val expiresAt = pgTimestamp("expiresAt", 3)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"TeamEmailVerification_pkey\"")

    init {
        uniqueIndex("\"TeamEmailVerification_teamId_key\"", id)
    }
}
