defmodule Trigger.WebhookEndpoint do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          friendly_id: String.t(),
          opaque_id: String.t(),
          organization_id: String.t(),
          project_id: String.t(),
          runtime_environment_id: String.t(),
          environment_type: atom(),
          endpoint_tenant_id: String.t(),
          endpoint_external_ref: String.t(),
          source: String.t(),
          declared_id: String.t(),
          routing_targets: map(),
          verifier_artifact: map(),
          metadata: map(),
          secret_provisioning: String.t(),
          setup_prompt: String.t() | nil,
          signing_secret_key: String.t() | nil,
          status: atom(),
          manually_deactivated_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "WebhookEndpoint" do
    field(:id, :string, primary_key: true)
    field(:friendly_id, :string, source: :friendlyId)
    field(:opaque_id, :string, source: :opaqueId)
    field(:organization_id, :string, source: :organizationId)
    field(:project_id, :string, source: :projectId)
    field(:runtime_environment_id, :string, source: :runtimeEnvironmentId)
    field(:environment_type, Ecto.Enum, values: [:PRODUCTION, :STAGING, :DEVELOPMENT, :PREVIEW], source: :environmentType)
    field(:endpoint_tenant_id, :string, default: "", source: :endpointTenantId)
    field(:endpoint_external_ref, :string, default: "", source: :endpointExternalRef)
    field(:source, :string)
    field(:declared_id, :string, source: :declaredId)
    field(:routing_targets, :map, source: :routingTargets)
    field(:verifier_artifact, :map, source: :verifierArtifact)
    field(:metadata, :map, default: %{})
    field(:secret_provisioning, :string, default: "either", source: :secretProvisioning)
    field(:setup_prompt, :string, source: :setupPrompt)
    field(:signing_secret_key, :string, source: :signingSecretKey)
    field(:status, Ecto.Enum, values: [:ACTIVE, :INACTIVE, :DELETING], default: :ACTIVE)
    field(:manually_deactivated_at, Trigger.PrismaDateTime, source: :manuallyDeactivatedAt)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end