package documenso.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object VerificationTokenTable : IdTable<Int>("\"VerificationToken\"") {
    override val id = integer("id").autoIncrement().entityId()
    val secondaryId =
        text("secondaryId").clientDefault { CUID.randomCUID1().toString() }.uniqueIndex("\"VerificationToken_secondaryId_key\"")
    val identifier = text("identifier")
    val token = text("token").uniqueIndex("\"VerificationToken_token_key\"")
    val completed = bool("completed").default(false)
    val expires = pgTimestamp("expires", 3)
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val metadata = jsonb<String>("metadata", { it }, { it }).nullable()
    val userId =
        reference(
            "userId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"VerificationToken_userId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"VerificationToken_pkey\"")
}
