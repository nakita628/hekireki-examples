package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object AuditLogTable : IdTable<String>("audit_logs") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val type =
        pgEnum("type", "\"AuditLogRecordType\"", AuditLogRecordType.entries, AuditLogRecordType::dbName).default(AuditLogRecordType.USER)
    val apiKeyId = text("api_key_id").nullable()
    val userId = text("user_id").nullable()
    val orgId = text("org_id")
    val userOrgRole = text("user_org_role").nullable()
    val projectId = text("project_id").nullable()
    val userProjectRole = text("user_project_role").nullable()
    val resourceType = text("resource_type")
    val resourceId = text("resource_id")
    val action = text("action")
    val before = text("before").nullable()
    val after = text("after").nullable()
    override val primaryKey = PrimaryKey(id, name = "audit_logs_pkey")

    init {
        index("audit_logs_project_id_idx", false, projectId)
        index("audit_logs_api_key_id_idx", false, apiKeyId)
        index("audit_logs_user_id_idx", false, userId)
        index("audit_logs_org_id_idx", false, orgId)
        index("audit_logs_created_at_idx", false, createdAt)
        index("audit_logs_updated_at_idx", false, updatedAt)
    }
}
