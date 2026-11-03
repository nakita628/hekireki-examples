package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class CredentialEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, CredentialEntity>(CredentialTable)

    var type by CredentialTable.type
    var key by CredentialTable.key
    var encryptedKey by CredentialTable.encryptedKey
    var user by UserEntity optionalReferencedOn CredentialTable.userId
    var userId by CredentialTable.userId
    var team by TeamEntity optionalReferencedOn CredentialTable.teamId
    var teamId by CredentialTable.teamId
    var app by AppEntity optionalReferencedOn CredentialTable.appId
    var appId by CredentialTable.appId
    var subscriptionId by CredentialTable.subscriptionId
    var paymentStatus by CredentialTable.paymentStatus
    var billingCycleStart by CredentialTable.billingCycleStart
    val destinationCalendars by DestinationCalendarEntity optionalReferrersOn DestinationCalendarTable.credentialId
    val selectedCalendars by SelectedCalendarEntity optionalReferrersOn SelectedCalendarTable.credentialId
    var invalid by CredentialTable.invalid
    val calendarCache by CalendarCacheEntity referrersOn CalendarCacheTable.credentialId
    val references by BookingReferenceEntity optionalReferrersOn BookingReferenceTable.credentialId
    var delegationCredentialId by CredentialTable.delegationCredentialId
    var delegationCredential by DelegationCredentialEntity optionalReferencedOn CredentialTable.delegationCredentialId
    val integrationAttributeSyncs by IntegrationAttributeSyncEntity optionalReferrersOn IntegrationAttributeSyncTable.credentialId
    val hostLocations by HostLocationEntity optionalReferrersOn HostLocationTable.credentialId
}
