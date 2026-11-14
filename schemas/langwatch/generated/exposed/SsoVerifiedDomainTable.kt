package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.IdTable

object SsoVerifiedDomainTable : IdTable<String>("\"SsoVerifiedDomain\"") {
    override val id = text("domain").entityId()
    val organizationId = text("organizationId")
    override val primaryKey = PrimaryKey(id, name = "\"SsoVerifiedDomain_pkey\"")

    init {
        index("\"SsoVerifiedDomain_organizationId_idx\"", false, organizationId)
        uniqueIndex("\"SsoVerifiedDomain_domain_organizationId_key\"", id, organizationId)
    }
}
