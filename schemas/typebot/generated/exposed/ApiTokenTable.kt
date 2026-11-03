package typebot.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object ApiTokenTable : IdTable<String>("\"ApiToken\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val token = text("token").uniqueIndex("\"ApiToken_token_key\"")
    val name = text("name")
    val ownerId =
        reference(
            "ownerId",
            UserTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"ApiToken_ownerId_fkey\"",
        )
    override val primaryKey = PrimaryKey(id, name = "\"ApiToken_pkey\"")
}
