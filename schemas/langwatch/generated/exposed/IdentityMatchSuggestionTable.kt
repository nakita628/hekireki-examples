package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object IdentityMatchSuggestionTable : IdTable<String>("\"IdentityMatchSuggestion\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val organizationId = text("organizationId")
    val discoveredPersonId = text("discoveredPersonId")
    val userId = text("userId")
    val score = double("score")
    val computedAt = pgTimestamp("computedAt", 3)
    override val primaryKey = PrimaryKey(id, name = "\"IdentityMatchSuggestion_pkey\"")

    init {
        index("\"IdentityMatchSuggestion_organizationId_score_idx\"", false, organizationId, score)
        uniqueIndex("IdentityMatchSuggestion_organizationId_discoveredPersonId_u_key", organizationId, discoveredPersonId, userId)
    }
}
