package linkwarden.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class UserEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, UserEntity>(UserTable)

    var uuid by UserTable.uuid
    var name by UserTable.name
    var username by UserTable.username
    var email by UserTable.email
    var emailVerified by UserTable.emailVerified
    var unverifiedNewEmail by UserTable.unverifiedNewEmail
    var image by UserTable.image
    var password by UserTable.password
    var locale by UserTable.locale
    var parentSubscription by SubscriptionEntity optionalReferencedOn UserTable.parentSubscriptionId
    var parentSubscriptionId by UserTable.parentSubscriptionId
    val accounts by AccountEntity referrersOn AccountTable.userId
    val collections by CollectionEntity referrersOn CollectionTable.ownerId
    val tags by TagEntity referrersOn TagTable.ownerId
    var pinnedLinks by LinkEntity via PinnedLinksTable
    val createdLinks by LinkEntity optionalReferrersOn LinkTable.createdById
    val createdCollections by CollectionEntity optionalReferrersOn CollectionTable.createdById
    val highlights by HighlightEntity referrersOn HighlightTable.userId
    val collectionsJoined by UsersAndCollectionsEntity referrersOn UsersAndCollectionsTable.userId
    var collectionOrder by UserTable.collectionOrder
    val accessTokens by AccessTokenEntity referrersOn AccessTokenTable.userId
    val subscriptions by SubscriptionEntity optionalBackReferencedOn SubscriptionTable.userId
    var linksRouteTo by UserTable.linksRouteTo
    var aiTaggingMethod by UserTable.aiTaggingMethod
    var aiPredefinedTags by UserTable.aiPredefinedTags
    var aiTagExistingLinks by UserTable.aiTagExistingLinks
    var theme by UserTable.theme
    var dismissedAnnouncementId by UserTable.dismissedAnnouncementId
    var readableFontFamily by UserTable.readableFontFamily
    var readableFontSize by UserTable.readableFontSize
    var readableLineHeight by UserTable.readableLineHeight
    var readableLineWidth by UserTable.readableLineWidth
    var preventDuplicateLinks by UserTable.preventDuplicateLinks
    var archiveAsScreenshot by UserTable.archiveAsScreenshot
    var archiveAsMonolith by UserTable.archiveAsMonolith
    var archiveAsPDF by UserTable.archiveAsPDF
    var archiveAsReadable by UserTable.archiveAsReadable
    var archiveAsWaybackMachine by UserTable.archiveAsWaybackMachine
    var referredBy by UserTable.referredBy
    val dashboardSections by DashboardSectionEntity referrersOn DashboardSectionTable.userId
    var lastPickedAt by UserTable.lastPickedAt
    var acceptPromotionalEmails by UserTable.acceptPromotionalEmails
    var shareUsageAnalytics by UserTable.shareUsageAnalytics
    var trialEndEmailSent by UserTable.trialEndEmailSent
    var sessionVersion by UserTable.sessionVersion
    var passwordRetries by UserTable.passwordRetries
    var passwordRetryAt by UserTable.passwordRetryAt
    var createdAt by UserTable.createdAt
    var updatedAt by UserTable.updatedAt

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
