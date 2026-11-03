package ghostfolio.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object SettingsTable : IdTable<String>("\"Settings\"") {
    val settings = jsonb<String>("settings", { it }, { it }).nullable()
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val id =
        text("userId")
            .references(
                UserTable.id,
                onDelete = ReferenceOption.CASCADE,
                onUpdate = ReferenceOption.CASCADE,
                fkName = "\"Settings_userId_fkey\"",
            ).entityId()
    override val primaryKey = PrimaryKey(id, name = "\"Settings_pkey\"")
}
