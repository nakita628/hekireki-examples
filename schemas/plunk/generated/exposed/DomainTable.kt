package plunk.models

import org.jetbrains.exposed.v1.core.ReferenceOption
import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit
import java.util.UUID

object DomainTable : IdTable<String>("domains") {
    override val id = text("id").clientDefault { UUID.randomUUID().toString() }.entityId()
    val domain = text("domain")
    val verified = bool("verified").default(false)
    val dkimTokens = jsonb<String>("dkimTokens", { it }, { it }).nullable()
    val projectId =
        reference(
            "projectId",
            ProjectTable,
            onDelete = ReferenceOption.CASCADE,
            onUpdate = ReferenceOption.CASCADE,
            fkName = "\"domains_projectId_fkey\"",
        )
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).clientDefault { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "domains_pkey")

    init {
        index("\"domains_projectId_idx\"", false, projectId)
        index("\"domains_projectId_verified_idx\"", false, projectId, verified)
        uniqueIndex("\"domains_projectId_domain_key\"", projectId, domain)
    }
}
