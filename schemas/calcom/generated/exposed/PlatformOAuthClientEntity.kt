package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class PlatformOAuthClientEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, PlatformOAuthClientEntity>(PlatformOAuthClientTable)

    var name by PlatformOAuthClientTable.name
    var secret by PlatformOAuthClientTable.secret
    var permissions by PlatformOAuthClientTable.permissions
    var users by UserEntity via PlatformOAuthClientToUserTable
    var logo by PlatformOAuthClientTable.logo
    var redirectUris by PlatformOAuthClientTable.redirectUris
    var organizationId by PlatformOAuthClientTable.organizationId
    var organization by TeamEntity referencedOn PlatformOAuthClientTable.organizationId
    val teams by TeamEntity optionalReferrersOn TeamTable.createdByOAuthClientId
    val accessTokens by AccessTokenEntity referrersOn AccessTokenTable.platformOAuthClientId
    val refreshToken by RefreshTokenEntity referrersOn RefreshTokenTable.platformOAuthClientId
    val authorizationTokens by PlatformAuthorizationTokenEntity referrersOn PlatformAuthorizationTokenTable.platformOAuthClientId
    val webhook by WebhookEntity optionalReferrersOn WebhookTable.platformOAuthClientId
    var bookingRedirectUri by PlatformOAuthClientTable.bookingRedirectUri
    var bookingCancelRedirectUri by PlatformOAuthClientTable.bookingCancelRedirectUri
    var bookingRescheduleRedirectUri by PlatformOAuthClientTable.bookingRescheduleRedirectUri
    var areEmailsEnabled by PlatformOAuthClientTable.areEmailsEnabled
    var areDefaultEventTypesEnabled by PlatformOAuthClientTable.areDefaultEventTypesEnabled
    var areCalendarEventsEnabled by PlatformOAuthClientTable.areCalendarEventsEnabled
    var createdAt by PlatformOAuthClientTable.createdAt
}
