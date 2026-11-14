package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.CompositeIdTable

object SsoVerifiedDomainHolderTable : CompositeIdTable("\"SsoVerifiedDomainHolder\"") {
    val domain = text("domain").entityId()
    val connectionId = text("connectionId").entityId()
    val organizationId = text("organizationId")
    override val primaryKey = PrimaryKey(domain, connectionId, name = "\"SsoVerifiedDomainHolder_pkey\"")

    init {
        index("\"SsoVerifiedDomainHolder_connectionId_idx\"", false, connectionId)
        index("\"SsoVerifiedDomainHolder_organizationId_idx\"", false, organizationId)
    }
}
