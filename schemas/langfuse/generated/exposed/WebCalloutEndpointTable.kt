package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.TextColumnType
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object WebCalloutEndpointTable : IdTable<String>("web_callout_endpoints") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val projectId =
        reference(
            "project_id",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "web_callout_endpoints_project_id_fkey",
        )
    val name = text("name").default("Default")
    val url = text("url")
    val enabled = bool("enabled").default(true)
    val toastMessage = text("toast_message").default("Callout sent")
    val requestHeaders = text("request_headers").nullable()
    val requestHeaderKeys = array("request_header_keys", TextColumnType()).nullable().databaseDefault("'{}'") { emptyList() }
    override val primaryKey = PrimaryKey(id, name = "web_callout_endpoints_pkey")

    init {
        index("web_callout_endpoints_project_id_idx", false, projectId)
        index("web_callout_endpoints_project_id_enabled_idx", false, projectId, enabled)
    }
}
