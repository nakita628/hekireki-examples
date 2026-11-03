package documenso.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AnonymousVerificationTokenTable : IdTable<String>("\"AnonymousVerificationToken\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val token = text("token").uniqueIndex("\"AnonymousVerificationToken_token_key\"")
    val expiresAt = pgTimestamp("expiresAt", 3)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"AnonymousVerificationToken_pkey\"")

    init {
        uniqueIndex("\"AnonymousVerificationToken_id_key\"", id)
    }
}
