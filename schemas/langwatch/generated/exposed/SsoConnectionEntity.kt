package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SsoConnectionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SsoConnectionEntity>(SsoConnectionTable)

    var organizationId by SsoConnectionTable.organizationId
    var type by SsoConnectionTable.type
    var state by SsoConnectionTable.state
    var claimedDomains by SsoConnectionTable.claimedDomains
    var domainClaims by SsoConnectionTable.domainClaims
    var approvedDomains by SsoConnectionTable.approvedDomains
    var verifiedDomains by SsoConnectionTable.verifiedDomains
    var lapsedDomains by SsoConnectionTable.lapsedDomains
    var domainVerifications by SsoConnectionTable.domainVerifications
    var pendingVerification by SsoConnectionTable.pendingVerification
    var idpMetadata by SsoConnectionTable.idpMetadata
    var arrivalPolicy by SsoConnectionTable.arrivalPolicy
    var allowsJit by SsoConnectionTable.allowsJit
    var arrivalPolicyDecidedAt by SsoConnectionTable.arrivalPolicyDecidedAt
    var source by SsoConnectionTable.source1
    var testLoginAccountId by SsoConnectionTable.testLoginAccountId
    var replacesConnectionId by SsoConnectionTable.replacesConnectionId
    var migrationPhase by SsoConnectionTable.migrationPhase
    var graceStartedAt by SsoConnectionTable.graceStartedAt
    var routeChangedAt by SsoConnectionTable.routeChangedAt
    var finalizationRequestedAt by SsoConnectionTable.finalizationRequestedAt
    var finalizedAt by SsoConnectionTable.finalizedAt
    var rejection by SsoConnectionTable.rejection
    var createdBy by SsoConnectionTable.createdBy
    var tearDownAfter by SsoConnectionTable.tearDownAfter
    var occurredAt by SsoConnectionTable.occurredAt
    var lastEventId by SsoConnectionTable.lastEventId
    var acceptedAt by SsoConnectionTable.acceptedAt
    var projectionVersion by SsoConnectionTable.projectionVersion
    var createdAt by SsoConnectionTable.createdAt
    var updatedAt by SsoConnectionTable.updatedAt
}
