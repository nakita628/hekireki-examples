package postiz.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object IntegrationsWebhooksTable : CompositeIdTable("\"IntegrationsWebhooks\"") {
    val integrationId =
        reference(
            "integrationId",
            IntegrationTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"IntegrationsWebhooks_integrationId_fkey\"",
        )
    val webhookId =
        reference(
            "webhookId",
            WebhooksTable,
            onDelete = ReferenceOption.RESTRICT,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"IntegrationsWebhooks_webhookId_fkey\"",
        )
    override val primaryKey = PrimaryKey(integrationId, webhookId, name = "\"IntegrationsWebhooks_pkey\"")

    init {
        addIdColumn(integrationId)
        addIdColumn(webhookId)
        index("\"IntegrationsWebhooks_integrationId_idx\"", false, integrationId)
        index("\"IntegrationsWebhooks_webhookId_idx\"", false, webhookId)
        uniqueIndex("\"IntegrationsWebhooks_integrationId_webhookId_key\"", integrationId, webhookId)
    }
}
