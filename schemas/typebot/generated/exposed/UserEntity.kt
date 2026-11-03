package typebot.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class UserEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, UserEntity>(UserTable)

    var createdAt by UserTable.createdAt
    var updatedAt by UserTable.updatedAt
    var lastActivityAt by UserTable.lastActivityAt
    var name by UserTable.name
    var email by UserTable.email
    var emailVerified by UserTable.emailVerified
    var image by UserTable.image
    var company by UserTable.company
    var onboardingCategories by UserTable.onboardingCategories
    var referral by UserTable.referral
    var graphNavigation by UserTable.graphNavigation
    var preferredAppAppearance by UserTable.preferredAppAppearance
    val accounts by AccountEntity referrersOn AccountTable.userId
    val apiTokens by ApiTokenEntity referrersOn ApiTokenTable.ownerId
    val collaboratorsOnTypebots by CollaboratorsOnTypebotsEntity referrersOn CollaboratorsOnTypebotsTable.userId
    val workspaces by MemberInWorkspaceEntity referrersOn MemberInWorkspaceTable.userId
    val sessions by SessionEntity referrersOn SessionTable.userId
    val bannedIps by BannedIpEntity referrersOn BannedIpTable.userId
    var displayedInAppNotifications by UserTable.displayedInAppNotifications
    val credentials by UserCredentialsEntity referrersOn UserCredentialsTable.userId
    var groupTitlesAutoGeneration by UserTable.groupTitlesAutoGeneration
    var preferredLanguage by UserTable.preferredLanguage
    var termsAcceptedAt by UserTable.termsAcceptedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
