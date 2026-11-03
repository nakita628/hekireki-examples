package calcom.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class DeploymentEntity(
    id: EntityID<Int>,
) : Entity<Int>(id) {
    companion object : EntityClass<Int, DeploymentEntity>(DeploymentTable)

    var logo by DeploymentTable.logo
    var theme by DeploymentTable.theme
    var licenseKey by DeploymentTable.licenseKey
    var signatureTokenEncrypted by DeploymentTable.signatureTokenEncrypted
    var agreedLicenseAt by DeploymentTable.agreedLicenseAt
}
