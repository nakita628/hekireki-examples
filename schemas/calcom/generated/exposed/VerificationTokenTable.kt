package calcom.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object VerificationTokenTable : IdTable<Int>("\"VerificationToken\"") {
    override val id = integer("id").autoIncrement().entityId()
    val identifier = text("identifier")
    val token = text("token").uniqueIndex("\"VerificationToken_token_key\"")
    val expires = pgTimestamp("expires", 3)
    val expiresInDays = integer("expiresInDays").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val teamId =
        optReference(
            "teamId",
            TeamTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"VerificationToken_teamId_fkey\"",
        )
    val secondaryEmailId =
        optReference(
            "secondaryEmailId",
            SecondaryEmailTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"VerificationToken_secondaryEmailId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"VerificationToken_pkey\"")

    init {
        index("\"VerificationToken_token_idx\"", false, token)
        index("\"VerificationToken_teamId_idx\"", false, teamId)
        index("\"VerificationToken_secondaryEmailId_idx\"", false, secondaryEmailId)
        uniqueIndex("\"VerificationToken_identifier_token_key\"", identifier, token)
    }
}
