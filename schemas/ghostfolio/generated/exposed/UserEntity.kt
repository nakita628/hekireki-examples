package ghostfolio.models

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

    val accessesGet by AccessEntity optionalReferrersOn AccessTable.granteeUserId
    val accessesGive by AccessEntity referrersOn AccessTable.userId
    var accessToken by UserTable.accessToken
    val accounts by AccountEntity referrersOn AccountTable.userId
    val activities by OrderEntity referrersOn OrderTable.userId
    val analytics by AnalyticsEntity optionalBackReferencedOn AnalyticsTable.id
    val apiKeys by ApiKeyEntity referrersOn ApiKeyTable.userId
    var authChallenge by UserTable.authChallenge
    val authDevices by AuthDeviceEntity referrersOn AuthDeviceTable.userId
    var createdAt by UserTable.createdAt
    var provider by UserTable.provider
    var role by UserTable.role
    val settings by SettingsEntity optionalBackReferencedOn SettingsTable.id
    val subscriptions by SubscriptionEntity referrersOn SubscriptionTable.userId
    val tags by TagEntity optionalReferrersOn TagTable.userId
    var thirdPartyId by UserTable.thirdPartyId
    var updatedAt by UserTable.updatedAt
    var watchlist by SymbolProfileEntity via UserWatchlistTable
    val symbolProfile by SymbolProfileEntity optionalReferrersOn SymbolProfileTable.userId

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(UserTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
