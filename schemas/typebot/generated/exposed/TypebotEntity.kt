package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class TypebotEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, TypebotEntity>(TypebotTable)

    var version by TypebotTable.version
    var createdAt by TypebotTable.createdAt
    var updatedAt by TypebotTable.updatedAt
    var icon by TypebotTable.icon
    var name by TypebotTable.name
    var folderId by TypebotTable.folderId
    var groups by TypebotTable.groups
    var events by TypebotTable.events
    var variables by TypebotTable.variables
    var edges by TypebotTable.edges
    var theme by TypebotTable.theme
    var selectedThemeTemplateId by TypebotTable.selectedThemeTemplateId
    var settings by TypebotTable.settings
    var publicId by TypebotTable.publicId
    var customDomain by TypebotTable.customDomain
    var workspaceId by TypebotTable.workspaceId
    var resultsTablePreferences by TypebotTable.resultsTablePreferences
    var folder by DashboardFolderEntity optionalReferencedOn TypebotTable.folderId
    var workspace by WorkspaceEntity referencedOn TypebotTable.workspaceId
    val collaborators by CollaboratorsOnTypebotsEntity referrersOn CollaboratorsOnTypebotsTable.typebotId
    val invitations by InvitationEntity referrersOn InvitationTable.typebotId
    val publishedTypebot by PublicTypebotEntity optionalBackReferencedOn PublicTypebotTable.typebotId
    val results by ResultEntity referrersOn ResultTable.typebotId
    val webhooks by WebhookEntity referrersOn WebhookTable.typebotId
    var isArchived by TypebotTable.isArchived
    var isClosed by TypebotTable.isClosed
    var whatsAppCredentialsId by TypebotTable.whatsAppCredentialsId
    var riskLevel by TypebotTable.riskLevel
    val bannedIps by BannedIpEntity referrersOn BannedIpTable.responsibleTypebotId
    var space by SpaceEntity optionalReferencedOn TypebotTable.spaceId
    var spaceId by TypebotTable.spaceId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(TypebotTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
