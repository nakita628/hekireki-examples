package umami.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.core.java.javaUUID
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object ReportTable : IdTable<UUID>("report") {
    override val id = javaUUID("report_id").entityId()
    val userId = javaUUID("user_id")
    val websiteId = javaUUID("website_id")
    val type = varchar("type", 50)
    val name = varchar("name", 200)
    val description = varchar("description", 500)
    val parameters = jsonb<String>("parameters", { it }, { it })
    val createdAt =
        pgTimestamptz("created_at", 6).nullable().databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamptz("updated_at", 6).nullable().clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "report_pkey")

    init {
        index("report_user_id_idx", false, userId)
        index("report_website_id_idx", false, websiteId)
        index("report_type_idx", false, type)
        index("report_name_idx", false, name)
    }
}
