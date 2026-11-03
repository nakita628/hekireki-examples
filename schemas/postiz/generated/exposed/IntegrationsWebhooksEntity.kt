package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeID
import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.CompositeEntity
import org.jetbrains.exposed.v1.dao.CompositeEntityClass

class IntegrationsWebhooksEntity(
    id: EntityID<CompositeID>,
) : CompositeEntity(id) {
    companion object : CompositeEntityClass<IntegrationsWebhooksEntity>(IntegrationsWebhooksTable)

    val integrationId by IntegrationsWebhooksTable.integrationId
    val webhookId by IntegrationsWebhooksTable.webhookId
    val integration by IntegrationEntity referencedOn IntegrationsWebhooksTable.integrationId
    val webhook by WebhooksEntity referencedOn IntegrationsWebhooksTable.webhookId
}
