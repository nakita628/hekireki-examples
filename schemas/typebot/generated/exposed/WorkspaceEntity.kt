package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class WorkspaceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, WorkspaceEntity>(WorkspaceTable)

    var createdAt by WorkspaceTable.createdAt
    var updatedAt by WorkspaceTable.updatedAt
    var name by WorkspaceTable.name
    var icon by WorkspaceTable.icon
    var plan by WorkspaceTable.plan
    var stripeId by WorkspaceTable.stripeId
    val credentials by CredentialsEntity referrersOn CredentialsTable.workspaceId
    val customDomains by CustomDomainEntity referrersOn CustomDomainTable.workspaceId
    val folders by DashboardFolderEntity referrersOn DashboardFolderTable.workspaceId
    val members by MemberInWorkspaceEntity referrersOn MemberInWorkspaceTable.workspaceId
    val typebots by TypebotEntity referrersOn TypebotTable.workspaceId
    val invitations by WorkspaceInvitationEntity referrersOn WorkspaceInvitationTable.workspaceId
    var additionalChatsIndex by WorkspaceTable.additionalChatsIndex
    var additionalStorageIndex by WorkspaceTable.additionalStorageIndex
    var chatsLimitFirstEmailSentAt by WorkspaceTable.chatsLimitFirstEmailSentAt
    var storageLimitFirstEmailSentAt by WorkspaceTable.storageLimitFirstEmailSentAt
    var chatsLimitSecondEmailSentAt by WorkspaceTable.chatsLimitSecondEmailSentAt
    var storageLimitSecondEmailSentAt by WorkspaceTable.storageLimitSecondEmailSentAt
    val claimableCustomPlan by ClaimableCustomPlanEntity optionalBackReferencedOn ClaimableCustomPlanTable.workspaceId
    var customChatsLimit by WorkspaceTable.customChatsLimit
    var customStorageLimit by WorkspaceTable.customStorageLimit
    var customSeatsLimit by WorkspaceTable.customSeatsLimit
    var isQuarantined by WorkspaceTable.isQuarantined
    var isSuspended by WorkspaceTable.isSuspended
    var isPastDue by WorkspaceTable.isPastDue
    var isVerified by WorkspaceTable.isVerified
    val themeTemplates by ThemeTemplateEntity referrersOn ThemeTemplateTable.workspaceId
    var chatsHardLimit by WorkspaceTable.chatsHardLimit
    var settings by WorkspaceTable.settings
    var lastActivityAt by WorkspaceTable.lastActivityAt
    val spaces by SpaceEntity referrersOn SpaceTable.workspaceId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(WorkspaceTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
