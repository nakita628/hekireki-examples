package documenso.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object SiteSettingsTable : IdTable<String>("\"SiteSettings\"") {
    override val id = text("id").entityId()
    val enabled = bool("enabled").default(false)
    val data = jsonb<String>("data", { it }, { it })
    val lastModifiedByUserId =
        optReference(
            "lastModifiedByUserId",
            UserTable,
            onDelete = ReferenceOption.SET_NULL,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"SiteSettings_lastModifiedByUserId_fkey\"",
        )
    val lastModifiedAt =
        pgTimestamp("lastModifiedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"SiteSettings_pkey\"")
}
