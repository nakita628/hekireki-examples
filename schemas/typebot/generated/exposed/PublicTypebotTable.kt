package typebot.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object PublicTypebotTable : IdTable<String>("\"PublicTypebot\"") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val version = text("version").nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val lastActivityAt = pgTimestamp("lastActivityAt", 3).nullable()
    val typebotId =
        reference(
            "typebotId",
            TypebotTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"PublicTypebot_typebotId_fkey\"",
        ).uniqueIndex("\"PublicTypebot_typebotId_key\"")
    val groups = jsonb<String>("groups", { it }, { it })
    val events = jsonb<String>("events", { it }, { it }).nullable()
    val variables = jsonb<String>("variables", { it }, { it })
    val edges = jsonb<String>("edges", { it }, { it })
    val theme = jsonb<String>("theme", { it }, { it })
    val settings = jsonb<String>("settings", { it }, { it })
    override val primaryKey = PrimaryKey(id, name = "\"PublicTypebot_pkey\"")

    init {
        index("\"PublicTypebot_lastActivityAt_idx\"", false, lastActivityAt)
    }
}
