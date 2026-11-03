package langfuse.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class ApiKeyEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, ApiKeyEntity>(ApiKeyTable)

    var createdAt by ApiKeyTable.createdAt
    var note by ApiKeyTable.note
    var publicKey by ApiKeyTable.publicKey
    var hashedSecretKey by ApiKeyTable.hashedSecretKey
    var fastHashedSecretKey by ApiKeyTable.fastHashedSecretKey
    var displaySecretKey by ApiKeyTable.displaySecretKey
    var lastUsedAt by ApiKeyTable.lastUsedAt
    var expiresAt by ApiKeyTable.expiresAt
    var isInAppAgentKey by ApiKeyTable.isInAppAgentKey
    var projectId by ApiKeyTable.projectId
    var project by ProjectEntity optionalReferencedOn ApiKeyTable.projectId
    var orgId by ApiKeyTable.orgId
    var organization by OrganizationEntity optionalReferencedOn ApiKeyTable.orgId
    var scope by ApiKeyTable.scope
    var createdByUserId by ApiKeyTable.createdByUserId
    var createdByUser by UserEntity optionalReferencedOn ApiKeyTable.createdByUserId
    var userId by ApiKeyTable.userId
    var user by UserEntity optionalReferencedOn ApiKeyTable.userId
    var createdByApiKeyId by ApiKeyTable.createdByApiKeyId
    var createdByApiKey by ApiKeyEntity optionalReferencedOn ApiKeyTable.createdByApiKeyId
    val apiKeysCreated by ApiKeyEntity optionalReferrersOn ApiKeyTable.createdByApiKeyId
    val gatewayAssociation by GatewayApiKeyAssociationEntity optionalBackReferencedOn GatewayApiKeyAssociationTable.id
    val roleAssignments by RoleAssignmentEntity optionalReferrersOn RoleAssignmentTable.apiKeyId
}
