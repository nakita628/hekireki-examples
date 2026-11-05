package inboxzero.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class ActionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ActionEntity>(ActionTable)

    var createdAt by ActionTable.createdAt
    var updatedAt by ActionTable.updatedAt
    var type by ActionTable.type
    var ruleId by ActionTable.ruleId
    var emailAccountId by ActionTable.emailAccountId
    var emailAccount by EmailAccountEntity referencedOn ActionTable.emailAccountId
    var messagingChannelId by ActionTable.messagingChannelId
    var messagingChannelEmailAccountId by ActionTable.messagingChannelEmailAccountId
    var label by ActionTable.label
    var labelId by ActionTable.labelId
    var subject by ActionTable.subject
    var content by ActionTable.content
    var to by ActionTable.to
    var cc by ActionTable.cc
    var bcc by ActionTable.bcc
    var url by ActionTable.url
    var folderName by ActionTable.folderName
    var folderId by ActionTable.folderId
    var delayInMinutes by ActionTable.delayInMinutes
    var staticAttachments by ActionTable.staticAttachments
    var integrationName by ActionTable.integrationName
    var integrationToolName by ActionTable.integrationToolName
    var integrationArgs by ActionTable.integrationArgs

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(ActionTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
