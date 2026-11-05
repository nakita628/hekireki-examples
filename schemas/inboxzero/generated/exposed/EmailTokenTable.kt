package inboxzero.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object EmailTokenTable : IdTable<String>("\"EmailToken\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val token = text("token").uniqueIndex("\"EmailToken_token_key\"")
    val expiresAt = pgTimestamp("expiresAt", 3)
    val emailAccountId =
        reference(
            "emailAccountId",
            EmailAccountTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"EmailToken_emailAccountId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"EmailToken_pkey\"")

    init {
        index("\"EmailToken_emailAccountId_idx\"", false, emailAccountId)
    }
}
