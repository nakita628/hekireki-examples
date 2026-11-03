defmodule Trigger.OrganizationProjectIntegration do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          external_entity_id: String.t(),
          integration_data: map(),
          installed_by: String.t() | nil,
          deleted_at: DateTime.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization_integration: Trigger.OrganizationIntegration.t() | nil,
          project: Trigger.Project.t() | nil
        }

  schema "OrganizationProjectIntegration" do
    field(:id, :string, primary_key: true)
    field(:external_entity_id, :string, source: :externalEntityId)
    field(:integration_data, :map, source: :integrationData)
    field(:installed_by, :string, source: :installedBy)
    field(:deleted_at, Trigger.PrismaDateTime, source: :deletedAt)
    field(:organization_integration_id, :string, source: :organizationIntegrationId)
    field(:project_id, :string, source: :projectId)
    belongs_to(:organization_integration, Trigger.OrganizationIntegration, foreign_key: :organization_integration_id, define_field: false, type: :string)
    belongs_to(:project, Trigger.Project, foreign_key: :project_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end