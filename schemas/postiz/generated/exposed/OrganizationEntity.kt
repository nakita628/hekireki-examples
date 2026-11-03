package postiz.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class OrganizationEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, OrganizationEntity>(OrganizationTable)

    var name by OrganizationTable.name
    var description by OrganizationTable.description
    var apiKey by OrganizationTable.apiKey
    var paymentId by OrganizationTable.paymentId
    var streakSince by OrganizationTable.streakSince
    var createdAt by OrganizationTable.createdAt
    var updatedAt by OrganizationTable.updatedAt
    var deletedAt by OrganizationTable.deletedAt
    var allowTrial by OrganizationTable.allowTrial
    var isTrailing by OrganizationTable.isTrailing
    var shortlink by OrganizationTable.shortlink
    val autoPost by AutoPostEntity referrersOn AutoPostTable.organizationId
    val comments by CommentsEntity referrersOn CommentsTable.organizationId
    val credits by CreditsEntity referrersOn CreditsTable.organizationId
    val clippings by ClippingEntity referrersOn ClippingTable.organizationId
    val customers by CustomerEntity referrersOn CustomerTable.orgId
    val errors by ErrorsEntity referrersOn ErrorsTable.organizationId
    val github by GitHubEntity referrersOn GitHubTable.organizationId
    val integration by IntegrationEntity referrersOn IntegrationTable.organizationId
    val media by MediaEntity referrersOn MediaTable.organizationId
    val buyerOrganization by MessagesGroupEntity referrersOn MessagesGroupTable.buyerOrganizationId
    val notifications by NotificationsEntity referrersOn NotificationsTable.organizationId
    val plugs by PlugsEntity referrersOn PlugsTable.organizationId
    val post by PostEntity referrersOn PostTable.organizationId
    val submittedPost by PostEntity optionalReferrersOn PostTable.submittedForOrganizationId
    val sets by SetsEntity referrersOn SetsTable.organizationId
    val signatures by SignaturesEntity referrersOn SignaturesTable.organizationId
    val subscription by SubscriptionEntity optionalBackReferencedOn SubscriptionTable.organizationId
    val tags by TagsEntity referrersOn TagsTable.orgId
    val thirdParty by ThirdPartyEntity referrersOn ThirdPartyTable.organizationId
    val usedCodes by UsedCodesEntity referrersOn UsedCodesTable.orgId
    val users by UserOrganizationEntity referrersOn UserOrganizationTable.organizationId
    val webhooks by WebhooksEntity referrersOn WebhooksTable.organizationId
    val oauthApp by OAuthAppEntity optionalReferrersOn OAuthAppTable.organizationId
    val oauthAuthorizations by OAuthAuthorizationEntity referrersOn OAuthAuthorizationTable.organizationId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(OrganizationTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
