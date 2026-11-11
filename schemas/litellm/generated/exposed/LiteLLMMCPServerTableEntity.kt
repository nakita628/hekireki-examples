package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityBatchUpdate
import org.jetbrains.exposed.v1.dao.EntityClass
import java.time.Instant
import java.time.temporal.ChronoUnit

class LiteLLMMCPServerTableEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMMCPServerTableEntity>(LiteLLMMCPServerTableTable)

    var serverName by LiteLLMMCPServerTableTable.serverName
    var alias by LiteLLMMCPServerTableTable.alias
    var description by LiteLLMMCPServerTableTable.description
    var instructions by LiteLLMMCPServerTableTable.instructions
    var url by LiteLLMMCPServerTableTable.url
    var specPath by LiteLLMMCPServerTableTable.specPath
    var transport by LiteLLMMCPServerTableTable.transport
    var authType by LiteLLMMCPServerTableTable.authType
    var credentials by LiteLLMMCPServerTableTable.credentials
    var createdAt by LiteLLMMCPServerTableTable.createdAt
    var createdBy by LiteLLMMCPServerTableTable.createdBy
    var updatedAt by LiteLLMMCPServerTableTable.updatedAt
    var updatedBy by LiteLLMMCPServerTableTable.updatedBy
    var mcpInfo by LiteLLMMCPServerTableTable.mcpInfo
    var mcpAccessGroups by LiteLLMMCPServerTableTable.mcpAccessGroups
    var allowedTools by LiteLLMMCPServerTableTable.allowedTools
    var toolNameToDisplayName by LiteLLMMCPServerTableTable.toolNameToDisplayName
    var toolNameToDescription by LiteLLMMCPServerTableTable.toolNameToDescription
    var pinnedTools by LiteLLMMCPServerTableTable.pinnedTools
    var extraHeaders by LiteLLMMCPServerTableTable.extraHeaders
    var staticHeaders by LiteLLMMCPServerTableTable.staticHeaders
    var envVars by LiteLLMMCPServerTableTable.envVars
    var status by LiteLLMMCPServerTableTable.status
    var lastHealthCheck by LiteLLMMCPServerTableTable.lastHealthCheck
    var healthCheckError by LiteLLMMCPServerTableTable.healthCheckError
    var command by LiteLLMMCPServerTableTable.command
    var args by LiteLLMMCPServerTableTable.args
    var env by LiteLLMMCPServerTableTable.env
    var issuer by LiteLLMMCPServerTableTable.issuer
    var authorizationUrl by LiteLLMMCPServerTableTable.authorizationUrl
    var tokenUrl by LiteLLMMCPServerTableTable.tokenUrl
    var registrationUrl by LiteLLMMCPServerTableTable.registrationUrl
    var oauth2Flow by LiteLLMMCPServerTableTable.oauth2Flow
    var tokenExchangeEndpoint by LiteLLMMCPServerTableTable.tokenExchangeEndpoint
    var audience by LiteLLMMCPServerTableTable.audience
    var subjectTokenType by LiteLLMMCPServerTableTable.subjectTokenType
    var tokenExchangeProfile by LiteLLMMCPServerTableTable.tokenExchangeProfile
    var allowAllKeys by LiteLLMMCPServerTableTable.allowAllKeys
    var availableOnPublicInternet by LiteLLMMCPServerTableTable.availableOnPublicInternet
    var delegateAuthToUpstream by LiteLLMMCPServerTableTable.delegateAuthToUpstream
    var oauthPassthrough by LiteLLMMCPServerTableTable.oauthPassthrough
    var dcrBridge by LiteLLMMCPServerTableTable.dcrBridge
    var perServerOauthDiscovery by LiteLLMMCPServerTableTable.perServerOauthDiscovery
    var isByok by LiteLLMMCPServerTableTable.isByok
    var byokDescription by LiteLLMMCPServerTableTable.byokDescription
    var byokApiKeyHelpUrl by LiteLLMMCPServerTableTable.byokApiKeyHelpUrl
    var sourceUrl by LiteLLMMCPServerTableTable.sourceUrl
    var timeout by LiteLLMMCPServerTableTable.timeout
    var maxConcurrentRequests by LiteLLMMCPServerTableTable.maxConcurrentRequests
    var rpm by LiteLLMMCPServerTableTable.rpm
    var approvalStatus by LiteLLMMCPServerTableTable.approvalStatus
    var submittedBy by LiteLLMMCPServerTableTable.submittedBy
    var submittedAt by LiteLLMMCPServerTableTable.submittedAt
    var reviewedAt by LiteLLMMCPServerTableTable.reviewedAt
    var reviewNotes by LiteLLMMCPServerTableTable.reviewNotes

    override fun flush(batch: EntityBatchUpdate?): Boolean {
        if (writeValues.isNotEmpty()) {
            if (!isWritten(LiteLLMMCPServerTableTable.updatedAt)) {
                updatedAt = Instant.now().truncatedTo(ChronoUnit.MILLIS)
            }
        }
        return super.flush(batch)
    }
}
