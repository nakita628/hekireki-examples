package litellm.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class LiteLLMSkillSubmissionEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, LiteLLMSkillSubmissionEntity>(LiteLLMSkillSubmissionTable)

    var pluginName by LiteLLMSkillSubmissionTable.pluginName
    var operation by LiteLLMSkillSubmissionTable.operation
    var status by LiteLLMSkillSubmissionTable.status
    var proposalJson by LiteLLMSkillSubmissionTable.proposalJson
    var archiveContent by LiteLLMSkillSubmissionTable.archiveContent
    var submittedBy by LiteLLMSkillSubmissionTable.submittedBy
    var createdAt by LiteLLMSkillSubmissionTable.createdAt
    var basePluginId by LiteLLMSkillSubmissionTable.basePluginId
    var baseUpdatedAt by LiteLLMSkillSubmissionTable.baseUpdatedAt
    var reviewedBy by LiteLLMSkillSubmissionTable.reviewedBy
    var reviewedAt by LiteLLMSkillSubmissionTable.reviewedAt
    var rejectionReason by LiteLLMSkillSubmissionTable.rejectionReason
    var scanJson by LiteLLMSkillSubmissionTable.scanJson
}
