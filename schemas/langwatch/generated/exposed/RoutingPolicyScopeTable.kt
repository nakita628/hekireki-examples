package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable
import java.time.Instant
import java.time.temporal.ChronoUnit

object RoutingPolicyScopeTable : IdTable<String>("\"RoutingPolicyScope\"") {
    override val id = text("id").clientDefault { nanoid(21) }.entityId()
    val routingPolicyId = text("routingPolicyId")
    val scopeType = pgEnum("scopeType", "\"RoutingPolicyScopeType\"", RoutingPolicyScopeType.entries, RoutingPolicyScopeType::dbName)
    val scopeId = text("scopeId")
    val createdAt = pgTimestamp("createdAt", 3).databaseDefault("CURRENT_TIMESTAMP") { Instant.now().truncatedTo(ChronoUnit.MILLIS) }
    override val primaryKey = PrimaryKey(id, name = "\"RoutingPolicyScope_pkey\"")

    init {
        index("\"RoutingPolicyScope_scopeType_scopeId_idx\"", false, scopeType, scopeId)
        index("\"RoutingPolicyScope_routingPolicyId_idx\"", false, routingPolicyId)
        uniqueIndex("\"RoutingPolicyScope_routingPolicyId_scopeType_scopeId_key\"", routingPolicyId, scopeType, scopeId)
    }
}
