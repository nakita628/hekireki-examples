defmodule Trigger.OrganizationIntegration do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          friendly_id: String.t(),
          service: atom(),
          external_organization_id: String.t() | nil,
          integration_data: map(),
          deleted_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          token_reference: Trigger.SecretReference.t() | nil,
          organization: Trigger.Organization.t() | nil,
          alert_channels: [Trigger.ProjectAlertChannel.t()],
          organization_project_integration: [Trigger.OrganizationProjectIntegration.t()]
        }

  schema "OrganizationIntegration" do
    field(:id, :string, primary_key: true)
    field(:friendly_id, :string, source: :friendlyId)
    field(:service, Ecto.Enum, values: [:SLACK, :VERCEL])
    field(:external_organization_id, :string, source: :externalOrganizationId)
    field(:integration_data, :map, source: :integrationData)
    field(:deleted_at, Trigger.PrismaDateTime, source: :deletedAt)
    field(:token_reference_id, :string, source: :tokenReferenceId)
    field(:organization_id, :string, source: :organizationId)
    belongs_to(:token_reference, Trigger.SecretReference, foreign_key: :token_reference_id, define_field: false, type: :string)
    belongs_to(:organization, Trigger.Organization, foreign_key: :organization_id, define_field: false, type: :string)
    has_many(:alert_channels, Trigger.ProjectAlertChannel, foreign_key: :integration_id)
    has_many(:organization_project_integration, Trigger.OrganizationProjectIntegration, foreign_key: :organization_integration_id)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end