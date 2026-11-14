package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import org.jetbrains.exposed.v1.json.jsonb
import java.time.Instant
import java.time.temporal.ChronoUnit

object CostTable : IdTable<String>("\"Cost\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val projectId = text("projectId")
    val costType = pgEnum("costType", "\"CostType\"", CostType.entries, CostType::dbName)
    val costName = text("costName").nullable()
    val referenceType = pgEnum("referenceType", "\"CostReferenceType\"", CostReferenceType.entries, CostReferenceType::dbName)
    val referenceId = text("referenceId")
    val amount = double("amount")
    val currency = text("currency")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val updatedAt = pgTimestamp("updatedAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    val extraInfo = jsonb<String>("extraInfo", { it }, { it }).nullable()
    override val primaryKey = PrimaryKey(id, name = "\"Cost_pkey\"")

    init {
        index("\"Cost_referenceType_referenceId_idx\"", false, referenceType, referenceId)
        index("\"Cost_costType_idx\"", false, costType)
        index("\"Cost_projectId_idx\"", false, projectId)
        index("\"Cost_projectId_createdAt_idx\"", false, projectId, createdAt)
    }
}
