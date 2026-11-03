package rallly.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class SpaceEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SpaceEntity>(SpaceTable)

    var name by SpaceTable.name
    var image by SpaceTable.image
    var ownerId by SpaceTable.ownerId
    var tier by SpaceTable.tier
    var spaceType by SpaceTable.spaceType
    var industry by SpaceTable.industry
    var primaryColor by SpaceTable.primaryColor
    var showBranding by SpaceTable.showBranding
    var hideAttribution by SpaceTable.hideAttribution
    var shared by SpaceTable.shared
    var nonprofitDiscountGrantedAt by SpaceTable.nonprofitDiscountGrantedAt
    var createdAt by SpaceTable.createdAt
    var updatedAt by SpaceTable.updatedAt
    var owner by UserEntity referencedOn SpaceTable.ownerId
    val polls by PollEntity optionalReferrersOn PollTable.spaceId
    val scheduledEvents by ScheduledEventEntity referrersOn ScheduledEventTable.spaceId
    val eventTypes by EventTypeEntity referrersOn EventTypeTable.spaceId
    val sheets by SheetEntity referrersOn SheetTable.spaceId
    val subscriptions by SubscriptionEntity referrersOn SubscriptionTable.spaceId
    val members by SpaceMemberEntity referrersOn SpaceMemberTable.spaceId
    val memberInvites by SpaceMemberInviteEntity referrersOn SpaceMemberInviteTable.spaceId
    val apiKeys by SpaceApiKeyEntity referrersOn SpaceApiKeyTable.spaceId
    val nonprofitApplications by NonprofitApplicationEntity referrersOn NonprofitApplicationTable.spaceId
    val webhooks by SpaceWebhookEntity referrersOn SpaceWebhookTable.spaceId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(SpaceTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
