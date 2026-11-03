package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object AppTable : IdTable<String>("\"App\"") {
    override val id = text("slug").entityId()
    val dirName = text("dirName").uniqueIndex("\"App_dirName_key\"")
    val keys = jsonb<String>("keys", { it }, { it }).nullable()
    val categories = array("categories", PgEnumColumnType("\"AppCategories\"", AppCategories.entries, AppCategories::dbName)).nullable()
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val enabled = bool("enabled").default(false)
    override val primaryKey = PrimaryKey(id, name = "\"App_pkey\"")

    init {
        uniqueIndex("\"App_slug_key\"", id)
        index("\"App_enabled_idx\"", false, enabled)
    }
}
