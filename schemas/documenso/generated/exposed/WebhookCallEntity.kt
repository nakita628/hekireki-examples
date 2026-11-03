package documenso.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class WebhookCallEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WebhookCallEntity>(WebhookCallTable)

    var status by WebhookCallTable.status
    var url by WebhookCallTable.url
    var event by WebhookCallTable.event
    var requestBody by WebhookCallTable.requestBody
    var responseCode by WebhookCallTable.responseCode
    var responseHeaders by WebhookCallTable.responseHeaders
    var responseBody by WebhookCallTable.responseBody
    var createdAt by WebhookCallTable.createdAt
    var webhookId by WebhookCallTable.webhookId
    var webhook by WebhookEntity referencedOn WebhookCallTable.webhookId
}
