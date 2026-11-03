package langfuse.models

import io.github.thibaultmeyer.cuid.CUID
import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object GatewayAiConnectionTable : IdTable<String>("gateway_ai_connections") {
    override val id = text("id").clientDefault { CUID.randomCUID1().toString() }.entityId()
    val organizationId =
        reference(
            "organization_id",
            OrganizationTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "gateway_ai_connections_organization_id_fkey",
        )
    val name = text("name")
    val provider = pgEnum("provider", "\"GatewayProvider\"", GatewayProvider.entries, GatewayProvider::dbName)
    val encryptedCredential = text("encrypted_credentials")
    val displaySecret = text("display_secret_key")
    val createdById = text("created_by_id").nullable()
    val status =
        pgEnum("status", "\"GatewayConnectionStatus\"", GatewayConnectionStatus.entries, GatewayConnectionStatus::dbName).default(
            GatewayConnectionStatus.ENABLED,
        )
    val allowedModels = jsonb<String>("allowed_models", { it }, { it }).nullable()
    val createdAt = pgTimestamp("created_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updated_at", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "gateway_ai_connections_pkey")

    init {
        index("gateway_ai_conn_org_created_at_id_idx", false, organizationId, createdAt, id)
        index("gateway_ai_conn_org_status_created_at_id_idx", false, organizationId, status, createdAt, id)
        uniqueIndex("gateway_ai_connections_organization_id_name_key", organizationId, name)
    }
}
