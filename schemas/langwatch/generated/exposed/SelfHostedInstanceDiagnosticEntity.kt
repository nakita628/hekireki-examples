package langwatch.models

import org.jetbrains.exposed.v1.core.dao.id.EntityID
import org.jetbrains.exposed.v1.dao.Entity
import org.jetbrains.exposed.v1.dao.EntityClass

class SelfHostedInstanceDiagnosticEntity(
    id: EntityID<String>,
) : Entity<String>(id) {
    companion object : EntityClass<String, SelfHostedInstanceDiagnosticEntity>(SelfHostedInstanceDiagnosticTable)

    var instanceId by SelfHostedInstanceDiagnosticTable.instanceId
    var stepId by SelfHostedInstanceDiagnosticTable.stepId
    var stepDescription by SelfHostedInstanceDiagnosticTable.stepDescription
    var release by SelfHostedInstanceDiagnosticTable.release
    var attempt by SelfHostedInstanceDiagnosticTable.attempt
    var trigger by SelfHostedInstanceDiagnosticTable.trigger
    var byteLength by SelfHostedInstanceDiagnosticTable.byteLength
    var objectKey by SelfHostedInstanceDiagnosticTable.objectKey
    var receivedAt by SelfHostedInstanceDiagnosticTable.receivedAt
    var expiresAt by SelfHostedInstanceDiagnosticTable.expiresAt
    var slackAnnouncedAt by SelfHostedInstanceDiagnosticTable.slackAnnouncedAt
}
